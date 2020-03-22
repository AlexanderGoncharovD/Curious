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

        public string Price { get; }

        public string SqM { get; }

        public string Address { get; }

        public ImageSource ImageSource { get; set; }


        public Advart(string header, string description, string price, string sqM, string address, ImageSource image)
        {
            Header = header;
            Description = description;
            Price = price;
            SqM = sqM;
            Address = address;
            ImageSource = image;

            if (!String.IsNullOrEmpty(description))
            {
                if (description.Length > 200)
                {
                    DescriptionShort = $"{description.Substring(0, 200)} ...";
                }
            }
        }
    }
}
