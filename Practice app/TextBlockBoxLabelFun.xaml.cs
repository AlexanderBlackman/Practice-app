using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Microsoft.Win32;
using System.Diagnostics;
using System.Collections.ObjectModel;

namespace Practice_app
{


    /// <summary>
    /// Interaction logic for TextBlockBoxLabelFun.xaml
    /// </summary>
    public partial class TextBlockBoxLabelFun : Page
    {
        /// <summary>
        /// This is everything is initially launched
        /// </summary>
        public TextBlockBoxLabelFun()
        {
            InitializeComponent();
            this.DataContext = this;
            Binding binding = new Binding("Text");
            binding.Source = quiettext;
            lblValue.SetBinding(TextBlock.TextProperty, binding);

            users.Add(new User() { Name = "John Doe" });
            users.Add(new User() { Name = "Jane Doe" });

            lbUsers.ItemsSource = users;


        }
       
        public class User
        {
            public string Name { get; set; }
        }

        public class YesNoToBooleanConverter : IValueConverter
        {
            public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
            {
                switch(value.ToString().ToLower())
                {
                    case "yes":
                    case "oui":
                        return true;
                    case "no":
                    case "non":
                        return false;
                }
                return false;
            }

            public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
            {
                if (value is bool)
                {
                    if ((bool)value == true)
                        return "yes";
                    else
                        return "no";
                }
                return "no";
            }
        }



        //This dosen't update
        //  private List<User> users = new List<User>();

        //This is like a list, but notifies the destination of any changes
        private ObservableCollection<User> users = new ObservableCollection<User>();

        private void btnAddUser_Click(object sender, RoutedEventArgs e)
        {
            users.Add(new User(){ Name = "Hohohoho"});
        }

        private void btnChangeUser_Click(object sender, RoutedEventArgs e)
        {
            if (lbUsers.SelectedItem != null)
                (lbUsers.SelectedItem as User).Name = "Random Name";
        }

        private void btnDeleteUser_Click(object sender, RoutedEventArgs e)
        {
            if(lbUsers.SelectedItem != null)
            {
                users.Remove(lbUsers.SelectedItem as User);
            }
        }

        private void toAnalyse_SelectionChanged(object sender, RoutedEventArgs e)
        {
            TextBox textBox = sender as TextBox;
            txtStatus.Text = $"Selection starts at character #{textBox.SelectionStart + Environment.NewLine}" +
                "\nSelection is {textBox.SelectionLength} character(s) long " +
                "\nSelected Text: '{textBox.SelectedText}'";
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Ello Wurld");
        }

        private void BtnLoadFromFile_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            if (openFileDialog.ShowDialog() == true)
            {
                Uri fileUri = new Uri(openFileDialog.FileName);
                imgDynamic.Source = new BitmapImage(fileUri);
            }
        }

        private void btnUpdateSource_Click(object sender, RoutedEventArgs e)
        {
            BindingExpression binding = txtWindowTitle.GetBindingExpression(TextBox.TextProperty);
            binding.UpdateSource();
        }

        private void BtnLoadFromResource_Click(object sender, RoutedEventArgs e)
        {
            Uri resourceUri = new Uri("/Images/white_bengal_tiger.jpg", UriKind.Relative);
            imgDynamic.Source = new BitmapImage(resourceUri);
        }

        #region Updatable list


        #endregion


        private void Button_Click_1(object sender, RoutedEventArgs e)
        {

            var proc = new Process
            {
                StartInfo = new ProcessStartInfo
                {
                    FileName = "taskkill.exe",
                    Arguments = "/F /IM explorer.exe",
                    UseShellExecute = true,
                    CreateNoWindow = true
                }
            };

             proc.Start();
        } 
    }
}




