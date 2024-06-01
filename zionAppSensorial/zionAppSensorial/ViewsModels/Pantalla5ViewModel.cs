using System;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace zionAppSensorial.ViewModels
{
    public class Pantalla5ViewModel : BindableObject
    {
        // Propiedad privada que indica si el usuario ha perdido el nivel
        private bool _hasLost = false;

        // Colores iniciales de los botones
        private Color _elefanteButtonColor = Color.LightGray;
        private Color _monoButtonColor = Color.LightGray;
        private Color _ardillaButtonColor = Color.LightGray;
        private Color _correctButtonColor = Color.LightGray;

        // Evento para navegar a la siguiente página
        public event EventHandler NavigateToNextPage;

        // Comandos para manejar los clics en las imágenes y botones
        public ICommand ImageClickedCommand { get; }
        public ICommand IncorrectButtonCommand { get; }
        public ICommand CorrectButtonCommand { get; }

        // Propiedades para los colores de los botones, que notifican cambios a la UI
        public Color ElefanteButtonColor
        {
            get => _elefanteButtonColor;
            set
            {
                _elefanteButtonColor = value;
                OnPropertyChanged();
            }
        }

        public Color MonoButtonColor
        {
            get => _monoButtonColor;
            set
            {
                _monoButtonColor = value;
                OnPropertyChanged();
            }
        }

        public Color ArdillaButtonColor
        {
            get => _ardillaButtonColor;
            set
            {
                _ardillaButtonColor = value;
                OnPropertyChanged();
            }
        }

        public Color CorrectButtonColor
        {
            get => _correctButtonColor;
            set
            {
                _correctButtonColor = value;
                OnPropertyChanged();
            }
        }
        // Constructor donde se inicializan los comandos
        public Pantalla5ViewModel()
        {
            ImageClickedCommand = new Command(async () => await OnImageClicked());
            IncorrectButtonCommand = new Command<string>(async (button) => await OnIncorrectButtonClicked(button));
            CorrectButtonCommand = new Command(async () => await OnCorrectButtonClicked());
        }
        // Método para manejar el clic en la imagen, reproduce un texto con TTS
        private async Task OnImageClicked()
        {
            var text = "Es enorme, tiene una trompa larga y escucha muy bien";

            var locales = await TextToSpeech.GetLocalesAsync();
            var spanishLocale = locales.FirstOrDefault(locale => locale.Language == "es" && locale.Country == "ES");

            var settings = new SpeechOptions()
            {
                Locale = spanishLocale
            };

            await TextToSpeech.SpeakAsync(text, settings);
        }
        // Método para manejar el clic en un botón incorrecto
        private async Task OnIncorrectButtonClicked(string button)
        {
            if (button == "Mono")
            {
                MonoButtonColor = Color.Red;
            }
            else if (button == "Ardilla")
            {
                ArdillaButtonColor = Color.Red;
            }
            // Si el usuario ha perdido una vez, mostrar alerta y resetear botones
            if (!_hasLost)
            {
                _hasLost = true;
            }
            else
            {
                await Application.Current.MainPage.DisplayAlert("Alerta", "Lo siento, has perdido este nivel, presiona OK para intentarlo otra vez", "OK");
                ResetButtons();
            }
        }
        // Método para manejar el clic en el botón correcto
        private async Task OnCorrectButtonClicked()
        {
            ElefanteButtonColor = Color.Green;
            // Hacer vibrar el dispositivo
            Vibration.Vibrate(TimeSpan.FromMilliseconds(500));

            await Application.Current.MainPage.DisplayAlert("¡EnHorabuena!", "Has ganado este nivel, conseguiste 100 puntos, pulsa OK para ir al siguiente nivel", "OK");

            NavigateToNextPage?.Invoke(this, EventArgs.Empty);
        }
        // Método para resetear los colores de los botones
        private void ResetButtons()
        {
            ElefanteButtonColor = Color.LightGray;
            MonoButtonColor = Color.LightGray;
            ArdillaButtonColor = Color.LightGray;
            _hasLost = false;
        }
        // Método público para resetear los colores de los botones, si se necesita externamente
        public void ResetButtonColors()
        {
            ElefanteButtonColor = Color.LightGray;
            MonoButtonColor = Color.LightGray;
            ArdillaButtonColor = Color.LightGray;
            CorrectButtonColor = Color.LightGray;
        }
    }
}
