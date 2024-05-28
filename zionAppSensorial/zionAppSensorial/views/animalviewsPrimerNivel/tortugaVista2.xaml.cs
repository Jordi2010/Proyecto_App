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
    public partial class Pantalla2 : ContentPage
    {
        private Pantalla2ViewModel _viewModel;


        public Pantalla2()
        {
            InitializeComponent();

            // Eliminar el botón de retroceso por defecto
            NavigationPage.SetHasBackButton(this, false);
            _viewModel = new Pantalla2ViewModel();
            _viewModel.NavigateToNextPage += async (sender, e) =>
            {
                await Navigation.PushAsync(new Panatalla3());
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
                await Navigation.PushAsync(new menu()); // Navigate to MenuPage
            }
        }
    }
}
