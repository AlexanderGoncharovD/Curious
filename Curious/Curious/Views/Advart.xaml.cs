using Curious.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
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

        #endregion

        #region .ctor

        public Advart(Cian.Advart advart)
        {
            InitializeComponent();
            _viewModel = new AdvartViewModel(advart);
            BindingContext = _viewModel;
        }
        
        #endregion
    }
}