namespace Aplicacao
{
    public class TipoCozinha
    {
        public int id_cozinha;
        public string descricao;

        public TipoCozinha(int id, string desc)
        {
            this.id_cozinha = id;
            this.descricao = desc;
        }
    }
}