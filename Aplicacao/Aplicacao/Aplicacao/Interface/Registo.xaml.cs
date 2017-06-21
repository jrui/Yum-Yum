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
        Database db;

        public Registo(Database d)
        {
            db = d;
            NavigationPage.SetHasNavigationBar(this, false);
            InitializeComponent();
        }

        private async void RegistarButton_Clicked(object sender, EventArgs e)
        {
            Boolean existente = false;
            if (Password.Text == null || Email.Text == null || ConfirmaPassword.Text == null || Utilizador.Text == null)
                await DisplayAlert("Aviso", "Preencha todos os Campos", "Ok");
            else
            {
                if (Password.Text.Equals(ConfirmaPassword.Text))
                {
                    foreach(Utilizador u in db.utilizador)
                    {
                        if (u.username.Equals(Utilizador.Text)) {
                            await DisplayAlert("Aviso", "Username Existente", "Ok");
                            existente = true;
                            break;
                        }
                    }
                    if (!existente)
                    {
                        Utilizador novo = new Utilizador(db.utilizador.Count + 1, Email.Text, Utilizador.Text, Password.Text, db.filtros.Count+1);
                        db.utilizador.Add(novo);
                        db.curr_user = novo;
                        Filtro filtro = new Filtro(db.filtros.Count + 1, 0, 0, 0);
                        db.filtros.Add(filtro);
                        for(int i = 1; i <= 27; i++)
                        {
                            FiltroCozinha cozinha = new FiltroCozinha(i, db.filtros.Count);
                            db.filtroCozinha.Add(cozinha);
                        }
                        await Navigation.PushAsync(new MenuPrincipal(db));
                    }
                }
                else await DisplayAlert("Aviso", "Passwords não coincidem", "Ok");
            }
        }
    }
}
