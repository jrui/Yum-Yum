namespace Aplicacao
{
    public class Utilizador
    {
        public int id_utilizador;
        public string email;
        public string username;
        public string password;
        public int filtro;

        public Utilizador(int id, string e, string u, string p, int f)
        {
            this.id_utilizador = id;
            this.email = e;
            this.username = u;
            this.password = p;
            this.filtro = f;
        }
    }
}