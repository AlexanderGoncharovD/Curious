using Cian;
using Cian.Polygon;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace Curious.ViewModels
{
    class MainPageViewModel
    {

        private const double KILOMETR = 0.0159;
        private readonly MainPage _parent;
        private Location _location;

        public double AdvartHeight { get; set; }

        public ObservableCollection<Advart> Advarts { get; set; } = new ObservableCollection<Advart>();

        public double Radius { get; set; } = 0.5;

        public List<string> Radiuses { get; set; } = new List<string>()
        {
            "50",
            "100",
            "150",
            "200",
            "250",
            "300",
            "350",
            "400",
            "450",
            "500",
        };

        public bool IsLoading { get; set; }

        public MainPageViewModel(MainPage parent)
        {
            _parent = parent;
        }

        public async Task Search()
        {
            IsLoading = true;
            Advarts.Clear();
            await GetLocation();
            await LoadPage();
        }

        private async Task GetLocation()
        {
            try
            {
                _location = await Geolocation.GetLastKnownLocationAsync();
            }
            catch (Exception)
            {
                throw new Exception();
            }
        }

        public async Task LoadPage()
        {
            var region = new Shape(new GeoPoint(_location), KILOMETR * Radius);
            var regionPoints = region.CreatePolygonLink();
            var cian = new CianPage(regionPoints);
            cian.Loaded += OnLoaded;
            await cian.Loading();
        }

        private void OnLoaded(object sender, EventArgs e)
        {
            var page = sender as CianPage;

            if (page != null)
            {
                var count = page.GetAdvartsCount();

                foreach (var advart in page.GetAdverts())
                {
                    Advarts.Add(advart);
                }
                _parent.UpdateCountAdvart(count);
                AdvartHeight = Advarts.Count * 430;
                _parent.UpdateCollectionAdvartsHeightSize(AdvartHeight);
                IsLoading = false;
            }
            else
            {
                throw new Exception();
            }
        }
    }
}
