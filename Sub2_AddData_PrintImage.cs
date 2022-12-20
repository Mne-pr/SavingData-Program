using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Datas
{
    public partial class Sub2_AddData_PrintImage : Form
    {
        public string path = "";
        public int ifloadfail = 0;

        ToolStripStatusLabel ownerStatusLabel = null;
        public Sub2_AddData_PrintImage(string path,ToolStripStatusLabel ownerStatus)
        {
            this.path = path;
            ownerStatusLabel = ownerStatus;
            InitializeComponent();
        }

        private void Sub2_AddData_PrintImage_Load(object sender, EventArgs e)
        {
            try
            {
                Image img = new Bitmap(path);
                pictureBox1.Width = img.Width;
                pictureBox1.Height = img.Height;
                pointLabel.Top = pictureBox1.Height + 2;
                pointLabel.Width = 100;
                this.AutoSize = true;

                this.Location = new Point(Owner.Right + 2, Owner.Location.Y + Owner.Height / 2 - this.Height / 2);
                pictureBox1.Image = img;
                ifloadfail = 0;
            }
            catch(Exception ex) 
            {
                ifloadfail = 1;
                ownerStatusLabel.Text = ex.Message;
                this.Close();
            }
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            pointLabel.Text = "좌표 : " + e.X.ToString() + ", " + e.Y.ToString();
        }
    }
}
