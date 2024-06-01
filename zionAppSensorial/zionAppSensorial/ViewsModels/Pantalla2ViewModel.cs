using System;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace zionAppSensorial.ViewModels
{
    public class Pantalla2ViewModel : BindableObject
    {
        private bool _hasLost = false;
        private Color _leonButtonColor = Color.LightGray;
        private Color _loboButtonColor = Color.LightGray;
        private Color _correctButtonColor = Color.LightGray;

        public event EventHandler NavigateToNextPage;

        public ICommand ImageClickedCommand { get; }
        public ICommand IncorrectButtonCommand { get; }
        public ICommand CorrectButtonCommand { get; }

        public Color LeonButtonColor
        {
            get => _leonButtonColor;
            set
            {
                _leonButtonColor = value;
                OnPropertyChanged();
            }
        }

        public Color LoboButtonColor
        {
            get => _loboButtonColor;
            set
            {
                _loboButtonColor = value;
                OnPropertyChanged();
            }
        }

        public Color CorrectButtonColor
        {
            get => _correctButtonColor;
            set
            {
                _correctButtonColor = value;
                OnPropertyChanged();
            }
        }

        public Pantalla2ViewModel()
        {
            ImageClickedCommand = new Command(async () => await OnImageClicked());
            IncorrectButtonCommand = new Command<string>(async (button) => await OnIncorrectButtonClicked(button));
            CorrectButtonCommand = new Command(async () => await OnCorrectButtonClicked());
        }

        private async Task OnImageClicked()
        {
            var text = "Tiene un cuerpo muy fuerte, camina con mucho cuidado y vive por muchos años";

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
            else if (button == "Lobo")
            {
                LoboButtonColor = Color.Red;
            }

            if (!_hasLost)
            {
                _hasLost = true;
            }
            else
            {
                await Application.Current.MainPage.DisplayAlert("Alerta", "Lo siento, has perdido este nivel, presiona OK para intentarlo otra vez", "OK");
                ResetButtons();
            }
        }

        private async Task OnCorrectButtonClicked()
        {
            CorrectButtonColor = Color.Green;
            Vibration.Vibrate(TimeSpan.FromMilliseconds(500));

            await Application.Current.MainPage.DisplayAlert("¡EnHorabuena!", "Has ganado este nivel, conseguiste 100 puntos, pulsa OK para ir al siguiente nivel", "OK");

            NavigateToNextPage?.Invoke(this, EventArgs.Empty);
        }

        private void ResetButtons()
        {
            LeonButtonColor = Color.LightGray;
            LoboButtonColor = Color.LightGray;
            _hasLost = false;
        }

        public void ResetButtonColors()
        {
            LeonButtonColor = Color.LightGray;
            LoboButtonColor = Color.LightGray;
            CorrectButtonColor = Color.LightGray;
        }
    }
}

