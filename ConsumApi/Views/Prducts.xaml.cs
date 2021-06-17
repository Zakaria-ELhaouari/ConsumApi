using ConsumApi.Models;
using ConsumApi.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ConsumApi.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Prducts : ContentPage
    {
        private IApiProduct _ApiProduct;
        public bool _UpdateMode { get; set; }
        public int _ProductToUpdate { get; set; }
        public Prducts()
        {
            InitializeComponent();
            _ApiProduct = new ApiProduct();
        }
        protected override async void OnAppearing()
        {
            base.OnAppearing();
            collectionView.ItemsSource = await _ApiProduct.GetAllProducts();

        }
      

        private async void Delete_Clicked(object sender, EventArgs e)
        {
            try
            {
                string id = (sender as Button).CommandParameter.ToString();
                if (!string.IsNullOrWhiteSpace(id))
                {
                    var product = await _ApiProduct.DetailProduct(id);
                    var result = await DisplayAlert("Confirm", "Do you want to delete Product: " + product.Name.ToString() + "?", "Yes", "No");
                    if (result)
                        await _ApiProduct.DeleteProduct(product);
                    OnAppearing();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private async void Edite_Clicked(object sender, EventArgs e)
        {
            try
            {
                string id = (sender as Button).CommandParameter.ToString();
                if (!string.IsNullOrWhiteSpace(id))
                {
                    var product = await _ApiProduct.DetailProduct(id);

                    nameEntry.Text = product.Name;
                    priceEntry.Text = product.Price.ToString();
                    quantityEntry.Text = product.Quantity.ToString();
                    SubmitBtn.Text = "Update Product";
                    _ProductToUpdate = product.Id;
                    _UpdateMode = true;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        async void AddUpdate(object sender, EventArgs e)
        {
            if (_UpdateMode)
            {
                await _ApiProduct.SaveProduct(new Product
                {
                    Id = _ProductToUpdate,
                    Name = nameEntry.Text,
                    Price = int.Parse(priceEntry.Text),
                    Quantity = int.Parse(quantityEntry.Text),
                    Image = "zack"
                }, _UpdateMode);

                OnAppearing();
            }

            if (!string.IsNullOrWhiteSpace(nameEntry.Text) && !string.IsNullOrWhiteSpace(priceEntry.Text) && !string.IsNullOrWhiteSpace(quantityEntry.Text) && !_UpdateMode)
            {
                await _ApiProduct.SaveProduct(new Product
                {
                    Name = nameEntry.Text,
                    Price = int.Parse(priceEntry.Text),
                    Quantity = int.Parse(quantityEntry.Text),
                    Image = "zack"
                }, _UpdateMode);
                nameEntry.Text = priceEntry.Text = quantityEntry.Text = string.Empty;
                OnAppearing();
            }
        }


    
    }
}