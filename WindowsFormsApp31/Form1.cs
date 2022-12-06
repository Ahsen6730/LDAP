using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.DirectoryServices;
using System.DirectoryServices.AccountManagement;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LDAP
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string uid = "ControlUser";
            string pwd = "ControlPwd";

            using (var context = new PrincipalContext(ContextType.Domain, "ipadress", uid, pwd))
            {
                using (UserPrincipal user = new UserPrincipal(context))
                {
                    user.SamAccountName = textBox1.Text; //Check user name
                    using (var searcher = new PrincipalSearcher(user))
                    {
                        foreach (var result in searcher.FindAll())
                        {
                            DirectoryEntry entry = result.GetUnderlyingObject() as DirectoryEntry;

                            System.DirectoryServices.PropertyCollection props = entry.Properties;


                            foreach (string propName in props.PropertyNames)
                            {
                                if (entry.Properties[propName].Value != null)
                                {
                                    string _Yazi = entry.Properties[propName].PropertyName+ " = "+ entry.Properties[propName].Value.ToString();
                                    textBox2.Text += _Yazi;
                                    textBox2.Text += Environment.NewLine;

                                }
                                else
                                {
                                    string _Yazi = entry.Properties[propName].PropertyName +  " = NULL";
                                    textBox2.Text += _Yazi;
                                    textBox2.Text += Environment.NewLine;
                                }
                            }
                        
                        }
                    }
                }
            }
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
