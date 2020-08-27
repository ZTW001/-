using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPF之MVVMLight.Models;

namespace WPF之MVVMLight.ViewModel
{
    public class StudentViewModel:ViewModelBase
    {
        public StudentViewModel()
        {
            if(IsInDesignMode)
            {
                StudentModel = new StudentModel() { Id = 1, Name = "张三"};
            }
            else
            {
                //从数据库获取数据
                StudentModel = new StudentModel() { Id = 2, Name = "李四" };
            }
        }
        public Models.StudentModel StudentModel  { get; set; }
    }
}
