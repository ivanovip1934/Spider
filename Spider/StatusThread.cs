using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Spider
{
    class StatusThread
    {
        public bool IsComplit = false;
    }
    class ServiceThread
    {

        public ServiceThread(ListViewItem listViewItem, StatusThread stThread)
        {
            this.Lstvwitem = listViewItem;
            this.StThread = stThread;

        }
        public ListViewItem Lstvwitem;
        public StatusThread StThread;
        public void SetColor(bool online)
        {
            Lstvwitem.ForeColor = online ? Color.Green : Color.Red;
        }

    }
}
