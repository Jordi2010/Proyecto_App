using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using zionAppSensorial.ViewModels;
using zionAppSensorial.views.pantallas;

namespace zionAppSensorial.views.animalesViewsTercerNivel
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class aguilaVista : ContentPage
    {
        public aguilaVista()
        {
            // Eliminar el botón de retroceso por defecto
            NavigationPage.SetHasBackButton(this, false);
            InitializeComponent();
            // Crea una instancia del ViewModel
            var viewModel = new AguilaVistaViewModel();
            // Suscribe el evento NavigateToNextPage al método OnNavigateToNextPage
            viewModel.NavigateToNextPage += OnNavigateToNextPage;
            // Establece el ViewModel como el contexto de enlace de datos de la vista
            BindingContext = viewModel;
        }

        private async void OnExitButtonClicked(object sender, EventArgs e)
        {
            // Muestra un cuadro de diálogo para confirmar si el usuario quiere salir
            bool answer = await DisplayAlert("¿Quieres finalizar?", "Si sales deberás iniciar la sección de nuevo.", "OK", "Cancelar");
            if (answer)
            {
                // Si el usuario presiona "OK", navega a la página de menú
                await Navigation.PushAsync(new menu());
            }
            // Si el usuario presiona "Cancelar", no hacemos nada y permanece en la misma pantalla
        }

        private async void OnNavigateToNextPage(object sender, EventArgs e)
        {
            // Verifica si el BindingContext es una instancia de AguilaVistaViewModel
            if (BindingContext is AguilaVistaViewModel viewModel)
            {
                // Restablece los colores de los botones antes de navegar
                viewModel.ResetButtonColors(); // Restablece los colores de los botones antes de navegar
            }
            // Navega a la siguiente vista (avestruzVista)
            await Navigation.PushAsync(new avestruzVista()); 
        }
    }
}