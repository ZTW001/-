using MicroMvvm;
using System.ComponentModel;
using System.Windows.Input;

namespace Example7
{
public class SongViewModel : ObservableObject
{
    public SongViewModel()
    {
        _song = new Song() { ArtistName = "Unknow", SongTitle = "Unknow" };
    }

    #region 字段
    Song _song;
    #endregion

    #region 属性
    public Song song
    {
        get { return song; }
        set { song = value; }
    }

    public string ArtistName
    {
        get { return _song.ArtistName; }
        set 
        { 
            _song.ArtistName = value;
            RaisePropertyChanged("ArtistName");
        }
    }

    public string SongTitle
    {
        get { return _song.SongTitle; }
        set
        {
            _song.SongTitle = value;
            RaisePropertyChanged("SongTitle");
        }
    }
    #endregion
}
}
