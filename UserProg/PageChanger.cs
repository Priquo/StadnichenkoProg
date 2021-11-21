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
        public event PropertyChangingEventHandler PropertyChanged;
        static int countitems = 5;
        public int[] NPage { get; set; } = new int[countitems];//номер страницы, при нажатии на соответствующую кнопку    
        public string[] Visible { get; set; } = new string[countitems];//Массив свойст, отвечающий за видимость объекта Visible - видимый, Hidden - скрытый
        public string[] Foreground { get; set; } = new string[countitems];
    }
}
