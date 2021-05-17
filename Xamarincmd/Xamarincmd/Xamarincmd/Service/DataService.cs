using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xamarincmd.Models;

namespace Xamarincmd.Service
{
    class DataService
    {
        HttpClient client = new HttpClient();
         public async Task<List<Commander>> GetCommandAsync()
        {
            try
            {
                string url = "http://192.168.0.4:8080/api/commands/";
                var response = await client.GetStringAsync(url);
                var Commands = JsonConvert.DeserializeObject<List<Commander>>(response);
                return Commands;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<bool> AddCommandAsync(Commander command)
        {
            try
            {
                string url = "http://192.168.0.4:8080/api/commands/";
                var uri = new Uri(string.Format(url, command.Id));
                var data = JsonConvert.SerializeObject(command);
                var content = new StringContent(data, Encoding.UTF8, "application/json");
                HttpResponseMessage response = null;
                response = await client.PostAsync(uri, content);

                if (!response.IsSuccessStatusCode)
                {
                    throw new Exception("Erro ao incluir");
                }
                return response.IsSuccessStatusCode;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task UpdateCommandAsync(Commander command)
        {
            string url = "http://192.168.0.4:8080/api/commands/{0}";
            var uri = new Uri(string.Format(url, command.Id));
            var data = JsonConvert.SerializeObject(command);
            var content = new StringContent(data, Encoding.UTF8, "application/json");
            HttpResponseMessage response = null;
            response = await client.PutAsync(uri, content);

            if (!response.IsSuccessStatusCode)
            {
                throw new Exception("Erro ao atualizar");
            }
        }
        public async Task DeleteCommandAsync(Commander command)
        {
            string url = "http://192.168.0.4:8080/api/commands/{0}";
            var uri = new Uri(string.Format(url, command.Id));
            await client.DeleteAsync(uri);
        }

    }
}