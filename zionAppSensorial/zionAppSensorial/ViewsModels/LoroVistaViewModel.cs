using System;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace zionAppSensorial.ViewModels
{
    public class LoroVistaViewModel : BindableObject
    {
        private bool _hasLost = false;
        private bool _hasSelectedCorrect = false;
        private Color _guacamayoButtonColor = Color.LightGray;
        private Color _joloteButtonColor = Color.LightGray;
        private Color _patoButtonColor = Color.LightGray;
        private Color _galloButtonColor = Color.LightGray;
        private Color _gallinaButtonColor = Color.LightGray;
        private Color _cacatuaButtonColor = Color.LightGray;
        private Color _loroButtonColor = Color.LightGray;
        private int _incorrectSelections = 0;

        public event EventHandler NavigateToNextPage;

        public ICommand IncorrectButtonCommand { get; }
        public ICommand LoroButtonClickedCommand { get; }
        public ICommand ImageClickedCommand { get; }

        public Color GuacamayoButtonColor
        {
            get => _guacamayoButtonColor;
            set
            {
                _guacamayoButtonColor = value;
                OnPropertyChanged();
            }
        }

        public Color JoloteButtonColor
        {
            get => _joloteButtonColor;
            set
            {
                _joloteButtonColor = value;
                OnPropertyChanged();
            }
        }

        public Color PatoButtonColor
        {
            get => _patoButtonColor;
            set
            {
                _patoButtonColor = value;
                OnPropertyChanged();
            }
        }

        public Color GalloButtonColor
        {
            get => _galloButtonColor;
            set
            {
                _galloButtonColor = value;
                OnPropertyChanged();
            }
        }

        public Color GallinaButtonColor
        {
            get => _gallinaButtonColor;
            set
            {
                _gallinaButtonColor = value;
                OnPropertyChanged();
            }
        }

        public Color CacatuaButtonColor
        {
            get => _cacatuaButtonColor;
            set
            {
                _cacatuaButtonColor = value;
                OnPropertyChanged();
            }
        }

        public Color LoroButtonColor
        {
            get => _loroButtonColor;
            set
            {
                _loroButtonColor = value;
                OnPropertyChanged();
            }
        }

        public LoroVistaViewModel()
        {
            IncorrectButtonCommand = new Command<string>(async (button) => await OnIncorrectButtonClicked(button));
            LoroButtonClickedCommand = new Command(async () => await OnLoroButtonClicked());
            ImageClickedCommand = new Command(async () => await OnImageClicked());
        }

        private async Task OnIncorrectButtonClicked(string button)
        {
            switch (button)
            {
                case "Guacamayo":
                    GuacamayoButtonColor = Color.Red;
                    break;
                case "Jolote":
                    JoloteButtonColor = Color.Red;
                    break;
                case "Pato":
                    PatoButtonColor = Color.Red;
                    break;
                case "Gallo":
                    GalloButtonColor = Color.Red;
                    break;
                case "Gallina":
                    GallinaButtonColor = Color.Red;
                    break;
                case "Cacatua":
                    CacatuaButtonColor = Color.Red;
                    break;
            }

            _incorrectSelections++;

            if (_incorrectSelections >= 6 && !_hasSelectedCorrect)
            {
                await Application.Current.MainPage.DisplayAlert("Alerta", "Lo siento, has perdido este nivel, presiona OK para intentarlo otra vez", "OK");
                ResetButtons();
            }
        }

        private async Task OnLoroButtonClicked()
        {
            _hasSelectedCorrect = true;
            LoroButtonColor = Color.Green;
            Vibration.Vibrate(TimeSpan.FromMilliseconds(500));
            await Application.Current.MainPage.DisplayAlert("¡Enhorabuena!", "Has ganado este nivel, conseguiste 100 puntos, pulsa OK para ir al siguiente nivel", "OK");
            ResetButtons(); // Restablece los colores de los botones antes de navegar
            NavigateToNextPage?.Invoke(this, EventArgs.Empty);
        }

        private void ResetButtons()
        {
            GuacamayoButtonColor = Color.LightGray;
            JoloteButtonColor = Color.LightGray;
            PatoButtonColor = Color.LightGray;
            GalloButtonColor = Color.LightGray;
            GallinaButtonColor = Color.LightGray;
            CacatuaButtonColor = Color.LightGray;
            LoroButtonColor = Color.LightGray;
            _hasLost = false;
            _incorrectSelections = 0;
            _hasSelectedCorrect = false;
        }

        private async Task OnImageClicked()
        {
            var text = "Tiene alas, pico, habla y habla sin saber lo que dice.";
            await TextToSpeech.SpeakAsync(text);
        }

        public void ResetButtonColors()
        {
            ResetButtons();
        }
    }
}
