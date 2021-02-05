﻿using System;
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
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void btnSignIn_Click(object sender, RoutedEventArgs e)
        {

            var user1 = EF.context.Employee.
            Where(i => i.Login == txtLogin.Text && i.Password == txtPassword.Password).ToList();

            if (user1.Count != 0)
            {
                ServiceWindow serviceWin = new ServiceWindow();
                this.Hide();
                serviceWin.ShowDialog();
                this.Show();
            }
            else
            {
                MessageBox.Show("Пользователь не найден");
            }

            
            
        }
    }
}
