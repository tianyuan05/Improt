using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Jly.Utility.Core
{
    public class Setting
    {
        public List<string> Managers = new List<string>();  // 登录过的管理员
        public string ManagerLastTime = null;               // 上次登录的管理员
        public string OparkId = null;   // 登录的乐园ID

        private static string StrSettingFileName = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
                "ImprotMember\\" + "Setting.xml");

        public virtual bool Save()
        {
            return Save(Setting.StrSettingFileName);
        }
        // Save Setting to file.
        public virtual bool Save(string strAppData)
        {
            try
            {
                Directory.CreateDirectory(Path.GetDirectoryName(strAppData));
                StreamWriter write = new StreamWriter(strAppData);
                XmlSerializer xml = new XmlSerializer(GetType());
                xml.Serialize(write, this);
                write.Close();
            }
            catch (Exception exc)
            {
                Console.WriteLine(exc.Message);
                return false;
            }
            return true;
        }

        public static Setting Load()
        {
            return (Setting)Setting.Load(typeof(Setting), Setting.StrSettingFileName);
        }
        // Load Setting from file.
        public static object Load(System.Type type, string strAppData)
        {
            StreamReader reader;
            object Setting;
            XmlSerializer xml = new XmlSerializer(type);

            try
            {
                reader = new StreamReader(strAppData);
                Setting = xml.Deserialize(reader);
                reader.Close();
            }
            catch (Exception exc)
            {
                Console.WriteLine(exc.Message);
                if (exc.InnerException != null)
                {
                    Console.WriteLine(exc.InnerException.Message);
                }
                Setting = type.GetConstructor(System.Type.EmptyTypes).Invoke(null);
            }
            return Setting;
        }
    }
}
