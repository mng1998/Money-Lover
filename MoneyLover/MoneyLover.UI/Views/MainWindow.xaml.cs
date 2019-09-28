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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace MoneyLover.UI.Views
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private ViewModels.Register registerView;
        private ViewModels.SignIn signinView;

        public MainWindow()
        {
            InitializeComponent();
            registerView = new ViewModels.Register();
            signinView = new ViewModels.SignIn();
        }

        private void btnRegister_Click(object sender, RoutedEventArgs e)
        {
            registerView.Show();
            this.Close();
        }

        private void btnSignIn_Click(object sender, RoutedEventArgs e)
        {
            signinView.Show();
            this.Close();
        }
    }
}
