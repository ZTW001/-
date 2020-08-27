using System;
using System.IO;
using System.Runtime.Serialization;
using System.Xml.Serialization;
namespace SerializableTest
{
    class Program
    {
        static void Main(string[] args)
        {
            //创建一个格式化程序的实例
            XmlSerializer formatter = new XmlSerializer(typeof(Person));
            Console.WriteLine("对象序列化开始……");
            var me = new Person
            {
                Sno = "200719",
                Name = "yuananyun",
                Sex = "man",
                Age = 22
            };
            //创建一个文件流
            Stream stream = new FileStream("c:/personInfo.txt", FileMode.OpenOrCreate, FileAccess.Write, FileShare.None);
            formatter.Serialize(stream, me);
            stream.Close();
            Console.WriteLine("序列化结束！\n");
            Console.WriteLine("反序列化开始……");
            //反序列化
            Stream destream = new FileStream("c:/personInfo.txt", FileMode.Open,
            FileAccess.Read, FileShare.Read);
            var stillme = (Person)formatter.Deserialize(destream);
            stream.Close();
            Console.WriteLine("反序列化结束，输出对象信息……");
            Console.WriteLine(stillme.DisplayInfo());
            Console.ReadKey();
        }
    }
}