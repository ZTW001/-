using MicroMvvm;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace Example6
{
    public class AlbumViewModel
    {
        #region 字段
        ObservableCollection<SongViewModel> _songs = new ObservableCollection<SongViewModel>();
        #endregion

        #region 属性
        public ObservableCollection<SongViewModel> songs
        {
            get { return _songs; }
            set { _songs = value; }
        }
        #endregion

        public AlbumViewModel()
        {
            _songs.Add(new SongViewModel() { ArtistName = "陈奕迅", SongTitle = "十年" });
            _songs.Add(new SongViewModel() { ArtistName = "周杰伦", SongTitle = "发如雪" });
            _songs.Add(new SongViewModel() { ArtistName = "蔡依林", SongTitle = "日不落" });
        }

        #region 命令

        void AddAlbumArtistExecute()
        {
            _songs.Add(new SongViewModel { ArtistName = "阿桑", SongTitle = "一直很安静" });
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
