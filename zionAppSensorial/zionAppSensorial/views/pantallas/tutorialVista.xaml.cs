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
	public partial class tutorialVista : ContentPage
	{
		public tutorialVista ()
		{
            // Eliminar el botón de retroceso por defecto
            NavigationPage.SetHasBackButton(this, false);
            InitializeComponent ();
		}

        private async void OnExitButtonClickedTutorial(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new inicio());
        }
    }
}