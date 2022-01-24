using ParfumUI.Load;
using ParfumUI.Parfum.Load;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ParfumUI.DataMsSqlModel;



namespace ParfumUI
{
    public partial class Parfum_Function : Form
    {

        public Parfum_Function()
        {
            InitializeComponent();
        }

        private void Parfum_Function_Load(object sender, EventArgs e)
        {
            // Parfum Load 
            ChangeParfum();
        }

        

        private void button3_Click(object sender, EventArgs e)
        {
            ParfumAdd parfumAdd = new ParfumAdd();
            RefresData.parfumAdd = parfumAdd;
            parfumAdd.ShowDialog();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            ParfumeUpdate parfumeUpdate = new ParfumeUpdate();
            RefresData.parfumeUpdate = parfumeUpdate;
            parfumeUpdate.ShowDialog();
        }


        public void ChangeParfum()
        {
            var parfumDetial = LoadCommonData._db.MidDetalParfumes.ToList();
            dataGridView1.DataSource = parfumDetial;
        }

       
        
    }
}
