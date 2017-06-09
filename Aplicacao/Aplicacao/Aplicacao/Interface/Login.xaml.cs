using Aplicacao.Interface;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Aplicacao
{

    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Login : ContentPage
    {
        public Database db;
        public Login(Database database)
        {
            NavigationPage.SetHasNavigationBar(this, false);
            InitializeComponent();
            db = database;
        }

        private async void IniciarButton_Clicked(object sender, EventArgs e)
        {
            Boolean password = false;
            if (Utilizador.Text == null || Password.Text == null) await DisplayAlert("Aviso", "Preencha todos os Campos", "Ok");
            else
            {
                foreach(Utilizador u in db.utilizador) {
                    if (u.username.Equals(Utilizador.Text))
                    {
                        if (u.password.Equals(Password.Text))
                        {
                            db.curr_user = u;
                            await Navigation.PushAsync(new MenuPrincipal(db));
                            password = true;
                            break;
                        }
                        else
                        {
                            await DisplayAlert("Aviso", "Password Errada", "Ok");
                            password = true;
                        }
                    }
                }
                if(!password) await DisplayAlert("Aviso", "Username inválido", "Ok");
            }
        }
    }
}
