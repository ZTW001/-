using Example.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Example.Views
{
    public class SongViewModel
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
            set { _song.ArtistName = value; }
        }
        #endregion
    }
}