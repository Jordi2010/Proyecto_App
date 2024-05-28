using System;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace zionAppSensorial.ViewModels
{
    public class Pantalla7ViewModel : BindableObject
    {
        private bool _hasLost = false;
        private int _incorrectAttempts = 0;
        private Color _manzanaButtonColor = Color.LightGray;
        private Color _piñaButtonColor = Color.LightGray;
        private Color _uvaButtonColor = Color.LightGray;
        private Color _naranjaButtonColor = Color.LightGray;

        public event EventHandler NavigateToNextPage;

        public ICommand ImageClickedCommand { get; }
        public ICommand IncorrectButtonCommand { get; }
        public ICommand CorrectButtonCommand { get; }

        public Color ManzanaButtonColor
        {
            get => _manzanaButtonColor;
            set
            {
                _manzanaButtonColor = value;
                OnPropertyChanged();
            }
        }

        public Color PiñaButtonColor
        {
            get => _piñaButtonColor;
            set
            {
                _piñaButtonColor = value;
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

        public Color NaranjaButtonColor
        {
            get => _naranjaButtonColor;
            set
            {
                _naranjaButtonColor = value;
                OnPropertyChanged();
            }
        }

        public Pantalla7ViewModel()
        {
            ImageClickedCommand = new Command(async () => await OnImageClicked());
            IncorrectButtonCommand = new Command<string>(async (button) => await OnIncorrectButtonClicked(button));
            CorrectButtonCommand = new Command(async () => await OnCorrectButtonClicked());
        }

        private async Task OnImageClicked()
        {
            var text = "Tiene escamas pero no es pez, tiene corona pero no es un rey";

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
                case "Manzana":
                    ManzanaButtonColor = Color.Red;
                    break;
                case "Piña":
                    PiñaButtonColor = Color.Red;
                    break;
                case "Uva":
                    UvaButtonColor = Color.Red;
                    break;
                case "Naranja":
                    NaranjaButtonColor = Color.Red;
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
            PiñaButtonColor = Color.Green;
            Vibration.Vibrate(TimeSpan.FromMilliseconds(500));
            await Application.Current.MainPage.DisplayAlert("¡Enhorabuena!", "Has ganado este nivel, conseguiste 100 puntos, pulsa OK para ir al siguiente nivel", "OK");

            NavigateToNextPage?.Invoke(this, EventArgs.Empty);
        }

        private void ResetButtons()
        {
            ManzanaButtonColor = Color.LightGray;
            PiñaButtonColor = Color.LightGray;
            UvaButtonColor = Color.LightGray;
            NaranjaButtonColor = Color.LightGray;
            _incorrectAttempts = 0;
            _hasLost = false;
        }

        public void ResetButtonColors()
        {
            ResetButtons();
        }
    }
}

