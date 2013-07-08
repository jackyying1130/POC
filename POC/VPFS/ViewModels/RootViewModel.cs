using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace VPFS.ViewModels
{
    class RootViewModel : Caliburn.Micro.Conductor<object>.Collection.OneActive
    {
        private string _windowTitle = "VPFS Main Window";

        public string windowTitle
        {
            get
            {
                return _windowTitle;
            }
            set
            {
                _windowTitle = value;
                NotifyOfPropertyChange(() => windowTitle);
            }
        }

        public void DisplayWindow()
        {
            ActivateItem(new OrderPlacementViewModel());
        }
    }
}
