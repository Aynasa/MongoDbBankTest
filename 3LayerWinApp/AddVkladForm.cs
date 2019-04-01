using DAL;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BLL;
using BLL.Models;

namespace _3LayerWinApp
{
    public partial class AddVkladForm : Form
    {
        private void init(List<ClientMod> mData, List<ProgMod> pData)
        {
            InitializeComponent();

            comboBoxClient.DataSource = mData;
            comboBoxClient.DisplayMember = "Name";
            comboBoxClient.ValueMember = "Id";
            comboBoxProg.DataSource = pData;
            comboBoxProg.DisplayMember = "name";
            comboBoxProg.ValueMember = "Id";
        }

        

        public AddVkladForm(List<ClientMod> mData, List<ProgMod> pData)
        {
            init(mData, pData);
            this.Text = "Добавить вклад";                    
        }
    }
}
