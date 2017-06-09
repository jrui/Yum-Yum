namespace Aplicacao
{
    public class Prato
    {
        public int id_prato;
        public double preco;
        public string nome;
        public int restaurante;

        public Prato(int id, double pr, string no, int res)
        {
            this.id_prato = id;
            this.preco = pr;
            this.nome = no;
            this.restaurante = res;
        }
    }
}