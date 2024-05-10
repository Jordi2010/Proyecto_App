using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using zionAppSensorial.views.animalesViewsTercerNivel;
using zionAppSensorial.views.animalviewsPrimerNivel;
using zionAppSensorial.views.frutasViewsTercerNivel;
using zionAppSensorial.views.objetosViewsTercerNivel;
using zionAppSensorial.views.pantallas;

namespace zionAppSensorial
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            MainPage = new NavigationPage(new aguilaVista());
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
