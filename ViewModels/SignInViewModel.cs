using KeepMyPass.Data;
using KeepMyPass.Services;
using KeepMyPass.Views;
using Prism.Commands;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace KeepMyPass.ViewModels
{
    public class SignInViewModel : BindableBase
    {
		private string _email = String.Empty;
        private string _password = String.Empty;
        FirebaseService _srv;
        MainWindowViewModel _mainWindowViewModel;
		public SignInViewModel(MainWindowViewModel mainWindowViewModel, FirebaseService srv)
		{
            _mainWindowViewModel = mainWindowViewModel;
            SignInCommand = new DelegateCommand(SignIn, CanSignIn);
            SignUpCommand = new DelegateCommand(SignUp);
            _srv = srv;
        }

        public string Email
        {
			get { return _email; }
			set 
			{
                SetProperty(ref _email, value);
                ValidateEmail();
                OnPropertyChanged(() => _email);
                SignInCommand.RaiseCanExecuteChanged();
            }
		}

		public string Password
		{
			get { return _password; }
			set 
			{
                SetProperty(ref _password, value);
                ValidatePassword();
                OnPropertyChanged(() => _password);
                SignInCommand.RaiseCanExecuteChanged();

            }
		}

        private void ValidatePassword()
        {
            ClearErrors(nameof(Password));
            if (string.IsNullOrEmpty(Password) || string.IsNullOrWhiteSpace(Password))
                AddError(nameof(Password), "Password cannot be empty.");
            if (Password == null || Password?.Length <= 7)
                AddError(nameof(Password), "Password must be at least 8 characters long.");
        }

        public DelegateCommand SignInCommand { get; set; }

        public DelegateCommand SignUpCommand { get; set; }

        private void ValidateEmail()
        {
            ClearErrors(nameof(Email));
            if (string.IsNullOrEmpty(Email) || string.IsNullOrWhiteSpace(Email))
                AddError(nameof(Email), "Email cannot be empty.");
            if (string.Equals(Email, "Admin", StringComparison.OrdinalIgnoreCase))
                AddError(nameof(Email), "Admin is not valid email.");
            if (Email?.Length <= 3)
                AddError(nameof(Email), "Email must be at least 4 characters long.");
        }

        private bool CanSignIn()
		{
            return GetErrorsList().Count == 0 && !string.IsNullOrEmpty(Email) && !string.IsNullOrEmpty(Password);
		}
		private void SignIn()
		{
            User user = new User(Email, Password);
            HttpStatusCode status =  _srv.AddUser(user);
            if (status == HttpStatusCode.OK)
            {

            }
            else
            {
                throw new Exception(status.ToString());
            }
        }

        private void SignUp()
        {
            _mainWindowViewModel.SetSignUpPage();
        }

	}
}
