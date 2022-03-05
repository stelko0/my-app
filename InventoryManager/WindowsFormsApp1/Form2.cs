using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class About : Form
    {
        public About()
        {
            InitializeComponent();

            // Взима сегашната година
            DateTime myDateTime = DateTime.Now;

            // Запазваме сегашната година като string(текст)
            string year = myDateTime.Year.ToString();

            // Поставяме годината в полето
            label2.Text = "© Stelian Hristov. All rights reserved. 2021-" + year ;


            // Забранява на потребителя да оразмерява прозореца на програмата
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            // Бутона [ОК] затваря прозореца About Inventory Manager
            this.Close();
        }

        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start("https://www.google.com/docs/about/");
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }
    }
}
