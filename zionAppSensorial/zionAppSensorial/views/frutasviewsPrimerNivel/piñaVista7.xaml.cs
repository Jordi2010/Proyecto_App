using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using zionAppSensorial.ViewModels;
using zionAppSensorial.views.pantallas;

namespace zionAppSensorial.views.frutasviews
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Pantalla7 : ContentPage
    {
        private Pantalla7ViewModel _viewModel;

        public Pantalla7()
        {
            // Eliminar el botón de retroceso por defecto
            NavigationPage.SetHasBackButton(this, false);
            InitializeComponent();
            _viewModel = new Pantalla7ViewModel();
            _viewModel.NavigateToNextPage += async (sender, e) =>
            {
                await Navigation.PushAsync(new felicidades("frutas")); // Ajusta esta línea para navegar a la siguiente vista correspondiente
                _viewModel.ResetButtonColors();
            };
            BindingContext = _viewModel;
        }

        private async void OnExitButtonClicked(object sender, EventArgs e)
        {
            bool answer = await DisplayAlert("¿Quieres finalizar?", "Si sales deberás iniciar la sección de nuevo.", "OK", "Cancelar");
            if (answer)
            {
                await Navigation.PushAsync(new menu()); // Navegar a la página de menú
            }
        }
    }
}