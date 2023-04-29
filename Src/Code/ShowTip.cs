using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace StarRailUnlock
{
    public static class ShowTipCode
    {

        /// <summary>
        /// 提示气泡对象
        /// </summary>
        public static readonly ToolTip TTip = new ToolTip();

        /// <summary>
        /// 在指定控件上显示提示气泡
        /// </summary>
        /// <param name="message">消息</param>
        /// <param name="control">控件</param>
        public static void ShowTip(string message, Control control)
        {
            try
            {
                TTip.Show(message, control, 0, control.Size.Height, 3000);
            }
            catch (Exception e)
            {
                MessageBox.Show(message, e.Message);
                return;
            }
        }
    }
}
