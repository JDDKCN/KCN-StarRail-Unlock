using System;
using System.Text;
using System.Windows.Forms;
using static StarRailUnlock.ShowTipCode;
using static StarRailUnlock.Unlock;

namespace StarRailUnlock
{
    public partial class Home : Form
    {
        public Home()
        {
            InitializeComponent();
        }

        public static bool IsNumber(string str)
        {
            if (str == null || str.Length == 0)
                return false;
            ASCIIEncoding ascii = new ASCIIEncoding();
            byte[] bytestr = ascii.GetBytes(str);

            foreach (byte c in bytestr)
            {
                if (c < 48 || c > 57)
                {
                    return false;
                }
            }
            return true;
        }

        private bool Start()
        {
            if (string.IsNullOrEmpty(FPS.Text))
            {
                ShowTip("请填写要解锁的帧率！", FPS);
                return false;
            }

            bool i = IsNumber(FPS.Text);
            if (i == false)
            {
                ShowTip("请输入正确的帧率！", FPS);
                return false;
            }

            int number = int.Parse(FPS.Text);
            FPS.Text = number.ToString();
            if (number > 400)
            {
                ShowTip("帧率不得大于400！", FPS);
                return false;
            }

            if (number < 10)
            {
                ShowTip("帧率不得小于10！", FPS);
                return false;
            }

            return true;

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if(!Start() == true)
            {
                return;
            }
            int number = int.Parse(FPS.Text);

            if (radioButton1.Checked == true)
            {
                UnlockMain(CN,number,richTextBox1);
            }
            else if(radioButton2.Checked == true)
            {
                UnlockMain(OS, number, richTextBox1);
            }

        }

        private void Home_Load(object sender, EventArgs e)
        {
            this.Text = Ver.APPName + Ver.Version;
            label3.Text = Ver.copyright;
            Console.SetOut(new TextBoxWriter(richTextBox1));
            Console.WriteLine($"欢迎使用！\n作者B站: {Ver.biliURL} ");

        }
    }
}
