﻿using System;
using System.Collections.Generic;
using System.Linq;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Aplicacao.Interface
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MenuPrincipal : ContentPage
    {
        Database db;
        Boolean carregar = true;
        List<int> filtros = new List<int>();
        double preco_max = 0;
        double distancia_max = 0;
        double rating_min = 0;
        List<int> restaurantes = new List<int>();

        //Carrega os componentes iniciais, dependendo se é convidado ou utilizador normal
        public MenuPrincipal(Database d)
        {
            db = d;
            NavigationPage.SetHasNavigationBar(this, false);
            InitializeComponent();
            if (db.curr_user != null)
            {
                CarregaPerfil();
                GetUltimosRest_User();
            }
            if (db.curr_user == null)
            {
                DisplayAlert("Login", "Iniciada sessão convidado.", "OK");
                PainelPerfil.IsVisible = false;
            }
            GetUltimos_Trending();
            carregar = false;
        }


        /** Esta funcao nao recebe argumentos e retorna uma lista contendo os restaurantes mais
         *  visitados na ultima semana, por todos os utilizadores.
         */
        private void GetUltimos_Trending()
        {
            List<Restaurante> ret = new List<Restaurante>();
            List<RestaurantesVisitados> temp = new List<RestaurantesVisitados>();
            DateTime data_now = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day);

            // seleciona ultima semana
            foreach (RestaurantesVisitados r in db.restaurantesVisitados)
            {
                DateTime data_vis = new DateTime(r.data_ano, r.data_mes, r.data_dia);
                if (data_vis.AddDays(7) >= data_now) temp.Add(r);
            }

            //int id  ->  int visitas
            // conta n visitas individuais
            Dictionary<int, int> visits = new Dictionary<int, int>();
            foreach (RestaurantesVisitados r in temp)
            {
                int n_visitas = 0;
                if (visits.TryGetValue(r.restaurante, out n_visitas)) visits[r.restaurante] = n_visitas++;
                else visits.Add(r.restaurante, 1);
            }

            //seleciona 5 melhores 
            List<int> best_ids = new List<int>();
            int n_max, index_best;
            bool more = visits.Count > 0 ? true : false;
            for (int i = 0; i < 5 && more; i++) {
                n_max = 0;
                index_best = -1;

                foreach (int j in visits.Keys)
                {
                    if (visits[j] > n_max)
                    {
                        n_max = visits[j];
                        index_best = j;
                    }
                }

                best_ids.Add(index_best);
                visits.Remove(index_best);
                more = best_ids.Count > 0 ? true : false;
            }


            // faz fetch dos restaurantes associados
            foreach (int i in best_ids)
                foreach(Restaurante r in db.restaurante)
                    if (i == r.id)
                    {
                        ret.Add(r);
                        break;
                    }

            //Reset da view
            int k = 0;
            for (k = SugestoesImagensTrending.Children.Count - 1; k >= 0; k--)
            {
                SugestoesImagensTrending.Children.RemoveAt(k);
                SugestoesButtonsTrending.Children.RemoveAt(k);
            }

            for (k = SugestoesInfoTrending.Children.Count - 1; k >= 0; k--)
            {
                SugestoesInfoTrending.Children.RemoveAt(k);
            }

            foreach (Restaurante r in ret) AddTrending(r);
        }

        private void AddTrending(Restaurante res)
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
            but.AutomationId = res.id.ToString();
            but.Clicked += ShowRestaurante;
            SugestoesImagensTrending.Children.Add(image);
            SugestoesButtonsTrending.Children.Add(but);
            Label lab = new Label { Text = res.nome, HorizontalTextAlignment = TextAlignment.Center, FontAttributes = FontAttributes.Bold };
            SugestoesInfoTrending.Children.Add(lab);
            Label labe = new Label { Text = "Preço Médio: " + res.preco_medio + "€" };
            SugestoesInfoTrending.Children.Add(labe);
            Label la = new Label { Text = "Distancia: N/A     " + "Rating: " + res.rating };
            SugestoesInfoTrending.Children.Add(la);
        }



        /** Esta funcao nao recebe argumentos e atualiza a view dos últimos restaurantes visitados com 
         *  os ultimos 5 restaurantes ordenados do mais recente para o mais antigo
         *  que o utilizador autenticado já visitou, caso nao haja 5 restaurantes, atualiza com o
         *  máximo possivel
         */
        private void GetUltimosRest_User()
        {
            int i = 0;
            List<Restaurante> ret = new List<Restaurante>();

            /*Tendo em conta que existe um objecto do tipo:    Database db
             * existente na classe em que esta função está implementada
             */
            List<Restaurante> aux = new List<Restaurante>(db.restaurante);
            List<RestaurantesVisitados> restVis = new List<RestaurantesVisitados>();

            foreach(RestaurantesVisitados r in db.restaurantesVisitados)
                if (r.utilizador == db.curr_user.id_utilizador)
                    restVis.Add(r);
            
            RestaurantesVisitados restSelected = null;
            List<RestaurantesVisitados> lastRestVisit = new List<RestaurantesVisitados>();
            int last_year, last_month, last_day;
            bool more = restVis.Count > 0 ? true : false;

            for (i = 0; i < 5 && more; i++)
            {
                last_year = last_month = last_day = -1;

                foreach (RestaurantesVisitados r in restVis)
                    if(r.data_ano >= last_year)
                        if(r.data_mes >= last_month)
                            if(r.data_dia >= last_day)
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

            //Reset da view
            for (i = SugestoesImagensUltimos.Children.Count - 1; i >= 0; i--)
            {
                SugestoesImagensUltimos.Children.RemoveAt(i);
                SugestoesButtonsUltimos.Children.RemoveAt(i);
            }

            for (i = SugestoesInfoUltimos.Children.Count - 1; i >= 0; i--)
            {
                SugestoesInfoUltimos.Children.RemoveAt(i);
            }
            //fim do reset

            foreach (Restaurante rest in ret)
                AddRestauranteUltimos(rest);

        }

        private List<Restaurante> GetHistoricoRest_User()
        {
            int i = 0;
            List<Restaurante> ret = new List<Restaurante>();

            /*Tendo em conta que existe um objecto do tipo:    Database db
             * existente na classe em que esta função está implementada
             */
            List<Restaurante> aux = new List<Restaurante>(db.restaurante);
            List<RestaurantesVisitados> restVis = new List<RestaurantesVisitados>();

            //Reset da view
            for (i = HistoricoImagens.Children.Count - 1; i >= 0; i--)
            {
                HistoricoImagens.Children.RemoveAt(i);
                HistoricoButtons.Children.RemoveAt(i);
            }

            for (i = HistoricoInfo.Children.Count - 1; i >= 0; i--)
            {
                HistoricoInfo.Children.RemoveAt(i);
            }
            //fim do reset

            foreach (RestaurantesVisitados r in db.restaurantesVisitados)
                if (r.utilizador == db.curr_user.id_utilizador)
                    restVis.Add(r);

            RestaurantesVisitados restSelected = null;
            List<RestaurantesVisitados> lastRestVisit = new List<RestaurantesVisitados>();
            int last_year, last_month, last_day;
            bool more = restVis.Count > 0 ? true : false;

            int n = restVis.Count();

            for (i = 0; i < n; i++)
            {
                last_year = last_month = last_day = -1;

                foreach (RestaurantesVisitados r in restVis)
                    if (r.data_ano >= last_year)
                        if (r.data_mes >= last_month)
                            if (r.data_dia >= last_day)
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

            foreach (RestaurantesVisitados rvistemp in lastRestVisit)
                foreach (Restaurante restaurante in db.restaurante)
                    if (restaurante.id == rvistemp.restaurante)
                        ret.Add(restaurante);

            return ret;

        }


        //Inicia os switchs dependendo do seu perfil
        private void CarregaPerfil()
        {
            if (db.curr_user != null)
            {
                List<Restaurante> historico = GetHistoricoRest_User();
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
                foreach (Restaurante r in historico)
                    AddHistorico(r);
            }

        }

        private void AddHistorico(Restaurante res)
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
            but.AutomationId = res.id.ToString();
            but.Clicked += ShowRestaurante;
            HistoricoImagens.Children.Add(image);
            HistoricoButtons.Children.Add(but);
            Label lab = new Label { Text = res.nome, HorizontalTextAlignment = TextAlignment.Center, FontAttributes = FontAttributes.Bold };
            HistoricoInfo.Children.Add(lab);
            Label labe = new Label { Text = "Preço Médio: " + res.preco_medio + "€" };
            HistoricoInfo.Children.Add(labe);
            Label la = new Label { Text = "Distancia: N/A     " + "Rating: " + res.rating };
            HistoricoInfo.Children.Add(la);
        }

        /*Atualiza as views, colocando a view do filtro visivel e todas as outras
        * não visiveis
        */
        private void FiltrosButton_Clicked(object sender, EventArgs e)
        {
            PainelPerfil.IsVisible = false;
            PainelFiltros.IsVisible = true;
            PainelSugestoes.IsVisible = false;
            PainelResultados.IsVisible = false;
        }

        /*Atualiza as views, colocando a view do perfil visivel e todas as outras
        * não visiveis, e carrega o perfil, atualizando o histórico
        */
        private void PerfilButton_Clicked(object sender, EventArgs e)
        {
            if (db.curr_user != null)
            {
                PainelPerfil.IsVisible = true;
                PainelFiltros.IsVisible = false;
                PainelSugestoes.IsVisible = false;
                PainelResultados.IsVisible = false;
                if (db.curr_user != null)
                    CarregaPerfil();
            }
        }

        /*Atualiza as views, colocando a view das sugestões visivel e todas as outras
        * não visiveis
        */
        private void SugestoesButton_Clicked(object sender, EventArgs e)
        {
            PainelPerfil.IsVisible = false;
            PainelFiltros.IsVisible = false;
            PainelSugestoes.IsVisible = true;
            PainelResultados.IsVisible = false;
            if(db.curr_user != null) GetUltimosRest_User();
            GetUltimos_Trending();
        }

        /*Atualiza as views, colocando a view dos resultados visivel e todas as outras
        * não visiveis, e atualiza os últimos restaurantes visitados
        */
        private void ResultadosButton_Clicked(object sender, EventArgs e)
        {
            PainelPerfil.IsVisible = false;
            PainelFiltros.IsVisible = false;
            PainelSugestoes.IsVisible = false;
            PainelResultados.IsVisible = true;
        }

        // Guarda a distância maxima dos filtros adicionais entre o utilizador e o restaurante
        private async void DistanciaFButton_Clicked(object sender, EventArgs e)
        {
            if (DistanciaF.Text != null)
                Double.TryParse(DistanciaF.Text, out distancia_max);
            else await DisplayAlert("Aviso", "Insira um número", "Ok");
        }

        // Guarda a distância maxima do perfil entre o utilizador e o restaurante
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

        // Guarda o rating minimo dos restaurantes no perfil do utilizador
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

        // Guarda o rating minimo dos restaurantes nos filtros adicionais do utilizador
        private async void RatingFButton_Clicked(object sender, EventArgs e)
        {
            if (RatingF.SelectedItem != null)
                Double.TryParse(RatingF.SelectedItem.ToString(), out rating_min);
            else await DisplayAlert("Aviso", "Escolha um rating", "Ok");
        }

        // Guarda o preço médio máximo dos restaurantes no perfil do utilizador
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

        // Guarda o preço médio máximo dos restaurantes nos filtros adicionais do utilizador
        private async void PrecoFButton_Clicked(object sender, EventArgs e)
        {
            if (PrecoF.Text != null)
                Double.TryParse(PrecoF.Text, out preco_max);
            else await DisplayAlert("Aviso", "Insira um número", "Ok");
        }

        // Função auxiliar que remove um tipo de cozinha do perfil do utilizador
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

        // Função auxiliar que adiciona um tipo de cozinha ao perfil do utilizador
        private void AdicionaCozinha(int id)
        {
            FiltroCozinha f = new FiltroCozinha(id, db.curr_user.filtro);
            db.filtroCozinha.Add(f);
        }

        /* Função que é executada aquando a modificação de um switch nos filtros
         * é modificados os filtros adicionais do utilizador, adicionando ou removendo
         * os tipos de cozinha dependendo do estado do switch depois da modificação
        */
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

        /* Função que é executada aquando a modificação de um switch nos filtros
         * é modificados os filtros do perfil do utilizador, adicionando ou removendo
         * os tipos de cozinha dependendo do estado do switch depois da modificação
        */
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

        /*Função que permite que o scroll dos tipos de cozinha e os respetivos switchs
         * sofram as mesmas modificações
        */
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

        /* Função que adiciona os últimos restaurantes à view respetiva. Adiciona
         * a imagem do restaurante, assim como o nome, informações e o butão que permite
         * ir para o menu do restaurante
         */
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
            but.AutomationId = res.id.ToString();
            but.Clicked += ShowRestaurante;
            SugestoesImagensUltimos.Children.Add(image);
            SugestoesButtonsUltimos.Children.Add(but);
            Label lab = new Label { Text = res.nome, HorizontalTextAlignment = TextAlignment.Center, FontAttributes = FontAttributes.Bold };
            SugestoesInfoUltimos.Children.Add(lab);
            Label labe = new Label { Text = "Preço Médio: " + res.preco_medio + "€"};
            SugestoesInfoUltimos.Children.Add(labe);
            Label la = new Label { Text = "Distancia: N/A     " + "Rating: " + res.rating };
            SugestoesInfoUltimos.Children.Add(la);
        }

        /* Função que irá preencher a view respetiva aos resultados da pesquisa
         * com os restaurantes que obedecem aos filtros do perfil, assim como aos filtros
         * adicionais, preenchendo com a imagem do respetivo restaurante, assim como
         * as informações sobre esse restaurante, e o butão que permite ir para o menu
         * do restaurante
         */
        private async void PesquisarButton_Clicked(object sender, EventArgs e)
        {
            restaurantes = new List<int>();
            List<int> perfil = new List<int>();
            Filtro filtro = null;
            if (db.curr_user != null)
            {
                foreach (FiltroCozinha f in db.filtroCozinha)
                {
                    if (f.filtro == db.curr_user.filtro)
                        perfil.Add(f.cozinha);
                }

                foreach (Filtro f in db.filtros)
                {
                    if (f.id_filtro == db.curr_user.filtro)
                    {
                        filtro = f;
                        break;
                    }
                }
            }

            SearchBar search = sender as SearchBar;
            int i = 0;
            String pesquisa = search.Text.ToUpper();
            foreach (Prato p in db.prato)
            {
                if (p.nome.Contains(pesquisa))
                {
                    if (!restaurantes.Contains(p.restaurante))
                    {
                        restaurantes.Add(p.restaurante);
                    }
                }
            }

            //Reset da view
            for(i= ResultadosImagens.Children.Count -1; i >=0  ; i--)
            {
                ResultadosImagens.Children.RemoveAt(i);
                ResultadosButtons.Children.RemoveAt(i);
            }

            for(i = ResultadosInfo.Children.Count -1; i >= 0; i--)
            {
                ResultadosInfo.Children.RemoveAt(i);
            }
            //fim do reset

            bool adicionou = false;
            if (db.curr_user != null)
            {
                // verificar se os retaurantes estão dentro dos requesitos
                for (i = 0; i < restaurantes.Count; i++)
                {
                    adicionou = false;
                    foreach (CozinhaRestaurante c in db.cozinhaRestaurante)
                    {
                        if (c.restaurante == restaurantes[i] && perfil.Contains(c.cozinha) && !filtros.Contains(c.cozinha))
                        {
                            foreach (Restaurante r in db.restaurante)
                            {
                                if (r.id == restaurantes[i] && ((((r.preco_medio <= filtro.preco_max || filtro.preco_max == 0) && (r.preco_medio <= preco_max || preco_max == 0)) && ((r.rating >= filtro.rating_min || filtro.rating_min == 0) && (r.rating >= rating_min || rating_min == 0))) || db.curr_user == null))
                                {
                                    AddResultados(restaurantes[i]);
                                    adicionou = true;
                                    break;
                                }
                            }
                        }
                        if (adicionou == true) break;
                    }
                }
            }
            else
            {
                for (i = 0; i < restaurantes.Count; i++)
                    AddResultados(restaurantes[i]);
            }
        }

        /* função auxiliar que preenche a view com os restaurantes dos resultados da pesquisa
        * com a imagem, informações relevantes sobre o restaurante e o butão que permite ir para 
        * o menu do restaurante
        */
        private void AddResultados(int id)
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
            but.AutomationId = id.ToString();
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

        // Função que abre o menu do restaurante, dando o restaurante como argumento
        private async void ShowRestaurante(object sender, EventArgs e)
        {
            int indice;
            Button but = sender as Button;
            Int32.TryParse(but.AutomationId, out indice);
            foreach(Restaurante r in db.restaurante)
            {
                if (r.id == indice)
                {
                    await Navigation.PushAsync(new Restaurantes(db, r));
                    break;
                }

            }
        }
    }
}
