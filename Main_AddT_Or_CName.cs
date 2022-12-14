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
    public partial class Main_AddT_Or_CName : Form
    //메인의 추가, 변경버튼에 사용
    {
        string what = ""; int whatt = 0;

        public Main_AddT_Or_CName()
        {
            InitializeComponent();
        }
        private void Main_AddT_Or_CName1_Load(object sender, EventArgs e)
        {
            this.Location = new Point(Owner.Location.X + Owner.Width / 2 - this.Width / 2,
                Owner.Location.Y + Owner.Height / 2 - this.Height / 2);

        }

        private void OK_Button_Click(object sender, EventArgs e)
        {
            if (whatt == 1)
            {
                Sub2_AskFinal AskFinal = new Sub2_AskFinal();
                AskFinal.Owner = this;
                AskFinal.SetLabel(what + TableNameTBox.Text);
                if (AskFinal.ShowDialog() == DialogResult.OK)
                {
                    DialogResult = DialogResult.OK;
                }
                else
                {
                    Cancel_Button_Click(sender, e);
                }
            }
            else
            {
                DialogResult = DialogResult.OK;
            }

        }

        private void Cancel_Button_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }

        public void whatType(string sth, string tableName = "")
        {
            if (sth.Equals("addTable"))
            {
                label1.Text = "생성할 테이블의 이름을 입력";
                this.Text = "테이블 생성";
            }
            else if (sth.Equals("changeTName"))
            {
                label1.Text = "변경할 이름을 입력(" + tableName + ")";
                this.Text = "이름 변경";
                what = what + "이름 변경 - ";
                whatt = 1;
            }
        }

        private void TableNameTBox_TextChanged(object sender, EventArgs e)
        {
            string text = TableNameTBox.Text;

            if (TableNameTBox.Text.Length > 0 && text[0] > 'A' && text[0] < 'z')
            {
                OK_Button.Enabled = true;
                toolStripStatusLabel1.Text = "입력한 이름으로 테이블을 생성합니다.";
            }
            else
            {
                OK_Button.Enabled = false;
                toolStripStatusLabel1.Text = "이름의 첫 문자가 공백, 숫자인 경우는 생성할 수 없습니다!";
            }

            if (whatt == 0)
            {
                for (int i = 0; i < TableNameTBox.Text.Length; i++)
                {
                    if (text[i] <='0' || text[i] >= '9')
                    {
                        if (text[i] <= 'A' || text[i] >= 'z')
                        {
                            toolStripStatusLabel1.Text = "테이블 이름으로 알파벳 이외의 문자가 포함될 수 없습니다.";
                            OK_Button.Enabled = false;
                            break;
                        }
                    }

                }
            }
        }


    }
}
