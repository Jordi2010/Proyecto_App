using System;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace zionAppSensorial.ViewModels
{
    public class AguilaVistaViewModel : BindableObject
    {
        // Variables booleanas y de tipo Color para controlar el estado de la interfaz de usuario
        private bool _hasLost = false;
        private bool _hasSelectedCorrect = false;
        private Color _pavoButtonColor = Color.LightGray;
        private Color _alaBlancaButtonColor = Color.LightGray;
        private Color _patoButtonColor = Color.LightGray;
        private Color _pericoButtonColor = Color.LightGray;
        private Color _gallinaButtonColor = Color.LightGray;
        private Color _guacamayoButtonColor = Color.LightGray;
        private Color _aguilaButtonColor = Color.LightGray;
        private int _incorrectSelections = 0;

        // Evento para navegar a la siguiente página
        public event EventHandler NavigateToNextPage;
        // Comandos para los botones y el clic en la imagen
        public ICommand IncorrectButtonCommand { get; }
        public ICommand AguilaButtonClickedCommand { get; }
        public ICommand ImageClickedCommand { get; }

        // Propiedades para enlazar los colores de los botones a la interfaz de usuario
        public Color PavoButtonColor
        {
            get => _pavoButtonColor;
            set
            {
                _pavoButtonColor = value;
                OnPropertyChanged();
            }
        }

        public Color AlaBlancaButtonColor
        {
            get => _alaBlancaButtonColor;
            set
            {
                _alaBlancaButtonColor = value;
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

        public Color GuacamayoButtonColor
        {
            get => _guacamayoButtonColor;
            set
            {
                _guacamayoButtonColor = value;
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
        // Inicialización de los comandos
        public AguilaVistaViewModel()
        {
            IncorrectButtonCommand = new Command<string>(async (button) => await OnIncorrectButtonClicked(button));
            AguilaButtonClickedCommand = new Command(async () => await OnAguilaButtonClicked());
            ImageClickedCommand = new Command(async () => await OnImageClicked());
        }
        // Cambia el color del botón incorrecto seleccionado a rojo
        private async Task OnIncorrectButtonClicked(string button)
        {
            switch (button)
            {
                case "Pavo":
                    PavoButtonColor = Color.Red;
                    break;
                case "AlaBlanca":
                    AlaBlancaButtonColor = Color.Red;
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
                case "Guacamayo":
                    GuacamayoButtonColor = Color.Red;
                    break;
            }

            _incorrectSelections++;
            // Si el usuario ha perdido después de 6 selecciones incorrectas y no ha seleccionado la respuesta correcta
            if (_incorrectSelections >= 6 && !_hasSelectedCorrect)
            {
                await Application.Current.MainPage.DisplayAlert("Alerta", "Lo siento, has perdido este nivel, presiona OK para intentarlo otra vez", "OK");
                ResetButtons();
            }
        }

        private async Task OnAguilaButtonClicked()
        { 
            // Maneja la selección de la respuesta correcta
            _hasSelectedCorrect = true;
            AguilaButtonColor = Color.Green;
            Vibration.Vibrate(TimeSpan.FromMilliseconds(500));// Vibra el dispositivo
            await Application.Current.MainPage.DisplayAlert("¡Enhorabuena!", "Has ganado este nivel, conseguiste 100 puntos, pulsa OK para ir al siguiente nivel", "OK");
            ResetButtons(); // Restablece los colores de los botones antes de navegar
            NavigateToNextPage?.Invoke(this, EventArgs.Empty);// Navega a la siguiente página
        }

        private void ResetButtons()
        {
            // Restablece los colores de los botones y las variables de estado
            PavoButtonColor = Color.LightGray;
            AlaBlancaButtonColor = Color.LightGray;
            PatoButtonColor = Color.LightGray;
            PericoButtonColor = Color.LightGray;
            GallinaButtonColor = Color.LightGray;
            GuacamayoButtonColor = Color.LightGray;
            AguilaButtonColor = Color.LightGray;
            _hasLost = false;
            _incorrectSelections = 0;
            _hasSelectedCorrect = false;
        }

        private async Task OnImageClicked()
        {
            // Reproduce un texto cuando se hace clic en la imagen
            var text = "Es conocido por su gran agudeza visual y vuelo majestuoso";
            await TextToSpeech.SpeakAsync(text);
        }

        public void ResetButtonColors()
        {
            // Método público para restablecer los colores de los botones
            ResetButtons();
        }
    }
}
