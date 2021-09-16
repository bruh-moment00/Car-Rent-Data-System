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
    public partial class OrderForm : Form
    {
        car_rent_dbEntities ctx = new car_rent_dbEntities();
        Clients client = new Clients();
        Vehicles car = new Vehicles();
        int days;
        public OrderForm()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            VehicleChoose vehicleChoose = new VehicleChoose();
            vehicleChoose.ShowDialog();
            if (vehicleChoose.DialogResult == DialogResult.OK)
            {
                car = ctx.Vehicles.Where(c => c.vehicle_code == vehicleChoose.car_id).FirstOrDefault();
                var query = (from br in ctx.Brands where br.code == car.brand_code select br).First();
                label4.Text = query.name + " " + query.specifications;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            ClientChoose clientChoose = new ClientChoose();
            clientChoose.ShowDialog();
            if (clientChoose.DialogResult == DialogResult.OK)
            {
                client = (from q in ctx.Clients where q.code == clientChoose.client_id select q).First();
                label6.Text = client.full_name;
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if(car != null)
            {
                days = (dateTimePicker2.Value - dateTimePicker1.Value).Days + 1;
                double price = days * car.rent_price;
                
                if (checkBox1.Checked)
                    price += 500;
                if (checkBox2.Checked)
                    price += 3000;
                if (checkBox3.Checked)
                    price += 1000;

                textBox1.Text = price.ToString();
            }
            else
            {
                return;
            }

        }

        private void button4_Click(object sender, EventArgs e)
        {
            if(client != null && car !=null &&textBox1.Text != "" && textBox2.Text != "")
            {
                Rentals new_rent = new Rentals();
                new_rent.issue_date = dateTimePicker1.Value;
                new_rent.return_date = dateTimePicker2.Value;
                new_rent.vehicle_code = car.vehicle_code;
                new_rent.client_code = client.code;

                if (checkBox1.Checked) new_rent.option_code1 = 1;
                if (checkBox2.Checked) new_rent.option_code2 = 2;
                if (checkBox3.Checked) new_rent.option_code3 = 3;

                new_rent.rental_period = days;
                new_rent.rental_cost = Convert.ToDouble(textBox1.Text);
                new_rent.payment_mark = Convert.ToDouble(textBox2.Text);

                new_rent.employee_code = 1;

                car.returned = false;
                ctx.Rentals.Add(new_rent);
                ctx.SaveChanges();

                DialogResult = DialogResult.OK;
                Close();
            }
            else
            {
                MessageBox.Show("Введите данные!");
            }

        }

        private void OrderForm_Load(object sender, EventArgs e)
        {
            label6.Text = "";
            label4.Text = "";
        }
    }
}
