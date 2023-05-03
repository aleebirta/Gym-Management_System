using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace scss_gymmanagement
{
    public partial class Coachs : Form
    {
        Functions Con;
        public Coachs()
        {
            InitializeComponent();
            Con = new Functions();
            ShowCoach();
        }
        private void ShowCoach()
        {
            string Query = "select * from CoachTbl";
            CoachsList.DataSource = Con.GetData(Query);
        }
        private void Reset()
        {
            ChNameTb.Text = "";        
            GenCb.SelectedIndex = -1; 
            PhoneTb.Text = "";
            ExpTb.Text = "";
            AddTb.Text = "";
            PassTb.Text = "";
        }
        private void SaveBtn_Click(object sender, EventArgs e)
        {
            try
            {
                if (ChNameTb.Text == "" || PhoneTb.Text == "" || ExpTb.Text == "" || PassTb.Text == "" || GenCb.SelectedIndex == -1)
                {
                    MessageBox.Show("Missing Data!");
                } else
                {
                    string CName = ChNameTb.Text;
                    string Gender = GenCb.SelectedItem.ToString();
                    string Phone = PhoneTb.Text;
                    int experience = Convert.ToInt32(ExpTb.Text);
                    string Add = AddTb.Text;
                    string Password = PassTb.Text;
                    string Query = "insert into CoachTbl values('{0}', '{1}', '{2}', '{3}', '{4}', '{5}', '{6}')";
                    Query = string.Format(Query, CName, Gender, DOBTb.Value.Date, Phone, experience, Add, Password);
                    Con.setData(Query);
                    ShowCoach();
                    MessageBox.Show("Coach Added");
                }
            }
            catch (Exception Ex)
            {

                MessageBox.Show(Ex.Message);
            }
        }

        int Key = 0;
        private void CoachsList_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            //if (CoachsList.SelectedRows.Count > 0)
            //{
            //    ChNameTb.Text = CoachsList.SelectedRows[0].Cells[1].Value.ToString();
            //    GenCb.SelectedItem = CoachsList.SelectedRows[0].Cells[2].Value.ToString();
            //    DOBTb.Text = CoachsList.SelectedRows[0].Cells[3].Value.ToString();
            //    PhoneTb.Text = CoachsList.SelectedRows[0].Cells[4].Value.ToString();
            //    ExpTb.Text = CoachsList.SelectedRows[0].Cells[5].Value.ToString();
            //    AddTb.Text = CoachsList.SelectedRows[0].Cells[6].Value.ToString();
            //    PassTb.Text = CoachsList.SelectedRows[0].Cells[7].Value.ToString();
            //    if (ChNameTb.Text == "")
            //    {
            //        Key = 0;
            //    }
            //    else
            //    {
            //        Key = Convert.ToInt32(CoachsList.SelectedRows[0].Cells[0].Value.ToString());
            //    }
            //}
        }

        private void DeleteBtn_Click(object sender, EventArgs e)
        {
            //if (CoachsList.SelectedRows.Count > 0) 
            //{
            //    DialogResult result = MessageBox.Show("Are you sure you want to delete this coach?", "Delete Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            //    if (result == DialogResult.Yes)
            //    {
            //        int selectedRowIndex = CoachsList.SelectedRows[0].Index;
            //        CoachsList.Rows.RemoveAt(selectedRowIndex);

            //        ChNameTb.Text = "";
            //        GenCb.SelectedItem = null;
            //        DOBTb.Text = "";
            //        PhoneTb.Text = "";
            //        ExpTb.Text = "";
            //        AddTb.Text = "";
            //        PassTb.Text = "";


            //        Key = 0;
            //    }
            //}
            if (CoachsList.SelectedRows.Count > 0)
            {
                string coachId = CoachsList.SelectedRows[0].Cells["CId"].Value.ToString();
                string Query = "DELETE FROM CoachTbl WHERE CId = @CId";
                SqlCommand cmd = new SqlCommand(Query, Con.GetConnection());
                cmd.Parameters.AddWithValue("@CId", coachId);
                Con.OpenConnection();
                int result = cmd.ExecuteNonQuery();
                Con.CloseConnection();
                if (result > 0)
                {
                    MessageBox.Show("Coach deleted successfully.");
                    ShowCoach();
                    Reset();
                }
                else
                {
                    MessageBox.Show("Failed to delete coach.");
                }
            }
            else
            {
                MessageBox.Show("No coach selected.");
            }
        }

        private void EditBtn_Click(object sender, EventArgs e)
        {
            try
            {
                if (ChNameTb.Text == "" || PhoneTb.Text == "" || ExpTb.Text == "" || PassTb.Text == "" || GenCb.SelectedIndex == -1)
                {
                    MessageBox.Show("Missing Data!");
                }
                else if (CoachsList.SelectedRows.Count == 0)
                {
                    MessageBox.Show("No coach selected!");
                }
                else
                {
                    string coachId = CoachsList.SelectedRows[0].Cells["CId"].Value.ToString();
                    string CName = ChNameTb.Text;
                    string Gender = GenCb.SelectedItem.ToString();
                    string Phone = PhoneTb.Text;
                    int experience = Convert.ToInt32(ExpTb.Text);
                    string Add = AddTb.Text;
                    string Password = PassTb.Text;

                    string Query = "UPDATE CoachTbl SET CName=@CName, CGen=@CGen, CDOB=@CDOB, CPhone=@CPhone, CExperience=@CExperience, CAddress=@CAddress, CPass=@CPass WHERE CId=@CId";
                    SqlCommand cmd = new SqlCommand(Query, Con.GetConnection());
                    cmd.Parameters.AddWithValue("@CName", CName);
                    cmd.Parameters.AddWithValue("@CGen", Gender);
                    cmd.Parameters.AddWithValue("@CDOB", DOBTb.Value.Date);
                    cmd.Parameters.AddWithValue("@CPhone", Phone);
                    cmd.Parameters.AddWithValue("@CExperience", experience);
                    cmd.Parameters.AddWithValue("@CAddress", Add);
                    cmd.Parameters.AddWithValue("@CPass", Password);
                    cmd.Parameters.AddWithValue("@CId", coachId);

                    Con.OpenConnection();
                    int result = cmd.ExecuteNonQuery();
                    Con.CloseConnection();

                    if (result > 0)
                    {
                        MessageBox.Show("Coach updated successfully.");
                        ShowCoach();
                        Reset();
                    }
                    else
                    {
                        MessageBox.Show("Failed to update coach.");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void MemberLbl_Click(object sender, EventArgs e)
        {
            Members Obj = new Members();
            Obj.Show();
            this.Hide();
        }

        private void MShipLbl_Click(object sender, EventArgs e)
        {
            Memberships Obj = new Memberships();
            Obj.Show();
            this.Hide();
        }

        private void RecepLbl_Click(object sender, EventArgs e)
        {
            Receptionists Obj = new Receptionists();
            Obj.Show();
            this.Hide();
        }

        private void label15_Click(object sender, EventArgs e)
        {
            Login Obj = new Login();
            Obj.Show();
            this.Hide();
        }

        private void DOBTb_ValueChanged(object sender, EventArgs e)
        {

        }

        private void label14_Click(object sender, EventArgs e)
        {
            Billing Obj = new Billing();
            Obj.Show();
            this.Hide();
        }

        private void CoachsList_SelectionChanged(object sender, EventArgs e)
        {
            if (CoachsList.SelectedRows.Count > 0)
            {
                ChNameTb.Text = CoachsList.SelectedRows[0].Cells[1].Value.ToString();
                GenCb.SelectedItem = CoachsList.SelectedRows[0].Cells[2].Value.ToString();
                DOBTb.Text = CoachsList.SelectedRows[0].Cells[3].Value.ToString();
                PhoneTb.Text = CoachsList.SelectedRows[0].Cells[4].Value.ToString();
                ExpTb.Text = CoachsList.SelectedRows[0].Cells[5].Value.ToString();
                AddTb.Text = CoachsList.SelectedRows[0].Cells[6].Value.ToString();              
                PassTb.Text = CoachsList.SelectedRows[0].Cells[7].Value.ToString();
            }
            else
            {
                Reset();
            }
        }
    }
}
