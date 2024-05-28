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
    public partial class inicio : ContentPage
    {
        public inicio()
        {
            // Eliminar el botón de retroceso por defecto
            NavigationPage.SetHasBackButton(this, false);
            InitializeComponent();
        }
       
        async void OnPlayButtonClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new menu());
        }
        async void OnHowToPlayButtonClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new tutorialVista());
        }
    }
}