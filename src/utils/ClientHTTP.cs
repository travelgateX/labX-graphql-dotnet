using System.Net.Http;
using System.Threading.Tasks;

namespace starwars.utils
{
    public class ClientHTTP
    {
        public async Task<bool> httpClient()
        {
            HttpClientHandler handler = new HttpClientHandler();
            handler.Proxy = null;
            handler.UseProxy = false;
            handler.AutomaticDecompression = System.Net.DecompressionMethods.GZip;

            using (HttpClient client = new HttpClient(handler))
            {

                await Task.Delay(0);
                //LLamada
                //HttpResponseMessage resp = await cliente.PostAsync(....);
            }

            return true;
        }

    }
}