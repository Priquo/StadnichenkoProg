﻿using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace UserProg.pages
{
    /// <summary>
    /// Логика взаимодействия для Login.xaml
    /// </summary>
    public partial class Login : Page
    {
        public Login()
        {
            InitializeComponent();
        }
        private void buttonReg_Click(object Sender, RoutedEventArgs e)
        {
            LoadPages.MainFrame.Navigate(new Registration());
        }

        private void buttonLogin_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                auth CurrUser = BaseConnect.BaseModel.auth.FirstOrDefault(x => x.login == textboxLogin.Text && x.password == psswBox1.Password);
                if (CurrUser != null)
                {
                    switch (CurrUser.role)
                    {
                        case 1:
                            MessageBox.Show("Вы вошли как крутой админ");
                            LoadPages.MainFrame.Navigate(new AdminPage());
                            break;
                        case 2:
                        default:
                            MessageBox.Show("Вы вошли как непримечательный пользователь");
                            LoadPages.MainFrame.Navigate(new Form(CurrUser));
                            break;
                    }
                }
                else
                    MessageBox.Show("Ты в курсе, что таких людей не существует?");
            }
            catch
            {
                MessageBox.Show("Невероятно, но что-то создает ошибку, программа не работает");
            }
        }
    }
}
