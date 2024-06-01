using System;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace zionAppSensorial.ViewModels
{
    public class AvestruzVistaViewModel : BindableObject
    {
        private bool _hasLost = false;
        private bool _hasSelectedCorrect = false;
        private Color _gansoButtonColor = Color.LightGray;
        private Color _avestruzButtonColor = Color.LightGray;
        private Color _galloJiroButtonColor = Color.LightGray;
        private Color _pavoRealButtonColor = Color.LightGray;
        private Color _galloButtonColor = Color.LightGray;
        private Color _pericoButtonColor = Color.LightGray;
        private Color _pollitoButtonColor = Color.LightGray;
        private int _incorrectSelections = 0;

        public event EventHandler NavigateToNextPage;

        public ICommand IncorrectButtonCommand { get; }
        public ICommand AvestruzButtonClickedCommand { get; }
        public ICommand ImageClickedCommand { get; }

        public Color GansoButtonColor
        {
            get => _gansoButtonColor;
            set
            {
                _gansoButtonColor = value;
                OnPropertyChanged();
            }
        }

        public Color AvestruzButtonColor
        {
            get => _avestruzButtonColor;
            set
            {
                _avestruzButtonColor = value;
                OnPropertyChanged();
            }
        }

        public Color GalloJiroButtonColor
        {
            get => _galloJiroButtonColor;
            set
            {
                _galloJiroButtonColor = value;
                OnPropertyChanged();
            }
        }

        public Color PavoRealButtonColor
        {
            get => _pavoRealButtonColor;
            set
            {
                _pavoRealButtonColor = value;
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

        public Color PollitoButtonColor
        {
            get => _pollitoButtonColor;
            set
            {
                _pollitoButtonColor = value;
                OnPropertyChanged();
            }
        }

        public AvestruzVistaViewModel()
        {
            IncorrectButtonCommand = new Command<string>(async (button) => await OnIncorrectButtonClicked(button));
            AvestruzButtonClickedCommand = new Command(async () => await OnAvestruzButtonClicked());
            ImageClickedCommand = new Command(async () => await OnImageClicked());
        }

        private async Task OnIncorrectButtonClicked(string button)
        {
            switch (button)
            {
                case "Ganso":
                    GansoButtonColor = Color.Red;
                    break;
                case "GalloJiro":
                    GalloJiroButtonColor = Color.Red;
                    break;
                case "PavoReal":
                    PavoRealButtonColor = Color.Red;
                    break;
                case "Gallo":
                    GalloButtonColor = Color.Red;
                    break;
                case "Perico":
                    PericoButtonColor = Color.Red;
                    break;
                case "Pollito":
                    PollitoButtonColor = Color.Red;
                    break;
            }

            _incorrectSelections++;

            if (_incorrectSelections >= 6 && !_hasSelectedCorrect)
            {
                await Application.Current.MainPage.DisplayAlert("Alerta", "Lo siento, has perdido este nivel, presiona OK para intentarlo otra vez", "OK");
                ResetButtons();
            }
        }

        private async Task OnAvestruzButtonClicked()
        {
            _hasSelectedCorrect = true;
            AvestruzButtonColor = Color.Green;
            Vibration.Vibrate(TimeSpan.FromMilliseconds(500));
            await Application.Current.MainPage.DisplayAlert("¡Enhorabuena!", "Has ganado este nivel, conseguiste 100 puntos, pulsa OK para ir al siguiente nivel", "OK");
            ResetButtons(); // Restablece los colores de los botones antes de navegar
            NavigateToNextPage?.Invoke(this, EventArgs.Empty);
        }

        private void ResetButtons()
        {
            GansoButtonColor = Color.LightGray;
            AvestruzButtonColor = Color.LightGray;
            GalloJiroButtonColor = Color.LightGray;
            PavoRealButtonColor = Color.LightGray;
            GalloButtonColor = Color.LightGray;
            PericoButtonColor = Color.LightGray;
            PollitoButtonColor = Color.LightGray;
            _hasLost = false;
            _incorrectSelections = 0;
            _hasSelectedCorrect = false;
        }

        private async Task OnImageClicked()
        {
            var text = "Es el ave más grande del mundo con grandes patas y es conocida por no volar y tener plumas muy suaves.";
            await TextToSpeech.SpeakAsync(text);
        }

        public void ResetButtonColors()
        {
            ResetButtons();
        }
    }
}
