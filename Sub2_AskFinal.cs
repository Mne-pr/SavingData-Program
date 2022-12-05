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
    public partial class Sub2_AskFinal : Form
    {
        public Sub2_AskFinal()
        {
            InitializeComponent();
        }

        public void SetLabel(string what)
        {
            label1.Text = "작업 : " + what;
        }
        private void Sub2_AskFinal_Load(object sender, EventArgs e)
        {
            this.Location = new Point(Owner.Location.X + Owner.Width / 2 - this.Width / 2,
                Owner.Location.Y + Owner.Height / 2 - this.Height / 2);
        }

        private void OK_Button_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
        }

        private void Cancel_Button_Click(object sender, EventArgs e)
        {
            DialogResult= DialogResult.Cancel;
        }
    }
}
