using System.ComponentModel;
/// <summary>
/// NotifyPropertyObject类用于属性值更改通知。
/// </summary>
public class NotifyPropertyObject : INotifyPropertyChanged
{
    //此事件用于监听属性值。
    public event PropertyChangedEventHandler PropertyChanged;

    /// <summary>
    /// 此方法用于一直监听属性。
    /// </summary>
    /// <param name="name"></param>
    public void OnPropertyChanged(string name)
    {
        if (PropertyChanged != null)
        {
            PropertyChanged(this, new PropertyChangedEventArgs(name));
        }
    }
}