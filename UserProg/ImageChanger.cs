using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media.Imaging;

namespace UserProg
{
    class ImageChanger : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        int countImages = 1;
        public List<BitmapImage> bitmaps = new List<BitmapImage>();
        int currentImage = 0;
        public int CurrentImage
        {
            get => currentImage;
            set
            {
                currentImage = value;
                if (currentImage >= countImages) currentImage = countImages - 1;
                else if (currentImage < 0) currentImage = 0;
                PropertyChanged(this, new PropertyChangedEventArgs("CurrentImage"));
            }
        }
        public ImageChanger(List<usersimage> images)
        {
            countImages = images.Count;
            int i = 0;
            foreach (var img in images)
            {
                if (img.path != null)
                {
                    bitmaps.Add(new BitmapImage(new Uri(img.path, UriKind.Relative)));
                }
                else
                {
                    bitmaps.Add(new BitmapImage());
                    bitmaps[i].BeginInit();
                    bitmaps[i].StreamSource = new MemoryStream(img.image);
                    bitmaps[i].EndInit();
                }
                i++;
            }
        }
    
    }
}
