using Curious.ViewModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Curious.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Advart : ContentPage
    {
        #region Fields

        private readonly AdvartViewModel _viewModel;
        const int _downloadImageTimeoutInSeconds = 15;
        readonly HttpClient _httpClient = new HttpClient { Timeout = TimeSpan.FromSeconds(_downloadImageTimeoutInSeconds) };

        private byte[] imageBytes;

        #endregion

        #region .ctor

        public Advart(Cian.Advart advart)
        {
            InitializeComponent();
            _viewModel = new AdvartViewModel(advart);
            BindingContext = _viewModel;
            LoadImage();
        }

        private async  void LoadImage()
        {
            imageBytes = await DownloadImageAsync("https://cdn-p.cian.site/images/1/174/148/kvartira-moskva-prospekt-vernadskogo-841471151-4.jpg");
            MytImage.Source = ImageSource.FromStream(() => new MemoryStream(imageBytes));
        }

        #endregion

        async Task<byte[]> DownloadImageAsync(string imageUrl)
        {
            try
            {
                using (var httpResponse = await _httpClient.GetAsync(imageUrl))
                {
                    if (httpResponse.StatusCode == HttpStatusCode.OK)
                    {
                        return await httpResponse.Content.ReadAsByteArrayAsync();
                    }
                    else
                    {
                        //Url is Invalid
                        return null;
                    }
                }
            }
            catch (Exception e)
            {
                //Handle Exception
                var message = e.Message;
                return null;
            }
        }

    }
}