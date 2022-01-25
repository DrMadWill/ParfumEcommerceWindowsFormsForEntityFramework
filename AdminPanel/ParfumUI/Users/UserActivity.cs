using ParfumUI.Common;
using ParfumUI.Load;
using ParfumUI.Parfum.Load;
using ParfumUI.SalePriceFolder;
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
    public partial class UserActivity : Form
    {
        public UserActivity()
        {
            InitializeComponent();
        }

        private string oldname="";
        private void UserActivity_Load(object sender, EventArgs e)
        {
            ChangeData();
        }

        private void ChangeData()
        {
            // Clear Data
            dataGridViewLoginUser.DataSource = null;
            dataGridViwUser.DataSource = null;
            // User Data
            dataGridViwUser.DataSource = LoadCommonData._db.ActiveUserTables.ToList();

            // Login User Data
            dataGridViewLoginUser.DataSource = LoadCommonData._db.ParfumLoginUsers.ToList();
            labelLoginUserCount.Text ="User Login :" + dataGridViewLoginUser.Rows.Count.ToString();
        }

        

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(oldname))
            {
                ParfumMessenge.Error("Please Select Item");
                return;
            }
            if (ParfumMessenge.IsAreYouSure($"Are You Sure Update {oldname}? "))
            {
                
                string fullname = textUserName.Text.Trim();
                string password = textPassword.Text.Trim();
                if (IsAdded(fullname))
                {
                    return;
                }

                if (string.IsNullOrEmpty(fullname) || string.IsNullOrEmpty(password))
                {
                    ParfumMessenge.Error("You Must Be Wrtie Information");
                    return;
                }

                var userUpdate = LoadCommonData._db.Users.FirstOrDefault(dr => dr.FullName.ToLower() == oldname.ToLower());
                if (userUpdate != null)
                {
                    userUpdate.FullName = fullname;
                    userUpdate.Password = password;
                    userUpdate.IsActive = checkActive.Checked;
                    userUpdate.IsEmployee = checkEmloyee.Checked;
                    userUpdate.IsUser = checkUser.Checked;

                    LoadCommonData._db.SaveChanges();


                    ChangeData();
                    ClearData();
                    ParfumMessenge.Warning($"{oldname} is new FullName {fullname}");
                }
                
            }
        }

        

        private void dataGridViwUser_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridViwUser.SelectedRows.Count > 0)
            {
                ClearData();
                string fullname = dataGridViwUser.Rows[e.RowIndex].Cells["FullName"].Value.ToString();
                oldname = fullname;
                string password = dataGridViwUser.Rows[e.RowIndex].Cells["Password"].Value.ToString();
                bool isActive = Convert.ToBoolean(dataGridViwUser.Rows[e.RowIndex].Cells["IsActive"].Value.ToString());
                bool isUser = Convert.ToBoolean(dataGridViwUser.Rows[e.RowIndex].Cells["IsUser"].Value.ToString());
                bool isEmploye = Convert.ToBoolean(dataGridViwUser.Rows[e.RowIndex].Cells["IsEmployee"].Value.ToString());
                if (isActive)
                    checkActive.Checked = true;
                if (isEmploye)
                    checkEmloyee.Checked = true;
                if (isUser)
                    checkUser.Checked = true;

                textPassword.Text = password;
                textUserName.Text = fullname;
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (ParfumMessenge.IsAreYouSure("Are You Sure Add"))
            {

                string fullname = textUserName.Text.Trim();
                string password = textPassword.Text.Trim();

                if (IsAdded(fullname))
                {
                    return;
                }

                if (string.IsNullOrEmpty(fullname) || string.IsNullOrEmpty(password))
                {
                    ParfumMessenge.Error("You Must Be Wrtie Information");
                    return;
                }

                DataModelMsSql.User user1 = new DataModelMsSql.User()
                {
                    FullName = fullname,
                    Password = password,
                    IsUser = checkUser.Checked,
                    IsActive = checkActive.Checked,
                    IsEmployee = checkEmloyee.Checked
                };

                LoadCommonData._db.Users.Add(user1);
                LoadCommonData._db.SaveChanges();

                ParfumMessenge.Warning($"User{fullname} Created");

                ChangeData();
                ClearData();
            }
        }

        
        private void ClearData()
        {
            textUserName.Text = "";
            textPassword.Text = "";
            checkUser.Checked = false;
            checkEmloyee.Checked = false;
            checkActive.Checked = false;
            oldname = "";
        }

        private void dataGridViewLoginUser_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            ClearData();
            if (dataGridViewLoginUser.SelectedRows.Count > 0)
            {
                string fullname = dataGridViewLoginUser.Rows[e.RowIndex].Cells["FullName"].Value.ToString();
                oldname = fullname;
                string password = dataGridViewLoginUser.Rows[e.RowIndex].Cells["Password"].Value.ToString();
                bool isActive = Convert.ToBoolean(dataGridViewLoginUser.Rows[e.RowIndex].Cells["IsActive"].Value.ToString());
                bool isUser = Convert.ToBoolean(dataGridViewLoginUser.Rows[e.RowIndex].Cells["IsUser"].Value.ToString());
                bool isEmploye = Convert.ToBoolean(dataGridViewLoginUser.Rows[e.RowIndex].Cells["IsEmployee"].Value.ToString());
                if (isActive)
                    checkActive.Checked = true;
                if (isEmploye)
                    checkEmloyee.Checked = true;
                if (isUser)
                    checkUser.Checked = true;

                textPassword.Text = password;
                textUserName.Text = fullname;
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            string fullname = textUserName.Text.Trim();
            if (string.IsNullOrEmpty(fullname))
            {
                ParfumMessenge.Error("Please FullName Write.");
                return;
            }
            if (ParfumMessenge.IsAreYouSure($"Are you Delete {fullname} user ?"))
            {
                // Sale Table Chack 
                var parfum_sales = LoadCommonData._db.Sales.FirstOrDefault(dr => dr.User.FullName.Trim().ToLower() == fullname.ToLower());
                if(parfum_sales != null)
                {
                    ParfumMessenge.Error($" This {fullname} Are Using");
                    return;
                }

                var ParfumUser = LoadCommonData._db.Users.FirstOrDefault(dr => dr.FullName.ToLower() == fullname.ToLower());
                if (ParfumUser != null)
                {
                    LoadCommonData._db.Users.Remove(ParfumUser);
                    LoadCommonData._db.SaveChanges();
                    ParfumMessenge.Warning($"{fullname} user Deleted");
                    ChangeData();
                    ClearData();
                }// Mesange

            }
            
        }

        private bool IsAdded(string name)
        {
            bool isAdd = false;
            var isAddedName = LoadCommonData._db.Users.FirstOrDefault(dr => dr.FullName.ToLower() == name.ToLower());
            if (isAddedName != null)
            {
                ParfumMessenge.Error($"This {name} Alredy Added ");
                isAdd = true;
            }
            return isAdd;
        }
    }
}
