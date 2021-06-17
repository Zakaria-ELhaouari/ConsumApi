using ConsumApi.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ConsumApi.Services
{
    interface IApiProduct
    {
        Task<List<Product>> GetAllProducts();
        Task<Product> DetailProduct(string ID);
        Task SaveProduct(Product product, bool isNew = false);
        Task DeleteProduct(Product product);
    }
}
