using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace Cian
{
    public class Advart
    {
        public string Header { get; }

        public string Description { get; }

        public string DescriptionShort { get; }

        public HtmlWebViewSource Images { get; set; }

        public string Price { get; }

        public string SqM { get; }

        public string Address { get; }

        public Advart(string header, string description, string price, string sqM, string address, string image)
        {
            Header = header;
            Description = description;
            Price = price;
            SqM = sqM;
            Address = address;

            if (!String.IsNullOrEmpty(description))
            {
                if (description.Length > 200)
                {
                    DescriptionShort = $"{description.Substring(0, 200)} ...";
                }
            }
            var htmlSource = new HtmlWebViewSource();
            htmlSource.Html = $@"<image src='{image}' width='380'/>";
            Images = htmlSource;
        }
    }
}
