using System;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace zionAppSensorial.ViewModels
{
    public class Pantalla1ViewModel : BindableObject
    {
        private bool _hasLost = false;
        private Color _gatoButtonColor = Color.LightGray;
        private Color _tortugaButtonColor = Color.LightGray;
        private Color _correctButtonColor = Color.LightGray;

        public event EventHandler NavigateToNextPage;

        public ICommand DogImageClickedCommand { get; }
        public ICommand IncorrectButtonCommand { get; }
        public ICommand PerroButtonClickedCommand { get; }

        public Color GatoButtonColor
        {
            get => _gatoButtonColor;
            set
            {
                _gatoButtonColor = value;
                OnPropertyChanged();
            }
        }

        public Color TortugaButtonColor
        {
            get => _tortugaButtonColor;
            set
            {
                _tortugaButtonColor = value;
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

        public Pantalla1ViewModel()
        {
            DogImageClickedCommand = new Command(async () => await OnDogImageClicked());
            IncorrectButtonCommand = new Command<string>(async (button) => await OnIncorrectButtonClicked(button));
            PerroButtonClickedCommand = new Command(async () => await OnPerroButtonClicked());
        }

        private async Task OnDogImageClicked()
        {
            var text = "Le gusta jugar, también ladrar y cuidar la casa cuando los amos no están";

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
            if (button == "Gato")
            {
                GatoButtonColor = Color.Red;
            }
            else if (button == "Tortuga")
            {
                TortugaButtonColor = Color.Red;
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

        private async Task OnPerroButtonClicked()
        {
            CorrectButtonColor = Color.Green;
            Vibration.Vibrate(TimeSpan.FromMilliseconds(500));

            await Application.Current.MainPage.DisplayAlert("¡EnHorabuena!", "Has ganado este nivel, conseguiste 100 puntos, pulsa OK para ir al siguiente nivel", "OK");

            NavigateToNextPage?.Invoke(this, EventArgs.Empty);
        }

        private void ResetButtons()
        {
            GatoButtonColor = Color.LightGray;
            TortugaButtonColor = Color.LightGray;
            _hasLost = false;
        }

        public void ResetButtonColors()
        {
            GatoButtonColor = Color.LightGray;
            TortugaButtonColor = Color.LightGray;
            CorrectButtonColor = Color.LightGray;
        }

    }
}

