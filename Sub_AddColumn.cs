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
    public partial class Sub_AddColumn : Form
    {
        public Sub_AddColumn()
        {
            InitializeComponent();
        }
        private void Sub_AddColumn_Load(object sender, EventArgs e)
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
            DialogResult = DialogResult.Cancel;
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            string text = ColumnNameTBox.Text;

            if (ColumnNameTBox.Text.Length > 0 && text[0] > 'A' && text[0] < 'z')
            {
                OK_Button.Enabled = true;
                toolStripStatusLabel1.Text = "입력한 이름과 타입으로 기준을 생성합니다.";
            }
            else
            {
                OK_Button.Enabled = false;
                toolStripStatusLabel1.Text = "TIP - 공백, 첫글자가 숫자 -> 생성불가";
            }
        }

    }
}
