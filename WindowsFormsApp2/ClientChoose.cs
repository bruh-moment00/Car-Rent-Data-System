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
    public partial class ClientChoose : Form
    {
        car_rent_dbEntities ctx = new car_rent_dbEntities();
        public int client_id;
        public ClientChoose()
        {
            InitializeComponent();
        }

        private void ClientChoose_Load(object sender, EventArgs e)
        {
            ctx.Clients.Load();
            dataGridView1.DataSource = ctx.Clients.Local.ToBindingList();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            client_id = Convert.ToInt32(dataGridView1.CurrentRow.Cells[0].Value);
            DialogResult = DialogResult.OK;
            Close();
        }
    }
}
