using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace MasterBarang005
{
    public partial class MasterBarang005 : Form
    {
        SqlConnection con = new SqlConnection(@" data source = DESKTOP-H0SV9CB\SQLEXPRESS; initial catalog = QuizMahasiswa;Integrated Security=True; ");
        public MasterBarang005()
        {
            InitializeComponent();
        }
        DataClasses1DataContext db = new DataClasses1DataContext();
        private void btnSave_Click(object sender, EventArgs e)
        {
            string nama_barang = txtNamaBarang.Text;
            int harga = int.Parse(txtHarga.Text);
            int stock = int.Parse(txtStock.Text);
            string nama_supplier = cbNamaSupp.Text;
            var data = new tbl_barang
            {
                nama_barang = nama_barang,
                harga = harga,
                stok = stock,
                nama_supplier = nama_supplier,
            };
            db.tbl_barangs.InsertOnSubmit(data);
            db.SubmitChanges();
            MessageBox.Show("Save Successfully");
            txtNamaBarang.Clear();
            txtHarga.Clear();
            txtStock.Clear();
            cbNamaSupp.Items.Clear();
            LoadData();
        }

        void LoadData()
        {
            var st = from tb in db.tbl_barangs select tb;
            dataGridView1.DataSource = st;
        }

        private void MasterBarang005_Load(object sender, EventArgs e)
        {
            con.Open();
            SqlDataAdapter sda = new SqlDataAdapter("select isnull(max (cast (ID as int)),0) +1 from TB_tbl_barang", con);
            DataTable dt = new DataTable();

            LoadData();
        }
    }
}
