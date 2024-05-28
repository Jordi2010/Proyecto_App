using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using zionAppSensorial.views.frutasviews;

namespace zionAppSensorial.views.pantallas
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class menu : ContentPage
    {
        public menu()
        {
            InitializeComponent();

            // Eliminar el botón de retroceso por defecto
            NavigationPage.SetHasBackButton(this, false);
        }
        // Método que se ejecuta al hacer clic en el botón de animales
        async void OnAnimalesButtonClicked(object sender, EventArgs e)
        {
            // Navegar a la página de niveles para la sección de animales
            await Navigation.PushAsync(new niveles("animales"));
        }
        // Método que se ejecuta al hacer clic en el botón de frutas
        async void OnFruitsButtonClicked(object sender, EventArgs e)
        {
            // Navegar a la página de niveles para la sección de frutas
            await Navigation.PushAsync(new niveles("frutas"));
        }
        // Método que se ejecuta al hacer clic en el botón de salida al inicio
        private async void OnExitButtonClickedInicio(object sender, EventArgs e)
        {
            // Navegar a la página de inicio
            await Navigation.PushAsync(new inicio());
        }
        // Método que se ejecuta al hacer clic en el botón de ver tabla de puntajes
        private async void ViewButtonClickedTabla(object sender, EventArgs e)
        {
            // Navegar a la página de tabla de puntajes
            await Navigation.PushAsync(new tablaPuntajes());
        }
        // Método que se ejecuta al hacer clic en el botón de salida en el menú
        private async void OnExitButtonClickedMenu(object sender, EventArgs e)
        {
            // Navegar a la página de inicio
            await Navigation.PushAsync(new inicio());
        }
    }
}