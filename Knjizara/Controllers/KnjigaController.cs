using Knjizara.Models;
using Knjizara.ViewModels;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web.Mvc;

namespace Knjizara.Controllers
{
    public class KnjigaController : Controller
    {
        string connectionString = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog = Knjizara; Integrated Security = True";

        // GET: Knjiga
        public ActionResult Index()
        {
            string query = "SELECT * FROM knjiga JOIN zanr ON zanr.id = zanr_id";
            query += "SELECT SCOPE_IDENTITY()";

            DataTable dt = new DataTable(); // Podaci knjiga
            DataSet ds = new DataSet();

            DataTable dt1 = new DataTable();    // Podaci zanrova
            DataSet ds1 = new DataSet();

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = query;

                    SqlDataAdapter dataAdapter = new SqlDataAdapter();
                    dataAdapter.SelectCommand = cmd;

                    dataAdapter.Fill(ds, "knjiga");
                    dt = ds.Tables["knjiga"];

                    dataAdapter.Fill(ds1, "zanr");
                    dt1 = ds.Tables["zanr"];

                    conn.Close();
                }
            }

            List<Knjiga> sveKnjige = new List<Knjiga>();
            List<Zanr> sviZanrovi = new List<Zanr>();

            // Zanrovi
            foreach (DataRow dataRow in dt1.Rows)
            {
                int zanrId = int.Parse(dataRow["Id"].ToString());
                string zanrNaslov = dataRow["naziv"].ToString();

                sviZanrovi.Add(new Zanr(zanrId, zanrNaslov));
            }

            // Knjige
            foreach (DataRow dataRow in dt.Rows)
            {
                int knjigaId = int.Parse(dataRow["Id"].ToString());
                string knjiganaslov = dataRow["naslov"].ToString();
                int knjigaCena = int.Parse(dataRow["cena"].ToString());
                int zanrId = int.Parse(dataRow["zanr_id"].ToString());

                foreach (Zanr zanr in sviZanrovi)
                {
                    if (zanrId == zanr.Id)
                    {
                        sveKnjige.Add(new Knjiga(knjigaId, knjiganaslov, knjigaCena, zanr));
                    }
                }
            }
            return View(sveKnjige);
        }

        // GET: Knjiga/Create
        public ActionResult Create()
        {
            KnjigaZanrViewModel knjigaZanrViewModel = new KnjigaZanrViewModel();

            string query = "SELECT * FROM zanr";

            DataTable dt = new DataTable();
            DataSet ds = new DataSet();

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = query;

                    SqlDataAdapter dataAdapter = new SqlDataAdapter();
                    dataAdapter.SelectCommand = cmd;

                    dataAdapter.Fill(ds, "zanr");
                    dt = ds.Tables["zanr"];

                    conn.Close();
                }
            }

            List<Zanr> sviZanrovi = new List<Zanr>();

            foreach (DataRow dataRow in dt.Rows)
            {
                int zanrId = int.Parse(dataRow["Id"].ToString());
                string zanrNaziv = dataRow["Naziv"].ToString();

                sviZanrovi.Add(new Zanr(zanrId, zanrNaziv));
            }

            knjigaZanrViewModel.SviZanrovi = sviZanrovi;
            return View(knjigaZanrViewModel);
        }

        // POST: Knjiga/Create
        [HttpPost]
        public ActionResult Create(Knjiga knjiga)
        {
            string query = "INSERT INTO knjiga VALUES ('" + knjiga.Naslov + "'," + knjiga.Cena + "," + knjiga.Zanr.Id + " );";
            query += " SELECT SCOPE_IDENTITY()";

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = query;
                    conn.Open();
                    var a = cmd.ExecuteScalar();
                    conn.Close();
                }
            }

            return RedirectToAction("Index");
        }

        // GET: Knjiga/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        [HttpPost]
        // POST: Knjiga/Edit
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                string query = "UPDATE knjiga SET (naslov=" + collection[1] + ",cena=" + collection[2] + ",zanr_id=" + collection[3] + ") WHERE id=" + id;
                query += " SELECT SCOPE_IDENTITY()";

                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    using (SqlCommand cmd = conn.CreateCommand())
                    {
                        cmd.CommandText = query;
                        conn.Open();
                        var a = cmd.ExecuteScalar();
                        conn.Close();
                    }
                }

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Knjiga/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: knjiga/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                string query = "DELETE FROM knjiga WHERE id = " + id;
                query += " SELECT SCOPE_IDENTITY()";

                using(SqlConnection conn = new SqlConnection(connectionString))
                {
                    using (SqlCommand cmd = conn.CreateCommand())
                    {
                        cmd.CommandText = query;
                        conn.Open();
                        var a = cmd.ExecuteScalar();
                        conn.Close();
                    }
                }

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}