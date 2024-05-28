using System;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace zionAppSensorial.ViewModels
{
    public class PavoVistaViewModel : BindableObject
    {
        private bool _hasLost = false;
        private bool _hasSelectedCorrect = false;
        private Color _pavoRealButtonColor = Color.LightGray;
        private Color _liquitaButtonColor = Color.LightGray;
        private Color _galloButtonColor = Color.LightGray;
        private Color _pericoButtonColor = Color.LightGray;
        private Color _gallinaButtonColor = Color.LightGray;
        private Color _periquitoButtonColor = Color.LightGray;
        private Color _cuervoButtonColor = Color.LightGray;
        private int _incorrectSelections = 0;

        public event EventHandler NavigateToNextPage;

        public ICommand IncorrectButtonCommand { get; }
        public ICommand PavoRealButtonClickedCommand { get; }
        public ICommand ImageClickedCommand { get; }

        public Color PavoRealButtonColor
        {
            get => _pavoRealButtonColor;
            set
            {
                _pavoRealButtonColor = value;
                OnPropertyChanged();
            }
        }

        public Color LiquitaButtonColor
        {
            get => _liquitaButtonColor;
            set
            {
                _liquitaButtonColor = value;
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

        public Color PeriquitoButtonColor
        {
            get => _periquitoButtonColor;
            set
            {
                _periquitoButtonColor = value;
                OnPropertyChanged();
            }
        }

        public Color CuervoButtonColor
        {
            get => _cuervoButtonColor;
            set
            {
                _cuervoButtonColor = value;
                OnPropertyChanged();
            }
        }

        public PavoVistaViewModel()
        {
            IncorrectButtonCommand = new Command<string>(async (button) => await OnIncorrectButtonClicked(button));
            PavoRealButtonClickedCommand = new Command(async () => await OnPavoRealButtonClicked());
            ImageClickedCommand = new Command(async () => await OnImageClicked());
        }

        private async Task OnIncorrectButtonClicked(string button)
        {
            switch (button)
            {
                case "Liquita":
                    LiquitaButtonColor = Color.Red;
                    break;
                case "Gallo":
                    GalloButtonColor = Color.Red;
                    break;
                case "Perico":
                    PericoButtonColor = Color.Red;
                    break;
                case "Gallina":
                    GallinaButtonColor = Color.Red;
                    break;
                case "Periquito":
                    PeriquitoButtonColor = Color.Red;
                    break;
                case "Cuervo":
                    CuervoButtonColor = Color.Red;
                    break;
            }

            _incorrectSelections++;

            if (_incorrectSelections >= 6 && !_hasSelectedCorrect)
            {
                await Application.Current.MainPage.DisplayAlert("Alerta", "Lo siento, has perdido este nivel, presiona OK para intentarlo otra vez", "OK");
                ResetButtons();
            }
        }

        private async Task OnPavoRealButtonClicked()
        {
            _hasSelectedCorrect = true;
            PavoRealButtonColor = Color.Green;
            Vibration.Vibrate(TimeSpan.FromMilliseconds(500));
            await Application.Current.MainPage.DisplayAlert("¡Enhorabuena!", "Has ganado este nivel, conseguiste 100 puntos, pulsa OK para ir al siguiente nivel", "OK");
            ResetButtons(); // Restablece los colores de los botones antes de navegar
            NavigateToNextPage?.Invoke(this, EventArgs.Empty);
        }

        private void ResetButtons()
        {
            PavoRealButtonColor = Color.LightGray;
            LiquitaButtonColor = Color.LightGray;
            GalloButtonColor = Color.LightGray;
            PericoButtonColor = Color.LightGray;
            GallinaButtonColor = Color.LightGray;
            PeriquitoButtonColor = Color.LightGray;
            CuervoButtonColor = Color.LightGray;
            _hasLost = false;
            _incorrectSelections = 0;
            _hasSelectedCorrect = false;
        }

        private async Task OnImageClicked()
        {
            var text = "Es conocido por su hermoso plumaje y su cola en forma de abanico.";
            await TextToSpeech.SpeakAsync(text);
        }

        public void ResetButtonColors()
        {
            ResetButtons();
        }
    }
}

