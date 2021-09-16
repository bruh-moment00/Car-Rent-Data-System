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
    public partial class RentalsList : Form
    {
        car_rent_dbEntities ctx;
        public RentalsList()
        {
            InitializeComponent();
            ctx = new car_rent_dbEntities();
        }

        private void RentalsList_Load(object sender, EventArgs e)
        {
            Refresh_Table();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            OrderForm orderForm = new OrderForm();
            orderForm.ShowDialog();
            if(orderForm.DialogResult == DialogResult.OK)
            {
                Refresh_Table();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            DialogResult yesno = MessageBox.Show("Машина возвращена и заказ оплачен?", "Возврат", MessageBoxButtons.YesNo);
            if (yesno == DialogResult.Yes)
            {
                int v_code = Convert.ToInt32(dataGridView1.Rows[dataGridView1.CurrentCell.RowIndex].Cells["Код_авто"].Value);
                int r_code = Convert.ToInt32(dataGridView1.Rows[dataGridView1.CurrentCell.RowIndex].Cells["Код"].Value);

                Vehicles returned_car = (from c in ctx.Vehicles
                                         where c.vehicle_code == v_code
                                         select c).First();
                returned_car.returned = true;
                Rentals rent = (from r in ctx.Rentals
                                where r.code == r_code
                                select r).First();
                rent.payment_mark = rent.rental_cost;

                ctx.SaveChanges();
                Refresh_Table();
            }
            else
            {
                return;
            }
        }

        private void Refresh_Table()
        {
            var query = from rental in ctx.RentView
                        select new
                        {
                            Код = rental.code,
                            Марка = rental.name,
                            Регистрационный_номер = rental.reg_number,
                            Дата_выдачи = rental.issue_date,
                            Дата_возврата = rental.return_date,
                            Детское_кресло = rental.option_code1,
                            Личный_авто = rental.option_code2,
                            Доставка = rental.option_code3,
                            ФИО_клиента = rental.full_name,
                            Срок_аренды = rental.rental_period,
                            Стоимость_аренды = rental.rental_cost,
                            Сколько_внесено = rental.payment_mark,
                            Код_авто = rental.vehicle_code
                        };
            //dataGridView1.Columns["Код_авто"].Visible = false;
            dataGridView1.DataSource = query.ToList();
        }

        private void RentalsList_FormClosing(object sender, FormClosingEventArgs e)
        {
            Owner.Show();
        }
    }
}
