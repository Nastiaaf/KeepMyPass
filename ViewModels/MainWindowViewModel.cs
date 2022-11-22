using KeepMyPass.Services;
using KeepMyPass.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KeepMyPass.ViewModels
{
    public class MainWindowViewModel : BindableBase
    {
        private BindableBase _currentVm;
        FirebaseService _srv;
        public BindableBase CurrentViewModel
        {
            get
            {
                return _currentVm;
            }
            set
            {
                SetProperty(ref _currentVm, value);
                OnPropertyChanged(() => _currentVm);
            }
        }

        public MainWindowViewModel()
        {
            _srv = new FirebaseService();
            SetSignInPage();
        }
        public void SetSignUpPage()
        {
            SignUpViewModel signUpVm = new SignUpViewModel(this, _srv);
            CurrentViewModel = signUpVm;
        }

        public void SetSignInPage()
        {
            CurrentViewModel = new SignInViewModel(this, _srv);
        }
    }
}
