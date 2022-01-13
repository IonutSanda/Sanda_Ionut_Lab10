using Sanda_Ionut_Lab10.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Sanda_Ionut_Lab10
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ProductPage : ContentPage
    {
        ShopList _shopList;
        public ProductPage(ShopList shopList)
        {
            InitializeComponent();
            _shopList = shopList;
        }

        async void OnSaveButtonClicked(object sender, EventArgs e)
        {
            var product = (Product)BindingContext;
            await App.Database.SaveProductAsync(product);
            listView.ItemsSource = await App.Database.GetProductsAsync();
        }

        async void OnDeleteButtonClicked(object sender, EventArgs e)
        {
            var product = (Product)BindingContext;
            await App.Database.DeleteProductAsync(product);
            listView.ItemsSource = await App.Database.GetProductsAsync();
        }

        async void OnListViewItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            Product _product;
            if(e.SelectedItem != null)
            {
                _product = e.SelectedItem as Product;
                var _listProduct = new ListProduct() 
                    {
                    ShopListID = _shopList.ID,
                    ProductID = _product.ID
                };
                await App.Database.SaveListProductAsync(_listProduct);
                _product.ListProducts = new List<ListProduct> { _listProduct };

                await Navigation.PopAsync();
            }
        }

        protected override async void OnAppearing() 
        {
            base.OnAppearing();
            listView.ItemsSource = await App.Database.GetProductsAsync();
        }
    }
}