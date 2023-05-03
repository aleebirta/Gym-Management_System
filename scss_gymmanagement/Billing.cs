using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace scss_gymmanagement
{
    public partial class Billing : Form
    {
        Functions Con;
        public Billing()
        {
            InitializeComponent();
            Con = new Functions();
            ShowBills();
            GetMembers();
        }

        private void ShowBills()
        {
            string Query = "select * from FinanceTbl";
            BillingList.DataSource = Con.GetData(Query);
        }
        private void GetMembers()
        {
            string Query = "select * from MemberTbl";
            MemberCb.DisplayMember = Con.GetData(Query).Columns["MName"].ToString();
            MemberCb.ValueMember = Con.GetData(Query).Columns["MId"].ToString();
            MemberCb.DataSource = Con.GetData(Query);
        }

        private void AddBtn_Click(object sender, EventArgs e)
        {
            try
            {
                if (MemberCb.Text == "" || AmountTb.Text == "" )
                {
                    MessageBox.Show("Missing Data!");
                }
                else
                {
                    int Agent = Login.UserId;
                    string Member = MemberCb.SelectedValue.ToString();
                    string Period = PeriodTb.Value.Date.Month + "-" + PeriodTb.Value.Date.Year;
                    string BDate = BDateTb.Value.Date.ToString();

                    string Amount = AmountTb.Text;
                    string Query = "insert into FinanceTbl values('{0}', '{1}', '{2}', '{3}', {4})";
                    Query = string.Format(Query, Agent, Member, Period, BDate, Amount);
                    Con.setData(Query);
                    ShowBills();
                    MessageBox.Show("Bill Added");
                }
            }
            catch (Exception Ex)
            {

                MessageBox.Show(Ex.Message);
            }
        }

        private void label11_Click(object sender, EventArgs e)
        {
            Members Obj = new Members();
            Obj.Show();
            this.Hide();
        }

        private void CancelBtn_Click(object sender, EventArgs e)
        {
            AmountTb.Text = "";
            MemberCb.SelectedIndex = -1;
        }

        private void label15_Click(object sender, EventArgs e)
        {
            Login Obj = new Login();
            Obj.Show();
            this.Hide();
        }
    }
}
