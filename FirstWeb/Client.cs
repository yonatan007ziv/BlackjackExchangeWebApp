using System.Net;
using System.Text;

namespace FirstWeb
{
    internal class Client
    {
        private static readonly Dictionary<IPAddress, Client> clients = new Dictionary<IPAddress, Client>();

        public static void RouteContext(HttpListenerContext httpListenerContext)
        {
            if (ClientExists(httpListenerContext.Request.RemoteEndPoint.Address))
                clients[httpListenerContext.Request.RemoteEndPoint.Address].HandleContext(httpListenerContext);
            else
                _ = new Client(httpListenerContext);
        }

        private static bool ClientExists(IPAddress key)
        {
            foreach (IPAddress current in clients.Keys)
                if (key.Equals(current))
                    return true;
            return false;
        }

        public Client(HttpListenerContext httpListenerContext)
        {
            clients.Add(httpListenerContext.Request.RemoteEndPoint.Address, this);
            SendHTMLPage("LoginRegister", httpListenerContext);
        }

        private void HandleContext(HttpListenerContext context)
        {
            if (context.Request.HttpMethod == "GET")
            {
                string fileName = Path.GetFileName(context.Request.RawUrl!.Split('.')[0]);
                if (context.Request.RawUrl.EndsWith(".js"))
                    SendJavaScript(fileName, context);
                else if (context.Request.RawUrl.EndsWith(".html"))
                    SendHTMLPage(fileName, context);
                else if (context.Request.RawUrl.EndsWith(".css"))
                    SendCSSFile(fileName, context);
                return;
            }

            using (StreamReader reader = new StreamReader(context.Request.InputStream, context.Request.ContentEncoding))
            {
                string request = reader.ReadToEnd();
                string requestPath = context.Request.Url?.LocalPath ?? "";
                Console.WriteLine(request);

                if (IsJsonRequest(context.Request))
                {
                    LoginRegisterHandler.RouteToAppropriateRequest(this, context, request);
                }
                else if (requestPath.EndsWith(".html"))
                {
                    if (request.Contains("Login"))
                        SendHTMLPage("LoginForm", context);
                    else if (request.Contains("Register"))
                        SendHTMLPage("RegisterForm", context);
                }
                else
                    SendHTMLPage("Entry", context);
            }
        }

        public void SendBodyMessage(string message, HttpListenerContext context)
        {
            Console.WriteLine($"sending body message: {message}");

            byte[] buffer = Encoding.UTF8.GetBytes(message);
            HttpListenerResponse response = context.Response;
            response.ContentType = "text/html";
            response.ContentLength64 = buffer.Length;
            using (Stream output = response.OutputStream)
                output.Write(buffer, 0, buffer.Length);

            response.Close();
        }

        private void SendHTMLPage(string page, HttpListenerContext context)
        {
            Console.WriteLine($"sending html page: {page}");

            string relativePath = $@"..\FirstWeb\Pages\HTML\{page}.html";
            string htmlContent = File.ReadAllText(relativePath);
            byte[] buffer = Encoding.UTF8.GetBytes(htmlContent);
            HttpListenerResponse response = context.Response;
            response.ContentType = "text/html";
            response.ContentLength64 = buffer.Length;
            response.OutputStream.Write(buffer, 0, buffer.Length);
            response.Close();
        }

        private void SendJavaScript(string script, HttpListenerContext context)
        {
            Console.WriteLine($"sending js script: {script}");

            string relativePath = $@"..\FirstWeb\Pages\JavaScript\{script}.js";
            string jsContent = File.ReadAllText(relativePath);
            byte[] buffer = Encoding.UTF8.GetBytes(jsContent);
            HttpListenerResponse response = context.Response;
            response.ContentType = "application/javascript";
            response.ContentLength64 = buffer.Length;
            response.OutputStream.Write(buffer, 0, buffer.Length);
            response.Close();
        }

        private void SendCSSFile(string file, HttpListenerContext context)
        {
            Console.WriteLine($"sending css file: {file}");

            string relativePath = $@"..\FirstWeb\Pages\CSS\{file}.css";
            string jsContent = File.ReadAllText(relativePath);
            byte[] buffer = Encoding.UTF8.GetBytes(jsContent);
            HttpListenerResponse response = context.Response;
            response.ContentType = "text/css";
            response.ContentLength64 = buffer.Length;
            response.OutputStream.Write(buffer, 0, buffer.Length);
            response.Close();
        }

        private static bool IsJsonRequest(HttpListenerRequest request)
        {
            string? contentType = request.ContentType;
            return !string.IsNullOrEmpty(contentType) && contentType.Contains("application/json");
        }
    }
}