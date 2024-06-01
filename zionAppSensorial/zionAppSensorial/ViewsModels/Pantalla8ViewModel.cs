using System;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace zionAppSensorial.ViewModels
{
    public class Pantalla8ViewModel : BindableObject
    {
        private bool _hasLost = false;
        private int _incorrectAttempts = 0;
        private Color _guineoButtonColor = Color.LightGray;
        private Color _manzanaButtonColor = Color.LightGray;
        private Color _peraButtonColor = Color.LightGray;
        private Color _uvaButtonColor = Color.LightGray;

        public event EventHandler NavigateToNextPage;

        public ICommand ImageClickedCommand { get; }
        public ICommand IncorrectButtonCommand { get; }
        public ICommand CorrectButtonCommand { get; }

        public Color GuineoButtonColor
        {
            get => _guineoButtonColor;
            set
            {
                _guineoButtonColor = value;
                OnPropertyChanged();
            }
        }

        public Color ManzanaButtonColor
        {
            get => _manzanaButtonColor;
            set
            {
                _manzanaButtonColor = value;
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

        public Color UvaButtonColor
        {
            get => _uvaButtonColor;
            set
            {
                _uvaButtonColor = value;
                OnPropertyChanged();
            }
        }

        public Pantalla8ViewModel()
        {
            ImageClickedCommand = new Command(async () => await OnImageClicked());
            IncorrectButtonCommand = new Command<string>(async (button) => await OnIncorrectButtonClicked(button));
            CorrectButtonCommand = new Command(async () => await OnCorrectButtonClicked());
        }

        private async Task OnImageClicked()
        {
            var text = "Soy amarillo y curvo, me encuentran en los trópicos y soy rico en potasio";

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
                case "Pera":
                    PeraButtonColor = Color.Red;
                    break;
                case "Uva":
                    UvaButtonColor = Color.Red;
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
            GuineoButtonColor = Color.Green;
            Vibration.Vibrate(TimeSpan.FromMilliseconds(500));
            await Application.Current.MainPage.DisplayAlert("¡Enhorabuena!", "Has ganado este nivel, conseguiste 100 puntos, pulsa OK para ir al siguiente nivel", "OK");

            NavigateToNextPage?.Invoke(this, EventArgs.Empty);
        }

        private void ResetButtons()
        {
            GuineoButtonColor = Color.LightGray;
            ManzanaButtonColor = Color.LightGray;
            PeraButtonColor = Color.LightGray;
            UvaButtonColor = Color.LightGray;
            _incorrectAttempts = 0;
            _hasLost = false;
        }

        public void ResetButtonColors()
        {
            ResetButtons();
        }
    }
}
