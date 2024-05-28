using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using zionAppSensorial.ViewModels;
using zionAppSensorial.views.frutasviews;
using zionAppSensorial.views.pantallas;

namespace zionAppSensorial.views.animalviews
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Pantalla5 : ContentPage
    {
        private Pantalla5ViewModel _viewModel;

        public Pantalla5()
        {

            // Eliminar el botón de retroceso por defecto
            NavigationPage.SetHasBackButton(this, false);
            InitializeComponent();
            // Inicializar el ViewModel
            _viewModel = new Pantalla5ViewModel();
            // Suscribirse al evento de navegación a la siguiente página
            _viewModel.NavigateToNextPage += async (sender, e) =>
            {
                await Navigation.PushAsync(new felicidades("animales"));
                _viewModel.ResetButtonColors();// Resetear colores de los botones al navegar
            };
            // Asignar el ViewModel al contexto de enlace de datos (BindingContext)
            BindingContext = _viewModel;
        }
        // Método que se ejecuta cuando la página aparece en pantalla
        protected override void OnAppearing()
        {
            base.OnAppearing();
            _viewModel.ResetButtonColors();// Resetear colores de los botones al aparecer la página
        }

        // Método que se ejecuta al hacer clic en el botón de salida
        private async void OnExitButtonClicked(object sender, EventArgs e)
        {
            bool answer = await DisplayAlert("¿Quieres finalizar?",
                                             "Si sales deberás iniciar la seccion de nuevo.",
                                             "OK",
                                             "Cancelar");
            if (answer)
            {
                // Navegar al menú principal si el usuario confirma
                await Navigation.PushAsync(new menu());
            }
        }
    }
}