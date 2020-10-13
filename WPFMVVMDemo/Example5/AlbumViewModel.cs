using MicroMvvm;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace Example5
{
    public class AlbumViewModel
    {
        #region 字段
        ObservableCollection<Song> _songs = new ObservableCollection<Song>();
        #endregion

        #region 属性
        public ObservableCollection<Song> songs
        {
            get { return _songs; }
            set { _songs = value; }
        }
        #endregion

        public AlbumViewModel()
        {
            _songs.Add(new Song() { ArtistName = "陈奕迅", SongTitle = "十年" });
            _songs.Add(new Song() { ArtistName = "周杰伦", SongTitle = "发如雪" });
            _songs.Add(new Song() { ArtistName = "蔡依林", SongTitle = "日不落" });
        }

        #region 命令

        void AddAlbumArtistExecute()
        {
            _songs.Add(new Song { ArtistName = "阿桑", SongTitle = "一直很安静" });
        }

        bool CanAddAlbumArtistExecute()
        {
            return true;
        }

        void UpdateAlbumArtistsExecute()
        {

            foreach (var song in _songs)
            {
                song.ArtistName = "Unknow";
            }
        }

        bool CanUpdateAlbumArtistsExecute()
        {
            return true;
        }

        public ICommand AddAlbumArtist { get { return new RelayCommand(AddAlbumArtistExecute, CanAddAlbumArtistExecute); } }

        public ICommand UpdateAlbumArtists { get { return new RelayCommand(UpdateAlbumArtistsExecute, CanUpdateAlbumArtistsExecute); } }

        #endregion
    }
}
