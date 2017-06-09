namespace Aplicacao
{
    public class Comentario
    {
        public int id_comentario;
        public string descricao;
        public int restaurante;

        public Comentario(int i, string desc, int rest) 
        {
            this.id_comentario = i;
            this.descricao = desc;
            this.restaurante = rest;
        }
    }
}