﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Aplicacao.Interface
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Restaurantes : ContentPage
    {
        Database db;
        Restaurante restaurante;
        List<RestaurantesVisitados> list = new List<RestaurantesVisitados>();

        public Restaurantes(Database d, Restaurante r)
        {
            int reviewsCounter = 0;
            db = d;
            restaurante = r;
            NavigationPage.SetHasNavigationBar(this, false);
            InitializeComponent();

            Titulo.Text = restaurante.nome;
            Rating.Text = restaurante.rating.ToString();
            Preco.Text = restaurante.preco_medio.ToString() + " €";
            Morada.Text = restaurante.morada;
            Distancia.Text = "--";
            Foto.Source = ImageSource.FromUri(new Uri(restaurante.imagem));
            Telefone.Text = restaurante.contacto;
            foreach (Prato p in db.prato)
            {
                if (p.restaurante == restaurante.id)
                {
                    Label nome = new Label { Text = p.nome };
                    PratosNome.Children.Add(nome);
                    Label preco = new Label { Text = p.preco.ToString() + "€" };
                    PratosPreco.Children.Add(preco);
                }
            }

            foreach (Comentario c in db.comentario)
            {
                if (c.restaurante == r.id)
                {
                    reviewsCounter++;
                    Label coment = new Label { Text = c.descricao , TextColor = Color.Black};
                    StackReviews.Children.Add(coment);

                    Label comentSep = new Label { Text = "----------------------" , TextColor = Color.Black };
                    StackReviews.Children.Add(comentSep);
                }
            }

            ReviewsButton.Text = "Reviews (" + reviewsCounter.ToString() + ")";
        }

        private void ReviewsButton_Clicked(object sender, EventArgs e)
        {
            if (ScrollReviews.IsVisible == true)
                    ScrollReviews.IsVisible = false;
            else ScrollReviews.IsVisible = true;
            ScrollComentario.IsVisible = false;
        }

        private void ComentarioButton_Clicked(object sender, EventArgs e)
        {
            if (ScrollComentario.IsVisible == true)
            {
                ComentarioTitulo.IsVisible = false;
                ScrollComentario.IsVisible = false;
                ReviewsButton.IsVisible = true;
                GuardarComentario.IsVisible = false;
            }
            else
            {
                ComentarioTitulo.IsVisible = true;
                ScrollComentario.IsVisible = true;
                ReviewsButton.IsVisible = false;
                GuardarComentario.IsVisible = true;
            }
            ScrollReviews.IsVisible = false;
        }

        private void ComentarioRealizado(object sender, EventArgs e)
        {
            if (db.curr_user != null)
            {
                if (StackComentario.Text != null)
                {
                    String text = StackComentario.Text;
                    Comentario c = new Comentario(db.comentario.Count + 1, text, restaurante.id);
                    db.comentario.Add(c);
                    ComentarioTitulo.IsVisible = false;
                    ScrollComentario.IsVisible = false;
                    ReviewsButton.IsVisible = true;
                    GuardarComentario.IsVisible = false;
                    StackComentario.IsVisible = false;
                }
            }
        }

        private void AddHistorico(object sender, EventArgs e)
        {
            if (db.curr_user != null)
            {
                Boolean r1 = false, r2 = false;
                foreach (RestaurantesVisitados r in db.restaurantesVisitados)
                    if (r.utilizador == db.curr_user.id_utilizador)
                        if (r.data_ano == DateTime.Now.Year && r.data_dia == DateTime.Now.Day && r.data_mes == DateTime.Now.Month)
                            list.Add(r);
                if (list.Count >= 2)
                {
                    SubstituirHistorico.IsVisible = true;
                    foreach (Restaurante r in db.restaurante)
                    {
                        if (r1 && r2)
                            break;
                        if (r.id == list[0].restaurante)
                        {
                            Restaurante1.Text = r.nome;
                            r1 = true;
                        }

                        if (r.id == list[1].restaurante)
                        {
                            Restaurante2.Text = r.nome;
                            r2 = true;
                        }
                    }
                }
                else
                {
                    RestaurantesVisitados res = new RestaurantesVisitados(DateTime.Now.Day, DateTime.Now.Month, DateTime.Now.Year, db.curr_user.id_utilizador, restaurante.id);
                    db.restaurantesVisitados.Add(res);
                }
            }
        }

        private void ButtonRestaurante1_Clicked(object sender, EventArgs e)
        {
            foreach(RestaurantesVisitados r in db.restaurantesVisitados)
            {
                if (r.utilizador == db.curr_user.id_utilizador)
                    if (r.data_ano == DateTime.Now.Year && r.data_dia == DateTime.Now.Day && r.data_mes == DateTime.Now.Month)
                        if (r.restaurante == list[0].restaurante)
                        {
                            db.restaurantesVisitados.Remove(r);
                            break;
                        }
            }
            RestaurantesVisitados res = new RestaurantesVisitados(DateTime.Now.Day, DateTime.Now.Month, DateTime.Now.Year, db.curr_user.id_utilizador, restaurante.id);
            db.restaurantesVisitados.Add(res);
            SubstituirHistorico.IsVisible = false;
        }

        private void ButtonRestaurante2_Clicked(object sender, EventArgs e)
        {
            foreach (RestaurantesVisitados r in db.restaurantesVisitados)
            {
                if (r.utilizador == db.curr_user.id_utilizador)
                    if (r.data_ano == DateTime.Now.Year && r.data_dia == DateTime.Now.Day && r.data_mes == DateTime.Now.Month)
                        if (r.restaurante == list[1].restaurante)
                        {
                            db.restaurantesVisitados.Remove(r);
                            break;
                        }
            }
            RestaurantesVisitados res = new RestaurantesVisitados(DateTime.Now.Day, DateTime.Now.Month, DateTime.Now.Year, db.curr_user.id_utilizador, restaurante.id);
            db.restaurantesVisitados.Add(res);
            SubstituirHistorico.IsVisible = false;
        }

        private void CancelarButton_Clicked(object sender, EventArgs e)
        {
            SubstituirHistorico.IsVisible = false;
        }
    }
}
