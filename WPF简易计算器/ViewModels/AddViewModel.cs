using System;
/// <summary>
/// 新建一个AddViewModel模型，让其继承NotifyPropertyObject。
/// </summary>
public class AddViewModel : NotifyPropertyObject
{
    //第1个数。
    public double _number1;
    public double Number1
    {
        get { return _number1; }
        set
        {
            _number1 = value;
            this.OnPropertyChanged("Number1");
        }
    }

    //第2个数。
    public double _number2;
    public double Number2
    {
        get { return _number2; }
        set
        {
            _number2 = value;
            this.OnPropertyChanged("Number2");
        }
    }

    //结果。
    public double _result;
    public double Result
    {
        get { return _result; }
        set
        {
            _result = value;
            this.OnPropertyChanged("Result");
        }
    }

    //声明AddCommand，一定要使用public修饰符修饰啊。
    public MVVM2.Commands.DelegateCommand AddCommand { get; set; }


    public AddViewModel()
    {
        //在构造函数中做做手脚。
        //在构造函数中实例化AddCommand对象。
        AddCommand = new MVVM2.Commands.DelegateCommand();
        //实例化AddCommand类中的ExcuteAction委托，为其指定一个方法。
        AddCommand.ExcuteAction = new Action<object>(Add);
    }
    /// <summary>
    /// 此方法用于将两个数相加。
    /// </summary>
    /// <param name="obj"></param>
    public void Add(object obj)
    {
        this.Result = this.Number1 + this.Number2;
    }
}