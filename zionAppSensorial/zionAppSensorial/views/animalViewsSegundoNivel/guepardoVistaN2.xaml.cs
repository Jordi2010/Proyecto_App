using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using zionAppSensorial.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using zionAppSensorial.views.pantallas;

namespace zionAppSensorial.views.animalViewsSegundoNivel
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class guepardoVistaN2 : ContentPage
    {
        public guepardoVistaN2()
        {
            NavigationPage.SetHasBackButton(this, false);
            InitializeComponent();
            var viewModel = new GuepardoVistaN2ViewModel();
            viewModel.NavigateToNextPage += OnNavigateToNextPage;
            BindingContext = viewModel;
        }

        private async void OnExitButtonClicked(object sender, EventArgs e)
        {
            bool answer = await DisplayAlert("¿Quieres finalizar?", "Si sales deberás iniciar la sección de nuevo.", "OK", "Cancelar");
            if (answer)
            {
                await Navigation.PushAsync(new menu());
            }
        }

        private async void OnNavigateToNextPage(object sender, EventArgs e)
        {
            if (BindingContext is GuepardoVistaN2ViewModel viewModel)
            {
                viewModel.ResetButtonColors();
            }
            await Navigation.PushAsync(new leonVistaN2()); // Cambiar "siguientePagina" por el nombre correcto de la siguiente página
        }
    }
}