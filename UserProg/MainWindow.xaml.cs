using System.Windows;
using UserProg.pages;

namespace UserProg
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            //pages.Navigate(new Login());
            pages.Navigate(new Login());
            LoadPages.MainFrame = pages;
            BaseConnect.BaseModel = new Entities();
        }
    }
}
