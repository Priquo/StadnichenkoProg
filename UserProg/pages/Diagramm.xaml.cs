using System;
using System.Collections;
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
    /// Логика взаимодействия для Diagramm.xaml
    /// </summary>    
    public partial class Diagramm : Page
    {
        public Diagramm()
        {
            InitializeComponent();
        }

        private void gridDiagram_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            List<users> users = BaseConnect.BaseModel.users.ToList();
            Dictionary<int, int> ages = new Dictionary<int, int>();
            ages.Add((int)(DateTime.Now.Subtract(users[0].dr).TotalDays / 365), 1);
            foreach (var user in users)
            {
                int age = (int)( DateTime.Now.Subtract(user.dr).TotalDays / 365);

                if (!ages.ContainsKey(age))
                    ages.Add(age, 1);
                else
                    ages[age]++;
            }
            ICollection keys = ages.Keys;
            int min = 0, max = 0;
            

            double maxX = gridDiagram.ActualWidth;//получаем текущую ширину и высоту
            double maxY = gridDiagram.ActualHeight;
            gridDiagram.Children.Clear();//очишаем графическое поле
            gridDiagram.Children.Add(line(maxX / 20, maxY / 20, maxX / 20, maxY - maxY / 20));//помещаем созданный объект на grid
            gridDiagram.Children.Add(line(maxX / 20, maxY - maxY / 20, maxX - maxX / 20, maxY - maxY / 20));
            double count = ages.Count();
            double stepX = (maxX - maxX / 10) / count;
            
            int i = 0;
            foreach(var key in keys)
            {            
               
                Line L = line(maxX / 20 + stepX * i, maxY - maxY / 20, maxX / 20 + stepX * i, maxY - maxY / 20 - 9 * i);
                L.Stroke = Brushes.HotPink;
                L.StrokeThickness = maxX / 100;
                gridDiagram.Children.Add(L);
                TextBlock TB = new TextBlock();
                TB.Margin = new Thickness(maxX / 20 + stepX * i, maxY - maxY / 20 - 9 * i, 0, 0);                
                TB.Text = (ages[(int)key]).ToString();
                gridDiagram.Children.Add(TB);
                //gridDiagram.Children.Add(polygon(maxX / 20 * i, (maxY - maxY / 20) * i, maxX / 20 * i + maxX / 40, maxY / 20));
                i++;
            }
        }
        private Line line(double x1, double y1, double x2, double y2)
        {
            Line L = new Line();
            L.X1 = x1;
            L.Y1 = y1;
            L.X2 = x2;
            L.Y2 = y2;
            L.Stroke = Brushes.Black;
            return L;
        }
    }
}
