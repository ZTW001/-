using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
namespace SerializableTest
{
    [Serializable]
    public class Person
    {
        public string Sno { get; set; }
        public string Name { get; set; }
        public string Sex { get; set; }
        public int Age { get; set; }
        public string DisplayInfo()
        {
            return "我的学号是：" + Sno + "\n我的名字是：" + Name + "\n我的性别为：" + Sex + "\n我的年龄：" + Age + "\n";
        }
    }
}