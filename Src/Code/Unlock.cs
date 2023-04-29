using System;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Microsoft.Win32;
using Newtonsoft.Json.Linq;

namespace StarRailUnlock
{
    public static class Unlock
    {
        //Game
        public static string CN = "CN";
        public static string OS = "OS";

        public static void UnlockMain(string Server, int FPS, RichTextBox richTextBox)
        {
            Console.SetOut(new TextBoxWriter(richTextBox));
            Console.WriteLine("开始解锁 崩坏:星穹铁道 游戏帧率... ");

            if (Server == CN)
            {
                var node = Registry.CurrentUser.OpenSubKey("Software")?.OpenSubKey("miHoYo")?.OpenSubKey("崩坏：星穹铁道", true);
                if (node == null)
                {
                    Console.WriteLine("打开国服注册表失败，请尝试至少在游戏中修改一次图形设置并关闭设置界面!");
                    return;
                }
                var keyName = node.GetValueNames().FirstOrDefault(x => x.StartsWith("GraphicsSettings_Model"));
                if (keyName == default)
                {
                    Console.WriteLine("获取国服注册表内容失败，请尝试至少在游戏中修改一次图形设置并关闭设置界面!");
                    return;
                }
                var key = node.GetValue(keyName);
                if (key == null)
                {
                    Console.WriteLine("获取国服注册表内容失败，请尝试至少在游戏中修改一次图形设置并关闭设置界面!");
                    return;
                }
                var value = Encoding.UTF8.GetString((byte[])key);
                var json = JObject.Parse(value);
                json["FPS"] = FPS;
                node.SetValue(keyName, Encoding.UTF8.GetBytes(json.ToString(Newtonsoft.Json.Formatting.None)));
                Console.WriteLine($"已把 国服-CN 的帧率修改为 {FPS} FPS.");

            }
            else if (Server == OS)
            {
                var intlNode = Registry.CurrentUser.OpenSubKey("Software")?.OpenSubKey("Cognosphere")?.OpenSubKey("Star Rail", true);
                if (intlNode != null)
                {
                    var intlKeyName = intlNode.GetValueNames().FirstOrDefault(x => x.StartsWith("GraphicsSettings_Model"));
                    if (intlKeyName == null)
                    {
                        Console.WriteLine("获取国际服注册表内容失败，请尝试至少在游戏中修改一次图形设置并关闭设置界面!");
                        return;
                    }
                    var intlKey = intlNode.GetValue(intlKeyName);
                    if (intlKey == null)
                    {
                        Console.WriteLine("获取国际服注册表内容失败，请尝试至少在游戏中修改一次图形设置并关闭设置界面!");
                        return;
                    }
                    var intlValue = Encoding.UTF8.GetString((byte[])intlKey);
                    var intlJson = JObject.Parse(intlValue);
                    intlJson["FPS"] = FPS;
                    intlNode.SetValue(intlKeyName, Encoding.UTF8.GetBytes(intlJson.ToString(Newtonsoft.Json.Formatting.None)));
                    Console.WriteLine($"已把 国际服-OS 的帧率修改为 {FPS} FPS.");
                }

            }

        }


    }
}
