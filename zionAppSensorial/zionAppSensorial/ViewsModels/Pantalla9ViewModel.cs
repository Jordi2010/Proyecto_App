using System;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace zionAppSensorial.ViewModels
{
    public class Pantalla9ViewModel : BindableObject
    {
        private bool _hasLost = false;
        private int _incorrectAttempts = 0;
        private Color _fresaButtonColor = Color.LightGray;
        private Color _tomateButtonColor = Color.LightGray;
        private Color _perroButtonColor = Color.LightGray;
        private Color _sandiaButtonColor = Color.LightGray;

        public event EventHandler NavigateToNextPage;

        public ICommand ImageClickedCommand { get; }
        public ICommand IncorrectButtonCommand { get; }
        public ICommand CorrectButtonCommand { get; }

        public Color FresaButtonColor
        {
            get => _fresaButtonColor;
            set
            {
                _fresaButtonColor = value;
                OnPropertyChanged();
            }
        }

        public Color TomateButtonColor
        {
            get => _tomateButtonColor;
            set
            {
                _tomateButtonColor = value;
                OnPropertyChanged();
            }
        }

        public Color PerroButtonColor
        {
            get => _perroButtonColor;
            set
            {
                _perroButtonColor = value;
                OnPropertyChanged();
            }
        }

        public Color SandiaButtonColor
        {
            get => _sandiaButtonColor;
            set
            {
                _sandiaButtonColor = value;
                OnPropertyChanged();
            }
        }

        public Pantalla9ViewModel()
        {
            ImageClickedCommand = new Command(async () => await OnImageClicked());
            IncorrectButtonCommand = new Command<string>(async (button) => await OnIncorrectButtonClicked(button));
            CorrectButtonCommand = new Command(async () => await OnCorrectButtonClicked());
        }

        private async Task OnImageClicked()
        {
            var text = "Yo rimo con esa, color de cereza, del suelo a la mesa te doy la sorpresa";

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
                case "Tomate":
                    TomateButtonColor = Color.Red;
                    break;
                case "Perro":
                    PerroButtonColor = Color.Red;
                    break;
                case "Sandia":
                    SandiaButtonColor = Color.Red;
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
            FresaButtonColor = Color.Green;
            Vibration.Vibrate(TimeSpan.FromMilliseconds(500));
            await Application.Current.MainPage.DisplayAlert("¡Enhorabuena!", "Has ganado este nivel, conseguiste 100 puntos, pulsa OK para ir al siguiente nivel", "OK");

            NavigateToNextPage?.Invoke(this, EventArgs.Empty);
        }

        private void ResetButtons()
        {
            FresaButtonColor = Color.LightGray;
            TomateButtonColor = Color.LightGray;
            PerroButtonColor = Color.LightGray;
            SandiaButtonColor = Color.LightGray;
            _incorrectAttempts = 0;
            _hasLost = false;
        }

        public void ResetButtonColors()
        {
            ResetButtons();
        }
    }
}
