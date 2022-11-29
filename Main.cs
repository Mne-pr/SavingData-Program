using System;
using System.CodeDom;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SQLite;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Datas
{
    public partial class Main : Form
    {
        private SQLiteConnection conn = null;
        private SQLiteCommand cmd = null;
        private SQLiteDataReader rdr = null;

        public Main()
        {
            InitializeComponent();
        }

        private void Main_Load(object sender, EventArgs e)
        {
            try
            {
                
                toolStripStatusLabel1.Text = "초기화 중...";
                conn = new SQLiteConnection("DataSource=data.sqlite; Version = 3;");
                conn.Open();
            }
            catch (FileNotFoundException ee)//못 열은 예외 뭐잇지
            {//적용이 안되고잇음..
                //SQLiteConnection.CreateFile("data.sqlite");
                //conn = new SQLiteConnection("DataSource=data.sqlite; Version = 3;");
            }
            //이 data.sqlite 파일에 데이터가 저장되어있을 것
        }

        //클릭시 AddData 폼 나오도록 해서 데이터 받고 data.sqlite에 저장하게
        private void addData_Click(object sender, EventArgs e)
        {
            AddData frm = new AddData();
            frm.Show();
        }

        //클릭시 `폼 나오도록 해서 이름, 입력값들 입력해서 바로 반영


        //나중에 데이터 자동으로 갱신되게 바꿔야할것
        private void listView1_KeyDown(object sender, KeyEventArgs e)
        {
            conn.Open();
            cmd = new SQLiteCommand("SELECT SongName, Score FROM main;", conn);
            rdr = cmd.ExecuteReader();

            listView1.Items.Clear();
            listView1.Columns.Clear();
            listView1.GridLines = true;
            listView1.View = View.Details;

            listView1.Columns.Add("SongName");
            listView1.Columns.Add("Score");

            while (rdr.Read())
            {
                string SongName = rdr["SongName"].ToString();
                string Score = rdr["Score"].ToString();

                string[] strs = new string[] { SongName, Score };
                ListViewItem lvi = new ListViewItem(strs);
                listView1.Items.Add(lvi);
            }
            //연동하는

            listView1.AutoResizeColumns(ColumnHeaderAutoResizeStyle.ColumnContent);
            ListView.ColumnHeaderCollection cc = listView1.Columns;
            for (int i = 0; i < cc.Count; i++)
            {
                int colwidth = TextRenderer.MeasureText(cc[i].Text, listView1.Font).Width + 10;
                if (colwidth > cc[i].Width)
                {
                    cc[i].Width = colwidth;
                }
            }
            //자동 간격 조절하는
            rdr.Close();
            conn.Close();
        }

        private void tableLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        //현재 테이블에 행이나 열을 추가하거나 삭제하는 버튼 작게?
    }
}
