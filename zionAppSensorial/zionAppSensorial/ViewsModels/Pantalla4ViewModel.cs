using System;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace zionAppSensorial.ViewModels
{
    public class Pantalla4ViewModel : BindableObject
    {
        private bool _hasLost = false;
        private Color _leonButtonColor = Color.LightGray;
        private Color _tigreButtonColor = Color.LightGray;
        private Color _pulpoButtonColor = Color.LightGray;
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

        public Color TigreButtonColor
        {
            get => _tigreButtonColor;
            set
            {
                _tigreButtonColor = value;
                OnPropertyChanged();
            }
        }

        public Color PulpoButtonColor
        {
            get => _pulpoButtonColor;
            set
            {
                _pulpoButtonColor = value;
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

        public Pantalla4ViewModel()
        {
            ImageClickedCommand = new Command(async () => await OnImageClicked());
            IncorrectButtonCommand = new Command<string>(async (button) => await OnIncorrectButtonClicked(button));
            CorrectButtonCommand = new Command(async () => await OnCorrectButtonClicked());
        }

        private async Task OnImageClicked()
        {
            var text = "Pertenece a la realeza, tiene un cabello esplendido y su rugido es feroz";

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
            if (button == "Tigre")
            {
                TigreButtonColor = Color.Red;
            }
            else if (button == "Pulpo")
            {
                PulpoButtonColor = Color.Red;
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
            LeonButtonColor = Color.Green;
            Vibration.Vibrate(TimeSpan.FromMilliseconds(500));

            await Application.Current.MainPage.DisplayAlert("¡EnHorabuena!", "Has ganado este nivel, conseguiste 100 puntos, pulsa OK para ir al siguiente nivel", "OK");

            NavigateToNextPage?.Invoke(this, EventArgs.Empty);
        }

        private void ResetButtons()
        {
            LeonButtonColor = Color.LightGray;
            TigreButtonColor = Color.LightGray;
            PulpoButtonColor = Color.LightGray;
            _hasLost = false;
        }

        public void ResetButtonColors()
        {
            LeonButtonColor = Color.LightGray;
            TigreButtonColor = Color.LightGray;
            PulpoButtonColor = Color.LightGray;
            CorrectButtonColor = Color.LightGray;
        }
    }
}
