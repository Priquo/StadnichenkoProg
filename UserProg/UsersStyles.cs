using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserProg
{
    public partial class users
    {
        public bool TooOld { get => (DateTime.Now - dr).Days >= 365; }
    }
}
