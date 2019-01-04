using Microsoft.WindowsAPICodePack.Dialogs;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Forms;
using System.Management.Automation;
using Path = System.IO.Path;
using MicrosoftEventsPublisher;

namespace MicrosoftEventsPublisher
{
    /// <summary>
    /// Interaction logic for LoginPage.xaml
    /// </summary>
    public partial class LoginPage : Page
    {
        public Credentials credentials = new Credentials();
        public NavigationService navService;

        public LoginPage()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

        }

        private void Login(object sender, RoutedEventArgs e)
        {
            var validationMessages = ValidateForm();

            if (validationMessages.Item1 && string.IsNullOrWhiteSpace(validationMessages.Item2))
            {
                credentials.Email = UserName.Text;
                credentials.Password = Password.Password;

                NavigationService.Navigate(new PublishPage(credentials));
            }
            else
            {
                ValidationMessage.Content = validationMessages.Item2;
            }
        }

        private Tuple<bool, string> ValidateForm()
        {
            var valid = true;
            string error = "";

            if (string.IsNullOrEmpty(UserName.Text))
            {
                valid = false;
                error = "Please enter Microsoft email";
            }
            if (string.IsNullOrEmpty(Password.Password))
            {
                valid = false;
                error = "Please enter Microsoft password";
            }

            return new Tuple<bool, string>(valid, error);
        }
    }
}
