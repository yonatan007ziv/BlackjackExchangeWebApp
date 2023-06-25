using System.Net;

namespace FirstWeb
{
    internal class Server
    {
        private readonly HttpListener listener;
        public Server(string ip, int port)
        {
            string url = $"http://{ip}:{port}/";
            listener = new HttpListener();
            listener.Prefixes.Add(url);
        }

        public void Start()
        {
            listener.Start();
            while (listener.IsListening)
                Client.RouteContext(listener.GetContext());
            listener.Stop();
        }
    }
}