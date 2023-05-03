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

namespace scss_gymmanagement
{
    public partial class Receptionists : Form
    {
        Functions Con;
        public Receptionists()
        {
            InitializeComponent();
            Con = new Functions();
            ShowReceptionist();
        }

        private void ShowReceptionist()
        {
            string Query = "select * from ReceptionistTbl";
            RecepList.DataSource = Con.GetData(Query);
        }

        private void Reset()
        {
            RecepNameTb.Text = "";
            PhoneTb.Text = "";
            PasswordTb.Text = "";
            RecepAddTb.Text = "";
            GenCb.SelectedIndex = -1;
        }
        private void SaveBtn_Click(object sender, EventArgs e)
        {
            try
            {
                if (RecepNameTb.Text == "" || PhoneTb.Text == "" || RecepAddTb.Text == "" || GenCb.SelectedIndex == -1 || PasswordTb.Text == "")
                {
                    MessageBox.Show("Missing Data!");
                }
                else
                {
                    string RName = RecepNameTb.Text;
                    string Gender = GenCb.SelectedItem.ToString();
                    string DOB = RecepDOBTb.Value.Date.ToString();
                    string Add = RecepAddTb.Text;
                    string Phone = PhoneTb.Text;
                    string Password = PasswordTb.Text;

                    string Query = "insert into ReceptionistTbl values('{0}', '{1}', '{2}', '{3}', '{4}', '{5}')";
                    Query = string.Format(Query, RName, Gender, DOB, Add, Phone, Password);
                    Con.setData(Query);
                    ShowReceptionist();
                    MessageBox.Show("Receptionist Added");
                    Reset();
                }
            }
            catch (Exception Ex)
            {

                MessageBox.Show(Ex.Message);
            }
        }
        int Key = 0;

        private void DeleteBtn_Click(object sender, EventArgs e)
        {
            if (RecepList.SelectedRows.Count > 0)
            {
                DialogResult result = MessageBox.Show("Are you sure you want to delete this receptionist?", "Delete Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result == DialogResult.Yes)
                {
                    int selectedRowIndex = RecepList.SelectedRows[0].Index;
                    RecepList.Rows.RemoveAt(selectedRowIndex);


                    RecepNameTb.Text = "";
                    GenCb.SelectedItem = null;
                    RecepDOBTb.Text = "";
                    PhoneTb.Text = "";
                    RecepAddTb.Text = "";
                    PasswordTb.Text = "";

                    Key = 0;
                }
            }
        }

        private void EditBtn_Click(object sender, EventArgs e)
        {
            try
            {
                if (RecepNameTb.Text == "" || PhoneTb.Text == "" || RecepAddTb.Text == "" || PasswordTb.Text == "" || GenCb.SelectedIndex == -1)
                {
                    MessageBox.Show("Missing Data!");
                }
                else if (RecepList.SelectedRows.Count == 0)
                {
                    MessageBox.Show("No receptionist selected!");
                }
                else
                {
                    string recepId = RecepList.SelectedRows[0].Cells["ReceptId"].Value.ToString();
                    string RName = RecepNameTb.Text;
                    string Gender = GenCb.SelectedItem.ToString();
                    DateTime DOB = RecepDOBTb.Value;
                    string Phone = PhoneTb.Text;
                    string Address = RecepAddTb.Text;
                    string Password = PasswordTb.Text;

                    string Query = "UPDATE ReceptionistTbl SET RecepName=@RecepName, RecepGen=@RecepGen, RecepDOB=@RecepDOB, RecepAdd=@RecepAdd, RecepPhone=@RecepPhone, RecepPass=@RecepPass WHERE ReceptId=@ReceptId";
                    SqlCommand cmd = new SqlCommand(Query, Con.GetConnection());
                    cmd.Parameters.AddWithValue("@RecepName", RName);
                    cmd.Parameters.AddWithValue("@RecepGen", Gender);
                    cmd.Parameters.AddWithValue("@RecepDOB", RecepDOBTb.Value.Date);
                    cmd.Parameters.AddWithValue("@RecepPhone", Phone);
                    cmd.Parameters.AddWithValue("@RecepAdd", Address);
                    cmd.Parameters.AddWithValue("@RecepPass", Password);
                    cmd.Parameters.AddWithValue("@ReceptId", recepId);

                    Con.OpenConnection();
                    int result = cmd.ExecuteNonQuery();
                    Con.CloseConnection();

                    if (result > 0)
                    {
                        MessageBox.Show("Receptionist updated successfully.");
                        ShowReceptionist();
                        Reset();
                    }
                    else
                    {
                        MessageBox.Show("Failed to update receptionist.");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void RecepList_SelectionChanged(object sender, EventArgs e)
        {
            if (RecepList.SelectedRows.Count > 0)
            {
                RecepNameTb.Text = RecepList.SelectedRows[0].Cells[1].Value.ToString();
                GenCb.SelectedItem = RecepList.SelectedRows[0].Cells[2].Value.ToString();
                RecepDOBTb.Value = Convert.ToDateTime(RecepList.SelectedRows[0].Cells[3].Value);
                RecepAddTb.Text = RecepList.SelectedRows[0].Cells[4].Value.ToString();
                PhoneTb.Text = RecepList.SelectedRows[0].Cells[5].Value.ToString();
                PasswordTb.Text = RecepList.SelectedRows[0].Cells[6].Value.ToString();
            }
            else
            {
                Reset();
            }
        }

        private void CoachLbl_Click(object sender, EventArgs e)
        {
            Coachs Obj = new Coachs();
            Obj.Show();
            this.Hide();
        }

        private void label15_Click(object sender, EventArgs e)
        {
            Login Obj = new Login();
            Obj.Show();
            this.Hide();
        }

        private void label11_Click(object sender, EventArgs e)
        {
            Members Obj = new Members();
            Obj.Show();
            this.Hide();
        }

        private void label12_Click(object sender, EventArgs e)
        {
            Memberships Obj = new Memberships();
            Obj.Show();
            this.Hide();
        }

        private void label14_Click(object sender, EventArgs e)
        {
            Billing Obj = new Billing();
            Obj.Show();
            this.Hide();
        }
    }
}
