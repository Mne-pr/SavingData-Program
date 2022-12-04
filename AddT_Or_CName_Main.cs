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
    public partial class AddT_Or_CName_Main : Form
        //메인의 추가, 변경버튼에 사용
    {
        public AddT_Or_CName_Main()
        {
            InitializeComponent();
        }

        private void OK_Button_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
        }

        private void Cancel_Button_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            this.Close();
        }

        public void whatType(string sth)
        {
            if (sth.Equals("addTable"))
            {
                label1.Text = "생성할 테이블의 이름을 입력";
                this.Text = "테이블 추가";
            }
            else if (sth.Equals("changeTName"))
            {
                label1.Text = "변경할 이름을 입력";
                this.Text = "이름 변경";
            }
        }

        private void TableNameTBox_TextChanged(object sender, EventArgs e)
        {
            string text = TableNameTBox.Text;
            
            if (TableNameTBox.Text.Length > 0 && text[0]> 'A' && text[0] < 'z') 
            { 
                OK_Button.Enabled = true;
                toolStripStatusLabel1.Text = "입력한 이름으로 테이블을 생성합니다.";
            }
            else
            {
                OK_Button.Enabled = false;
                toolStripStatusLabel1.Text = "TIP - 공백, 첫글자가 숫자 -> 생성불가";
            }
        }
    }
}
