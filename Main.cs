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
using System.Threading;
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
        private CheckBox[] checkBoxes= null;

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
            //
            listView1.DrawColumnHeader += new DrawListViewColumnHeaderEventHandler(drawListColumnHeader);
            listView1.DrawItem += new DrawListViewItemEventHandler(lv_DrawItem);
            listView1.DrawSubItem += new DrawListViewSubItemEventHandler(lv_DrawSubItem);
            listView1.ColumnClick += new ColumnClickEventHandler(lv_ColumnClick);
            //
            aboutMainStatus.Text = "초기화 완료";
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

            while (rdr.Read())
            { 
                string table = rdr["name"].ToString();
                ListViewItem lvi = new ListViewItem();
                lvi.SubItems.Add(table);
                listView1.Items.Add(lvi);
            }
            label1.Text = "Tables";

            //
            listView1.CheckBoxes = true;
            ColumnHeader headerCheck = new ColumnHeader();
            ColumnHeader header1 = new ColumnHeader();
            headerCheck.Text = null;
            header1.Text = "테이블 목록";
            listView1.Columns.AddRange(new ColumnHeader[] { headerCheck, header1 });
            resizeColumns();
            listView1.Columns[1].Width = -2;
            //

            aboutMainStatus.Text = "메인";
            rdr.Close();
        }
        //윈도우 위치 탐지
        private void Main_LocationChanged(object sender, EventArgs e)
        {
            aboutMainStatus.Text = Location.ToString() + " - 윈도우 이동";
        }
        //출력 시 checkbox 자동으로 렌더링 되도록
        private void drawListColumnHeader(object sender, DrawListViewColumnHeaderEventArgs e)
        {
            if (e.ColumnIndex == 0)
            {
                e.DrawBackground();
                bool value = false;
                try
                {
                    value = Convert.ToBoolean(e.Header.Tag);
                }
                catch (Exception)
                { }
                CheckBoxRenderer.DrawCheckBox(e.Graphics, new Point(e.Bounds.Left + 4, e.Bounds.Top + 4),
                    value ? System.Windows.Forms.VisualStyles.CheckBoxState.CheckedNormal :
                    System.Windows.Forms.VisualStyles.CheckBoxState.UncheckedNormal);
            }
            else e.DrawDefault = true;
        }
        private void lv_ColumnClick(object sender, ColumnClickEventArgs e)
        {
            if (e.Column == 0)
            {
                bool value = false;
                try
                {
                    value = Convert.ToBoolean(this.listView1.Columns[e.Column].Tag);
                }
                catch (Exception)
                {
                }
                this.listView1.Columns[e.Column].Tag = !value;
                foreach (ListViewItem item in this.listView1.Items) item.Checked = !value;
                this.listView1.Invalidate();
            }
        }
        private void lv_DrawItem(object sender, DrawListViewItemEventArgs e)
        {
            e.DrawDefault = true;
        }
        private void lv_DrawSubItem(object sender, DrawListViewSubItemEventArgs e)
        {
            e.DrawDefault = true;
        }
        

        //테이블 클릭 (선택)
        private void listView1_Click(object sender, EventArgs e)
        {
            try
            {
                int selectRow = listView1.SelectedItems[0].Index;
                if (listView1.Items[selectRow].Checked == false)
                {
                    listView1.Items[selectRow].Checked = true;
                }
                else
                {
                    listView1.Items[selectRow].Checked = false;
                }
            }
            catch (Exception) { }
        }
        //테이블 더블클릭 (이동)
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
                int selectRow = listView1.SelectedItems[0].Index;
                tableName = listView1.Items[selectRow].SubItems[1].Text;
                isMain = 1; label1.Text = tableName;

                MainButtonPanel.Hide();
                tableLayoutPanel2.Visible = true; tableLayoutPanel4.Visible = true;
            }

            aboutMainStatus.Text = tableName + " 테이블로 이동";
            //Column 개수 가져오기
            cmd = new SQLiteCommand("SELECT count(*) FROM pragma_table_info('" + tableName + "')", conn);
            rdr = cmd.ExecuteReader();
            rdr.Read();

            int columnAndCheckCount = (int)Convert.ToInt32(rdr["count(*)"].ToString()) + 1;
            if(columnAndCheckCount - 1 <= 1) { delColumnBtn.Enabled = false; }
            else { delColumnBtn.Enabled = true; }
            rdr.Close();

            //해당 테이블의 Column 정보 가져오기
            cmd = new SQLiteCommand("PRAGMA table_info(" + tableName + ");", conn);
            rdr = cmd.ExecuteReader();

            //해당 테이블 출력하기, rowid는 단지 자리를 만들기 위해..
            cmd = new SQLiteCommand("SELECT rowid, * FROM " + tableName + ";", conn);
            rdr2 = cmd.ExecuteReader();

            //클리어
            listView1.Items.Clear(); listView1.Columns.Clear(); 
            listView1.GridLines = true; listView1.View = View.Details;

            ColumnHeader[] columnList = new ColumnHeader[columnAndCheckCount+1];
            columnList[0] = new ColumnHeader { Text = "" };
            string[] strs = new string[columnAndCheckCount];

            //데이터 추가
            int rowCount = 1;
            while (rdr2.Read())
            {
                ListViewItem lviData = new ListViewItem();
                for (int i = 0; i < columnAndCheckCount; i++)
                {
                    if (i == 0)
                    {
                        lviData.SubItems.Add((rowCount++).ToString());
                    }
                    else
                    {
                        lviData.SubItems.Add(rdr2[i].ToString());
                    }
                }
                listView1.Items.Add(lviData);
            }

            int ind = 0;
            columnList[ind++] = new ColumnHeader { Text = "" };
            columnList[ind++] = new ColumnHeader { Text = "" };
            //행 확인
            while (rdr.Read()) 
            {
                ColumnHeader tempHeader = new ColumnHeader();
                tempHeader.Text = rdr["name"].ToString();

                columnList[ind++] = tempHeader;

            }
            listView1.Columns.AddRange(columnList);

            resizeColumns();
            rdr.Close(); rdr2.Close();
            
        }


        //메인버튼 - 추가
        public void addTableBtn_Click(object sender, EventArgs e)
        {
            aboutMainStatus.Text = "테이블 추가 - 클릭";
            try
            {
                Main_AddT_Or_CName addTableWindow = new Main_AddT_Or_CName();
                addTableWindow.Owner = this;
                addTableWindow.whatType("addTable");
                if (addTableWindow.ShowDialog(this) == DialogResult.OK)
                {
                    cmd = new SQLiteCommand("CREATE TABLE "
                        + (addTableWindow.TableNameTBox.Text).ToString() + " (DFTEXT TEXT);", conn);
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
            if (listView1.CheckedIndices.Count <= 0)
            {
                aboutMainStatus.Text = "삭제할 항목을 선택하고 실행하십시오..";
            }
            else
            {
                aboutMainStatus.Text = "테이블 삭제 - 클릭";
                try
                {
                    int seleced = listView1.CheckedIndices.Count;
                    int[] selectRow = new int[seleced]; int number = 0; int i = 0;
                    tableName = "";
                    foreach (ListViewItem item in listView1.Items)
                    {
                        if (item.Checked)
                        {
                            selectRow[number] = i;
                            tableName += item.SubItems[1].Text + ", ";
                            number++;
                        }
                        i++;
                    }
                    tableName = tableName.Substring(0, tableName.Length - 2);

                    Sub2_AskFinal AskFinal = new Sub2_AskFinal();
                    AskFinal.Owner = this;
                    AskFinal.SetLabel("테이블 삭제 - " + tableName);
                    if (AskFinal.ShowDialog() == DialogResult.OK)
                    {
                        string[] tableNames = tableName.Split(',');
                        foreach (string name in tableNames)
                        {
                            cmd = new SQLiteCommand("DROP TABLE IF EXISTS " + name + ";", conn);
                            cmd.ExecuteNonQuery();
                        }
                        
                        PrintSQLMain();
                        aboutMainStatus.Text = "테이블 " + tableName + " - 삭제됨";
                    }
                    else
                    {
                        aboutMainStatus.Text = "테이블 삭제 - 취소";
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
            if (listView1.SelectedIndices.Count != 1)
            {
                aboutMainStatus.Text = "이름을 변경할 항목을 선택하고 실행하십시오..";
            }
            else
            {
                aboutMainStatus.Text = "이름 변경 - 클릭";
                try
                {
                    int selectRow = listView1.SelectedItems[0].Index;
                    tableName = listView1.Items[selectRow].SubItems[1].Text;
                    Main_AddT_Or_CName chTableWindow = new Main_AddT_Or_CName();
                    chTableWindow.whatType("changeTName",tableName);
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

        //리스트뷰 - 해당 기준(Column)에 정렬 - 미완
        private void listView1_ColumnClick(object sender, ColumnClickEventArgs e)
        {

        }


        //서브테이블버튼 - 기준(Column) 추가
        private void addColumnBtn_Click(object sender, EventArgs e)
        {
            aboutMainStatus.Text = "기준 추가 - 클릭";
            try
            {
                Sub_AddColumn addColumnWindow = new Sub_AddColumn();
                addColumnWindow.Owner = this;
                if (addColumnWindow.ShowDialog(this) == DialogResult.OK)
                {
                    cmd = new SQLiteCommand("ALTER TABLE " + tableName +
                        " ADD COLUMN " + (addColumnWindow.ColumnNameTBox.Text).ToString() +
                        " " + (addColumnWindow.TypeComboBox.SelectedItem).ToString() + ";", conn);
                    cmd.ExecuteNonQuery(); PrintSQLSub();
                    this.aboutMainStatus.Text = addColumnWindow.ColumnNameTBox.Text + " 기준(Column) 생성";

                }
                else
                {
                    aboutMainStatus.Text = "기준 추가 - 취소";
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
            aboutMainStatus.Text = "기준 삭제 - 클릭";
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
            else
            {
                aboutMainStatus.Text = "기준 삭제 - 취소";
            }
        }
        //서브테이블버튼 - 데이터(Row) 추가
        private void addDataBtn_Click(object sender, EventArgs e)
        {
            aboutMainStatus.Text = "데이터 추가 - 클릭";
            Sub_AddData AddDataWindow = new Sub_AddData();
            AddDataWindow.Owner = this;
            AddDataWindow.FirstSet(conn, tableName); 

            if(AddDataWindow.ShowDialog() == DialogResult.OK)
            {
                cmd = new SQLiteCommand("SELECT ROWID FROM " + tableName + " WHERE " 
                    + AddDataWindow.columns[0] + " = '" + AddDataWindow.firstData + "';",conn);
                try
                {
                    rdr = cmd.ExecuteReader();
                    rdr.Read();
                    //읽어지면 이미 있다는것. 수정작업으로
                    cmd = new SQLiteCommand("REPLACE INTO " + tableName
                        + " (ROWID, " + AddDataWindow.columnEnum + ") VALUES ("
                        + rdr["rowid"] + ", " + AddDataWindow.dataEnum + ");", conn);
                    cmd.ExecuteNonQuery(); PrintSQLSub();
                    aboutMainStatus.Text = AddDataWindow.columns[0] + "-" + AddDataWindow.firstData + " : 행 갱신";
                }
                catch (Exception)
                {
                    try
                    {
                        //예외로 넘어와진다면 새로 추가만하면 됨
                        cmd = new SQLiteCommand("INSERT INTO " + tableName
                            + " (" + AddDataWindow.columnEnum + ") VALUES ("
                            + AddDataWindow.dataEnum + ");", conn);
                        cmd.ExecuteNonQuery(); PrintSQLSub();
                        aboutMainStatus.Text = AddDataWindow.columns[0] + "-" + AddDataWindow.firstData + " : 행 생성";
                    }
                    catch(Exception ex)
                    {
                        aboutMainStatus.Text = ex.ToString();
                    }
                }
                    
            }
            else
            {
                aboutMainStatus.Text = "데이터 추가 - 취소";
            }
        }
        //서브테이블버튼 - 데이터(Row) 삭제
        private void delDataBtn_Click(object sender, EventArgs e)
        {
            //첫번째 데이터(row번호 뒤!)를 기준으로 삭제함
            if (listView1.CheckedIndices.Count <= 0)
            {
                aboutMainStatus.Text = "삭제할 항목을 선택하고 실행하십시오..";
            }
            else
            {
                aboutMainStatus.Text = "데이터 삭제 - 클릭";
                try
                {
                    int seleced = listView1.CheckedIndices.Count;
                    int[] selectRow = new int[seleced]; int number = 0; int i = 0;
                    string delThingsList = "";
                    foreach (ListViewItem item in listView1.Items)
                    {
                        if (item.Checked)
                        {
                            selectRow[number] = i;
                            delThingsList += item.SubItems[2].Text + ",";
                            number++;
                        }
                        i++;
                    }
                    delThingsList = delThingsList.Substring(0, delThingsList.Length - 1);

                    cmd = new SQLiteCommand("PRAGMA table_info('" + tableName + "');",conn);
                    rdr = cmd.ExecuteReader(); rdr.Read();
                    string firstColumn = rdr["name"].ToString();

                    Sub2_AskFinal AskFinal = new Sub2_AskFinal();
                    AskFinal.Owner = this;
                    AskFinal.SetLabel("데이터 삭제");
                    if (AskFinal.ShowDialog() == DialogResult.OK)
                    {
                        string[] delThings = delThingsList.Split(',');
                        foreach (string name in delThings)
                        {
                            cmd = new SQLiteCommand("DELETE FROM " + tableName + " WHERE " + firstColumn + " = '" + name + "';", conn);
                            cmd.ExecuteNonQuery();
                            aboutMainStatus.Text = name + " 삭제";
                        }
                        PrintSQLSub();
                        aboutMainStatus.Text = "데이터 " + delThingsList + " - 삭제됨";
                    }
                    else
                    {
                        aboutMainStatus.Text = "데이터 삭제 - 취소";
                    }
                }
                catch (Exception err)
                {
                    MessageBox.Show(err.Message);
                }
            }
        }
        //서브테이블버튼 - 데이터(Row) 수정
        private void chDataBtn_Click(object sender, EventArgs e)
        {
            if (listView1.SelectedIndices.Count != 1)
            {
                aboutMainStatus.Text = "수정할 항목을 선택하고 실행하십시오..";
            }
            else
            {
                //이거 지금 데이터 추가에서 들고온거라 엉망진창임!!
                aboutMainStatus.Text = "데이터 수정 - 클릭";
                Sub_ChangeData changeDataWindow = new Sub_ChangeData();
                changeDataWindow.Owner = this;
                changeDataWindow.FirstSet(conn,tableName,listView1);

                if (changeDataWindow.ShowDialog() == DialogResult.OK)
                {
                    cmd = new SQLiteCommand("SELECT ROWID FROM '" + tableName + "' WHERE "
                    + changeDataWindow.columns[0] + " = '" + changeDataWindow.preFirstData + "';", conn);
                    try {
                        rdr = cmd.ExecuteReader();
                        rdr.Read();
                        string targetRow = rdr["rowid"].ToString();

                        cmd = new SQLiteCommand("SELECT ROWID, * FROM '" + tableName + "';", conn);
                        rdr = cmd.ExecuteReader();

                        //중복된 첫 열의 데이터가 있는지 확인
                        while (rdr.Read())
                        {
                            if (changeDataWindow.firstData.Equals(rdr[changeDataWindow.columns[0]]) &&
                                !(targetRow.Equals(rdr["rowid"].ToString())) )
                            {
                                throw new Exception("중복");
                            }
                        }

                        //수정작업
                        cmd = new SQLiteCommand("REPLACE INTO " + tableName
                            + " (ROWID, " + changeDataWindow.columnEnum + ") VALUES ("
                            + targetRow + ", " + changeDataWindow.dataEnum + ");", conn);
                        cmd.ExecuteNonQuery(); PrintSQLSub();
                        aboutMainStatus.Text = changeDataWindow.columns[0] + "-" + changeDataWindow.firstData + " : 행 갱신";
                    }
                    catch (Exception ent)
                    {
                        if (ent.Message.Equals("중복"))
                        {
                            aboutMainStatus.Text = "첫 열의 데이터는 중복될 수 없습니다!";
                        }
                        else { MessageBox.Show(ent.Message); }
                    }
                 }
                else
                {
                    aboutMainStatus.Text = "데이터 수정 - 취소";
                }
            }
        }
    }
}
