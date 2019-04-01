using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Windows.Forms;
using BLL;
using BLL.Interfaces;
using BLL.Models;
using BLL.Services;
using DAL;

namespace _3LayerWinApp
{
    public partial class Form1 : Form
    {
        List<VkladMod> allVklads;
        List<ClientMod> allClients;
        List<ProgMod> allProgs;

        IVkladService service;
        IReportService reportservice;
        IDbCrud dbOperations;

        public Form1(IDbCrud crudDb, IVkladService vkladservice, IReportService reportservice)
        {
            service = vkladservice;
            this.reportservice = reportservice;
            dbOperations = crudDb;
            InitializeComponent();
            loadData();
        }
        private void RefreshgvVklads()
        {

            dataGridView1.DataSource = dbOperations.GetAllVklads();
            dataGridView1.Refresh();
        }
        //загрузка данных в таблицы
        private void loadData()
        {
            loadVklads();
            loadClients();
            loadProgs();
        }

        private void loadVklads()
        {

            allVklads = dbOperations.GetAllVklads();
            dataGridView1.DataSource = allVklads;
           
        }
        private void loadProgs()
        {

            allProgs = dbOperations.GetProgs();
            dataGridView1.DataSource = allProgs;

        }
        private void loadClients()
        {

            allClients = dbOperations.GetAllClients();
            dataGridView1.DataSource = allClients;

        }



        private void btnNewPhone_Click(object sender, EventArgs e)
        {
            AddVkladForm f = new AddVkladForm(allClients, allProgs);
            DialogResult result = f.ShowDialog(this);

            if (result == DialogResult.Cancel)
                return;
            VkladMod vklad = new VkladMod();

            vklad.Balance = (int)f.comboBoxClient.SelectedValue;
            vklad.DateOpen = DateTime.Now;

            dbOperations.CreateVklad(vklad);
            RefreshgvVklads();

            //MessageBox.Show("Новый объект добавлен");
        }



      
      

       
       

        private int getSelectedRow(DataGridView dataGridView)
        {
            int index = -1;
            if (dataGridView.SelectedRows.Count > 0 || dataGridView.SelectedCells.Count == 1)
            {
                if (dataGridView.SelectedRows.Count > 0)
                    index = dataGridView.SelectedRows[0].Index;
                else index = dataGridView.SelectedCells[0].RowIndex;
            }
            return index;
        }

 
        //выполнить ХП
        private void ExecuteSP()
        {
            ReportServices rs = new ReportServices();
            dataGridView1.DataSource = rs.ExecuteSP(dateTimePicker1.Value);
        }

     

        private void button2_Click(object sender, EventArgs e)
        {
            AddVkladForm f = new AddVkladForm(allClients, allProgs);
            DialogResult result = f.ShowDialog(this);

            if (result == DialogResult.Cancel)
                return;
            VkladMod vklad = new VkladMod();

            vklad.Balance = (int)f.comboBoxClient.SelectedValue;
            vklad.DateOpen = DateTime.Now;

            dbOperations.CreateVklad(vklad);
            RefreshgvVklads();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0 || dataGridView1.SelectedCells.Count == 1)
            {
                VkladMod c;
                c = (VkladMod)dataGridView1.CurrentRow.DataBoundItem;
                if (c != null)
                {
                    
                    AddVkladForm f = new AddVkladForm(allClients, allProgs);
                    DialogResult result = f.ShowDialog(this);

                    if (result == DialogResult.Cancel)
                        return;

                    //c.Cost = f.numericUpDown1.Value;
                    //ph.Name = f.textBox1.Text;
                    //ph.ManufacturerId = (int)f.comboBoxManuf.SelectedValue;

                   
            
                    c.Balance = int.Parse(f.textBox1.Text);
                    c.DateOpen = DateTime.Now;

                    dbOperations.UpdateVklad(c);
                    RefreshgvVklads();
                }
            }
            else
                MessageBox.Show("Ни один объект не выбран!");

        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (hasSelectedRow(dataGridView1))
            {
                dbOperations.DeleteVklad(((VkladMod)dataGridView1.CurrentRow.DataBoundItem).Id);
                RefreshgvVklads();
            }
            else
                MessageBox.Show("Ни один объект не выбран!");
        }
        private bool hasSelectedRow(DataGridView dataGridView)
        {
            return (dataGridView.SelectedRows.Count > 0 || dataGridView.SelectedCells.Count == 1);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ExecuteSP();
        }
    }
}
