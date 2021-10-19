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
    public partial class UsersDataPage : Page
    {
        List<users> users;
        public UsersDataPage()
        {
            InitializeComponent();
            users = BaseConnect.BaseModel.users.ToList();
            listbUsersList.ItemsSource = users;
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
            int OT = Convert.ToInt32(txtOT.Text) - 1;
            int DO = Convert.ToInt32(txtDO.Text);
            List<users> lu1 = users.Skip(OT).Take(DO - OT).ToList();
            listbUsersList.ItemsSource = lu1;
        }

        private void btReset_Click(object sender, RoutedEventArgs e)
        {
            listbUsersList.ItemsSource = users;
        }
    }
}
