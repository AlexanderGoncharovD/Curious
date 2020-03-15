using Cian;
using System;
using System.Collections.Generic;
using System.Text;

namespace Curious.ViewModels
{
    public class AdvartViewModel
    {
        public Advart Advart { get; set; }

        public AdvartViewModel(Cian.Advart advart)
        {
            Advart = advart;
        }

    }
}
