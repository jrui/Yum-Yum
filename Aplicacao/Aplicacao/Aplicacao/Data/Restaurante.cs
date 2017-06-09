namespace Aplicacao
{
    public class Restaurante
    {
        public int id;
        public string nome;
        public string morada;
        public double rating;
        public double preco_medio;
        public string contacto;
        public string imagem;
        public int gestor;

        public Restaurante(int id, string n, string m, double r, double p, string c, string i, int g)
        {
            this.id = id;
            this.nome = n;
            this.morada = m;
            this.rating = r;
            this.preco_medio = p;
            this.contacto = c;
            this.imagem = i;
            this.gestor = g;
        }
    }
}