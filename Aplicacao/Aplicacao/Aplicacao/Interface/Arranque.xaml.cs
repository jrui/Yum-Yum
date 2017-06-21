using Aplicacao.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Aplicacao
{
    public partial class Arranque : ContentPage
    {
        Database database;
        public Arranque()
        {
            NavigationPage.SetHasNavigationBar(this, false);
            InitializeComponent();
            database = new Database();
        }

        private async void LoginButton_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Login(database));
        }

        private async void ConvidadoButton_Clicked(object sender, EventArgs e)
        {
            database.curr_user = null;
            await Navigation.PushAsync(new MenuPrincipal(database));
        }

        private async void RegistarButton_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Registo(database));
        }

        private async void AboutButton_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new About());
        }
    }
}
