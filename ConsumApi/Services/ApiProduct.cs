using ConsumApi.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace ConsumApi.Services
{
    public class ApiProduct : IApiProduct
    {
        HttpClient client;
        string ApiUrl = "https://productapidep.herokuapp.com/api/product/";
        public ApiProduct()
        {
            client = new HttpClient();
        }

        public async Task SaveProduct(Product product, bool findItme = false)
        {
            Uri uri = new Uri(string.Format(ApiUrl, string.Empty));
            //var options = new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase };
            string json = JsonConvert.SerializeObject(product);
            StringContent content = new StringContent(json, Encoding.UTF8, "application/json");
            HttpResponseMessage response = null;
            if (!findItme)
            {
                response = await client.PostAsync(uri, content);
            }
            if (findItme)
            {
                response = await client.PutAsync(uri, content);
            }
            if (response.IsSuccessStatusCode)
            {
                Debug.WriteLine("product is saved");
            }

        }


        public async Task<Product> DetailProduct(string id)
        {
            Uri uri = new Uri($"{ApiUrl}{id}");

            try
            {
                HttpResponseMessage response = await client.GetAsync(uri);
                if (response.IsSuccessStatusCode)
                {
                    string content = await response.Content.ReadAsStringAsync();
                    var Products = JsonConvert.DeserializeObject<Product>(content);
                    return Products;
                }
            }
            catch (Exception ex)
            {

                Console.WriteLine(ex.Message);
            }

            return null;

        }

        

        public async Task<List<Product>> GetAllProducts()
        {

            Uri uri = new Uri(string.Format(ApiUrl));


            try
            {
                HttpResponseMessage response = await client.GetAsync(uri);
                if (response.IsSuccessStatusCode)
                {
                    string content = await response.Content.ReadAsStringAsync();
                    var Products = JsonConvert.DeserializeObject<List<Product>>(content);
                    return Products;
                }
            }
            catch (Exception ex)
            {

                Console.WriteLine(ex.Message);
            }

            return null;
        }

        public async Task DeleteProduct(Product product)
        {
            Uri uri = new Uri($"{ApiUrl}{product.Id}");
            HttpResponseMessage response = await client.DeleteAsync(uri);
            if (response.IsSuccessStatusCode)
            {
                Debug.WriteLine("product is deleted");
            }

        }
    }
}

