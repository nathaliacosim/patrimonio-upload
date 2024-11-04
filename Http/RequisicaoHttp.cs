using System;
using System.Text;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace PatrimonioDourados.Http;

public class RequisicaoHttp
{
    public static async Task<string> GetRequisicao(string token, string url)
    {
        Uri urlNova = new Uri(url);
        HttpClient httpClient = new HttpClient();

        try
        {
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, urlNova);
            HttpResponseMessage responseGet = await httpClient.SendAsync(request);
            string responseContent = await responseGet.Content.ReadAsStringAsync();

            return responseContent;
        }
        finally
        {
            httpClient.Dispose();
        }
    }

    public static async Task<string> PostRequisicao(string token, string json, string url)
    {
        Uri urlNova = new Uri(url);

        HttpClient httpClient = new HttpClient();

        try
        {
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            // Verifica se o json é null e ajusta o conteúdo da requisição
            HttpContent content = null;
            if (json != null)
            {
                content = new StringContent(json, Encoding.UTF8, "application/json");
            }

            // Envia a requisição, passando null para o body se json for null
            HttpResponseMessage response;
            if (content != null)
            {
                response = await httpClient.PostAsync(urlNova, content);
            }
            else
            {
                response = await httpClient.PostAsync(urlNova, null); // Sem body
            }

            string responseContent = await response.Content.ReadAsStringAsync();
            return responseContent;
        }
        finally
        {
            httpClient.Dispose();
        }
    }


    public static async Task<string> PatchRequisicao(string token, string json, string url)
    {
        Uri urlNova = new Uri(url);

        using (HttpClient httpClient = new HttpClient())
        {
            try
            {
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                // Criando o HttpRequestMessage para PATCH com using
                using (HttpRequestMessage request = new HttpRequestMessage(new HttpMethod("PATCH"), urlNova)
                {
                    Content = content
                })
                {
                    HttpResponseMessage response = await httpClient.SendAsync(request);

                    // Verifica se a resposta é de sucesso
                    response.EnsureSuccessStatusCode();

                    string responseContent = await response.Content.ReadAsStringAsync();
                    return responseContent;
                }
            }
            catch (HttpRequestException e)
            {
                Console.WriteLine($"Erro na requisição HTTP: {e.Message}");
                throw;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro inesperado: {ex.Message}");
                throw;
            }
        }
    }

}