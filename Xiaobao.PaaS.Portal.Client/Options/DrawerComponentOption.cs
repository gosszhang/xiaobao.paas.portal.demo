using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Xiaobao.PaaS.Portal.Client.Options
{
    public class DrawerComponentOption
    {
        public bool Visible { get; set; }

        public void Open() => Visible = true;

        public void Close() => Visible = false;

        public string Title { get; set; }
    }
}
