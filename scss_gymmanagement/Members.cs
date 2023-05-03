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
    public partial class Members : Form
    {
        Functions Con;
        SqlConnection connection;
        public Members()
        {
            InitializeComponent();
            Con = new Functions();
            connection = Con.GetConnection();
            ShowMembers();
            GetCoaches();
            GetMships();
        }

        private void ShowMembers()
        {
            string Query = "select * from MemberTbl";
            MembersList.DataSource = Con.GetData(Query);
        }

        private void CoachLbl_Click(object sender, EventArgs e)
        {
            Coachs Obj = new Coachs();
            Obj.Show();
            this.Hide();
        }
        private void GetCoaches()
        {
            string Query = "select * from CoachTbl";
            CoachCb.DisplayMember = Con.GetData(Query).Columns["CName"].ToString();
            CoachCb.ValueMember = Con.GetData(Query).Columns["CId"].ToString();
            CoachCb.DataSource = Con.GetData(Query);
        }

        private void GetMships()
        {
            string Query = "select * from MembershipTbl";
            MShipCb.DisplayMember = Con.GetData(Query).Columns["MName"].ToString();
            MShipCb.ValueMember = Con.GetData(Query).Columns["MShipId"].ToString();
            MShipCb.DataSource = Con.GetData(Query);
        }
        private void Reset()
        {
            MNameTb.Text = "";
            PhoneTb.Text = "";
            CoachCb.SelectedIndex = -1;
            GenCb.SelectedIndex = -1;   
            MShipCb.SelectedIndex = -1;
            StatusCb.SelectedIndex = -1;
            TimingCb.SelectedIndex = -1;
        }

        private void SaveBtn_Click(object sender, EventArgs e)
        {
            try
            {
                if (MNameTb.Text == "" || PhoneTb.Text == "" || CoachCb.SelectedIndex == -1 || GenCb.SelectedIndex == -1 || MShipCb.SelectedIndex == -1 || StatusCb.SelectedIndex == -1)
                {
                    MessageBox.Show("Missing Data!");
                }
                else
                {
                    string MName = MNameTb.Text;
                    string Gender = GenCb.SelectedItem.ToString();
                    string Phone = PhoneTb.Text;
                    string DOB = DOBTb.Value.Date.ToString();
                    string MJDate = JDateTb.Value.Date.ToString();
                    int MShip = Convert.ToInt32(MShipCb.SelectedValue.ToString());
                    int Coach = Convert.ToInt32(CoachCb.SelectedValue.ToString());
                    string Timing = TimingCb.SelectedItem.ToString();
                    string Status = StatusCb.SelectedItem.ToString();

                    string Query = "insert into MemberTbl values('{0}','{1}', '{2}', '{3}', '{4}', '{5}', '{6}', '{7}', '{8}')";
                    Query = string.Format(Query, MName, Gender, DOBTb.Value.Date, JDateTb.Value.Date, MShip, Coach, Phone, Timing, Status);
                    Con.setData(Query);
                    ShowMembers();
                    MessageBox.Show("Member Added");
                    Reset();
                }
            }
            catch (Exception Ex)
            {

                MessageBox.Show(Ex.Message);
            }
        }
        private void MembersList_SelectionChanged(object sender, EventArgs e)
        {
            if (MembersList.SelectedRows.Count > 0)
            {
                MNameTb.Text = MembersList.SelectedRows[0].Cells[1].Value.ToString();
                GenCb.SelectedItem = MembersList.SelectedRows[0].Cells[2].Value.ToString();
                DOBTb.Text = MembersList.SelectedRows[0].Cells[3].Value.ToString();
                JDateTb.Text = MembersList.SelectedRows[0].Cells[4].Value.ToString();
                MShipCb.Text = MembersList.SelectedRows[0].Cells[5].Value.ToString();
                CoachCb.Text = MembersList.SelectedRows[0].Cells[6].Value.ToString();
                PhoneTb.Text = MembersList.SelectedRows[0].Cells[7].Value.ToString();
                TimingCb.Text = MembersList.SelectedRows[0].Cells[8].Value.ToString();
                StatusCb.Text = MembersList.SelectedRows[0].Cells[9].Value.ToString();
            } 
            else
            {
                Reset();
            }
        }

        private void EditBtn_Click(object sender, EventArgs e)
        {
            try
            {
                if (MNameTb.Text == "" || PhoneTb.Text == "" || CoachCb.SelectedIndex == -1 || GenCb.SelectedIndex == -1 || MShipCb.SelectedIndex == -1 || StatusCb.SelectedIndex == -1)
                {
                    MessageBox.Show("Missing Data!");
                }
                else
                {
                    string MName = MNameTb.Text;
                    string Gender = GenCb.SelectedItem.ToString();
                    string Phone = PhoneTb.Text;
                    string DOB = DOBTb.Value.Date.ToString();
                    string MJDate = JDateTb.Value.Date.ToString();
                    int MShip = Convert.ToInt32(MShipCb.SelectedValue.ToString()); //CAND EDITEZI, SA ALEGI COACH-UL SI MMSHIP-UL CORECT CE CORESPUNDE DATELOR CName(CoachsTbl) si MName(MembershiTbl) DIN TABELUL MEMBER
                    int Coach = Convert.ToInt32(CoachCb.SelectedValue.ToString());
                    string Timing = TimingCb.SelectedItem.ToString();
                    string Status = StatusCb.SelectedItem.ToString();

                    int selectedRowIndex = MembersList.SelectedCells[0].RowIndex;
                    DataGridViewRow selectedRow = MembersList.Rows[selectedRowIndex];
                    int memberId = Convert.ToInt32(selectedRow.Cells["MId"].Value.ToString());

                    string Query = "update MemberTbl set MName = '{0}', MGen = '{1}', MDOB = '{2}', MDate = '{3}', MMembership = {4}, MCoach = {5}, MPhone = '{6}', MTiming = '{7}', MStatus = '{8}' where MId = {9}";
                    Query = string.Format(Query, MName, Gender, DOB, MJDate, MShip, Coach, Phone, Timing, Status, memberId);
                    Con.setData(Query);
                    ShowMembers();
                    MessageBox.Show("Member updated successfully!");
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
            if (MembersList.SelectedRows.Count > 0)
            {
                string memberId = MembersList.SelectedRows[0].Cells["MId"].Value.ToString();
                string Query = "DELETE FROM MemberTbl WHERE MId = @MId";
                SqlCommand cmd = new SqlCommand(Query, Con.GetConnection());
                cmd.Parameters.AddWithValue("@MId", memberId);
                Con.OpenConnection();
                int result = cmd.ExecuteNonQuery();
                Con.CloseConnection();
                if (result > 0)
                {
                    MessageBox.Show("Member deleted successfully.");
                    ShowMembers();
                    Reset();
                }
                else
                {
                    MessageBox.Show("Failed to delete member.");
                }
            }
            else
            {
                MessageBox.Show("No member selected.");
            }
        }

        private void label14_Click(object sender, EventArgs e)
        {
            Billing Obj = new Billing();
            Obj.Show();
            this.Hide();
        }

        private void label15_Click(object sender, EventArgs e)
        {
            Login Obj = new Login();
            Obj.Show();
            this.Hide();
        }

        private void label12_Click(object sender, EventArgs e)
        {
            Memberships Obj = new Memberships();
            Obj.Show();
            this.Hide();
        }

        private void label13_Click(object sender, EventArgs e)
        {
            Receptionists Obj = new Receptionists();
            Obj.Show();
            this.Hide();
        }
    }
}
