namespace Aplicacao
{
    public class RestaurantesVisitados
    {
        public int data_dia;
        public int data_mes;
        public int data_ano;
        public int utilizador;
        public int restaurante;

        public RestaurantesVisitados(int dd, int dm, int da, int u, int res)
        {
            this.data_dia = dd;
            this.data_mes = dm;
            this.data_ano = da;
            this.utilizador = u;
            this.restaurante = res;
        }
    }
}