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
using ParfumUI.Load;
using ParfumUI.Parfum.Load;

namespace ParfumUI
{
    public partial class BrendAdd : Form
    {
        public BrendAdd()
        {
            InitializeComponent();
        }

        // Brend Add Click
        private void button1_Click(object sender, EventArgs e)
        {
            if (LoadParfumItems.IsAreYouSure("Create"))
            {

                string name = textName.Text.Trim();
                string descript = textDescript.Text.Trim();

                Brend brend = new Brend()
                {
                    Name = name,
                    Decription=descript
                };

                LoadCommonData._db.Brends.Add(brend);
                //Save
                LoadCommonData._db.SaveChanges();

                LoadParfumItems.MessengeWarning("Created");
                textName.Text = "";
                textDescript.Text = "";
            }
        }
    }
}
