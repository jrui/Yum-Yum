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

        private void AddImagemTrending(object sender, EventArgs e)
        {
            Button but = new Button();
            but.Image = "icon";
            SugestoesImagensTrending.Children.Add(but);
            Label lab = new Label { Text = "Taberna Só", HorizontalTextAlignment=TextAlignment.Center, FontAttributes=FontAttributes.Bold};
            SugestoesInfoTrending.Children.Add(lab);
            Label labe = new Label { Text = "Taberna Aleh" };
            SugestoesInfoTrending.Children.Add(labe);
            Label la = new Label { Text = "Taberna Aleh && rating" };
            SugestoesInfoTrending.Children.Add(la);

        }

        private void AddImagemUltimos(object sender, EventArgs e)
        {
            Button but = new Button();
            but.Image = "icon";
            SugestoesImagensUltimos.Children.Add(but);
            Label lab = new Label { Text = "Taberna Só", HorizontalTextAlignment = TextAlignment.Center, FontAttributes = FontAttributes.Bold };
            SugestoesInfoUltimos.Children.Add(lab);
            Label labe = new Label { Text = "Taberna Aleh" };
            SugestoesInfoUltimos.Children.Add(labe);
            Label la = new Label { Text = "Taberna Aleh && rating" };
            SugestoesInfoUltimos.Children.Add(la);

        }

        private void FiltrosButton_Clicked(object sender, EventArgs e)
        {
            PainelPerfil.IsVisible = false;
            PainelFiltros.IsVisible = true;
            PainelSugestoes.IsVisible = false;
        }

        private void PerfilButton_Clicked(object sender, EventArgs e)
        {
            PainelPerfil.IsVisible = true;
            PainelFiltros.IsVisible = false;
            PainelSugestoes.IsVisible = false;
        }

        private void SugestoesButton_Clicked(object sender, EventArgs e)
        {
            PainelPerfil.IsVisible = false;
            PainelFiltros.IsVisible = false;
            PainelSugestoes.IsVisible = true;
        }

        private async void ResultadosButton_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Restaurante());
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
