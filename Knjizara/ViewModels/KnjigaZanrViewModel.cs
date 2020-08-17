using Knjizara.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Knjizara.ViewModels
{
    public class KnjigaZanrViewModel
    {
        public Knjiga Knjiga { get; set; }
        public List<Zanr> SviZanrovi { get; set; }
        public int IdSelektovanogZanra { get; set; }

    }
}