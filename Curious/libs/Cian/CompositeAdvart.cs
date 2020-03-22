using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Cian
{
    public class CompositeAdvart
    {
        public HtmlNodeCollection Headers { get; set; }

        public HtmlNodeCollection Prices { get; set; }

        public HtmlNodeCollection SqMs { get; set; }

        public HtmlNodeCollection Addresses { get; set; }

        public HtmlNodeCollection Descriptionses { get; set; }

        public HtmlNodeCollection Images { get; set; }
       
        public async Task<Advart> GetAdvartAsync(int count)
        {
            
            return new Advart(Headers[count].InnerText,
                Descriptionses[count].InnerText,
                Prices[count].InnerText,
                SqMs[count].InnerText,
                Addresses[count].InnerText,
                await CianImage.LoadAsync(Images[count].Attributes["src"].Value)
            );
        }
    }
}
