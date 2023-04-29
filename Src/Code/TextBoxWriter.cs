using System;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace StarRailUnlock
{
    public class TextBoxWriter : System.IO.TextWriter
    {
        RichTextBox rtBox;
        delegate void VoidAction();

        public TextBoxWriter(RichTextBox box)
        {
            Control.CheckForIllegalCrossThreadCalls = false;
            rtBox = box;
        }
        public override void WriteLine(string value)
        {
            VoidAction action = delegate
            {
                try
                {
                    DbLogger.LogWrite(value);
                    string[] strLines = rtBox.Text.Split('\n');
                    if (strLines.Length > 1000)
                    {
                        rtBox.Clear();
                    }
                    if (!string.IsNullOrEmpty(value))
                    {
                        rtBox.Focus();
                        //滚动到控件光标处  
                        rtBox.ScrollToCaret();
                        rtBox.AppendText(string.Format("\r\n[{0:HH:mm:ss}]{1}\r\n", DateTime.Now, value));
                        rtBox.ForeColor = Color.FromArgb(0, 0, 0);
                    }
                    else
                    {
                        rtBox.AppendText("No Msg");
                    }


                }
                catch (Exception ex)
                {
                    DbLogger.LogException(ex, ex.Message);
                }

            };
            if (rtBox.IsHandleCreated)
            {
                try
                {
                    rtBox.BeginInvoke(action);
                }
                catch { }
            }

        }
        public override Encoding Encoding
        {
            get { return System.Text.Encoding.UTF8; }
        }

    }

    public static class DbLogger
    {
        public static void LogWrite(string context)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine(string.Concat(context));
        }
        public static void LogException(Exception ex, string lastCommand = null)
        {
            StringBuilder sb = new StringBuilder();

            if (!string.IsNullOrWhiteSpace(lastCommand))
                sb.AppendLine(string.Concat("LastCommand:", lastCommand));

            if (ex.InnerException != null)
                ex = ex.InnerException;

            sb.AppendLine(string.Concat("异常信息: " + ex.Message));
            sb.AppendLine(string.Concat("错误源:" + ex.Source));
            sb.AppendLine(string.Concat("堆栈信息:\r\n" + ex.StackTrace));

        }
    }

}
