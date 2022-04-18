using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OOP_tiles
{
    public partial class FormSetValues : Form
    {
        public FormSetValues()
        {
            InitializeComponent();
            tbHeight.KeyPress += Validation;
            tbWidth.KeyPress += Validation;
        }

        public int rectWidth = 0;
        public int rectHeight = 0;

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(tbHeight.Text) || String.IsNullOrEmpty(tbWidth.Text)) return;

            var height = Convert.ToInt32(tbHeight.Text);
            var width = Convert.ToInt32(tbWidth.Text);

            rectWidth = width;
            rectHeight = height;
            this.Close();
        }

        private void Validation(object sender, KeyPressEventArgs e)
        {
            var Sender = ((TextBox)sender);

            if ((e.KeyChar < 48 || e.KeyChar > 57) && e.KeyChar != '\b')
            {
                e.Handled = true;
            }
        }

    }
}
