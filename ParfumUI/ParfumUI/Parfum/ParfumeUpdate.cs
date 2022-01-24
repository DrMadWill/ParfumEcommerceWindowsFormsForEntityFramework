﻿using ParfumUI.Load;
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
using ParfumUI.Common;

namespace ParfumUI
{
    public partial class ParfumeUpdate : Form
    {

        int ParfumId = 0;
        string parfumFulName = "";
        public ParfumeUpdate()
        {
            InitializeComponent();
        }

        private void ParfumeUpdate_Load(object sender, EventArgs e)
        {
            LoadCommonData.LoadBrend(combBrend);
            LoadCommonData.LoadDensity(combDensity);
            LoadCommonData.LoadGender(combGender);
            combBrend.SelectedIndex = 0;
            combDensity.SelectedIndex = 0;
            combGender.SelectedIndex = 0;
            ChangeData();
        }

        public void ChangeData()
        {
            var allparfums = LoadCommonData._db.MidDetalParfumes.ToList();
            dataGridViewUpdate.DataSource = allparfums;
        }


        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(parfumFulName) || ParfumId == 0)
            {
                ParfumMessenge.Error("You Must Be Seleced Parfum From Parfum List");
                return;
            }
            if (ParfumMessenge.IsAreYouSure($"Are you {parfumFulName} delete?"))
            {


                var parfumDelete = LoadCommonData._db.Parfumes.Find(ParfumId);
                if (parfumDelete != null)
                {
                    var isUsingElementCategory = LoadCommonData._db.CategoryToParfums.FirstOrDefault(dr => dr.ParfumId == ParfumId);
                    if (isUsingElementCategory != null)
                    {
                        ParfumMessenge.Error("This Elemet is Using");
                        return;
                    }

                    var isUsingElementPrice = LoadCommonData._db.SalePrices.FirstOrDefault(dr => dr.ParfumId == ParfumId);
                    if (isUsingElementPrice != null)
                    {
                        ParfumMessenge.Error("This Elemet is Using");
                        return;
                    }

                    LoadCommonData._db.Parfumes.Remove(parfumDelete);

                    // Save
                    LoadCommonData._db.SaveChanges();


                    // Change DataGridView
                    RefresData.parfum_Function.ChangeParfum();

                    // Clear Elemets
                    ClearBoxs();

                    ParfumMessenge.Warning($"{parfumFulName} Parfum Deleted");

                    ChangeData();
                }
                else
                    ParfumMessenge.Error("Not Found This Elemet");


               
            }
        }

        private void dataGridViewUpdate_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {

            if (dataGridViewUpdate.SelectedRows.Count > 0)
            {
                textName.Text =  dataGridViewUpdate.Rows[e.RowIndex].Cells["Name"].Value.ToString();
                textDescription.Text = dataGridViewUpdate.Rows[e.RowIndex].Cells["Description"].Value.ToString();
                combBrend.SelectedItem  = dataGridViewUpdate.Rows[e.RowIndex].Cells["Brend"].Value.ToString();
                combGender.SelectedItem = dataGridViewUpdate.Rows[e.RowIndex].Cells["Gender"].Value.ToString();
                combDensity.SelectedItem = dataGridViewUpdate.Rows[e.RowIndex].Cells["Density"].Value.ToString();

                ParfumId =Convert.ToInt32(dataGridViewUpdate.Rows[e.RowIndex].Cells["Id"].Value.ToString());
                parfumFulName = combBrend.SelectedItem.ToString()+"/" + textName.Text;

            }
        }

        private void btnUpfate_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(parfumFulName) || ParfumId==0)
            {
                ParfumMessenge.Error("You Must Be Seleced Parfum From Parfum List");
                return;
            }
            if (ParfumMessenge.IsAreYouSure($"Are you {parfumFulName} Update"))
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
                    .FirstOrDefault(es => es.Name.Trim().ToLower() == name.ToLower() && es.BrendId == brendId && es.Id!=ParfumId);

                // Parfum Added Check
                if (parfumIsAdd != null)
                {
                    ParfumMessenge.Error("This Parfum Already Added");
                    return;
                }

                var parfumUpdate = LoadCommonData._db.Parfumes.FirstOrDefault(es => es.Id == ParfumId);
                if (parfumUpdate != null)
                {
                    parfumUpdate.Name = name;
                    parfumUpdate.Description = decrip;
                    parfumUpdate.DensityId = densityId;
                    parfumUpdate.BrendId = brendId;
                    parfumUpdate.GenderId = gederId;

                    LoadCommonData._db.SaveChanges();
                }
                // Save
               


                // Change DataGridView
                RefresData.parfum_Function.ChangeParfum();

                ClearBoxs();

                ChangeData();

                ParfumMessenge.Warning("Parfum Updated");

                
            }
        }


        private void btnSave_Click(object sender, EventArgs e)
        {
            if(ParfumMessenge.IsAreYouSure("Are You Sure Create ?"))
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
                if (parfumIsAdd != null)
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

                ClearBoxs();
                ParfumMessenge.Warning($"{name} Parfum Created");
                
                ChangeData();


            }

           
        }

        private void ClearBoxs()
        {
            // Clear Boxs
            textName.Text = "";
            textImage.Text = "";
            textDescription.Text = "";
            combBrend.SelectedIndex = 0;
            combGender.SelectedIndex = 0;
            combDensity.SelectedIndex = 0;
            ParfumId = 0;
            parfumFulName = "";
        }
    }
}