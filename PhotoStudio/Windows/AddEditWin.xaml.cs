using Microsoft.Win32;
using System;
using System.Collections.Generic;
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
using System.Windows.Shapes;

namespace PhotoStudio
{
    /// <summary>
    /// Логика взаимодействия для AddEditWin.xaml
    /// </summary>
    public partial class AddEditWin : Window
    {
        string pathPhoto = string.Empty;
        private bool isEdit;
        private Employee editEmployee;

        public AddEditWin()
        {
            InitializeComponent();
            isEdit = false;
        }

        public AddEditWin(Employee empl)
        {
            editEmployee = empl;

            InitializeComponent();
            isEdit = true;
            tbTitle.Text = "Изменение данных пользователя";
            btnAdd.Content = "Изменить";
            txtFName.Text = empl.FirstName;
            txtLName.Text = empl.LastName;
            txtMName.Text = empl.MiddleName;
            txtLogin.Text = empl.Login;
            txtPassw.Text = empl.Password;
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtLName.Text))
            {
                MessageBox.Show("Поле Фамилия не может быть пустым");
                return;
            }
            if (string.IsNullOrWhiteSpace(txtFName.Text))
            {
                MessageBox.Show("Поле Имя не может быть пустым");
                return;
            }
           
            if (string.IsNullOrWhiteSpace(txtLogin.Text))
            {
                MessageBox.Show("Поле Логин не может быть пустым");
                return;
            }
            if (string.IsNullOrWhiteSpace(txtPassw.Text))
            {
                MessageBox.Show("Поле Пароль не может быть пустым");
                return;
            }

            if (!isEdit)
            {
                var unicLog = EF.Context.Employee.ToList().
                Where(i => i.Login == txtLogin.Text)
                .FirstOrDefault();

                if (unicLog != null)
                {
                    MessageBox.Show($"Логин {txtLogin.Text} занят");
                    return;
                }

                Employee addEmployee = new Employee();
                addEmployee.FirstName = txtFName.Text;
                addEmployee.LastName = txtLName.Text;
                addEmployee.MiddleName = txtMName.Text;
                addEmployee.Login = txtLogin.Text;
                addEmployee.Password = txtPassw.Text;

                addEmployee.Photo = File.ReadAllBytes(pathPhoto);

                EF.Context.Employee.Add(addEmployee);
                EF.Context.SaveChanges();
                MessageBox.Show("Все ХАРАШО");
                this.Close();
            }
            else if (isEdit)
            {
                editEmployee.LastName = txtLName.Text;
                editEmployee.FirstName = txtFName.Text;
                editEmployee.MiddleName = txtMName.Text;
                editEmployee.Login = txtLogin.Text;
                editEmployee.Password = txtPassw.Text;
                if (pathPhoto != null)
                {
                    editEmployee.Photo = File.ReadAllBytes(pathPhoto);
                }
                EF.Context.SaveChanges();
                MessageBox.Show("Данные изменены");
                this.Close();
            }

            
        }

        private void btnChoosePhoto_Click(object sender, RoutedEventArgs e)
        {
            pathPhoto = string.Empty;
            OpenFileDialog openFile = new OpenFileDialog();
            openFile.ShowDialog();
            pathPhoto = openFile.FileName;
            imgPhoto.Source = new BitmapImage(new Uri(pathPhoto));
        }
    }
}
