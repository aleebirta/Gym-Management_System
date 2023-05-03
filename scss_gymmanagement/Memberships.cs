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
    public partial class Memberships : Form
    {
        Functions Con;
        public Memberships()
        {
            InitializeComponent();
            Con = new Functions();
            ShowMShips();
        }

        private void ShowMShips()
        {
            string Query = "select * from MembershipTbl";
            MShipList.DataSource = Con.GetData(Query);
        }
        private void Reset()
        {
            MNameTb.Text = "";
            CostTb.Text = "";
            MDurationTb.Text = "";
            GoalTb.Text = "";
        }
        private void SaveBtn_Click(object sender, EventArgs e)
        {
            try
            {
                if (MNameTb.Text == "" || MDurationTb.Text == "" || GoalTb.Text == "" || CostTb.Text == "")
                {
                    MessageBox.Show("Missing Data!");
                }
                else
                {
                    string MName = MNameTb.Text;
                    int Duration = Convert.ToInt32(MDurationTb.Text);
                    string Goal = GoalTb.Text;
                    int Cost = Convert.ToInt32(CostTb.Text);
                    string Query = "insert into MembershipTbl values('{0}', {1}, '{2}', {3})";
                    Query = string.Format(Query, MName, Duration, Goal, Cost);
                    Con.setData(Query);
                    ShowMShips();
                    MessageBox.Show("Membership Added");
                    Reset();
                }
            }
            catch (Exception Ex)
            {

                MessageBox.Show(Ex.Message);
            }
        }
        int Key = 0;
        private void MShipList_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            //if (MShipList.SelectedRows.Count > 0)
            //{
            //    MNameTb.Text = MShipList.SelectedRows[0].Cells[1].Value.ToString();
            //    MDurationTb.Text = MShipList.SelectedRows[0].Cells[2].Value.ToString();
            //    GoalTb.Text = MShipList.SelectedRows[0].Cells[3].Value.ToString();
            //    CostTb.Text = MShipList.SelectedRows[0].Cells[4].Value.ToString();
            //    if (MNameTb.Text == "")
            //    {
            //        Key = 0;
            //    }
            //    else
            //    {
            //        Key = Convert.ToInt32(MShipList.SelectedRows[0].Cells[0].Value.ToString());
            //    }
            //}
        }

        private void DeleteBtn_Click(object sender, EventArgs e)
        {
            if (MShipList.SelectedRows.Count > 0)
            {
                string MsId = MShipList.SelectedRows[0].Cells["MShipId"].Value.ToString();
                string Query = "DELETE FROM MembershipTbl WHERE MShipId = @MShipId";
                SqlCommand cmd = new SqlCommand(Query, Con.GetConnection());
                cmd.Parameters.AddWithValue("@MShipId", MsId);
                Con.OpenConnection();
                int result = cmd.ExecuteNonQuery();
                Con.CloseConnection();
                if (result > 0)
                {
                    MessageBox.Show("Membership deleted successfully.");
                    ShowMShips();
                    Reset();
                }
                else
                {
                    MessageBox.Show("Failed to delete Membership.");
                }
            }
            else
            {
                MessageBox.Show("No Membership selected.");
            }
        }

        private void EditBtn_Click(object sender, EventArgs e)
        {
            try
            {
                if (MShipList.SelectedRows.Count > 0)
                {
                    if (MNameTb.Text == "" || MDurationTb.Text == "" || GoalTb.Text == "" || CostTb.Text == "")
                    {
                        MessageBox.Show("Missing Data!");
                    }
                    else
                    {
                        string MName = MNameTb.Text;
                        int Duration = Convert.ToInt32(MDurationTb.Text);
                        string Goal = GoalTb.Text;
                        int Cost = Convert.ToInt32(CostTb.Text);
                        int selectedRowIndex = MShipList.SelectedRows[0].Index;
                        int MShipId = Convert.ToInt32(MShipList.Rows[selectedRowIndex].Cells[0].Value);
                        string Query = "update MembershipTbl set MName = '{0}',MDuration = {1},MGoal = '{2}',MCost = {3} where MShipId = {4}";
                        Query = string.Format(Query, MName, Duration, Goal, Cost, MShipId);
                        Con.setData(Query);
                        ShowMShips();
                        MessageBox.Show("Membership Updated");
                        Reset();
                    }
                }
                else
                {
                    MessageBox.Show("No membership selected!");
                }
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
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

        private void label13_Click(object sender, EventArgs e)
        {
            Receptionists Obj = new Receptionists();
            Obj.Show();
            this.Hide();
        }

        private void label14_Click(object sender, EventArgs e)
        {
            Billing Obj = new Billing();
            Obj.Show();
            this.Hide();
        }

        private void MShipList_SelectionChanged(object sender, EventArgs e)
        {
            if (MShipList.SelectedRows.Count > 0)
            {
                MNameTb.Text = MShipList.SelectedRows[0].Cells[1].Value.ToString();              
                MDurationTb.Text = MShipList.SelectedRows[0].Cells[2].Value.ToString();
                GoalTb.Text = MShipList.SelectedRows[0].Cells[3].Value.ToString();
                CostTb.Text = MShipList.SelectedRows[0].Cells[4].Value.ToString();
            }
            else
            {
                Reset();
            }
        }
    }
}
