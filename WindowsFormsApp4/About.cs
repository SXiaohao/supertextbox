using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CCWin;
using System.Windows.Forms;

namespace WindowsFormsApp4
{
    public partial class About : CCSkinMain
    {
        public About()
        {
            InitializeComponent();
        }

        private void skinLabel1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start("http://wpa.qq.com/msgrd?v=3&uin=820044908");
            
        }

        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start("http://www.hckj99.cn");
        }
    }
}
