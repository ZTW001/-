using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace 序列化
{
    public class Peoson
    {
        private int age;
        private string name;
        public int Age
        {
            get { return age; }
            set { age = value; }
        }
        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        public Peoson() { }
        public Peoson(string name, int age)
        {
            this.age = age;
            this.name = name;
        }
        public void Say()
        {
            Console.WriteLine("名字：{0}，年龄{1}", name, age);
        }
        static void Main(string[] args)
        {
            //准备集合并为其添加数据
            List<Peoson> list = new List<Peoson>();
            Peoson p1 = new Peoson("小黄", 18);
            Peoson p2 = new Peoson("小白", 28);
            Peoson p3 = new Peoson("小青", 15);

            list.Add(p1);
            list.Add(p2);
            list.Add(p3);

            //序列化
            SerializeMethod(list);

            //反序列化
            List<Peoson> list2 = ReserializeMethod();//调用反序列化的方法  其方法返回值是一个List集合
            foreach (Peoson item in list2)//遍历集合中的元素
            {
                item.Say();
            }

            Console.ReadKey();

        }

        //序列化操作
        public static void SerializeMethod(List<Peoson> list)
        {
            using (FileStream fs = new FileStream("序列化.btn", FileMode.Create))
            {
                BinaryFormatter bf = new BinaryFormatter();
                bf.Serialize(fs, list);
                Console.WriteLine("序列化成功!");
            }
        }

        //反序列化操作
        public static List<Peoson> ReserializeMethod()
        {
            using (FileStream fs = new FileStream("序列化.btn", FileMode.Open))
            {

                BinaryFormatter bf = new BinaryFormatter();
                List<Peoson> list = (List<Peoson>)bf.Deserialize(fs);
                return list;
            }
        }
    }
}

