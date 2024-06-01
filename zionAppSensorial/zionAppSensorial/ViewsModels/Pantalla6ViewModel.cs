using System;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;
using zionAppSensorial.views.pantallas;

namespace zionAppSensorial.ViewModels
{
    public class Pantalla6ViewModel : BindableObject
    {
        private bool _hasLost = false;
        private int _incorrectAttempts = 0;
        private Color _peraButtonColor = Color.LightGray;
        private Color _manzanaButtonColor = Color.LightGray;
        private Color _zanahoriaButtonColor = Color.LightGray;
        private Color _tomateButtonColor = Color.LightGray;

        public event EventHandler NavigateToNextPage;

        public ICommand ImageClickedCommand { get; }
        public ICommand IncorrectButtonCommand { get; }
        public ICommand CorrectButtonCommand { get; }

        public Color PeraButtonColor
        {
            get => _peraButtonColor;
            set
            {
                _peraButtonColor = value;
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

        public Color ZanahoriaButtonColor
        {
            get => _zanahoriaButtonColor;
            set
            {
                _zanahoriaButtonColor = value;
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

        public Pantalla6ViewModel()
        {
            ImageClickedCommand = new Command(async () => await OnImageClicked());
            IncorrectButtonCommand = new Command<string>(async (button) => await OnIncorrectButtonClicked(button));
            CorrectButtonCommand = new Command(async () => await OnCorrectButtonClicked());
        }

        private async Task OnImageClicked()
        {
            var text = "Soy blanca por dentro y roja por fuera, soy dulce y sabrosa, no hay quien no me quiera";

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
                case "Pera":
                    PeraButtonColor = Color.Red;
                    break;
                case "Zanahoria":
                    ZanahoriaButtonColor = Color.Red;
                    break;
                case "Tomate":
                    TomateButtonColor = Color.Red;
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
            ManzanaButtonColor = Color.Green;
            Vibration.Vibrate(TimeSpan.FromMilliseconds(500));
            await Application.Current.MainPage.DisplayAlert("¡Enhorabuena!", "Has ganado este nivel, conseguiste 100 puntos, pulsa OK para ir al siguiente nivel", "OK");

            NavigateToNextPage?.Invoke(this, EventArgs.Empty);
        }

        private void ResetButtons()
        {
            PeraButtonColor = Color.LightGray;
            ManzanaButtonColor = Color.LightGray;
            ZanahoriaButtonColor = Color.LightGray;
            TomateButtonColor = Color.LightGray;
            _incorrectAttempts = 0;
            _hasLost = false;
        }

        public void ResetButtonColors()
        {
            ResetButtons();
        }


        public async void OnExitButtonClicked()
        {
            bool answer = await Application.Current.MainPage.DisplayAlert("¿Quieres finalizar?", "Si sales deberás iniciar la sección de nuevo.", "OK", "Cancelar");
            if (answer)
            {
                await Application.Current.MainPage.Navigation.PushAsync(new menu()); // Navegar a la página de menú
            }
        }
    }
}
