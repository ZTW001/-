using MicroMvvm;
using System.ComponentModel;
using System.Windows.Input;

namespace Example4
{
    public class SongViewModel : ObservableObject
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

        #region 命令
        void UpdateArtistNameExecute()
        {
            this.ArtistName = "中孝介";
        }

        bool CanUpdateArtistNameExecute()
        {
            return true;
        }

        public ICommand UpdateArtistName { get { return new RelayCommand(UpdateArtistNameExecute, CanUpdateArtistNameExecute); } }

        #endregion
    }
}
