using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace Curious.ViewModels
{
    public class AdvartsViewModel
    {
        private readonly Advarts _parent;

        public AdvartsViewModel(Advarts parent)
        {
            _parent = parent;
        }
    }
}
