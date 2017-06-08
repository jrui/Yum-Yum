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
    public partial class MenuPrincipal : ContentPage
    {
        public MenuPrincipal()
        {
            NavigationPage.SetHasNavigationBar(this, false);
            InitializeComponent();
        }

        private void FiltrosButton_Clicked(object sender, EventArgs e)
        {
            PainelPerfil.IsVisible = false;
            PainelFiltros.IsVisible = true;
        }

        private void PerfilButton_Clicked(object sender, EventArgs e)
        {
            PainelPerfil.IsVisible = true;
            PainelFiltros.IsVisible = false;
        }

        private async void CozinhasToggle(object sender, EventArgs e)
        {
            Switch sw = sender as Switch;
            if (sw != null)
            {
                if (sw.IsToggled == true)
                {
                    await Navigation.PushAsync(new MenuPrincipal());
                }
            }
        }

        private void ScrollChanged(object sender, ScrolledEventArgs e)
        {
            ScrollView sen = sender as ScrollView;
            if (sen.Id.Equals("sv1"))
            {
                EstadosF.ScrollToAsync(0,e.ScrollY, true);
                EstadosP.ScrollToAsync(0, e.ScrollY, true);
            }
            else
            {
                NomesF.ScrollToAsync(0, e.ScrollY, true);
                NomesP.ScrollToAsync(0, e.ScrollY, true);
            }
        }
    }
}
