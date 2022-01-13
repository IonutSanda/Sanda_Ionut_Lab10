using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sanda_Ionut_Lab10.Models;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Sanda_Ionut_Lab10
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ListPage : ContentPage
    {

        async void OnSaveButtonClicked(object sender, EventArgs e)
        {
            var sList = (ShopList)BindingContext;
            sList.Date = DateTime.UtcNow;
            await App.Database.SaveShopListAsync(sList);
            await Navigation.PopAsync();
        }

        async void OnDeleteButtonClicked(object sender, EventArgs e)
        {
            var sList = (ShopList)BindingContext;
            await App.Database.DeleteShopListAsync(sList);
            await Navigation.PopAsync();
        }

        async void OnChooseButtonClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new ProductPage((ShopList)this.BindingContext)
            {
                BindingContext = new Product()
            });
        }

        public ListPage()
        {
            InitializeComponent();
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            var shopl = (ShopList)BindingContext;
            listView.ItemsSource = await App.Database.GetListProductsAsync(shopl.ID);
        }
    }
}