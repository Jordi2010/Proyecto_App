using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using zionAppSensorial.views.animalesViewsTercerNivel;
using zionAppSensorial.views.animalviews;
using zionAppSensorial.views.animalViewsSegundoNivel;
using zionAppSensorial.views.frutasviews;

namespace zionAppSensorial.views.pantallas
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class niveles : ContentPage
    {
        // Campo privado para almacenar la sección actual
        private readonly string section;
        // Constructor que recibe una sección como parámetro
        public niveles(string section)
        {
            // Eliminar el botón de retroceso por defecto
            NavigationPage.SetHasBackButton(this, false);
            InitializeComponent();
            // Asignar el parámetro de sección al campo privado
            this.section = section;
        }
        // Método que se ejecuta al hacer clic en el botón de nivel fácil
        async void OnButtonClickedEasy(object sender, EventArgs e)
        {
            // Navegar a la pantalla de nivel fácil de animales
            if (section == "animales")
            {
                await Navigation.PushAsync(new Pantalla1());
            }
            else if (section == "frutas")
            {
                // Navegar a la pantalla de nivel fácil de frutas
                await Navigation.PushAsync(new Pantalla9());
            }
        }

        // Método que se ejecuta al hacer clic en el botón de nivel medio
        async void OnButtonClickedMiddle(object sender, EventArgs e)
        {
            // Método que se ejecuta al hacer clic en el botón de nivel difícil
            if (section == "animales")
            {
                await Navigation.PushAsync(new gatoVistaN2());
            }
            else if (section == "frutas")
            {
                // Navegar a la pantalla de nivel medio de frutas (comentado porque no está implementado)
                // await Navigation.PushAsync(new PantallaDeFrutasNivelMedio());
            }
        }
        // Método que se ejecuta al hacer clic en el botón de nivel difícil
        async void OnButtonClickedDifficult(object sender, EventArgs e)
        {
            // Navegar a la pantalla de nivel difícil de animales
            if (section == "animales")
            {
                await Navigation.PushAsync(new aguilaVista());
            }
            else if (section == "frutas")
            {
                // Navegar a la pantalla de nivel difícil de frutas (comentado porque no está implementado)
                // await Navigation.PushAsync(new PantallaDeFrutasNivelDificil());
            }
        }
        // Método que se ejecuta al hacer clic en el botón de salida
        private async void OnExitButtonClickedNiveles(object sender, EventArgs e)
        {
            // Navegar a la página del menú principal
            await Navigation.PushAsync(new menu());
        }
    }
}