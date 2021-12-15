using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;

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
        string kode;
        bool flagKode = false;
        private void buttonReg_Click(object Sender, RoutedEventArgs e)
        {
            LoadPages.MainFrame.Navigate(new Registration());
        }
        Random random = new Random();
        DispatcherTimer dispatcherTimer = new DispatcherTimer();
        private void generateKey()
        {
            buttResetCod.IsEnabled = false;
            kode = "";

            for (int i = 0; i < 8; i++)
            {
                kode += ((char)random.Next(33, 122)).ToString();
            }
            tboxCod.Text = kode;
            MessageBox.Show(kode, "введите код в соответствующее поле на форме.", MessageBoxButton.OK, MessageBoxImage.Information, MessageBoxResult.OK, MessageBoxOptions.RightAlign);            
            dispatcherTimer.Interval = new TimeSpan(0, 0, 10);
            dispatcherTimer.Tick += new EventHandler(dispatcherTimer_Tick);
            dispatcherTimer.Start();
        }
        private void dispatcherTimer_Tick(object sender, EventArgs e)
        {
            buttResetCod.IsEnabled = true;
            for (int i = 0; i < 8; i++)
            {
                kode += ((char)random.Next(33, 122)).ToString();
            }
            MessageBox.Show("время вышло");
        }
        private void buttonLogin_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                auth CurrUser = BaseConnect.BaseModel.auth.FirstOrDefault(x => x.login == textboxLogin.Text && x.password == psswBox1.Password);
                if (CurrUser != null)
                {
                    if (flagKode == false)
                    {
                        generateKey();
                        flagKode = true;
                    }
                    else if (tboxCod.Text == kode)
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
                        dispatcherTimer.Stop();
                        tboxCod.Text = "";
                    }         
                    else
                    {
                        MessageBox.Show("Неверный код");
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

        private void buttResetCod_Click(object sender, RoutedEventArgs e)
        {
            generateKey();
            flagKode = true;
        }
    }
}
