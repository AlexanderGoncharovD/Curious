using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Curious.ViewModels;
using Curious.Views;

namespace Curious
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class MainPage : ContentPage
    {

        private readonly MainPageViewModel _viewModel;
        private UriImageSource _imageSource;

        public MainPage()
        {
            NavigationPage.SetHasNavigationBar(this, false);
            InitializeComponent();
            _viewModel = new MainPageViewModel(this);
            BindingContext = _viewModel;
            picker.SelectedIndex = 1;
            _imageSource = new UriImageSource
            {
                Uri = new Uri("https://xamarin.com/content/images/pages/forms/example-app.png"),
                CachingEnabled = true,
                CacheValidity = new TimeSpan(5, 0, 0, 0)
            };
        }

        public void UpdateCountAdvart(string count)
        {
            CountAdvarts.Text = $"Найдено объявлений: {count}";
        }

        private void Picker_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                _viewModel.Radius = double.Parse(picker.Items[picker.SelectedIndex]) / 1000;
            }
            catch (Exception)
            {
                throw new Exception();
            }
        }

        private async void SearchButton_Clicked(object sender, EventArgs e)
        {
            LoadingIndicator.IsVisible = true;
            UpdateCountAdvart(String.Empty);
            await _viewModel.Search();
            LoadingIndicator.IsVisible = false;
        }

        public void UpdateCollectionAdvartsHeightSize(double height)
        {
            collectionAdvarts.HeightRequest = height;
        }

        public void OpenSearchParameters_Click(object sender, EventArgs e)
        {
        }

        private async void AdvertItem_Tapped(object sender, EventArgs e)
        {
            var viewCell = ((ViewCell)((StackLayout)((Button)sender)?.Parent)?.Parent);
            var advart = viewCell?.BindingContext as Cian.Advart;
            advart.ImageSource = _imageSource;
            if (advart != null)
            {
                await Navigation.PushAsync(new Advart(advart));
            }
            else
            {
                throw new Exception();
            }
        }
    }
}
