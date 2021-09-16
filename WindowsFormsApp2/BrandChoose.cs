using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Entity;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp2
{
    public partial class BrandChoose : Form
    {
        car_rent_dbEntities ctx;
        public int brand_id;
        public BrandChoose()
        {
            InitializeComponent();
            ctx = new car_rent_dbEntities();
        }

        private void BrandChoose_Load(object sender, EventArgs e)
        {
            ctx.Brands.Load();
            dataGridView1.DataSource = ctx.Brands.Local.ToBindingList();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            brand_id = Convert.ToInt32(dataGridView1.CurrentRow.Cells[0].Value);
            DialogResult = DialogResult.OK;
            this.Close();
        }
    }
}
