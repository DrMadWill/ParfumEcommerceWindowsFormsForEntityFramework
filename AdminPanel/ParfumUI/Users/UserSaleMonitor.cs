using ParfumUI.Common;
using ParfumUI.DataModelMsSql;
using ParfumUI.Load;
using ParfumUI.Parfum.Load;
using ParfumUI.SalePriceFolder;
using ParfumUI.Users;
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
namespace ParfumUI.Users
{
    public partial class UserSaleMonitor : Form
    {
        public UserSaleMonitor()
        {
            InitializeComponent();
        }

        DataTable table = new DataTable();

        DataGridView gridView = new DataGridView();

        DataTable dataTableShearch = new DataTable();

        private void UserSaleMonitor_Load(object sender, EventArgs e)
        {
            LoadCommonData.LoadCategory(combCatogory);


            var userNames = LoadCommonData._db.Users
                .Where(dr => dr.IsActive == true && dr.IsUser == true)
                .Select(ds => ds.FullName).ToList();

            combUser.Items.Clear();
            foreach (var item in userNames)
            {
                combUser.Items.Add(item);
            }
            combUser.SelectedIndex = 0;
            combUser.DropDownStyle = ComboBoxStyle.DropDownList;
            combCatogory.DropDownStyle = ComboBoxStyle.DropDownList;
            combEmploye.DropDownStyle = ComboBoxStyle.DropDownList;
            var eploNames = LoadCommonData._db.Users
                .Where(dr => dr.IsActive == true && dr.IsEmployee == true)
                .Select(ds=>ds.FullName).ToList();

            combEmploye.Items.Clear();
            foreach (var item in eploNames)
            {
                combEmploye.Items.Add(item);
            }
            combEmploye.SelectedIndex = 0;


            ChangeData();

        }

        public void ChangeData()
        {
            dataGridViewShearch.DataSource = null;
            dataGridViewShearch.DataSource = LoadCommonData._db.SaleDetailParfums.ToList();
            textcatogory.Text = "All Parfums";
        }


        public void ChangeCatogory()
        {

            string catogory = combCatogory.SelectedItem.ToString().Trim();
            var ParfumIds = LoadCommonData._db.CategoryToParfums
                .Where(dr => dr.Catogory.Name == catogory)
                .Select(sd=>sd.ParfumId).ToList();
            List<DataModelMsSql.SaleDetailParfum> sales = new List<DataModelMsSql.SaleDetailParfum>();
            sales.Clear();
            dataGridViewShearch.DataSource = null;
            foreach (var parfumId in ParfumIds)
            {
                var parfums = LoadCommonData._db.SaleDetailParfums.Where(dr => dr.Id == parfumId);
                sales.AddRange(parfums);
            }
            dataGridViewShearch.DataSource = sales;
            textcatogory.Text = catogory;

        }

        private void combCatogory_SelectedIndexChanged(object sender, EventArgs e)
        {
            ChangeCatogory();
        }

        private void btn_allparfums_Click(object sender, EventArgs e)
        {
            ChangeData();
        }



        private void dataGridViewShearch_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridViewShearch.SelectedRows.Count > 0)
            {
                string id = dataGridViewShearch.Rows[e.RowIndex].Cells["Id"].Value.ToString();
                string PriceId = dataGridViewShearch.Rows[e.RowIndex].Cells["PriceId"].Value.ToString();
                string Name = dataGridViewShearch.Rows[e.RowIndex].Cells["Name"].Value.ToString();
                string Description = dataGridViewShearch.Rows[e.RowIndex].Cells["Description"].Value.ToString();
                string Brend = dataGridViewShearch.Rows[e.RowIndex].Cells["Brend"].Value.ToString();
                string Gender = dataGridViewShearch.Rows[e.RowIndex].Cells["Gender"].Value.ToString();
                string Density = dataGridViewShearch.Rows[e.RowIndex].Cells["Density"].Value.ToString();
                string Size = dataGridViewShearch.Rows[e.RowIndex].Cells["Size"].Value.ToString();
                string price = dataGridViewShearch.Rows[e.RowIndex].Cells["Price"].Value.ToString();
                string Number = dataGridViewShearch.Rows[e.RowIndex].Cells["Number"].Value.ToString();

                //Distinct
                foreach (DataGridViewRow item in dataGridViewSales.Rows)
                {
                    if (item.Cells["PriceId"].Value.ToString().Trim() == PriceId)
                    {
                        return;
                    }
                }
                
                dataGridViewSales.Rows.Add(id, PriceId, Name, Description, Brend, Gender, Density, Size,price, Number, "");
            }
        }

        
        private void btnClear_Click(object sender, EventArgs e)
        {
            
            dataGridViewSales.Rows.Clear();
        }

        private void btnSale_Click(object sender, EventArgs e)
        {
            if (LoadParfumItems.IsAreYouSure())
            {
                
                string UserName = combUser.SelectedItem.ToString();
                int userId = LoadCommonData._db.Users.FirstOrDefault(dr => dr.FullName.ToLower() == UserName.ToLower()).Id;
                int basecount = 0;

            IsEmptyDataGrid:;

                // Foreach Last Elemet Prablem

                foreach (DataGridViewRow row in dataGridViewSales.Rows)
                {
                    int saleCount = 0;
                    DataModelMsSql.SalePrice Price;
                    int PriceIdId = 0;
                    try
                    {
                        PriceIdId = int.Parse(row.Cells["PriceId"].Value.ToString().Trim());
                        saleCount = int.Parse(row.Cells["SaleCount"].Value.ToString().Trim());
                        Price = LoadCommonData._db.SalePrices.Find(PriceIdId);
                    }
                    catch 
                    {
                        LoadParfumItems.MessengeWarning("PLease Count Add.");
                        return;
                    }
                     
                   
                    
                    if (saleCount > Price.number)
                    {
                        
                        ParfumMessenge.Error("There is not so much perfume.");
                        return;
                    }

                    if (saleCount == 0)
                    {
                        ParfumMessenge.Error("PLease Count Add.");
                        return;
                    }

                    Sale sale = new Sale()
                    {
                        SalePriceId = Price.Id,
                        Date = dateTimeSale.Value,

                    };

                    LoadParfumItems.MessengeWarning($": Parfum {parfumname} Saled ");
                    dataGridViewSales.Rows.Remove(row);
                    ChangeData();
                }

                if (dataGridViewSales.Rows.Count > 0)
                    goto IsEmptyDataGrid;

                RefresData.salePriceLists.ChangeData();
            }

        }

        private void dataGridViewSales_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridViewSales.SelectedRows.Count > 0)
            {
                // Remove Dobul click elemet
                dataGridViewSales.Rows.Remove(dataGridViewSales.Rows[e.RowIndex]);
                
            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            string shrearchname = textSearchName.Text.Trim().ToLower();
            string loadnames = "";
            table.Rows.Clear();
            List<int> indexs = new List<int>();
            foreach (DataRow row in dataTableShearch.Rows)
            {
                loadnames = row["Name"].ToString().Trim().ToLower();
                if (loadnames.Contains(shrearchname))
                {
                    table.Rows.Add(row.ItemArray);
                }
            }
            dataGridViewShearch.DataSource = table;
        }
    }
}
