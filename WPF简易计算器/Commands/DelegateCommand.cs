using System.Windows.Input;
using System;
/// <summary>
/// DelegateCommand类，实现ICommand接口
/// </summary>
class DelegateCommand : ICommand
{
    public bool CanExecute(object parameter)
    {
        if (CanExecuteFunc == null)
        {
            return true;
        }
        return this.CanExecuteFunc(parameter);
    }

    public event EventHandler CanExecuteChanged;

    public void Execute(object parameter)
    {
        if (ExcuteAction == null)
        {
            return;
        }
        ExcuteAction(parameter);
    }

    //声明一个委托，当执行Execute方法时，执行ExcuteAction委托指向的方法。
    public Action<object> ExcuteAction { get; set; }

    public Func<object, bool> CanExecuteFunc { get; set; }
}