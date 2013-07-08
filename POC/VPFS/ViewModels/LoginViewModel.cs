using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Caliburn.Micro;
using System.ComponentModel.Composition;

namespace VPFS.ViewModels
{
    [Export(typeof(LoginViewModel))]
    class LoginViewModel : Screen
    {
        private readonly IWindowManager _windowManager;

        [ImportingConstructor]
        public LoginViewModel(IWindowManager windowManager)
        {
          _windowManager = windowManager;
        }

        public void Login()
        {
            _windowManager.ShowWindow(new RootViewModel());
            TryClose();
        }

        public void Cancel()
        {
            TryClose();
        }
    }
}
