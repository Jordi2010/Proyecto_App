using System;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace zionAppSensorial.ViewModels
{
    public class Pantalla10ViewModel : BindableObject
    {
        private bool _hasLost = false;
        private int _incorrectAttempts = 0;
        private Color _mandarinaButtonColor = Color.LightGray;
        private Color _zanahoriaButtonColor = Color.LightGray;
        private Color _uvaButtonColor = Color.LightGray;
        private Color _peraButtonColor = Color.LightGray;

        public event EventHandler NavigateToNextPage;

        public ICommand ImageClickedCommand { get; }
        public ICommand IncorrectButtonCommand { get; }
        public ICommand CorrectButtonCommand { get; }

        public Color MandarinaButtonColor
        {
            get => _mandarinaButtonColor;
            set
            {
                _mandarinaButtonColor = value;
                OnPropertyChanged();
            }
        }

        public Color ZanahoriaButtonColor
        {
            get => _zanahoriaButtonColor;
            set
            {
                _zanahoriaButtonColor = value;
                OnPropertyChanged();
            }
        }

        public Color UvaButtonColor
        {
            get => _uvaButtonColor;
            set
            {
                _uvaButtonColor = value;
                OnPropertyChanged();
            }
        }

        public Color PeraButtonColor
        {
            get => _peraButtonColor;
            set
            {
                _peraButtonColor = value;
                OnPropertyChanged();
            }
        }

        public Pantalla10ViewModel()
        {
            ImageClickedCommand = new Command(async () => await OnImageClicked());
            IncorrectButtonCommand = new Command<string>(async (button) => await OnIncorrectButtonClicked(button));
            CorrectButtonCommand = new Command(async () => await OnCorrectButtonClicked());
        }

        private async Task OnImageClicked()
        {
            var text = "La calabaza, me ha copiado el color, la naranja el sabor, pero sobre todo yo soy la mejor";

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
            switch (button)
            {
                case "Zanahoria":
                    ZanahoriaButtonColor = Color.Red;
                    break;
                case "Uva":
                    UvaButtonColor = Color.Red;
                    break;
                case "Pera":
                    PeraButtonColor = Color.Red;
                    break;
            }

            _incorrectAttempts++;
            if (_incorrectAttempts == 3)
            {
                await Application.Current.MainPage.DisplayAlert("Alerta", "Lo siento, has perdido este nivel, presiona la X para intentarlo otra vez", "OK");
                ResetButtons();
            }
        }

        private async Task OnCorrectButtonClicked()
        {
            MandarinaButtonColor = Color.Green;
            Vibration.Vibrate(TimeSpan.FromMilliseconds(500));
            await Application.Current.MainPage.DisplayAlert("¡Enhorabuena!", "Has ganado este nivel, conseguiste 100 puntos, pulsa OK para ir al siguiente nivel", "OK");

            NavigateToNextPage?.Invoke(this, EventArgs.Empty);
        }

        private void ResetButtons()
        {
            MandarinaButtonColor = Color.LightGray;
            ZanahoriaButtonColor = Color.LightGray;
            UvaButtonColor = Color.LightGray;
            PeraButtonColor = Color.LightGray;
            _incorrectAttempts = 0;
            _hasLost = false;
        }

        public void ResetButtonColors()
        {
            ResetButtons();
        }
    }
}
