using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System.Diagnostics;

namespace Aplicacao.Interface
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MenuPrincipal : ContentPage
    {
        Database db;
        Boolean carregar = true;
        List<int> filtros = new List<int>();
        double preco_max = -1;
        double distancia_max = -1;
        double rating_min = -1;
        List<int> ultimos = null;
        List<int> restaurantes = new List<int>();

        public MenuPrincipal(Database d)
        {
            db = d;
            NavigationPage.SetHasNavigationBar(this, false);
            InitializeComponent();
            AddRestauranteTrending();
            if (db.curr_user != null)
            {
                CarregaPerfil();
                //CarregaUltimos();
            }
            carregar = false;
        }




        /** Esta funcao nao recebe argumentos e devolve uma lista com o objecto restaurante
         *  contendo os ultimos 5 restaurantes ordenados do mais recente para o mais antigo
         *  que o utilizador autenticado já visitou, caso nao haja 5 restaurantes, devolve o
         *  máximo possivel
         */
        private List<Restaurante> GetUltimosRest_User()
        {
            List<Restaurante> ret = new List<Restaurante>();

            //Tendo em conta que existe um objecto do tipo:    Database db
            // existente na classe em que esta função está implementada
            List<Restaurante> aux = new List<Restaurante>(db.restaurante);
            List<RestaurantesVisitados> restVis = new List<RestaurantesVisitados>();

            foreach(RestaurantesVisitados r in db.restaurantesVisitados)
                if (r.utilizador == db.curr_user.id_utilizador)
                    restVis.Add(r);
            
            RestaurantesVisitados restSelected = null;
            List<RestaurantesVisitados> lastRestVisit = new List<RestaurantesVisitados>();
            int last_year, last_month, last_day;
            bool more = restVis.Count > 0 ? true : false;

            for (int i = 0; i < 5 && more; i++)
            {
                last_year = last_month = last_day = -1;

                foreach (RestaurantesVisitados r in restVis)
                    if(r.data_ano >= last_year)
                        if(r.data_mes >= last_month)
                            if(r.data_mes >= last_day)
                            {
                                restSelected = r;
                                last_year = r.data_ano;
                                last_month = r.data_mes;
                                last_day = r.data_dia;
                            }

                lastRestVisit.Add(restSelected);
                restVis.Remove(restSelected);
                more = restVis.Count > 0 ? true : false;
            }

            foreach(RestaurantesVisitados rvistemp in lastRestVisit)
                foreach(Restaurante restaurante in db.restaurante)
                    if (restaurante.id == rvistemp.restaurante)
                        ret.Add(restaurante);

            return ret;
        } 



        //OBSOLETE
        /*private void CarregaUltimos()
        {
            int size = 0, k = 0, j = 0;
            Dictionary<int, int> aux = new Dictionary<int, int>();
            DateTime now = DateTime.Now;
            List<int> aux2 = new List<int>();
            foreach (RestaurantesVisitados r in db.restaurantesVisitados)
            {
                if (now.Year * 365.25 + now.Month * 30 + now.Day <= 14 + r.data_dia + r.data_ano * 365.25 + r.data_mes * 31 && r.utilizador == db.curr_user.id_utilizador)
                {
                    if (aux.ContainsKey(r.restaurante))
                    {
                        aux[r.restaurante]++;
                    }
                    else
                    {
                        aux2.Add(r.restaurante);
                        aux.Add(r.restaurante, 1);
                    }
                }
            }
            for (int a = 0; a < 5; a++)
                AddRestauranteUltimos(aux2[a]);
        }*/

        private void CarregaPerfil()
        {

            foreach (FiltroCozinha f in db.filtroCozinha)
            {
                if (f.filtro == db.curr_user.filtro)
                {
                    if (f.cozinha == 1) Switch1.IsToggled = true;
                    else if (f.cozinha == 2) Switch2.IsToggled = true;
                    else if (f.cozinha == 3) Switch3.IsToggled = true;
                    else if (f.cozinha == 4) Switch4.IsToggled = true;
                    else if (f.cozinha == 5) Switch5.IsToggled = true;
                    else if (f.cozinha == 6) Switch6.IsToggled = true;
                    else if (f.cozinha == 7) Switch7.IsToggled = true;
                    else if (f.cozinha == 8) Switch8.IsToggled = true;
                    else if (f.cozinha == 9) Switch9.IsToggled = true;
                    else if (f.cozinha == 10) Switch10.IsToggled = true;
                    else if (f.cozinha == 11) Switch11.IsToggled = true;
                    else if (f.cozinha == 12) Switch12.IsToggled = true;
                    else if (f.cozinha == 13) Switch13.IsToggled = true;
                    else if (f.cozinha == 14) Switch14.IsToggled = true;
                    else if (f.cozinha == 15) Switch15.IsToggled = true;
                    else if (f.cozinha == 16) Switch16.IsToggled = true;
                    else if (f.cozinha == 17) Switch17.IsToggled = true;
                    else if (f.cozinha == 18) Switch18.IsToggled = true;
                    else if (f.cozinha == 19) Switch19.IsToggled = true;
                    else if (f.cozinha == 20) Switch20.IsToggled = true;
                    else if (f.cozinha == 21) Switch21.IsToggled = true;
                    else if (f.cozinha == 22) Switch22.IsToggled = true;
                    else if (f.cozinha == 23) Switch23.IsToggled = true;
                    else if (f.cozinha == 24) Switch24.IsToggled = true;
                    else if (f.cozinha == 25) Switch25.IsToggled = true;
                    else if (f.cozinha == 26) Switch26.IsToggled = true;
                    else if (f.cozinha == 27) Switch27.IsToggled = true;
                }
            }

        }

        private void FiltrosButton_Clicked(object sender, EventArgs e)
        {
            PainelPerfil.IsVisible = false;
            PainelFiltros.IsVisible = true;
            PainelSugestoes.IsVisible = false;
            PainelResultados.IsVisible = false;
        }

        private void PerfilButton_Clicked(object sender, EventArgs e)
        {
            PainelPerfil.IsVisible = true;
            PainelFiltros.IsVisible = false;
            PainelSugestoes.IsVisible = false;
            PainelResultados.IsVisible = false;
        }

        private void SugestoesButton_Clicked(object sender, EventArgs e)
        {
            PainelPerfil.IsVisible = false;
            PainelFiltros.IsVisible = false;
            PainelSugestoes.IsVisible = true;
            PainelResultados.IsVisible = false;
        }

        private async void ResultadosButton_Clicked(object sender, EventArgs e)
        {
            PainelPerfil.IsVisible = false;
            PainelFiltros.IsVisible = false;
            PainelSugestoes.IsVisible = false;
            PainelResultados.IsVisible = true;
        }

        private async void DistanciaFButton_Clicked(object sender, EventArgs e)
        {
            if (DistanciaF.Text != null)
                Double.TryParse(DistanciaF.Text, out distancia_max);
            else await DisplayAlert("Aviso", "Insira um número", "Ok");
        }

        private async void DistanciaPButton_Clicked(object sender, EventArgs e)
        {
            if (DistanciaP.Text != null)
            {
                int f = db.curr_user.filtro;
                foreach (Filtro filtro in db.filtros)
                {
                    if (filtro.id_filtro == f)
                    {
                        Double.TryParse(DistanciaP.Text, out filtro.distancia_max);
                        break;
                    }
                }
            }
            else await DisplayAlert("Aviso", "Insira um número", "Ok");
        }

        private async void RatingPButton_Clicked(object sender, EventArgs e)
        {
            if (RatingP.SelectedItem != null)
            {
                int f = db.curr_user.filtro;
                foreach (Filtro filtro in db.filtros)
                {
                    if (filtro.id_filtro == f)
                    {
                        Double.TryParse(RatingP.SelectedItem.ToString(), out filtro.rating_min);
                        break;
                    }
                }
            }
            else await DisplayAlert("Aviso", "Escolha um rating", "Ok");
        }

        private async void RatingFButton_Clicked(object sender, EventArgs e)
        {
            if (RatingF.SelectedItem != null)
                Double.TryParse(RatingF.SelectedItem.ToString(), out rating_min);
            else await DisplayAlert("Aviso", "Escolha um rating", "Ok");
        }

        private async void PrecoPButton_Clicked(object sender, EventArgs e)
        {
            if (PrecoP.Text != null)
            {
                int f = db.curr_user.filtro;
                foreach (Filtro filtro in db.filtros)
                {
                    if (filtro.id_filtro == f)
                    {
                        Double.TryParse(PrecoP.Text, out filtro.preco_max);
                        break;
                    }
                }
            }
            else await DisplayAlert("Aviso", "Insira um número", "Ok");
        }

        private async void PrecoFButton_Clicked(object sender, EventArgs e)
        {
            if (PrecoF.Text != null)
                Double.TryParse(PrecoF.Text, out preco_max);
            else await DisplayAlert("Aviso", "Insira um número", "Ok");
        }

        private void RemoveCozinha(int id)
        {
            foreach (FiltroCozinha f in db.filtroCozinha)
            {
                if (f.filtro == db.curr_user.filtro)
                {
                    if (f.cozinha == id)
                    {
                        db.filtroCozinha.Remove(f);
                        break;
                    }

                }
            }
        }

        private void AdicionaCozinha(int id)
        {
            FiltroCozinha f = new FiltroCozinha(id, db.curr_user.filtro);
            db.filtroCozinha.Add(f);
        }

        private void CozinhasToggleFiltros(object sender, EventArgs e)
        {
            Switch sw = sender as Switch;
            if (sw != null)
            {
                int id;
                Int32.TryParse(sw.AutomationId, out id);
                if (sw.IsToggled) filtros.Add(id);
                else filtros.Remove(id);
            }
        }

        private void CozinhasTogglePerfil(object sender, EventArgs e)
        {
            if (!carregar)
            {
                Switch sw = sender as Switch;
                if (sw != null)
                {
                    int id;
                    Int32.TryParse(sw.AutomationId, out id);
                    if (sw.IsToggled) AdicionaCozinha(id);
                    else RemoveCozinha(id);
                }
            }
        }

        private void ScrollChanged(object sender, ScrolledEventArgs e)
        {
            ScrollView sen = sender as ScrollView;
            if (sen.Id.Equals("sv1"))
            {
                EstadosF.ScrollToAsync(0, e.ScrollY, true);
                EstadosP.ScrollToAsync(0, e.ScrollY, true);
            }
            else
            {
                NomesF.ScrollToAsync(0, e.ScrollY, true);
                NomesP.ScrollToAsync(0, e.ScrollY, true);
            }
        }

        private void AddRestauranteTrending()
        {


        }

        private void AddRestauranteUltimos(Restaurante res)
        {
            Image image = new Image
            {
                Aspect = Aspect.AspectFill,
                WidthRequest = 70,
                HeightRequest = 70
            };
            image.Source = ImageSource.FromUri(new Uri(res.imagem));
            Button but = new Button
            {
                BackgroundColor = Color.Transparent,
                WidthRequest = 70,
                HeightRequest = 70,
            };

            SugestoesImagensUltimos.Children.Add(image);
            SugestoesButtonsUltimos.Children.Add(but);
            Label lab = new Label { Text = res.nome, HorizontalTextAlignment = TextAlignment.Center, FontAttributes = FontAttributes.Bold };
            SugestoesInfoUltimos.Children.Add(lab);
            Label labe = new Label { Text = "Preço Médio: " + res.preco_medio + "€"};
            SugestoesInfoUltimos.Children.Add(labe);
            Label la = new Label { Text = "Distancia: N/A     " + "Rating: " + res.rating };
            SugestoesInfoUltimos.Children.Add(la);
        }

        private void PesquisarButton_Clicked(object sender, EventArgs e)
        {
            restaurantes = new List<int>();
            List<int> perfil = new List<int>();
            foreach(FiltroCozinha f in db.filtroCozinha)
            {
                if (f.filtro == db.curr_user.filtro)
                    perfil.Add(f.cozinha);
            }
            SearchBar search = sender as SearchBar;
            int i = 0;
            String pesquisa = search.Text.ToUpper();
            foreach (Prato p in db.prato)
            {
                if (p.nome.Contains(pesquisa))
                {
                    if (!restaurantes.Contains(p.restaurante))
                        restaurantes.Add(p.restaurante);
                }
            }

            /*
             * FALTA DAR RESET AOS RESTAURANTES JA PRESENTES NA LISTAGEM
             * TANTO NAS SUGESTOES POR TRENDING COMO POR ULTIMOS
             */
            for (i = 0; i < restaurantes.Count; i++)
            {
                foreach (CozinhaRestaurante c in db.cozinhaRestaurante)
                {
                    if (c.restaurante == restaurantes[i] && perfil.Contains(c.cozinha))
                    {
                        AddResultados(restaurantes[i], i);
                        break;
                    }
                }
            }
            foreach(Restaurante rest in GetUltimosRest_User())
                AddRestauranteUltimos(rest);
        }

        private void AddResultados(int id, int indice)
        {
            Restaurante res = null;
            foreach (Restaurante r in db.restaurante)
            {
                if (r.id == id)
                {
                    res = r;
                    break;
                }

            }

            Image image = new Image
            {
                Aspect = Aspect.AspectFill,
                WidthRequest = 70,
                HeightRequest = 70
            };
            image.Source = ImageSource.FromUri(new Uri(res.imagem));
            Button but = new Button
            {
                BackgroundColor = Color.Transparent,
                WidthRequest = 70,
                HeightRequest = 70,
            };
            but.AutomationId = indice.ToString();
            but.Clicked += ShowRestaurante;
            ResultadosImagens.Children.Add(image);
            ResultadosButtons.Children.Add(but);
            Label lab = new Label { Text = res.nome, HorizontalTextAlignment = TextAlignment.Center, FontAttributes = FontAttributes.Bold };
            ResultadosInfo.Children.Add(lab);
            Label labe = new Label { Text = "Preço Médio: " + res.preco_medio + "€" };
            ResultadosInfo.Children.Add(labe);
            Label la = new Label { Text = "Distancia: N/A     " + "Rating: " + res.rating };
            ResultadosInfo.Children.Add(la);
        }

        private async void ShowRestaurante(object sender, EventArgs e)
        {
            int indice;
            Button but = sender as Button;
            Int32.TryParse(but.AutomationId, out indice);
            int id_res = restaurantes[indice];
            foreach(Restaurante r in db.restaurante)
            {
                if (r.id == id_res)
                {
                    await Navigation.PushAsync(new Restaurantes(db, r));
                    break;
                }

            }
        }
    }
}
