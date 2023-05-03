using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace scss_gymmanagement
{
    public partial class Login : Form
    {
        Functions Con;
        public Login()
        {
            InitializeComponent();
            Con = new Functions();
        }
        public static int UserId;
        private void AdmLbl_Click(object sender, EventArgs e)
        {
            Receptionists Obj = new Receptionists();
            Obj.Show();
            this.Hide();
        }

        private void LoginBtn_Click(object sender, EventArgs e)
        {
            if(UNameTb.Text == "" || PasswordTb.Text == "")
            {
                MessageBox.Show("Missing information");
            } else
            {
                try
                {
                    string Query = "select * from ReceptionistTbl where RecepName = '{0}' and RecepPass = '{1}'";
                    Query = string.Format(Query, UNameTb.Text, PasswordTb.Text);
                    DataTable dt = Con.GetData(Query);
                    if(dt.Rows.Count == 0)
                    {
                        MessageBox.Show("Invalid Receptionist");
                    }else
                    {
                        UserId = Convert.ToInt32(dt.Rows[0][0].ToString());
                        Members Obj = new Members();
                        Obj.Show();
                        this.Hide();
                    }
                }
                catch (Exception Ex)
                {

                    MessageBox.Show(Ex.Message);
                }
            }
        }
    }
}
