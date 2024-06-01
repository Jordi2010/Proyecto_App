using System;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace zionAppSensorial.ViewModels
{
    public class GalloVistaViewModel : BindableObject
    {
        private bool _hasLost = false;
        private bool _hasSelectedCorrect = false;
        private Color _galloButtonColor = Color.LightGray;
        private Color _gavilanButtonColor = Color.LightGray;
        private Color _patoButtonColor = Color.LightGray;
        private Color _pericoButtonColor = Color.LightGray;
        private Color _gallinaButtonColor = Color.LightGray;
        private Color _aguilaButtonColor = Color.LightGray;
        private Color _loroButtonColor = Color.LightGray;
        private int _incorrectSelections = 0;

        public event EventHandler NavigateToNextPage;

        public ICommand IncorrectButtonCommand { get; }
        public ICommand GalloButtonClickedCommand { get; }
        public ICommand ImageClickedCommand { get; }

        public Color GalloButtonColor
        {
            get => _galloButtonColor;
            set
            {
                _galloButtonColor = value;
                OnPropertyChanged();
            }
        }

        public Color GavilanButtonColor
        {
            get => _gavilanButtonColor;
            set
            {
                _gavilanButtonColor = value;
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

        public Color PericoButtonColor
        {
            get => _pericoButtonColor;
            set
            {
                _pericoButtonColor = value;
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

        public Color AguilaButtonColor
        {
            get => _aguilaButtonColor;
            set
            {
                _aguilaButtonColor = value;
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

        public GalloVistaViewModel()
        {
            IncorrectButtonCommand = new Command<string>(async (button) => await OnIncorrectButtonClicked(button));
            GalloButtonClickedCommand = new Command(async () => await OnGalloButtonClicked());
            ImageClickedCommand = new Command(async () => await OnImageClicked());
        }

        private async Task OnIncorrectButtonClicked(string button)
        {
            switch (button)
            {
                case "Gavilan":
                    GavilanButtonColor = Color.Red;
                    break;
                case "Pato":
                    PatoButtonColor = Color.Red;
                    break;
                case "Perico":
                    PericoButtonColor = Color.Red;
                    break;
                case "Gallina":
                    GallinaButtonColor = Color.Red;
                    break;
                case "Aguila":
                    AguilaButtonColor = Color.Red;
                    break;
                case "Loro":
                    LoroButtonColor = Color.Red;
                    break;
            }

            _incorrectSelections++;

            if (_incorrectSelections >= 6 && !_hasSelectedCorrect)
            {
                await Application.Current.MainPage.DisplayAlert("Alerta", "Lo siento, has perdido este nivel, presiona OK para intentarlo otra vez", "OK");
                ResetButtons();
            }
        }

        private async Task OnGalloButtonClicked()
        {
            _hasSelectedCorrect = true;
            GalloButtonColor = Color.Green;
            Vibration.Vibrate(TimeSpan.FromMilliseconds(500));
            await Application.Current.MainPage.DisplayAlert("¡Enhorabuena!", "Has ganado este nivel, conseguiste 100 puntos, pulsa OK para ir al siguiente nivel", "OK");
            ResetButtons(); // Restablece los colores de los botones antes de navegar
            NavigateToNextPage?.Invoke(this, EventArgs.Empty);
        }

        private void ResetButtons()
        {
            GalloButtonColor = Color.LightGray;
            GavilanButtonColor = Color.LightGray;
            PatoButtonColor = Color.LightGray;
            PericoButtonColor = Color.LightGray;
            GallinaButtonColor = Color.LightGray;
            AguilaButtonColor = Color.LightGray;
            LoroButtonColor = Color.LightGray;
            _hasLost = false;
            _incorrectSelections = 0;
            _hasSelectedCorrect = false;
        }

        private async Task OnImageClicked()
        {
            var text = "Es conocido por cantar al amanecer y tiene cresta roja en la cabeza.";
            await TextToSpeech.SpeakAsync(text);
        }

        public void ResetButtonColors()
        {
            ResetButtons();
        }
    }
}
