using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SQLite;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Datas
{
    public partial class Sub_ChangeData : Form
    {
        public int columnCount = 0;
        public int rowCount = 0;
        public string[] columns = null;
        public string[] types = null;
        public string[] datas = null;
        public string[] listDatas = null;
        public string columnEnum = "";
        public string dataEnum = "";
        public string preFirstData = "";
        public string firstData = "";

        public Sub_ChangeData()
        {
            InitializeComponent();
        }

        private void Cancel_Button_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }

        private void OK_Button_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < columnCount; i++)
            {
                System.Windows.Forms.TextBox tempTxt = (System.Windows.Forms.TextBox)textBoxPanel.GetControlFromPosition(i, 0);
                if (tempTxt.Text.Length > 0)
                {
                    columnEnum += columns[i] + ", ";
                    dataEnum += "'" + tempTxt.Text + "', ";
                }
                if (i == 0)
                {
                    firstData = tempTxt.Text;
                }
            }
            columnEnum = columnEnum.Substring(0, columnEnum.Length - 2);
            dataEnum = dataEnum.Substring(0, dataEnum.Length - 2);
            DialogResult = DialogResult.OK;
        }

        private void Sub_ChangeData_Load(object sender, EventArgs e)
        {
            this.Location = new Point(Owner.Location.X + Owner.Width / 2 - this.Width / 2,
              Owner.Location.Y + Owner.Height / 2 - this.Height / 2);

            textBoxPanel.Controls.Clear(); labelsPanel.Controls.Clear(); typeLabelPanel.Controls.Clear();
            //LabelsPanel.CellBorderStyle = TableLayoutPanelCellBorderStyle.Single;

            for (int i = 0; i < columnCount; i++)
            {
                labelsPanel.Controls.Add(new Label { Text = columns[i], Anchor = AnchorStyles.Bottom, AutoSize = true }, i, 0);
                typeLabelPanel.Controls.Add(new Label { Text = types[i], Anchor = AnchorStyles.Top, AutoSize = true });
                textBoxPanel.Controls.Add(new System.Windows.Forms.TextBox { Text = listDatas[i], Anchor = AnchorStyles.Top, AutoSize = true }, i, 0);
                try
                {
                    labelsPanel.ColumnStyles[i] = new ColumnStyle(SizeType.Absolute, 530 / columnCount);
                }
                catch (Exception)
                {
                    labelsPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 530 / columnCount));
                    labelsPanel.ColumnCount = labelsPanel.ColumnStyles.Count;
                }
                try
                {
                    typeLabelPanel.ColumnStyles[i] = new ColumnStyle(SizeType.Absolute, 530 / columnCount);
                }
                catch (Exception)
                {
                    typeLabelPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 530 / columnCount));
                    typeLabelPanel.ColumnCount = typeLabelPanel.ColumnStyles.Count;
                }
                try
                {
                    textBoxPanel.ColumnStyles[i] = new ColumnStyle(SizeType.Absolute, 530 / columnCount);
                    
                }
                catch (Exception)
                {
                    textBoxPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 530 / columnCount));
                    textBoxPanel.ColumnCount = labelsPanel.ColumnStyles.Count;
                }

                //첫번째의 텍스트박스에 이벤트를 추가한 모습임!!!
                System.Windows.Forms.TextBox tempTxt = (System.Windows.Forms.TextBox)textBoxPanel.GetControlFromPosition(0, 0);
                tempTxt.TextChanged += delegate
                {
                    if (tempTxt.Text.Length > 0)
                    {
                        OK_Button.Enabled = true;
                        toolStripStatusLabel1.Text = "입력한 대로 데이터를 수정합니다.";
                    }
                    else
                    {
                        OK_Button.Enabled = false;
                        toolStripStatusLabel1.Text = "첫 번째 열은 데이터가 있어야 합니다.";
                    }
                };
                //레이아웃만 만들어진 상태
                //첫 번째는 무조건 입력받아야하는 것으로 설정함
            }
        }

        public void FirstSet(SQLiteConnection conn, string tableName, System.Windows.Forms.ListView listView1)
        {
            int selectRow = listView1.SelectedItems[0].Index;
            SQLiteCommand cmd = new SQLiteCommand("SELECT count(*) FROM pragma_table_info('" + tableName + "')", conn);
            SQLiteDataReader rdr = cmd.ExecuteReader(); rdr.Read();
            columnCount = (int)Convert.ToInt32(rdr["count(*)"].ToString());
            columns = new string[columnCount]; types = new string[columnCount]; listDatas = new string[columnCount];
            int t = 0; rdr.Close();

            cmd = new SQLiteCommand("SELECT count(*) FROM '" + tableName + "';", conn);
            rdr = cmd.ExecuteReader(); rdr.Read();
            rowCount = (int)Convert.ToInt32(rdr["count(*)"].ToString()); rdr.Close();

            cmd = new SQLiteCommand("PRAGMA table_info(" + tableName + ");", conn);
            rdr = cmd.ExecuteReader();
            while (rdr.Read())
            {
                columns[t] = rdr["name"].ToString();
                types[t++] = "(" + rdr["type"].ToString() + ")";
            }
            rdr.Close();
            //column, type의 이름들 다 불러옴, 크기도 함께

            for (int i = 0; i < columnCount; i++)
            {
                listDatas[i] = listView1.Items[selectRow].SubItems[i+2].Text;
                if (i == 0)
                    preFirstData = listDatas[0];
            }
            //이미 저장되어있는 데이터도 출력하기 위해
        }
    }
}
