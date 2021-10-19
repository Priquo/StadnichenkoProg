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

namespace UserProg.pages
{
    /// <summary>
    /// Логика взаимодействия для AdminPage.xaml
    /// </summary>
    public partial class AdminPage : Page
    {
        public AdminPage()
        {
            InitializeComponent();
            dgUsers.ItemsSource = BaseConnect.BaseModel.auth.ToList();
        }

        private void buttBack_Click(object sender, RoutedEventArgs e)
        {
            LoadPages.MainFrame.GoBack();
        }

        private void buttEditUser_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Эта функция пока в разработке... :(");
        }

        private void buttDeleteUser_Click(object sender, RoutedEventArgs e)
        {
            auth SelectedUser = (auth)dgUsers.SelectedItem;//сохраняем выбранную строку datagrid в отдельный объект
            BaseConnect.BaseModel.auth.Remove(SelectedUser);//удаляем эту строку из модели
            BaseConnect.BaseModel.SaveChanges();//синхронизируем изменения с сервером
            MessageBox.Show("Выбранный пользователь удален");//обратная связь с оператором программы
            dgUsers.ItemsSource = BaseConnect.BaseModel.auth.ToList();//обновить строки в datagrid
        }

        private void buttSaveCahanges_Click(object sender, RoutedEventArgs e)
        {
            BaseConnect.BaseModel.SaveChanges();
        }

        private void buttShowList_Click(object sender, RoutedEventArgs e)
        {
            LoadPages.MainFrame.Navigate(new UsersDataPage());
        }

        private void buttAddUser_Click(object sender, RoutedEventArgs e)
        {
            LoadPages.MainFrame.Navigate(new Registration());
        }
    }
}
