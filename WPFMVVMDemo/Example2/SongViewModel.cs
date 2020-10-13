using System.ComponentModel;

namespace Example2
{
    public class SongViewModel : INotifyPropertyChanged
    {
        public SongViewModel()
        {
            _song = new Song() { ArtistName = "陈奕迅", SongTitle = "十年" };
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
        #endregion

        #region INotifyPropertyChanged属性
        public event PropertyChangedEventHandler PropertyChanged;
        #endregion

        #region 方法
        private void RaisePropertyChanged(string propertyName)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if(handler != null)
            {
                handler(this, new PropertyChangedEventArgs(propertyName));
            }
        }
        #endregion
    }
}
