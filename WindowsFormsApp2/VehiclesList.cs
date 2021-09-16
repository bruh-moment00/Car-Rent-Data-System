using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Entity;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp2
{
    public partial class VehiclesList : Form
    {
        car_rent_dbEntities ctx;
        public VehiclesList()
        {
            InitializeComponent();
            ctx = new car_rent_dbEntities();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            VehicleAdd vehicleAdd = new VehicleAdd();
            vehicleAdd.ShowDialog();
            if (vehicleAdd.DialogResult == DialogResult.OK)
            {
                Refresh_Table();
            }
        }

        private void VehiclesList_Load(object sender, EventArgs e)
        {
            Refresh_Table();
        }

        private void dataGridView1_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            pictureBox1.Image = null;
            int ID = Convert.ToInt32(dataGridView1.Rows[dataGridView1.CurrentCell.RowIndex].Cells["Код"].Value);
            try
            {
                Vehicles v = ctx.Vehicles.FirstOrDefault(c => c.vehicle_code == ID);
                byte[] data = v.image;
                MemoryStream ms = new MemoryStream(data);
                pictureBox1.Image = Image.FromStream(ms);
                pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
            }
            catch (Exception)
            {

            }

        }

        private void Refresh_Table()
        {
            var query = from vehicle in ctx.Vehicles
                        join br in ctx.Brands on vehicle.brand_code equals br.code
                        select new
                        {
                            Код = vehicle.vehicle_code,
                            Название = br.name,
                            Характеристки = br.specifications,
                            Регистрационный_номер = vehicle.reg_number,
                            Год = vehicle.year,
                            Цена_аренды = vehicle.rent_price,
                            image = vehicle.image
                        };
            dataGridView1.DataSource = query.ToList();
            dataGridView1.Columns["image"].Visible = false;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            int code = Convert.ToInt32(dataGridView1.CurrentRow.Cells["Код"].Value);
            VehicleEdit vehicleEdit = new VehicleEdit(code);
            vehicleEdit.ShowDialog();
            if (vehicleEdit.DialogResult == DialogResult.OK)
            {
                Refresh_Table();
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Вы уверены?", "Удаление", MessageBoxButtons.YesNo);
            if (result == DialogResult.Yes)
            {
                int id = Convert.ToInt32(dataGridView1.CurrentRow.Cells["Код"].Value);
                Vehicles vehicle1 = (from vehicle in ctx.Vehicles 
                                     where vehicle.vehicle_code == id
                                     select vehicle).First();
                ctx.Vehicles.Remove(vehicle1);
                ctx.SaveChanges();
                Refresh_Table();
            }
            else
                return;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            RentalsList rentalsList = new RentalsList();
            rentalsList.Owner = this;
            rentalsList.Show();
            Hide();
        }
    }
}
