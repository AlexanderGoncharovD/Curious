using System;
using System.Collections.Generic;
using System.Text;

namespace Cian
{
    public class CianPageEventArgs : EventArgs
    {
        public CianPage Page { get; set; }

        public CianPageEventArgs(CianPage page)
        {
            Page = page;
        }
    }
}
