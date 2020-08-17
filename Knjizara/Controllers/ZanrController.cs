using Knjizara.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Knjizara.Controllers
{
    public class ZanrController : Controller
    {
        string connectionString = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog = Knjizara; Integrated Security = True";


        // GET: Zanr
        public ActionResult Index()
        {
            string query = "SELECT * FROM zanr"; 
            
            DataTable dt = new DataTable();
            DataSet ds = new DataSet();

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = query;

                    SqlDataAdapter dadapter = new SqlDataAdapter();
                    dadapter.SelectCommand = cmd;                   

                    dadapter.Fill(ds, "ProductCategory"); 
                    dt = ds.Tables["ProductCategory"];    
                    conn.Close();                  
                }
            }
            List<Zanr> sviZanrovi = new List<Zanr>();

            foreach (DataRow dataRow in dt.Rows)
            {
                int zanrId = int.Parse(dataRow["zanrId"].ToString());
                string zanrNaziv = dataRow["zanrNaziv"].ToString();

                sviZanrovi.Add(new Zanr() { Id = zanrId, Naziv = zanrNaziv });
            }

            return View(sviZanrovi);
        }
    }
}