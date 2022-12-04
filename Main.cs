using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.Data.Common;
using System.Data.SQLite;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using ListView = System.Windows.Forms.ListView;

namespace Datas
{
    public partial class Main : Form
    {

        private SQLiteConnection conn = null;
        private SQLiteCommand cmd = null;
        private SQLiteDataReader rdr = null;
        private SQLiteDataReader rdr2 = null;
        private string tableName = null;
        private int isMain = 0;

        public Main()
        {
            InitializeComponent();
        }

        //초기화
        public void Main_Load(object sender, EventArgs e)
        {
            aboutMainStatus.Text = "초기화 중...";
            conn = new SQLiteConnection("DataSource=data.sqlite; Version = 3;");
            this.Location = new Point(100, 100);
            conn.Open();
            PrintSQLMain();
            listView1.FullRowSelect = true;
            aboutMainStatus.Text = "초기화 완료";
            //파일 없으면 알아서 생성하나봐,,
            //이 data.sqlite 파일에 데이터가 저장
        }
        //초기화 시 출력
        public void PrintSQLMain()
        {
            //테이블 목록 명령어 실행하여 rdr에 저장
            cmd = new SQLiteCommand(
                "SELECT name FROM sqlite_master WHERE type='table';"
                , conn);
            rdr = cmd.ExecuteReader();

            //출력하기 전 리스트뷰 정리
            listView1.Items.Clear();
            listView1.Columns.Clear();
            listView1.GridLines = true;
            listView1.View = View.Details;

            //열 추가, 크기조정(화면에 맞게)
            listView1.Columns.Add("테이블 목록");
            listView1.Columns[0].Width = -2;

            //rdr에서 순차적으로 읽어서 name 열의 데이터 출력
            while (rdr.Read())
            {
                string table = rdr["name"].ToString();
                aboutMainStatus.Text = "로딩중...(" + table + ")";
                ListViewItem lvi = new ListViewItem(table);
                listView1.Items.Add(lvi);
            }
            label1.Text = "Tables";

            aboutMainStatus.Text = "메인";
            rdr.Close();
        }
        //윈도우의 위치 탐지 - 의도가 사라졌다..
        private void Main_LocationChanged(object sender, EventArgs e)
        {
            aboutMainStatus.Text = Location.ToString() + " - 윈도우 이동";
        }

        //테이블 선택 (이동)
        private void listView1_MouseDoubleClick(object sender, EventArgs e)
        {
            if (isMain == 0)
            {
                PrintSQLSub();
            }
            else { }//서브윈도우에서 더블클릭하는 경우 - 처리없음
        }
        //Column간격 최적화
        private void resizeColumns()
        {
            //칼럼 간격 조정
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
        }
        //테이블 이동 시 출력
        public void PrintSQLSub() 
        {
            if (isMain == 0)
            {
                tableName = listView1.SelectedItems[0].Text;
                isMain = 1; label1.Text = tableName;

                MainButtonPanel.Hide();
                tableLayoutPanel2.Visible = true; tableLayoutPanel4.Visible = true;
            }

            int columns = 0;
            aboutMainStatus.Text = tableName + " 테이블로 이동";

            //해당 테이블의 Column 정보 가져오기
            cmd = new SQLiteCommand("PRAGMA table_info(" + tableName + ");", conn);
            rdr = cmd.ExecuteReader();

            //해당 테이블 출력하기
            cmd = new SQLiteCommand(
                "SELECT * FROM " + tableName + ";", conn);
            rdr2 = cmd.ExecuteReader();

            //클리어
            listView1.Items.Clear(); listView1.Columns.Clear(); 
            listView1.GridLines = true; listView1.View = View.Details;

            //행 추가
            while (rdr.Read())
            {
                listView1.Columns.Add(rdr["name"].ToString());
                columns++;
            }
            string[] strs = new string[columns];

            //데이터 추가
            while (rdr2.Read())
            {
                for (int i = 0; i < columns; i++)
                {
                    string temp = rdr2[i].ToString();
                    strs[i] = temp;
                }
                ListViewItem lvi = new ListViewItem(strs);
                listView1.Items.Add(lvi);
            }

            resizeColumns();
            rdr.Close();
            
        }

        //메인버튼 - 추가
        public void addTableBtn_Click(object sender, EventArgs e)
        {
            try
            {
                Main_AddT_Or_CName1 addTableWindow = new Main_AddT_Or_CName1();
                addTableWindow.Owner = this;
                addTableWindow.whatType("addTable");
                if (addTableWindow.ShowDialog(this) == DialogResult.OK)
                {
                    cmd = new SQLiteCommand("CREATE TABLE "
                        + (addTableWindow.TableNameTBox.Text).ToString() + " (DFT INTEGER);", conn);
                    cmd.ExecuteNonQuery();
                    PrintSQLMain();
                    this.aboutMainStatus.Text = addTableWindow.TableNameTBox.Text + " 테이블 생성";

                }
                else
                {
                    this.aboutMainStatus.Text = "테이블 추가 - 취소";
                }
            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message);
            }
        }
        //메인버튼 - 삭제
        private void delTableBtn_Click(object sender, EventArgs e)
        {
            if (listView1.SelectedIndices.Count <= 0)
            {
                aboutMainStatus.Text = "삭제할 항목을 선택하고 실행하십시오..";
            }
            else
            {
                try
                {
                    tableName = listView1.SelectedItems[0].Text;

                    Sub2_AskFinal AskFinal = new Sub2_AskFinal();
                    AskFinal.Owner = this;
                    AskFinal.SetLabel("테이블 삭제 - " + tableName);
                    if(AskFinal.ShowDialog() == DialogResult.OK)
                    {
                        cmd = new SQLiteCommand("DROP TABLE IF EXISTS " + tableName + ";", conn);
                        cmd.ExecuteNonQuery(); PrintSQLMain();
                        aboutMainStatus.Text = "테이블 " + tableName + " - 삭제됨";
                    }
                }
                catch (Exception err)
                {
                    MessageBox.Show(err.Message);
                }
            }
        }
        //메인버튼 - 이름변경
        private void changeTNameBtn_Click(object sender, EventArgs e)
        {
            if (listView1.SelectedIndices.Count <= 0)
            {
                aboutMainStatus.Text = "이름을 변경할 항목을 선택하고 실행하십시오..";
            }
            else
            {
                try
                {
                    tableName = listView1.SelectedItems[0].Text;
                    aboutMainStatus.Text = tableName;
                    Main_AddT_Or_CName1 chTableWindow = new Main_AddT_Or_CName1();
                    chTableWindow.whatType("changeTName");
                    chTableWindow.Owner = this;
                    if (chTableWindow.ShowDialog(this) == DialogResult.OK)
                    {
                        cmd = new SQLiteCommand("ALTER TABLE " + tableName + " RENAME TO " + chTableWindow.TableNameTBox.Text + ";", conn);
                        cmd.ExecuteNonQuery(); PrintSQLMain();
                        this.aboutMainStatus.Text = tableName + " -to-> " + chTableWindow.TableNameTBox.Text;
                    }
                    else
                    {
                        this.aboutMainStatus.Text = "이름 변경 - 취소";
                    }
                }
                catch (Exception err)
                {
                    MessageBox.Show(err.Message);
                }
            }

        }

     
        //업서브테이블버튼 - 메인으로
        private void toHomeBtn_Click(object sender, EventArgs e)
        {
            MainButtonPanel.Visible = true;
            tableLayoutPanel2.Visible = false;
            tableLayoutPanel4.Visible = false;
            PrintSQLMain();
            isMain = 0;
        }


        //서브테이블버튼 - 기준(Column) 추가
        private void addColumnBtn_Click(object sender, EventArgs e)
        {
            try
            {
                Sub_AddColumn addColumnWindow = new Sub_AddColumn();
                addColumnWindow.Owner = this;
                if (addColumnWindow.ShowDialog(this) == DialogResult.OK)
                {
                    cmd = new SQLiteCommand("ALTER TABLE " + tableName +
                        " ADD COLUMN " + (addColumnWindow.ColumnNameTBox.Text).ToString() +
                        " " + (addColumnWindow.TypeComboBox.SelectedItem).ToString() + ";", conn); ;
                    cmd.ExecuteNonQuery(); PrintSQLSub();
                    this.aboutMainStatus.Text = addColumnWindow.ColumnNameTBox.Text + " 기준(Column) 생성";

                }
            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message);
            }
        }
        //서브테이블버튼 - 기준(Column) 삭제
        private void delColumnBtn_Click(object sender, EventArgs e)
        {
            Sub_DelColumn DelColumnWindow = new Sub_DelColumn();
            DelColumnWindow.Owner = this;
            DelColumnWindow.SetItems(conn, tableName);
            //column들 출력해야지
            if (DelColumnWindow.ShowDialog(this) == DialogResult.OK)
            {
                //행 삭제하는 방법 - 해당 행을 제외한 부분을 다른 테이블에 옮기고 그 테이블을 삭제하는것..
                //이게말이되냐
                string columnsNameType = ""; string columnsName = "";
                cmd = new SQLiteCommand("PRAGMA table_info(" + tableName + ");", conn);
                rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    if (DelColumnWindow.ColumnComboBox.SelectedItem.ToString().Equals(rdr["name"])) continue;
                    columnsNameType = columnsNameType + rdr["name"] + " " + rdr["type"] + ", ";
                    columnsName = columnsName + rdr["name"] + ", ";
                }
                columnsNameType = columnsNameType.Substring(0, columnsNameType.Length - 2);
                columnsName = columnsName.Substring(0, columnsName.Length - 2);
                rdr.Close();

                cmd = new SQLiteCommand("CREATE TABLE temp1106 (" + columnsNameType + ");", conn); cmd.ExecuteNonQuery();
                cmd = new SQLiteCommand("INSERT INTO temp1106 (" + columnsName + ") SELECT " + columnsName + " FROM " + tableName + ";", conn); cmd.ExecuteNonQuery();
                cmd = new SQLiteCommand("DROP TABLE " + tableName + ";", conn); cmd.ExecuteNonQuery();
                cmd = new SQLiteCommand("ALTER TABLE temp1106 RENAME TO " + tableName + ";", conn); cmd.ExecuteNonQuery();

                PrintSQLSub();
                aboutMainStatus.Text = DelColumnWindow.ColumnComboBox.SelectedItem + " - 기준 삭제완료";
            }
        }

        private void listView1_ColumnClick(object sender, ColumnClickEventArgs e)
        {
            //정렬할거
        }
    }
}
