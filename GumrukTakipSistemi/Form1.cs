using GumrukTakipSistemi.Services;

namespace GumrukTakipSistemi
{
    public partial class Form1 : Form
    {
        IsService service = new IsService();
        public Form1()
        {
            InitializeComponent();


            cmbDurum.Items.Clear();
            cmbDurum.Items.Add("Bekliyor");
            cmbDurum.Items.Add("Devam Ediyor");
            cmbDurum.Items.Add("Tamamlandı");
            cmbDurum.SelectedIndex = 0;
            service.Yukle();
            // load data asynchronously after form loads to avoid blocking the UI
            this.Load += Form1_Load;
            cmbDurum.DataSource = Enum.GetValues(typeof(Durum));

            cmbIsTipi.DataSource = Enum.GetValues(typeof(IsTipi));
        }

        private async void btnEkle_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtFirmaAdi.Text))
            {
                MessageBox.Show("Firma adı boş olamaz!");
                return;
            }

            IsTakip yeniIs = new IsTakip
            {
                FirmaAdi = txtFirmaAdi.Text,
                IsTipi = (IsTipi)cmbIsTipi.SelectedItem,
                Aciklama = txtAciklama.Text
            };

            // Call async add to avoid freezing UI. Await so we update grid after persistence.
            await service.EkleAsync(yeniIs);

            dataGridView1.DataSource = null;
            dataGridView1.DataSource = await service.ListeleAsync();
        }

        private int seciliId = 0;

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            // Ignore header or invalid clicks
            if (e.RowIndex < 0)
                return;

            // Do not re-subscribe to the event here.

            var row = dataGridView1.Rows[e.RowIndex];

            // Safely read cells using indices (Id=0, FirmaAdi=1, IsTipi=2, Aciklama=3, Durum=4)
            cmbDurum.Text = row.Cells.Count > 4 ? row.Cells[4].Value?.ToString() : string.Empty;

            if (row.Cells.Count > 0 && int.TryParse(row.Cells[0].Value?.ToString(), out var idFromIndex))
            {
                seciliId = idFromIndex;
            }

            txtFirmaAdi.Text = row.Cells.Count > 1 ? row.Cells[1].Value?.ToString() : string.Empty;
            cmbIsTipi.SelectedItem = row.Cells.Count > 2 ? row.Cells[2].Value : null;
            txtAciklama.Text = row.Cells.Count > 3 ? row.Cells[3].Value?.ToString() : string.Empty;
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            dataGridView1.AutoGenerateColumns = true;
            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridView1.MultiSelect = false;

            if (e.RowIndex < 0) return;

            var row = dataGridView1.Rows[e.RowIndex];

            // Use indices to read values safely
            if (row.Cells.Count > 0 && int.TryParse(row.Cells[0].Value?.ToString(), out var id))
                seciliId = id;

            txtFirmaAdi.Text = row.Cells.Count > 1 ? row.Cells[1].Value?.ToString() : string.Empty;
            cmbIsTipi.SelectedItem = row.Cells.Count > 2 ? row.Cells[2].Value : null;
            txtAciklama.Text = row.Cells.Count > 3 ? row.Cells[3].Value?.ToString() : string.Empty;
            cmbDurum.Text = row.Cells.Count > 4 ? row.Cells[4].Value?.ToString() : string.Empty;
        }

        private async void btnSil_Click(object sender, EventArgs e)
        {
            if (seciliId == 0)
            {
                MessageBox.Show("Lütfen silinecek kaydı seçin!");
                return;
            }

            service.Sil(seciliId);
            dataGridView1.DataSource = null;
            dataGridView1.DataSource = await service.ListeleAsync();
        }

        private async void btnGuncelle_Click(object sender, EventArgs e)
        {
            // Use in-memory list for quick lookup and update
            var item = service.GetAll().FirstOrDefault(x => x.Id == seciliId);
            if (item == null)
            {
                MessageBox.Show("Kayıt bulunamadı!");
                return;
            }

            item.FirmaAdi = txtFirmaAdi.Text;
            item.Aciklama = txtAciklama.Text;

            if (Enum.TryParse(cmbDurum.Text, out Durum yeniDurum))
                item.Durum = yeniDurum;

            if (Enum.TryParse(cmbIsTipi.Text, out IsTipi yeniIsTipi))
                item.IsTipi = yeniIsTipi;

            // Persist update to JSON
            service.Update(item);

            // Refresh grid from DB if available, otherwise from in-memory list
            dataGridView1.DataSource = null;
            dataGridView1.DataSource = await service.ListeleAsync();
        }

        private void cmbDurum_SelectedIndexChanged(object sender, EventArgs e)
        {


        }

        private void txtAra_TextChanged(object sender, EventArgs e)
        {
            var all = service.GetAll();
            var liste = all
                .Where(x => x.FirmaAdi != null && x.FirmaAdi.ToLower().Contains(txtAra.Text.ToLower()))
                .ToList();

            dataGridView1.DataSource = null;
            dataGridView1.DataSource = liste;
            if (string.IsNullOrWhiteSpace(txtAra.Text))
            {
                dataGridView1.DataSource = service.GetAll();
                return;
            }

            var filtre = all
                .Where(x => x.FirmaAdi != null && x.FirmaAdi.ToLower().Contains(txtAra.Text.ToLower()))
                .ToList();

            dataGridView1.DataSource = filtre;
        }

        private async void Form1_Load(object sender, EventArgs e)
        {
            dataGridView1.DataSource = await service.ListeleAsync();
        }
    }
}
     


    



    








