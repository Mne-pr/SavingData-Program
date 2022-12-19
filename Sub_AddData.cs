using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SQLite;
using System.Drawing;
using System.Drawing.Printing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;
using Tesseract;

namespace Datas
{
    public partial class Sub_AddData : Form
    {
        private int columnConut = 0;
        public string[] columns = null;
        public string[] types = null;
        public string columnEnum = "";
        public string dataEnum = "";
        public string firstData = "";
        public string path="";
        public int typeOrOcr = 0;
        public string point = "";

        public Sub_AddData()
        {
            InitializeComponent();
        }

        private void Sub_AddData_Load(object sender, EventArgs e)
        {
            this.Location = new Point(Owner.Location.X + Owner.Width / 2 - this.Width / 2,
               Owner.Location.Y + Owner.Height / 2 - this.Height / 2);
        }

        public void FirstSet(SQLiteConnection conn, string tableName)
        {
            SQLiteCommand cmd = new SQLiteCommand("SELECT count(*) FROM pragma_table_info('" + tableName + "')", conn);
            SQLiteDataReader rdr = cmd.ExecuteReader();
            rdr.Read();
            columnConut = (int)Convert.ToInt32(rdr["count(*)"].ToString());
            columns = new string[columnConut]; types = new string[columnConut];
            int t = 0; rdr.Close();

            cmd = new SQLiteCommand("PRAGMA table_info(" + tableName + ");", conn);
            rdr = cmd.ExecuteReader();
            while (rdr.Read())
            {
                columns[t] = rdr["name"].ToString();
                types[t++] = "(" + rdr["type"].ToString() + ")";
            }
            rdr.Close();
            //column의 이름들 다 불러옴, 크기도 함께
        }

        private void toTypeBtn_Click(object sender, EventArgs e)
        {
            typeOrOcr = 1;
            textBoxPanel.Controls.Clear(); labelsPanel.Controls.Clear(); typeLabelPanel.Controls.Clear();
            //LabelsPanel.CellBorderStyle = TableLayoutPanelCellBorderStyle.Single;

            for (int i = 0; i < columnConut; i++)
            {
                labelsPanel.Controls.Add(new Label { Text = columns[i], Anchor = AnchorStyles.Bottom, AutoSize = true }, i, 0);
                typeLabelPanel.Controls.Add(new Label { Text = types[i], Anchor = AnchorStyles.Top, AutoSize = true });
                textBoxPanel.Controls.Add(new TextBox { Anchor = AnchorStyles.Top, AutoSize = true }, i, 0);
                try
                {
                    labelsPanel.ColumnStyles[i] = new ColumnStyle(SizeType.Absolute, 530 / columnConut);
                }
                catch (Exception)
                {
                    labelsPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 530 / columnConut));
                    labelsPanel.ColumnCount = labelsPanel.ColumnStyles.Count;
                }
                try
                {
                    typeLabelPanel.ColumnStyles[i] = new ColumnStyle(SizeType.Absolute, 530 / columnConut);
                }
                catch (Exception)
                {
                    typeLabelPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 530 / columnConut));
                    typeLabelPanel.ColumnCount = typeLabelPanel.ColumnStyles.Count;
                }
                try
                {
                    textBoxPanel.ColumnStyles[i] = new ColumnStyle(SizeType.Absolute, 530 / columnConut);
                }
                catch (Exception)
                {
                    textBoxPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 530 / columnConut));
                    textBoxPanel.ColumnCount = labelsPanel.ColumnStyles.Count;               
                }

                //첫번째의 텍스트박스에 이벤트를 추가한 모습임!!!
                TextBox tempTxt = (TextBox)textBoxPanel.GetControlFromPosition(0,0);
                tempTxt.TextChanged += delegate
                {
                    if(tempTxt.Text.Length > 0)
                    {
                        OK_Button.Enabled = true;
                    }
                    else
                    {
                        OK_Button.Enabled = false;
                    }
                };
                //레이아웃만 만들어진 상태
                //첫 번째는 무조건 입력받아야하는 것으로 설정함
            }

        }

        private void OK_Button_Click(object sender, EventArgs e)
        {
            if(typeOrOcr == 1)
            {
                for (int i = 0; i < columnConut; i++)
                {
                    TextBox tempTxt = (TextBox)textBoxPanel.GetControlFromPosition(i, 0);
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
            else if(typeOrOcr == 2)
            {
                //이제 입력한 갯수대로 쪼개고 인식하고를 반복해서 값을 알아낸다. 해야할것
                getToOcr(point);
            }
        }
        private void Cancel_Button_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }

        private void toOCRbtn_Click(object sender, EventArgs e)
        {
            typeOrOcr = 2;
            typeLabelPanel.ColumnCount = 1;
            typeLabelPanel.Controls.Add(new TextBox { Text = "", Anchor = AnchorStyles.Top, Width = 500, Margin = new Padding(0,0,0,0) });

            //일단 입력, ocr 버튼 없애고 큰 텍스트박스 하나랑 버튼 하나 만들기
            label2.Text = "추가할 파일 주소를 입력하고, 추출할 범위를 작성하세요";
            textBoxPanel.Controls.Clear();
            textBoxPanel.Controls.Add(new TextBox { Text = " ", Anchor = AnchorStyles.Top, Width=480 }, 0, 0);
            textBoxPanel.Controls.Add(new Button { Text = "...", Anchor = AnchorStyles.Top, Width = 40 }, 1, 0);
            textBoxPanel.ColumnCount = 2;
            //textBoxPanel.CellBorderStyle = TableLayoutPanelCellBorderStyle.Single;
            textBoxPanel.ColumnStyles[0] = new ColumnStyle(SizeType.Absolute, 480);
            textBoxPanel.ColumnStyles[1] = new ColumnStyle(SizeType.Absolute, 40);

            Button tempBtn = (Button)textBoxPanel.GetControlFromPosition(1, 0);
            tempBtn.Click += delegate
            {
                openFileDia();
                textBoxPanel.Controls[0].Text = path;
            };

            TextBox tempTbx = (TextBox)textBoxPanel.GetControlFromPosition(0, 0);
            tempTbx.TextChanged += delegate
            {
                try
                {
                    //이미지 띄워서 범위 확인하게 하기
                    toolStripStatusLabel1.Text = "이미지 불러오기 성공";
                    Sub2_AddData_PrintImage imageWindow = new Sub2_AddData_PrintImage(tempTbx.Text,toolStripStatusLabel1);
                    imageWindow.Owner = this;
                    imageWindow.Show(this);
                }
                catch(Exception ex)
                {
                    toolStripStatusLabel1.Text = ex.Message.ToString();
                }
            };
        }

        private void openFileDia()
        {
            OpenFileDialog openFileD = new OpenFileDialog();

            openFileD.Filter = "사진 파일 (*.jpg) |*.jpg";
            openFileD.ShowDialog();

            path = openFileD.FileName;
        }

        private void getToOcr(string point)
        {
            using (var engine = new TesseractEngine("./tessdata", "eng", EngineMode.Default))
            {
                using (var img = Pix.LoadFromFile(path))
                {
                    using (var page = engine.Process(img))
                    {
                        var text = page.GetText();
                        Console.WriteLine("Mean confidence: {0}", page.GetMeanConfidence());

                        Console.WriteLine("Text (GetText): \r\n{0}", text);
                        Console.WriteLine("Text (iterator):");
                    }
                }
            }
        }
    }
}
