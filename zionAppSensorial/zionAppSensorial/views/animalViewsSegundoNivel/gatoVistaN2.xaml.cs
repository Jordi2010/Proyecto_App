using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using zionAppSensorial.ViewModels;
using zionAppSensorial.views.pantallas;

namespace zionAppSensorial.views.animalViewsSegundoNivel
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class gatoVistaN2 : ContentPage
    {
        public gatoVistaN2()
        { // Eliminar el botón de retroceso por defecto
            NavigationPage.SetHasBackButton(this, false);
            InitializeComponent();
            InitializeComponent();
            var viewModel = new GatoVistaN2ViewModel();
            viewModel.NavigateToNextPage += OnNavigateToNextPage;
            BindingContext = viewModel;
        }

        private async void OnExitButtonClicked(object sender, EventArgs e)
        {
            bool answer = await DisplayAlert("¿Quieres finalizar?", "Si sales deberás iniciar la sección de nuevo.", "OK", "Cancelar");
            if (answer)
            {
                // Navega a la página de menú
                await Navigation.PushAsync(new menu());
            }
            // Si el usuario presiona "Cancelar", no hacemos nada y permanece en la misma pantalla
        }

        private async void OnNavigateToNextPage(object sender, EventArgs e)
        {
            if (BindingContext is GatoVistaN2ViewModel viewModel)
            {
                viewModel.ResetButtonColors(); // Restablece los colores de los botones antes de navegar
            }
            await Navigation.PushAsync(new guepardoVistaN2());
        }
    }
}
