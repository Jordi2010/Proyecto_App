using System;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace zionAppSensorial.ViewModels
{
    public class Pantalla3ViewModel : BindableObject
    {
        private bool _hasLost = false;
        private Color _gatoButtonColor = Color.LightGray;
        private Color _leonButtonColor = Color.LightGray;
        private Color _amadilloButtonColor = Color.LightGray;

        public event EventHandler NavigateToNextPage;

        public ICommand ImageClickedCommand { get; }
        public ICommand IncorrectButtonCommand { get; }
        public ICommand CorrectButtonCommand { get; }

        public Color GatoButtonColor
        {
            get => _gatoButtonColor;
            set
            {
                _gatoButtonColor = value;
                OnPropertyChanged();
            }
        }

        public Color LeonButtonColor
        {
            get => _leonButtonColor;
            set
            {
                _leonButtonColor = value;
                OnPropertyChanged();
            }
        }

        public Color AmadilloButtonColor
        {
            get => _amadilloButtonColor;
            set
            {
                _amadilloButtonColor = value;
                OnPropertyChanged();
            }
        }

        public Pantalla3ViewModel()
        {
            ImageClickedCommand = new Command(async () => await OnImageClicked());
            IncorrectButtonCommand = new Command<string>(async (button) => await OnIncorrectButtonClicked(button));
            CorrectButtonCommand = new Command(async () => await OnCorrectButtonClicked());
        }

        private async Task OnImageClicked()
        {
            var text = "Es adorable, es muy astuto y tiene 9 vidas";

            var locales = await TextToSpeech.GetLocalesAsync();
            var spanishLocale = locales.FirstOrDefault(locale => locale.Language == "es" && locale.Country == "ES");

            var settings = new SpeechOptions()
            {
                Locale = spanishLocale
            };

            await TextToSpeech.SpeakAsync(text, settings);
        }

        private async Task OnIncorrectButtonClicked(string button)
        {
            if (button == "Leon")
            {
                LeonButtonColor = Color.Red;
            }
            else if (button == "Amadillo")
            {
                AmadilloButtonColor = Color.Red;
            }

            if (!_hasLost)
            {
                _hasLost = true;
            }
            else
            {
                await Application.Current.MainPage.DisplayAlert("Alerta", "Lo siento, has perdido este nivel, presiona la X para intentarlo otra vez", "OK");
                ResetButtons();
            }
        }

        private async Task OnCorrectButtonClicked()
        {
            GatoButtonColor = Color.Green;
            Vibration.Vibrate(TimeSpan.FromMilliseconds(500));
            await Application.Current.MainPage.DisplayAlert("¡EnHorabuena!", "Has ganado este nivel, conseguiste 100 puntos, pulsa OK para ir al siguiente nivel", "OK");

            NavigateToNextPage?.Invoke(this, EventArgs.Empty);
        }

        private void ResetButtons()
        {
            GatoButtonColor = Color.LightGray;
            LeonButtonColor = Color.LightGray;
            AmadilloButtonColor = Color.LightGray;
            _hasLost = false;
        }

        public void ResetButtonColors()
        {
            GatoButtonColor = Color.LightGray;
            LeonButtonColor = Color.LightGray;
            AmadilloButtonColor = Color.LightGray;
        }
    }
}
