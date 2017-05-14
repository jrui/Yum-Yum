using System.Net;
using System.Threading;
using System.Text;
using System;



public class RunServer {
  static HttpListener _httpListener = new HttpListener();

  static void Main(string[] args) {
    Console.WriteLine("Starting server...");
    _httpListener.Prefixes.Add("http://localhost:8080/"); // add prefix "https://localhost:8080/"
    _httpListener.Start(); // start server (Run application as Administrator!)
    Console.WriteLine("Server started.");
    Console.WriteLine("Setting up listener...");
    Thread _responseThread = new Thread(ResponseThread);
    _responseThread.Start(); // start the response thread
    Console.WriteLine("Listener started.");
  }

  static void ResponseThread() {
    while(true) {
      HttpListenerContext context = _httpListener.GetContext(); // get a context
      // Now, you'll find the request URL in context.Request.Url

      string message = "";
      string[] dados = context.Request.Url.ToString().Replace("http://localhost:8080/", "").Split('/');
      string user = "";
      int length = dados.Length;
      if(dados[length-1] == null || dados[length-1] == "") length--;

      bool access = false;
      if(dados[0] == "auth") access = true;

      switch(length) {
        case 0:
        case 1: //1º Campo para auth ou noauth
        case 2: //2º Campo para username
        case 3: //3º Campo para app
          break;
        default:
          if(access) {
            user = dados[1];
            string app = dados[2];
            string req = dados[3];
            message = "<html><head><title>YUM-YUM</title></head>" +
                      "<body>Welcome to the <strong>YUM-YUM server</strong><br><br>" +
                      "<em>Username: </em>" + user + "<br>" +
                      "<em>Credentials for: </em>" + app + "<br>" +
                      "<em>Request for: </em>" + req + "<br>" +
                      "</body></html>";
          }
          else {
            //4º Campo para pass
            user = dados[1];
            string pass = dados[3];

            //VALIDAR A PALAVRA PASSE
            //Se valido message = true;
            //Se invalido message = false;
            message = "true";
            //message = "false";
          }
          break;
      }

      byte[] _responseArray = Encoding.UTF8.GetBytes(message); // get the bytes to response
      context.Response.OutputStream.Write(_responseArray, 0, _responseArray.Length); // write bytes to the output stream
      context.Response.KeepAlive = false; // set the KeepAlive bool to false
      context.Response.Close(); // close the connection
      Console.WriteLine("Respone given to a request.");
    }
  }
}
