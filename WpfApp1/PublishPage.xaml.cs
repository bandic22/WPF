using Microsoft.WindowsAPICodePack.Dialogs;
using MicrosoftEventsPublisher;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Management.Automation;
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

namespace MicrosoftEventsPublisher
{
    /// <summary>
    /// Interaction logic for PublishPage.xaml
    /// </summary>
    public partial class PublishPage : Page
    {
        public Slots slots = new Slots();
        public Websites sites = new Websites();
        public Selections selections = new Selections();
        public Credentials credentials;

        public PublishPage(Credentials credentials)
        {
            InitializeComponent();
            this.credentials = credentials;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

        }

        private void PublishButton_Click(object sender, RoutedEventArgs e)
        {
            var validationMessage = ValidateForm();
            if (validationMessage.Item1 && !string.IsNullOrWhiteSpace(validationMessage.Item2))
            {
                using (PowerShell PowerShellInstance = PowerShell.Create())
                {
                    PowerShellInstance.AddScript(File.ReadAllText(@"..\..\Publish.ps1"));

                    PowerShellInstance.AddParameter("WebAppName", selections.GetWebAppName());
                    PowerShellInstance.AddParameter("Slot", selections.SelectedSlot);
                    PowerShellInstance.AddParameter("ProjectDirectory", selections.ProjectDirectory);
                    PowerShellInstance.AddParameter("PublishingProfile", selections.PublishProfileDirectory);
                    PowerShellInstance.AddParameter("Password", credentials.Password);
                    PowerShellInstance.AddParameter("Email", credentials.Email);

                    Collection<PSObject> PSOutput = PowerShellInstance.Invoke();

                    foreach (PSObject outputItem in PSOutput)
                    {
                        if (outputItem != null)
                        {
                            ValidationMessage1.Content = outputItem.BaseObject.GetType().FullName;
                            ValidationMessage2.Content = outputItem.BaseObject.ToString() + "\n";
                        }
                    }
                }
            }
            else
            {
                ValidationMessage1.Content = validationMessage.Item2;
            }
        }

        private void ProjectDirectoryButton_Click_1(object sender, RoutedEventArgs e)
        {
            CommonOpenFileDialog dialog = new CommonOpenFileDialog();
            dialog.IsFolderPicker = true;
            if (dialog.ShowDialog() == CommonFileDialogResult.Ok)
            {
                selections.ProjectDirectory = dialog.FileName;
                PathDisplay.Content = selections.ProjectDirectory;
            }
        }

        private void SiteSelector_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            selections.SelectedSite = SiteSelector.SelectedValue.ToString();
        }

        private void SlotSelector_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            selections.SelectedSlot = SlotSelector.SelectedValue.ToString();
        }

        private Tuple<bool, string> ValidateForm()
        {
            var valid = true;
            string error = "";

            if (string.IsNullOrWhiteSpace(selections.ProjectDirectory))
            {
                valid = false;
                error = "Please select solution location";
            }
            if (string.IsNullOrWhiteSpace(selections.SelectedSite))
            {
                valid = false;
                error = "Please select a site";
            }
            if (string.IsNullOrWhiteSpace(selections.SelectedSlot))
            {
                valid = false;
                error = "Please select a slot";
            }

            return new Tuple<bool, string>(valid, error);
        }
    }

    public class Websites : ObservableCollection<string>
    {
        public Websites()
        {
            var Sites = ConfigurationManager.AppSettings["eventSites"].Split('|').ToList();

            foreach (var site in Sites)
            {
                Add(site);
            }
        }
    }

    public class Slots : ObservableCollection<string>
    {
        public Slots()
        {
            var Slots = ConfigurationManager.AppSettings["eventSlots"].Split('|').ToList();

            foreach (var slot in Slots)
            {
                Add(slot);
            }
        }
    }

    public class Selections
    {
        public string SelectedSlot { get; set; }
        public string SelectedSite { get; set; }
        public string ProjectDirectory { get; set; }
        public string PublishProfileDirectory { get; set; }

        public string GetWebAppName()
        {
            return ConfigurationManager.AppSettings["eventSites"].Split('|').Select(w => w == SelectedSite).ToString();
        }

        public void GetLocalPublishingProfile()
        {
            CommonOpenFileDialog dialog = new CommonOpenFileDialog();
            dialog.ShowDialog();

            PublishProfileDirectory = dialog.FileName;
        }

        public string GetPublishingProfileSaveLocation()
        {
            BlobHelper blobHelper = new BlobHelper();

            Microsoft.Win32.SaveFileDialog dlg = new Microsoft.Win32.SaveFileDialog();
            dlg.FileName = "EventsPublishingProfile";
            dlg.DefaultExt = ".xml"; // Default file extension
            dlg.Filter = "Text documents (.xml)|*.xml"; // Filter files by extension
            string fileName = string.Empty;

            Nullable<bool> result = dlg.ShowDialog();

            if (result == true)
            {
                fileName = dlg.FileName;
            }

            return fileName;
        }
    }

    public class Credentials
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
