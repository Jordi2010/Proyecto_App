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
	public partial class tablaPuntajes : ContentPage
	{
		public tablaPuntajes ()
		{

			InitializeComponent ();
            // Eliminar el botón de retroceso por defecto
            NavigationPage.SetHasBackButton(this, false);

        }
        private async void OnSeccionesClickedBack(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new menu());
        }
        private async void OnExitButtonClickedInicio(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new menu()); // Navigate to MenuPage directly
        }
    }
}