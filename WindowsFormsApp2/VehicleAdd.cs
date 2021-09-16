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
using System.Windows.Media.Imaging;

namespace WindowsFormsApp2
{
    public partial class VehicleAdd : Form
    {
        car_rent_dbEntities ctx = new car_rent_dbEntities();
        Brands query;
        byte[] imageData;
        public VehicleAdd()
        {
            InitializeComponent();
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            BrandChoose brandChoose = new BrandChoose();
            brandChoose.ShowDialog();
            if (brandChoose.DialogResult == DialogResult.OK)
            {
                query = (from brands in ctx.Brands 
                                where brands.code == brandChoose.brand_id 
                                select brands).First();
                label2.Text = query.name + " " + query.specifications;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Vehicles vehicle = new Vehicles();
            vehicle.brand_code = query.code;
            vehicle.reg_number = textBox1.Text;
            vehicle.body_num = textBox2.Text;
            vehicle.engine_num = textBox3.Text;
            vehicle.year = textBox4.Text;
            vehicle.vehicle_price = Convert.ToDouble(numericUpDown1.Value);
            vehicle.rent_price = Convert.ToDouble(numericUpDown2.Value);
            if (imageData != null) vehicle.image = imageData;

            ctx.Vehicles.Add(vehicle);
            ctx.SaveChanges();
            this.DialogResult = DialogResult.OK;
            Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            openFileDialog1.ShowDialog();
            if (openFileDialog1.FileName != "")
            {
                
                using (FileStream fs = new FileStream(openFileDialog1.FileName, FileMode.Open))
                {
                    pictureBox1.Image = Image.FromStream(fs);
                    imageData = new byte[fs.Length];
                    fs.Read(imageData, 0, imageData.Length);
                }
            }
        }

    }

}
