using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Cian
{
    public class CianPage
    {
        private readonly string _url;
        private HtmlDocument _htmlDocument;
        private const string DEFAULT_LINK = @"https://www.cian.ru/cat.php?deal_type=sale&engine_version=2&in_polygon%5B1%5D=###polygon###&offer_type=flat&polygon_name%5B1%5D=Область+поиска";
                                              // https://www.cian.ru/cat.php?deal_type=sale&engine_version=2&in_polygon%5B1%5D=###polygon###&offer_type=flat&polygon_name%5B1%5D=Область+поиска&room1=1&room2=1
        public bool IsLoaded { get; private set; } = false;

        public int AdvartsCount { get; private set; }

        public event EventHandler Loaded;

        public CianPage(string region)
        {
            var url = DEFAULT_LINK.Replace("###polygon###", $"{region}");
            _url = url;
        }

        public async Task Loading()
        {
            _htmlDocument = null;
            IsLoaded = false;
            var request = (HttpWebRequest)WebRequest.Create(_url);
            var result = String.Empty;

            //TODO: Бывают ошибки получения Response
            using (var response = (HttpWebResponse)request.GetResponse())
            {
                if (response.StatusCode == HttpStatusCode.OK)
                {
                    var receiveStream = response.GetResponseStream();
                    if (receiveStream != null)
                    {
                        StreamReader readStream;
                        if (response.CharacterSet == null)
                        {
                            readStream = new StreamReader(receiveStream);
                        }
                        else
                        {
                            readStream =  new StreamReader(receiveStream, Encoding.GetEncoding(response.CharacterSet));
                        }
                        result = await readStream.ReadToEndAsync();
                        readStream.Close();
                    }
                }
            }

            _htmlDocument = new HtmlDocument();
            _htmlDocument.LoadHtml(result);
            OnLoaded();
        }

        private void OnLoaded()
        {
            IsLoaded = true;
            Loaded?.Invoke(this, EventArgs.Empty);
        }

        public ObservableCollection<Advart> GetAdverts()
        {
            var advarts = new ObservableCollection<Advart>();
            var headers = _htmlDocument.DocumentNode.SelectNodes(".//div[@class='c6e8ba5398--title--2CW78']");
            var prices = _htmlDocument.DocumentNode.SelectNodes(".//div[@class='c6e8ba5398--header--1df-X']");
            var sqMs = _htmlDocument.DocumentNode.SelectNodes(".//div[@class='c6e8ba5398--term--3kvtJ']");
            var addresses = _htmlDocument.DocumentNode.SelectNodes(".//div[@class='c6e8ba5398--address-links--1tfGW']");
            var descriptionses = _htmlDocument.DocumentNode.SelectNodes(".//div[@class='c6e8ba5398--container--F3yyv c6e8ba5398--info-section--Sfnx- c6e8ba5398--titled-description--1OX7l']");
            var images = _htmlDocument.DocumentNode.SelectNodes(".//img[@class='c6e8ba5398--image--3ua1b']");

            var composite = new CompositeAdvart()
            {
                Headers = headers,
                Prices = prices,
                SqMs = sqMs,
                Addresses = addresses,
                Descriptionses = descriptionses,
                Images = images
            };

            for (int i = 0; i < composite?.Headers?.Count; i++)
            {
                advarts.Add(composite.GetAdvart(i));
            }
            
            AdvartsCount = advarts.Count;
            return advarts;
        }

        public string GetAdvartsCount()
        {
            
            var texts = _htmlDocument.DocumentNode.SelectNodes(".//div[@class='_93444fe79c--totalOffers--22-FL']");
            if (texts != null)
            {
                foreach (var text in texts)
                    return text.InnerText.Substring(0, text.InnerText.IndexOf(' '));
            }
            return "0";
        }
    }
}
