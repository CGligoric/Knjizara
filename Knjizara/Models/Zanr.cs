using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace Knjizara.Models
{
    public class Zanr
    {
        public int Id { get; set; }
        public string Naziv { get; set; }

        public Zanr()
        {

        }

        public Zanr(int id, string naziv)
        {
            Id = id;
            Naziv = naziv;
        }
    }
}