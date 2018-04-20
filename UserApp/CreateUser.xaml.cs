using System;
using System.Collections.Generic;
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
using System.Windows.Shapes;

namespace UserApp
{
    /// <summary>
    /// Логика взаимодействия для CreateUser.xaml
    /// </summary>
    public partial class CreateUser : Window
    {
        private MainWindow Window { get; set; }
        public CreateUser(MainWindow mainWindow)
        {
            InitializeComponent();
            Window = mainWindow;
        }

        private void CreateButton_Click(object sender, RoutedEventArgs e)
        {
            DbService dbService = new DbService();
            dbService.CreateUser(new User()
            {
                Login = LoginText.Text,
                Password = PasswordText.Text,
                Address = AdressText.Text,
                Phone = TelephoneText.Text,
                IsAdmin = IsAdminCheckBox.IsChecked.Value,
            });

            Window.UpdateList();
            Close();
        }
    }
}
