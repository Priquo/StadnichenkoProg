using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace UserProg
{
    class PageChanger : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        static int countitems = 5;
        public int[] NPage { get; set; } = new int[countitems];//номер страницы, при нажатии на соответствующую кнопку    
        public string[] Visible { get; set; } = new string[countitems];//Массив свойст, отвечающий за видимость объекта Visible - видимый, Hidden - скрытый
        public string[] Foreground { get; set; } = new string[countitems];//Массив свойств, отвечающий за изменение цвета текущей страницы
        int countPages;
        public int CountPages
        {
            get => countPages;
            set
            {
                countPages = value;
                for (int i = 0; i < countitems; i++)
                {
                    if (CountPages <= i) Visible[i] = "Hidden";
                    else Visible[i] = "Visible";
                }
            }
        }
        int countPageOnList;//количество на странице
        public int CountPageOnList  //количество на странице
        {
            get => countPageOnList;
            set
            {
                countPageOnList = value;
                if (Countlist % value == 0) CountPages = Countlist / value;//определение количества страниц
                else CountPages = 1 + Countlist / value;//если получается не целое еоличество страниц
            }
        }
        int countList;
        public int Countlist //количество записей в листе
        {
            get => countList;
            set
            {
                countList = value;
                if (value % CountPageOnList == 0) CountPages = value / CountPageOnList;//определение количества страниц
                else CountPages = 1 + value / CountPageOnList;
            }
        }
        int currentPage;
        public int CurrentPage
        {
            get => currentPage;
            set
            {
                currentPage = value;
                currentPage = currentPage < 1 ? 1 : currentPage;
                currentPage = currentPage >= countPages ? countPages : currentPage;
                // отрисовка элементов в трех вариациях
                for (int i = 0; i < countitems; i++)// рассматриваем три возможных случая
                {
                    if (currentPage < (1 + (countitems / 2)) || countPages < countitems) NPage[i] = i + 1;
                    else if (currentPage > CountPages - (countitems / 2 + 1)) NPage[i] = CountPages - (countitems - 1) + i;
                    else NPage[i] = currentPage + i - (countitems / 2);
                }
                for (int i = 0; i < countitems; i++)//выделяем активную страницу жирным
                {
                    if (NPage[i] == currentPage) Foreground[i] = "Red";
                    else Foreground[i] = "Purple";
                }
                //вызываем созбытие, связанное с изменением свойств, используемых в привязке на странице
                PropertyChanged(this, new PropertyChangedEventArgs("NPage"));
                PropertyChanged(this, new PropertyChangedEventArgs("Visible"));
                PropertyChanged(this, new PropertyChangedEventArgs("Foreground"));
            }
        }
        public PageChanger()
        {
            for (int i = 0; i < countitems; i++)
            {
                Visible[i] = "Visible";
                NPage[i] = i + 1;
                Foreground[i] = "Purple";
            }
            currentPage = 1;
            countPageOnList = 1;
            countList = 1;
        }
    }
}
