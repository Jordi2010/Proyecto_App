using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace zionAppSensorial.views.pantallas
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class felicidades : ContentPage
    {
        // Campo privado para almacenar la sección actual
        private readonly string section;
        // Constructor que recibe una sección como parámetro
        public felicidades(string section)
        {
            // Eliminar el botón de retroceso por defecto
            NavigationPage.SetHasBackButton(this, false);
            InitializeComponent();
            // Asignar el parámetro de sección al campo privado
            this.section = section;
        }
        // Método que maneja el clic en la imagen de puntajes
        private async void OnImgPuntajeClicked(object sender, EventArgs e)
        {
            // Navegar a la página de tabla de puntajes
            await Navigation.PushAsync(new tablaPuntajes());
        }
        // Método que maneja el clic en la imagen de dificultades
        private async void OnImgDificultadesClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new niveles(section));
        }
        // Navegar a la página de niveles, pasando la sección actual
        private async void OnImgSeccionesClicked(object sender, EventArgs e)
        {
            // Navegar a la página del menú principal
            await Navigation.PushAsync(new menu());
        }
    }
}