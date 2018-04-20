using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

namespace UserApp
{
    
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public DbService Dbservice { get; set; }
        public MainWindow()
        {
            InitializeComponent();
            Dbservice = new DbService();
            UpdateList();
        }
        public void UpdateList()
        {
            List<User> users = Dbservice.GetUsers(IsAdminCheckBox.IsChecked.Value);
            UsersList.ItemsSource = new ObservableCollection<User>(users);
        }
        private void CreateUserButton_Click(object sender, RoutedEventArgs e)
        {
            Window createUser = new CreateUser(this);
            createUser.Show();
        }
    }
}
