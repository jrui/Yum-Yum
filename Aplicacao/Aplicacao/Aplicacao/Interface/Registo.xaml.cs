using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Aplicacao.Interface
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Registo : ContentPage
    {
        public Registo()
        {
            NavigationPage.SetHasNavigationBar(this, false);
            InitializeComponent();
        }

        private async void RegistarButton_Clicked(object sender, EventArgs e)
        {
            if (Password.Text == null || Email.Text == null || ConfirmaPassword.Text == null || Utilizador.Text == null)
                await DisplayAlert("Aviso", "Preencha todos os Campos", "Ok");
            else
            {
                if (Password.Text.Equals(ConfirmaPassword.Text))
                {
                    if (!Utilizador.Text.Equals("Username"))
                    {
                        Utilizador.Text = " Registado Utilizador " + Utilizador.Text;
                        Email.Text = " Registado Utilizador " + Email.Text;
                    }
                    else await DisplayAlert("Aviso", "Username Utilizado", "Ok");
                }
                else await DisplayAlert("Aviso", "Passwords não coincidem", "Ok");
            }
        }
    }
}
