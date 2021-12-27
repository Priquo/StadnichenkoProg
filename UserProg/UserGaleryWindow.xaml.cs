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

namespace UserProg
{
    /// <summary>
    /// Логика взаимодействия для UserGaleryWindow.xaml
    /// </summary>
    public partial class UserGaleryWindow : Window
    {
        ImageChanger ich;
        public UserGaleryWindow(List<usersimage> images)
        {
            InitializeComponent();
            ich = new ImageChanger(images);
            DataContext = ich;
        }

        private void imgUsers_Loaded(object sender, RoutedEventArgs e)
        {
            Image img = sender as Image;
            img.Source = ich.bitmaps[ich.CurrentImage];
        }
        private void ChangeImage_Click(object sender, RoutedEventArgs e)
        {
            Button bt = (Button)sender;
            switch(bt.Name)
            {
                case "btPred":
                    ich.CurrentImage--;
                    
                    break;
                case "btNext":
                    ich.CurrentImage++;
                    break;
                case "btSetAvatar":
                    MessageBox.Show("Нам очень жаль, но это пока не работает...");
                    break;
            }
            imgUsers.Source = ich.bitmaps[ich.CurrentImage];
        }
    }
}
