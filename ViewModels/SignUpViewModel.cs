using Prism.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KeepMyPass.ViewModels
{
    public class SignUpViewModel : BindableBase
    {
        MainWindowViewModel _mainWindowViewModel;
        Services.FirebaseService _srv;
        private string _newEmail;
        private string _newPassword;
        private string _repeatPassword;

        public SignUpViewModel(MainWindowViewModel mainWindowViewModel, Services.FirebaseService srv)
        {
            _mainWindowViewModel = mainWindowViewModel;
            _srv = srv;
            SignUpCommand = new DelegateCommand(SignUpNewUser, CanSignUp);
            BackCommand = new DelegateCommand(Back);
        }

        DelegateCommand SignUpCommand { get; set; }
        DelegateCommand BackCommand { get; set; }

        public string NewEmail
        {
            get 
            { 
                return _newEmail; 
            }
            set 
            {
                SetProperty(ref _newEmail, value);
                ValidateEmail();
                OnPropertyChanged(() => _newEmail);
                SignUpCommand.RaiseCanExecuteChanged();
            }
        }

        public string NewPassword
        {
            get
            {
                return _newPassword;
            }
            set
            {
                SetProperty(ref _newPassword, value);
                ValidatePassword();
                OnPropertyChanged(() => _newPassword);
                SignUpCommand.RaiseCanExecuteChanged();
            }
        }

        public string RepeatPassword
        {
            get
            {
                return _repeatPassword;
            }
            set
            {
                SetProperty(ref _repeatPassword, value);
                ValidateRepeatedPassword();
                OnPropertyChanged(() => _repeatPassword);
                SignUpCommand.RaiseCanExecuteChanged();
            }
        }

        private void ValidatePassword()
        {
            ClearErrors(nameof(NewPassword));
            if (string.IsNullOrEmpty(NewPassword) || string.IsNullOrWhiteSpace(NewPassword))
                AddError(nameof(NewPassword), "Password cannot be empty.");
            if (NewPassword == null || NewPassword?.Length <= 7)
                AddError(nameof(NewPassword), "Password must be at least 8 characters long.");
        }

        private void ValidateRepeatedPassword()
        {
            ClearErrors(nameof(RepeatPassword));
            if (string.IsNullOrEmpty(RepeatPassword) || string.IsNullOrWhiteSpace(RepeatPassword))
                AddError(nameof(RepeatPassword), "Password cannot be empty.");
            if (RepeatPassword == null || RepeatPassword?.Length <= 7)
                AddError(nameof(RepeatPassword), "Password must be at least 8 characters long.");
            if (!String.IsNullOrEmpty(RepeatPassword) && !RepeatPassword.Equals(NewPassword))
                AddError(nameof(RepeatPassword), "The repeated password must be equal to your new password.");
        }

        private void ValidateEmail()
        {
            ClearErrors(nameof(NewEmail));
            if (string.IsNullOrEmpty(NewEmail) || string.IsNullOrWhiteSpace(NewEmail))
                AddError(nameof(NewEmail), "Email cannot be empty.");
            if (string.Equals(NewEmail, "Admin", StringComparison.OrdinalIgnoreCase))
                AddError(nameof(NewEmail), "Admin is not valid email.");
            if (NewEmail?.Length <= 3)
                AddError(nameof(NewEmail), "Email must be at least 4 characters long.");
        }

        private void Back()
        {
            _mainWindowViewModel.SetSignInPage();
        }

        private bool CanSignUp()
        {
            return GetErrorsList().Count == 0 && !string.IsNullOrEmpty(NewEmail) && !string.IsNullOrEmpty(NewPassword) && !string.IsNullOrEmpty(RepeatPassword);
        }

        private void SignUpNewUser()
        {
            
        }

    }
}
