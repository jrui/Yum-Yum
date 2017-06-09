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
        public Login()
        {
            NavigationPage.SetHasNavigationBar(this, false);
            InitializeComponent();
        }

        private async void IniciarButton_Clicked(object sender, EventArgs e)
        {
            if (Utilizador.Text == null || Password.Text == null) await DisplayAlert("Aviso", "Preencha todos os Campos", "Ok");
            else
            {
                if (Password.Text.Equals("Password") && Utilizador.Text.Equals("Username"))
                    await Navigation.PushAsync(new MenuPrincipal());
                else await DisplayAlert("Aviso", "Credenciais Erradas", "Ok");
            }
        }
    }
}
