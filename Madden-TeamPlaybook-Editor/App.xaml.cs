using System;
using System.Collections;
using System.IO;
using System.IO.IsolatedStorage;
using System.Windows;

namespace Madden20PlaybookEditor
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        string filename = "MaddenTeamPlaybookEditor.settings.txt";

        public App()
        {
        }

        private void App_Startup(object sender, StartupEventArgs e)
        {
            var primaryColor = Application.Current.Resources["Primary"];

            // Restore application-scope property from isolated storage
            IsolatedStorageFile storage = IsolatedStorageFile.GetStore(IsolatedStorageScope.User | IsolatedStorageScope.Assembly, null, null);
            try
            {
                using (IsolatedStorageFileStream stream = new IsolatedStorageFileStream(filename, FileMode.Open, storage))
                using (StreamReader reader = new StreamReader(stream))
                {
                    // Restore each application-scope property individually
                    while (!reader.EndOfStream)
                    {
                        string[] keyValue = reader.ReadLine().Split(new char[] { ',' });
                        if (Application.Current.Resources[keyValue[0]] is System.Windows.Media.Color)
                        {
                            System.Windows.Media.Color color = (System.Windows.Media.Color)System.Windows.Media.ColorConverter.ConvertFromString(keyValue[1]);
                            Application.Current.Resources[keyValue[0]] = color;
                        }
                        else if (keyValue[0] == "UseTeamColors")
                        {
                            Application.Current.Resources[keyValue[0]] = Boolean.Parse(keyValue[1]);
                        }
                    }
                }
            }
            catch (FileNotFoundException ex)
            {
                // Handle when file is not found in isolated storage:
                // * When the first application session
                // * When file has been deleted
            }
        }

        private void App_Exit(object sender, ExitEventArgs e)
        {
            // Persist application-scope property to isolated storage
            IsolatedStorageFile storage = IsolatedStorageFile.GetStore(IsolatedStorageScope.User | IsolatedStorageScope.Assembly, null, null);
            using (IsolatedStorageFileStream stream = new IsolatedStorageFileStream(filename, FileMode.Create, storage))
            using (StreamWriter writer = new StreamWriter(stream))
            {
                // Persist each application-scope property individually
                foreach (DictionaryEntry entry in Application.Current.Resources)
                {
                    if (entry.Value is System.Windows.Media.Color || entry.Key.ToString() == "UseTeamColors")
                    {
                        writer.WriteLine("{0},{1}", entry.Key, entry.Value.ToString());
                    }
                }
            }
        }
    }
}
