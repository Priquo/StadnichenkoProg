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
    /// Логика взаимодействия для Form.xaml
    /// </summary>
    public partial class Form : Page
    {
        public Form()
        {
            InitializeComponent();
        }

        public Form(auth CurrUser)
        {
            InitializeComponent();
            try
            {
                textName.Text = CurrUser.users.name.ToString();
                textDateBirth.Text = CurrUser.users.dr.ToString("dd MMMM yyyy");
                textGender.Text = CurrUser.users.genders.gender;
                List<users_to_traits> utt = BaseConnect.BaseModel.users_to_traits.Where(x => x.id_user == CurrUser.id).ToList();
                foreach (var ut in utt)
                {
                    textOtherInfo.Text += ut.traits.trait + "; ";
                }
            }
            catch
            {
                MessageBox.Show("У вас нет данных для вывода");
            }
        }

        private void buttBack_Click(object sender, RoutedEventArgs e)
        {
            LoadPages.MainFrame.GoBack();
        }
    }
}
