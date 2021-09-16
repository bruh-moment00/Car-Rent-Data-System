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
    public partial class VehicleEdit : Form
    {
        car_rent_dbEntities ctx = new car_rent_dbEntities();
        Brands query;
        byte[] imageData;
        int code;
        Vehicles editing_vehicle;

        public VehicleEdit(int code)
        {
            InitializeComponent();
            this.code = code;
        }
        private void VehicleEdit_Load(object sender, EventArgs e)
        {
            editing_vehicle = (from vehicle in ctx.Vehicles where vehicle.vehicle_code == code select vehicle).First();

            query = (from brands in ctx.Brands
                     where brands.code == editing_vehicle.brand_code
                     select brands).First();
            label2.Text = query.name + " " + query.specifications;

            textBox1.Text = editing_vehicle.reg_number;
            textBox2.Text = editing_vehicle.body_num;
            textBox3.Text = editing_vehicle.engine_num;
            textBox4.Text = editing_vehicle.year;
            numericUpDown1.Value = Convert.ToDecimal(editing_vehicle.vehicle_price);
            numericUpDown2.Value = Convert.ToDecimal(editing_vehicle.vehicle_price);

            try
            {
                imageData = ctx.Vehicles.Find(code).image;
                MemoryStream ms = new MemoryStream(imageData);
                pictureBox1.Image = Image.FromStream(ms);
            } catch (Exception) { }
                
            
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
            editing_vehicle.reg_number = textBox1.Text;
            editing_vehicle.body_num = textBox2.Text;
            editing_vehicle.engine_num = textBox3.Text;
            editing_vehicle.year = textBox4.Text;
            editing_vehicle.vehicle_price = Convert.ToDouble(numericUpDown1.Value);
            editing_vehicle.vehicle_price = Convert.ToDouble(numericUpDown2.Value);
            if (imageData != null) editing_vehicle.image = imageData;
            ctx.SaveChanges();
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
                    imageData = null;
                    pictureBox1.Image = Image.FromStream(fs);
                    pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
                    imageData = new byte[fs.Length];
                    fs.Read(imageData, 0, imageData.Length);
                }
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
