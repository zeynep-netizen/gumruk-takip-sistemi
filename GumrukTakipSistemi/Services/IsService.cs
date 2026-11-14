using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace GumrukTakipSistemi.Services
{
    internal class IsService
    {
        private readonly string dosyaYolu = Path.Combine(Application.StartupPath, "veriler.json");
        private List<IsTakip> isler = new List<IsTakip>();
        private int sayac = 1;
       
        public IsService()
        {

            Yukle();
            if (isler.Any())
                sayac = isler.Max(x => x.Id) + 1;
        }

        private void Kaydet()
        {
            var json = JsonConvert.SerializeObject(isler, Formatting.Indented);
            File.WriteAllText(dosyaYolu, json);
        }

        public void Yukle()
        {
            if (!File.Exists(dosyaYolu))
            {
                isler = new List<IsTakip>();
                return;
            }

            var json = File.ReadAllText(dosyaYolu);
            isler = JsonConvert.DeserializeObject<List<IsTakip>>(json) ?? new List<IsTakip>();
        }


        
        // Add a short connect timeout to avoid long hangs when SQL Server is unavailable
        private string connectionString = @"Server=(localdb)\MSSQLLocalDB;Database=GumrukDB;Trusted_Connection=True;Connect Timeout=5;";

        // Async add: performs DB insert with timeout and saves locally. Keeps UI responsive.
        public async Task EkleAsync(IsTakip isTakip)
        {
            // Prepare local object properties immediately so UI can update after await
            isTakip.Id = sayac++;
            isTakip.BaslangicTarihi = DateTime.Now;
            isTakip.Durum = Durum.Bekliyor;
            isTakip.Tarih = DateTime.Now;

            // Try to insert into DB but don't block UI for long
            try
            {
                using (var conn = new SqlConnection(connectionString))
                {
                    using (var cmd = conn.CreateCommand())
                    {
                        cmd.CommandText = "INSERT INTO Islemler (FirmaAdi, IsTipi, Aciklama, Durum) VALUES (@FirmaAdi, @IsTipi, @Aciklama, @Durum)";
                        cmd.Parameters.AddWithValue("@FirmaAdi", isTakip.FirmaAdi ?? string.Empty);
                        cmd.Parameters.AddWithValue("@IsTipi", isTakip.IsTipi.ToString());
                        cmd.Parameters.AddWithValue("@Aciklama", isTakip.Aciklama ?? string.Empty);
                        cmd.Parameters.AddWithValue("@Durum", isTakip.Durum.ToString());
                        cmd.CommandTimeout = 5; // seconds

                        await conn.OpenAsync().ConfigureAwait(false);
                        await cmd.ExecuteNonQueryAsync().ConfigureAwait(false);
                    }
                }
            }
            catch (Exception)
            {
                // If DB insert fails, swallow exception so app doesn't freeze; data will still be saved locally.
                // Consider logging the exception to a log file in future.
            }

            // Add to local list and persist to JSON
            lock (isler)
            {
                isler.Add(isTakip);
                Kaydet();
            }
        }

        

        public void Sil(int id)
        {
            var isItem = isler.FirstOrDefault(x => x.Id == id);
            if (isItem != null)
            {
                isler.Remove(isItem);
                Kaydet();
            }
        }

        // Asynchronously read all records from the database and return as a list.
        // Falls back to the in-memory list if DB access fails.
        public async Task<List<IsTakip>> ListeleAsync()
        {
            List<IsTakip> liste = new List<IsTakip>();

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    await conn.OpenAsync().ConfigureAwait(false);

                    string query = "SELECT * FROM Islemler";
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        using (SqlDataReader reader = await cmd.ExecuteReaderAsync().ConfigureAwait(false))
                        {
                            while (await reader.ReadAsync().ConfigureAwait(false))
                            {
                                IsTakip isTakip = new IsTakip
                                {
                                    Id = reader["Id"] != DBNull.Value ? Convert.ToInt32(reader["Id"]) : 0,
                                    FirmaAdi = reader["FirmaAdi"]?.ToString() ?? string.Empty,
                                    Aciklama = reader["Aciklama"]?.ToString() ?? string.Empty,
                                    IsTipi = Enum.TryParse(reader["IsTipi"]?.ToString(), out IsTipi it) ? it : IsTipi.Ihracat,
                                    Durum = Enum.TryParse(reader["Durum"]?.ToString(), out Durum d) ? d : Durum.Bekliyor
                                };

                                liste.Add(isTakip);
                            }
                        }
                    }
                }

                // Update in-memory list to reflect DB
                lock (isler)
                {
                    isler = liste.ToList();
                }

                return liste;
            }
            catch (Exception)
            {
                // If DB access fails, return the in-memory list to avoid throwing to UI
                return isler;
            }
        }

        // Return a copy of the in-memory list (fast, non-blocking)
        public List<IsTakip> GetAll()
        {
            lock (isler)
            {
                return isler.ToList();
            }
        }

        // Update existing item in-memory and persist to JSON. Does not update DB table.
        public void Update(IsTakip updated)
        {
            lock (isler)
            {
                var idx = isler.FindIndex(x => x.Id == updated.Id);
                if (idx >= 0)
                {
                    isler[idx] = updated;
                    Kaydet();
                }
            }
        }

       
    }


}
