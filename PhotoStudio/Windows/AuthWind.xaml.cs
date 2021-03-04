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

namespace PhotoStudio
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class AuthWind : Window
    {
        public AuthWind()
        {
            InitializeComponent();
        }

        private void btnSignIn_Click(object sender, RoutedEventArgs e)
        {
            var user1 = EF.Context.Employee.ToList().
                Where(i => i.Login == txtLogin.Text && i.Password == txtPassword.Password)
                .FirstOrDefault();

            if (user1 != null)
            {
                ServiceWindow serviceWin = new ServiceWindow();
                serviceWin.Show();
                this.Close();
            }
            else
            {
                MessageBox.Show("Пользователь не найден");
            }
            
        }
    }
}
