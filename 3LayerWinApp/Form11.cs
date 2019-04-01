using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data;
using System.Data.SqlClient;
using BLL;
using BLL.Services;

namespace _3LayerWinApp
{
    public partial class Form1 : Form
    {
        string CS = System.Configuration.ConfigurationManager.ConnectionStrings["dbConnection"].ConnectionString;
                // Создаем объект Dataset
        private DataSet dataSet = new DataSet();
        SqlDataAdapter adapterPhones;
        SqlDataAdapter adapterOrders;
        SqlDataAdapter adapterPhonesOrders;
        SqlCommandBuilder commandBuilderPhones;

        public Form1()
        {
            InitializeComponent();

                // Создаем объект DataAdapter
                adapterPhones = new SqlDataAdapter("SELECT * FROM Phone", CS);
            adapterOrders = new SqlDataAdapter("SELECT * FROM [Order]", CS);
            adapterPhonesOrders = new SqlDataAdapter("SELECT * FROM [PhoneOrder]", CS);
            // создаем объект SqlCommandBuilder
            commandBuilderPhones = new SqlCommandBuilder(adapterPhones);

            loadData();

             // установка отношений между таблицами
            dataSet.Relations.Add("PhonesOrders",  dataSet.Tables["Orders"].Columns["Id"], dataSet.Tables["PhonesOrders"].Columns["OrderId"]);
       }

        private void loadData() {
            using (SqlConnection connection = new SqlConnection(CS))
            {
                connection.Open();
                // Заполняем Dataset
                adapterPhones.Fill(dataSet, "Phones");

                adapterOrders.Fill(dataSet, "Orders");
                adapterPhonesOrders.Fill(dataSet, "PhonesOrders");

            }
            // Отображаем данные
            gvPhonesList.DataSource = dataSet.Tables["Phones"];
            gvOrders.DataSource = dataSet.Tables["Orders"];

            // Заполнить комбобокс 
            cbPhones.DataSource =            dataSet.Tables["Phones"];
            cbPhones.DisplayMember = "Name";
            cbPhones.ValueMember = "Id";

        }

        private void loadPhonesTo_gvPhonesList()
        {
            using (SqlConnection connection = new SqlConnection(CS))
            {
                connection.Open();
                SqlCommand selectCommand = new SqlCommand("SELECT * FROM Phone", connection);
                SqlDataAdapter adapter = new SqlDataAdapter();
                adapter.SelectCommand = selectCommand;
                DataSet myDataSet = new DataSet();
                adapter.Fill(myDataSet, "Ph");
                gvPhonesList.DataSource = myDataSet.Tables["Ph"];
            }
        }

        private void btnNewPhone_Click(object sender, EventArgs e)
        {
                //  обновление только одной таблицы
                adapterPhones.Update(dataSet, "Phones");
            using (SqlConnection connection = new SqlConnection(CS))
            {
                connection.Open();

                // заново получаем данные из бд
                // очищаем полностью DataSet
                dataSet.Tables["Phones"].Clear();
                // перезагружаем данные
                adapterPhones.Fill(dataSet, "Phones");
            }
        }

        private void gvOrders_CellClick(object sender, DataGridViewCellEventArgs e)
        {
        }

        private void gvOrders_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            var row = dataSet.Tables["Orders"].Rows[e.RowIndex];
            DataRow[] orderedPhones =  row.GetChildRows(dataSet.Relations["PhonesOrders"]);
            string text = "orderedPhonesIds: ";
            foreach (DataRow r in orderedPhones)
            {
                text+= r["PhoneId"]+",";
            }
            text = text.TrimEnd(',');
            MessageBox.Show(text);

        }

        private void button1_Click(object sender, EventArgs e)
        {
            OrderModel order = new OrderModel()
            {
                Address = tbAddress.Text,
                Customer = tbCustomer.Text,
                OrderedPhonesIds = new List<int> { (int)cbPhones.SelectedValue }
            };
            OrderService service = new OrderService();
            bool result = service.MakeOrder(order);
            if (result) {
                MessageBox.Show("Success");
                dataSet.Tables["Orders"].Clear();
                dataSet.Tables["PhonesOrders"].Clear();
                // перезагружаем данные
                adapterOrders.Fill(dataSet, "Orders");
                adapterPhonesOrders.Fill(dataSet, "PhonesOrders");
            }
            else MessageBox.Show("Failed");

        }
    }
}
