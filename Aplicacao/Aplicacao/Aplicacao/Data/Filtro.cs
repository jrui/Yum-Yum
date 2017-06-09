namespace Aplicacao
{
    public class Filtro
    {
        public int id_filtro;
        public double preco_max;
        public double distancia_max;
        public double rating_min;

        public Filtro(int id, double pre, double dis, double rat)
        {
            this.id_filtro = id;
            this.preco_max = pre;
            this.distancia_max = dis;
            this.rating_min = rat;
        }
    }
}