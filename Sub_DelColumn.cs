using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SQLite;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Datas
{

    public partial class Sub_DelColumn : Form
    {
        public Sub_DelColumn()
        {
            InitializeComponent();
        }
        private void Sub_DelColumn_Load(object sender, EventArgs e)
        {
            this.Location = new Point(Owner.Location.X + Owner.Width/2 - this.Width/2,
                Owner.Location.Y + Owner.Height/2 - this.Height/2);   
        }

        private void ColumnComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            OK_Button.Enabled = true;
        }


        public void SetItems(SQLiteConnection conn, string tableName)
        {
            //해당 테이블의 Column 정보 가져오기
            SQLiteCommand cmd = new SQLiteCommand("PRAGMA table_info(" + tableName + ");", conn);
            SQLiteDataReader rdr = cmd.ExecuteReader();

            //콤보박스 초기화
            ColumnComboBox.Items.Clear();

            //콤보박스에 Column 이름 추가
            while (rdr.Read())
            {
                ColumnComboBox.Items.Add(rdr["name"].ToString());
            }

            rdr.Close();
        }


        private void OK_Button_Click(object sender, EventArgs e)
        {
            Sub2_AskFinal AskFinalWindow = new Sub2_AskFinal();
            AskFinalWindow.Owner = this;
            AskFinalWindow.SetLabel("기준 삭제 - " + ColumnComboBox.SelectedItem.ToString());
            if(AskFinalWindow.ShowDialog() == DialogResult.OK)
            {
                this.DialogResult = DialogResult.OK;
            }
            else
            {
                Cancel_Button_Click(sender, e);
            }
        }

        private void Cancel_Button_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }

    }

}
