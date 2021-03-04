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
using static PhotoStudio.EF;

namespace PhotoStudio
{
    /// <summary>
    /// Логика взаимодействия для ServiceWindow.xaml
    /// </summary>
    public partial class ServiceWindow : Window
    {
        public ServiceWindow()
        {
            InitializeComponent();
            lvEmpl.ItemsSource = Context.Employee.ToList();
        }

        private void btnDel_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (lvEmpl.SelectedItem is Employee employee)
                {
                    var result = MessageBox.Show("вы уверены?",
                        "УДАЛЕНИЕ",
                        MessageBoxButton.YesNo,
                        MessageBoxImage.Question);

                    if (result == MessageBoxResult.Yes)
                    {
                        Context.Employee.Remove(employee);
                        Context.SaveChanges();
                        MessageBox.Show("Удалили " + employee.ID);
                        lvEmpl.ItemsSource = Context.Employee.ToList();
                    }

                }
                else
                {
                    MessageBox.Show("Запись не выбрана");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);               
            }
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            AddEditWin addEditWin = new AddEditWin();
            this.Hide();
            addEditWin.ShowDialog();
            lvEmpl.ItemsSource = Context.Employee.ToList();
            this.Show();            
        }

        private void btnEdit_Click(object sender, RoutedEventArgs e)
        {
            if (lvEmpl.SelectedItem is Employee employee)
            {
                AddEditWin addEditWin = new AddEditWin(employee);
                this.Hide();
                addEditWin.ShowDialog();
                lvEmpl.ItemsSource = Context.Employee.ToList();
                this.Show();
            }    
        }
    }
}
