using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Drawing;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using Image = System.Windows.Controls.Image;

namespace UserProg.pages
{
    public partial class UsersDataPage : Page
    {
        List<users> users;
        List<users> lu;
        PageChanger pch = new PageChanger();
        public UsersDataPage()
        {
            InitializeComponent();
            users = BaseConnect.BaseModel.users.ToList();
            listbUsersList.ItemsSource = users;            
            cbGender.ItemsSource = BaseConnect.BaseModel.genders.ToList();
            cbGender.SelectedValuePath = "id";
            cbGender.DisplayMemberPath = "gender";
            lu = users;
            DataContext = pch;
        }

        private void listb_traits_Loaded(object sender, RoutedEventArgs e)
        {
            ListBox list_u = (ListBox)sender;
            int id = Convert.ToInt32(list_u.Uid);
            list_u.ItemsSource = BaseConnect.BaseModel.users_to_traits.Where(x => x.id_user == id).ToList();
            list_u.DisplayMemberPath = "traits.trait";
        }

        private void btGo_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                int OT = Convert.ToInt32(txtOT.Text) - 1;
                int DO = Convert.ToInt32(txtDO.Text);
                List<users> lu1 = users.Skip(OT).Take(DO - OT).ToList();
                listbUsersList.ItemsSource = lu1;
            }
            catch
            {
                MessageBox.Show("Введите числа в обе графы!");
            }
        }

        private void btReset_Click(object sender, RoutedEventArgs e)
        {
            listbUsersList.ItemsSource = users;
            txtOT.Text = "";
            txtDO.Text = "";
            txtbSearchName.Text = "";
            cbGender.SelectedItem = null;
            txtbShowPages.Text = "";
        }

        private void buttBack_Click(object sender, RoutedEventArgs e)
        {
            LoadPages.MainFrame.GoBack();
        }
        private void Filters(object sender, RoutedEventArgs e)
        {
            try
            {
                lu = users.ToList();
                //фильтр по полу
                if (cbGender.SelectedValue != null)
                {
                    lu = lu.Where(x => x.gender == (int)cbGender.SelectedValue).ToList();
                }
                //поиск по имени
                if (txtbSearchName.Text != "")
                {
                    lu = lu.Where(x => x.name.Contains(txtbSearchName.Text)).ToList();
                }
                listbUsersList.ItemsSource = lu;
            }
            catch
            {
                MessageBox.Show("Да и черт с ним");
            }
        }
        private void ChangePage_MouseDown(object sender, MouseButtonEventArgs e)
        {
            TextBlock tb = (TextBlock)sender;
            switch(tb.Name)
            {
                case "tbPred":
                    pch.CurrentPage--;
                    break;
                case "tbnext":
                    pch.CurrentPage++;
                    break;
                default:
                    pch.CurrentPage = Convert.ToInt32(tb.Text);
                    break;
            }
            listbUsersList.ItemsSource = lu.Skip(pch.CurrentPage * pch.CountPageOnList - pch.CountPageOnList).Take(pch.CountPageOnList).ToList();
        }

        private void txtbShowPages_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                pch.CountPageOnList = Convert.ToInt32(txtbShowPages.Text);
            }
            catch
            {                
                pch.CountPageOnList = lu.Count;
            }
            pch.Countlist = users.Count;
            listbUsersList.ItemsSource = lu.Skip(0).Take(pch.CountPageOnList).ToList();
        }

        private void tbPred_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {

        }

        private void userImage_Loaded(object sender, RoutedEventArgs e)
        {
            System.Windows.Controls.Image IMG = sender as Image;
            int ind = Convert.ToInt32(IMG.Uid);
            users user = BaseConnect.BaseModel.users.FirstOrDefault(x => x.id == ind);
            usersimage uIm = BaseConnect.BaseModel.usersimage.FirstOrDefault(x => x.id_user == ind);
            BitmapImage image = new BitmapImage();
            if(uIm != null)
            {
                if (uIm.path != null)
                {
                    image = new BitmapImage(new Uri(uIm.path, UriKind.Relative));
                }
                else
                {
                    image.BeginInit();
                    image.StreamSource = new MemoryStream(uIm.image);
                    image.EndInit();
                }
            }
            else
            {
                switch(user.gender)
                {
                    case 1:
                        image = new BitmapImage(new Uri(@"/images/genders/boy.png", UriKind.Relative));
                        break;
                    case 2:
                        image = new BitmapImage(new Uri(@"/images/genders/girl.png", UriKind.Relative));
                        break;
                    default:
                        image = new BitmapImage(new Uri(@"/images/genders/other.png", UriKind.Relative));
                        break;
                }
            }
            IMG.Source = image;
            IMG.MaxHeight = 400;
        }

        private void btLoadImage_Click(object sender, RoutedEventArgs e)
        {
            Button butt = (Button)sender;
            int ind = Convert.ToInt32(butt.Uid);
            OpenFileDialog open = new OpenFileDialog();
            open.DefaultExt = ".png";
            open.Filter = "Изорбражения |*.jpg;*.png";
            var success = open.ShowDialog();
            if ((bool)success)
            {
                System.Drawing.Image UserImage = System.Drawing.Image.FromFile(open.FileName);//создаем изображение
                ImageConverter IC = new ImageConverter();//конвертер изображения в массив байт
                byte[] ByteArr = (byte[])IC.ConvertTo(UserImage, typeof(byte[]));//непосредственно конвертация
                usersimage UI = new usersimage() { id_user = ind, image = ByteArr };//создаем новый объект usersimage
                BaseConnect.BaseModel.usersimage.Add(UI);//добавляем его в модель
                BaseConnect.BaseModel.SaveChanges();//синхронизируем с базой
                MessageBox.Show("картинка пользователя добавлена в базу");
            }
            else
            {
                MessageBox.Show("картинка не была выбрана");
            }
        }
    }
}
