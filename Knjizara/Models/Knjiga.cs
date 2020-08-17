using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Knjizara.Models
{
    public class Knjiga
    {
        public int Id { get; set; }
        public string Naslov { get; set; }
        public int Cena { get; set; }
        public Zanr Zanr { get; set; }

        public Knjiga() { }
        public Knjiga(int id, string naslov, int cena, Zanr z)
        {
            Id = id;
            Naslov = naslov;
            Cena = cena;
            Zanr = z;
        }

        public string ToFileString()
        {
            return String.Format("{0},{1},{2},{3}", Id, Naslov, Cena, Zanr.Naziv);
        }
    }
}