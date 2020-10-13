
namespace Example7
{
    public class Song 
    {
        #region 字段
        string _artistName;
        string _songTitle;
        #endregion

        #region 属性
        public string ArtistName
        {
            get { return _artistName; }
            set { _artistName = value; }
        }

        public string SongTitle
        {
            get { return _songTitle; }
            set { _songTitle = value; }
        }
        #endregion
    }
}
