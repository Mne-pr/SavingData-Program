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
                //입력된 구간의 형태는 '왼쪽위의점위치,오른쪽아래의점위치', 각 구간은 공백으로 나뉘어짐 으로 가정 

                Bitmap allImage = new Bitmap(path); // 전체 이미지 가져옴
                Bitmap tempCropImage = null;

                string[] templocations = typeLabelPanel.Controls[0].Text.Split(' ');//구간별로 나눔
                
                for( int i=0;i< templocations.Length; i++)//구간별로 반복
                {
                    string[] tempLocationXYXY = templocations[i].Split('~',',');//XY 두 쌍으로 나눔
                    int[] tempRect = new int[4] { 0, 0, 0, 0 };
                    for(int j = 0; j < 4; j++)
                    {
                        tempRect[j] = (int)Convert.ToInt32(tempLocationXYXY[j]);
                    }
                    //분리완료
                    Rectangle boxRect = new Rectangle(tempRect[0], tempRect[1], tempRect[2] - tempRect[0], tempRect[3] - tempRect[1]);
                    tempCropImage = allImage.Clone(boxRect,allImage.PixelFormat);
                    //사진 자르기
                    getToOcr(tempCropImage, i);
                }
                dataEnum = dataEnum.Substring(0, dataEnum.Length - 2);
                columnEnum = columnEnum.Substring(0, columnEnum.Length - 2);
                toolStripStatusLabel1.Text = "추출된 데이터는 : " + dataEnum;
                DialogResult = DialogResult.OK;
            }
        }
        private void Cancel_Button_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }

        private void toOCRbtn_Click(object sender, EventArgs e)
        {
            typeOrOcr = 2;
            typeLabelPanel.Controls.Clear();
            typeLabelPanel.ColumnCount = 1;
            tableLayoutPanel1.RowStyles[3] = new RowStyle(SizeType.Absolute, tableLayoutPanel1.RowStyles[3].Height + 5);
            typeLabelPanel.Controls.Add(new TextBox { Text = "", Enabled = false, Anchor = AnchorStyles.Top, Width = 500, Height = 30, Margin = new Padding(0, 0, 0, 0) }); ; ; ;

            //일단 입력, ocr 버튼 없애고 큰 텍스트박스 하나랑 버튼 하나 만들기
            label2.Text = "추가할 파일 주소를 입력하고, 추출할 범위를 작성하세요";
            textBoxPanel.Controls.Clear();
            textBoxPanel.Controls.Add(new TextBox { Text = " ", Anchor = AnchorStyles.Top, Width=480 }, 0, 0);
            textBoxPanel.Controls.Add(new Button { Text = "...", Anchor = AnchorStyles.Top, Width = 40 }, 1, 0);
            textBoxPanel.ColumnCount = 2;
            //textBoxPanel.CellBorderStyle = TableLayoutPanelCellBorderStyle.Single;
            textBoxPanel.ColumnStyles[0] = new ColumnStyle(SizeType.Absolute, 480);
            textBoxPanel.ColumnStyles[1] = new ColumnStyle(SizeType.Absolute, 40);

            TextBox tempLocationBox = (TextBox)typeLabelPanel.GetControlFromPosition(0, 0);
            tempLocationBox.TextChanged += delegate
            {
                if (tempLocationBox.Text.Length > 0)
                {
                    OK_Button.Enabled = true;
                }
                else
                {
                    OK_Button.Enabled = false;
                }
                toolStripStatusLabel1.Text = "Tip : 왼쪽 위의 점 좌표~오른쪽 아래의 점 좌표, 점은 공백으로 분리";
            };

            Button tempBtn = (Button)textBoxPanel.GetControlFromPosition(1, 0);
            tempBtn.Click += delegate
            {
                openFileDia();
                textBoxPanel.Controls[0].Text = path;
            };

            TextBox tempImageLocationBox = (TextBox)textBoxPanel.GetControlFromPosition(0, 0);
            tempImageLocationBox.TextChanged += delegate
            {
                try
                {
                    //이미지 띄워서 범위 확인하게 하기
                    //이미지 크기가 너무 큰 경우는 아직 고려하지 않았지만 아이디어
                    //애초에 이미지 가로크기를 정해놓고 원래 이미지의 크기랑 비례식 세워서 입력받은 값에 처리해서 실제로 하기?
                    toolStripStatusLabel1.Text = "이미지 불러오기 성공";
                    Sub2_AddData_PrintImage imageWindow = new Sub2_AddData_PrintImage(tempImageLocationBox.Text,toolStripStatusLabel1);
                    imageWindow.Owner = this;
                    imageWindow.Show(this);
                    if(imageWindow.ifloadfail == 0)
                    {
                        typeLabelPanel.Controls[0].Enabled = true;
                    }
                    else
                    {
                        typeLabelPanel.Controls[0].Enabled = false;
                    }
                }
                catch(Exception ex)
                {
                    //자식윈도우로부터 throw받는 건 없을까? 일단 여기로 오지 않음
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

        private void getToOcr(Bitmap cropedImage, int i)
        {
            using (var engine = new TesseractEngine("./tessdata", "eng", EngineMode.Default))
            {
                using (var img = cropedImage)
                {
                    using (var page = engine.Process(img))
                    {
                        var text = page.GetText();
                        text = text.Substring(0,text.Length - 1);

                        columnEnum += columns[i] + ", ";
                        dataEnum += "'" + text + "', ";
                        if (i == 0)
                        {
                            firstData = text;
                        }

                        page.GetMeanConfidence().ToString();

                        //아래는 레퍼런스
                        //Console.WriteLine("Mean confidence: {0}", page.GetMeanConfidence());
                        //Console.WriteLine("Text (GetText): \r\n{0}", text);
                        //Console.WriteLine("Text (iterator):");
                    }
                }
            }
        }

    }
}
