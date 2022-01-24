using ParfumUI.Common;
using ParfumUI.Parfum.Brend;
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
using ParfumUI.Load;

namespace ParfumUI
{
    public partial class ParfumAdd : Form
    {
        public ParfumAdd()
        {
            InitializeComponent();
            
        }
        private void ParfumAdd_Load(object sender, EventArgs e)
        {
            // Load Parfum Items
            LoadCommonData.LoadBrend(combBrend);
            LoadCommonData.LoadDensity(combDensity);
            LoadCommonData.LoadGender(combGender);

            combBrend.SelectedIndex = 0;
            combDensity.SelectedIndex = 0;
            combGender.SelectedIndex = 0;

        }

        private void button1_Click(object sender, EventArgs e)
        {

            // Save Click
            if (LoadParfumItems.IsAreYouSure("Created"))
            {
                string name = textName.Text.Trim();
                string image = textImage.Text.Trim();
                string decrip = textDescription.Text.Trim();
                string brend = combBrend.SelectedItem.ToString().Trim();
                string gender = combGender.SelectedItem.ToString().Trim();
                string density = combDensity.SelectedItem.ToString().Trim();

               
                // find BrendId
                int brendId = LoadCommonData._db.Brends
                    .Select(es => new { es.Id, es.Name })
                    .FirstOrDefault(ead => ead.Name.Trim().ToLower() == brend.ToLower()).Id;

                // Find gender
                int gederId = LoadCommonData._db.Genders
                    .Select(es => new { es.Id, es.Name })
                    .FirstOrDefault(ead => ead.Name.Trim().ToLower() == gender.ToLower()).Id;

                // Find gender
                int densityId = LoadCommonData._db.Densities
                    .Select(es => new { es.Id, es.Name })
                    .FirstOrDefault(ead => ead.Name.Trim().ToLower() == density.ToLower()).Id;

                var parfumIsAdd = LoadCommonData._db.Parfumes
                    .FirstOrDefault(es => es.Name.Trim().ToLower() == name.ToLower() && es.BrendId == brendId);

                // Parfum Added Check
                if (parfumIsAdd!=null)
                {
                    ParfumMessenge.Error("This Parfum Already Added");
                    return;
                }

                // Parfum
                DataMsSqlModel.Parfume parfume = new Parfume()
                {
                    Name = name,
                    Description = decrip,
                    BrendId = brendId,
                    GenderId = gederId,
                    DensityId = densityId
                };


                LoadCommonData._db.Parfumes.Add(parfume);
                // Save
                LoadCommonData._db.SaveChanges();

                // Change DataGridView
                RefresData.parfum_Function.ChangeParfum();

                // Clear Boxs
                textName.Text = "";
                textImage.Text ="";
                textDescription.Text ="";
                combBrend.SelectedIndex =0;
                combGender.SelectedIndex = 0;
                combDensity.SelectedIndex = 0;
                ParfumMessenge.Warning($"{name} Parfum Created");
            }

            
        }

        
    }
}
