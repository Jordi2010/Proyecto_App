using System;
using System.Linq;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using zionAppSensorial.ViewModels;
using zionAppSensorial.views.pantallas;

namespace zionAppSensorial.views.animalviews
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Panatalla3 : ContentPage
    {
        private Pantalla3ViewModel _viewModel;

        public Panatalla3()
        {

            // Eliminar el botón de retroceso por defecto
            NavigationPage.SetHasBackButton(this, false);
            InitializeComponent();
            _viewModel = new Pantalla3ViewModel();
            _viewModel.NavigateToNextPage += async (sender, e) =>
            {
                await Navigation.PushAsync(new Pantalla4());
                _viewModel.ResetButtonColors();
            };
            BindingContext = _viewModel;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            _viewModel.ResetButtonColors();
        }
        private async void OnExitButtonClicked(object sender, EventArgs e)
        {
            bool answer = await DisplayAlert("¿Quieres finalizar?",
                                             "Si sales deberás iniciar la seccion de nuevo.",
                                             "OK",
                                             "Cancelar");
            if (answer)
            {
                await Navigation.PushAsync(new menu()); 
            }
        }

    }
}  

