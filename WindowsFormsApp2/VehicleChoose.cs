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
    public partial class VehicleChoose : Form
    {
        public int car_id;
        car_rent_dbEntities ctx = new car_rent_dbEntities();
        public VehicleChoose()
        {
            InitializeComponent();
        }

        private void VehicleChoose_Load(object sender, EventArgs e)
        {
            var query = from vehicle in ctx.Vehicles
                        join br in ctx.Brands on vehicle.brand_code equals br.code
                        where vehicle.returned == true
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

        private void button1_Click(object sender, EventArgs e)
        {
            car_id = Convert.ToInt32(dataGridView1.CurrentRow.Cells[0].Value);
            DialogResult = DialogResult.OK;
            Close();
        }
    }
}
