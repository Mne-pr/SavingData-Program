namespace Datas
{
    partial class Main
    {
        /// <summary>
        /// 필수 디자이너 변수입니다.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 사용 중인 모든 리소스를 정리합니다.
        /// </summary>
        /// <param name="disposing">관리되는 리소스를 삭제해야 하면 true이고, 그렇지 않으면 false입니다.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form 디자이너에서 생성한 코드

        /// <summary>
        /// 디자이너 지원에 필요한 메서드입니다. 
        /// 이 메서드의 내용을 코드 편집기로 수정하지 마세요.
        /// </summary>
        private void InitializeComponent()
        {
            this.addTableBtn = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.MainPanel = new System.Windows.Forms.TableLayoutPanel();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.aboutMainStatus = new System.Windows.Forms.ToolStripStatusLabel();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.toHomeBtn = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.button4 = new System.Windows.Forms.Button();
            this.tableLayoutPanel4 = new System.Windows.Forms.TableLayoutPanel();
            this.addColumnBtn = new System.Windows.Forms.Button();
            this.delColumnBtn = new System.Windows.Forms.Button();
            this.addDataBtn = new System.Windows.Forms.Button();
            this.delDataBtn = new System.Windows.Forms.Button();
            this.button10 = new System.Windows.Forms.Button();
            this.chDataBtn = new System.Windows.Forms.Button();
            this.MainButtonPanel = new System.Windows.Forms.TableLayoutPanel();
            this.changeTNameBtn = new System.Windows.Forms.Button();
            this.delTableBtn = new System.Windows.Forms.Button();
            this.listView1 = new System.Windows.Forms.ListView();
            this.MainPanel.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.tableLayoutPanel4.SuspendLayout();
            this.MainButtonPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // addTableBtn
            // 
            this.addTableBtn.Location = new System.Drawing.Point(0, 0);
            this.addTableBtn.Margin = new System.Windows.Forms.Padding(0);
            this.addTableBtn.Name = "addTableBtn";
            this.addTableBtn.Size = new System.Drawing.Size(105, 45);
            this.addTableBtn.TabIndex = 2;
            this.addTableBtn.Text = "테이블 추가";
            this.addTableBtn.UseVisualStyleBackColor = true;
            this.addTableBtn.Click += new System.EventHandler(this.addTableBtn_Click);
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("HY견고딕", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label1.ImageAlign = System.Drawing.ContentAlignment.TopLeft;
            this.label1.Location = new System.Drawing.Point(47, 12);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(61, 15);
            this.label1.TabIndex = 1;
            this.label1.Text = "Tables";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // MainPanel
            // 
            this.MainPanel.AutoSize = true;
            this.MainPanel.ColumnCount = 3;
            this.MainPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 111F));
            this.MainPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 654F));
            this.MainPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 111F));
            this.MainPanel.Controls.Add(this.label1, 0, 0);
            this.MainPanel.Controls.Add(this.statusStrip1, 0, 2);
            this.MainPanel.Controls.Add(this.tableLayoutPanel2, 1, 0);
            this.MainPanel.Controls.Add(this.tableLayoutPanel4, 2, 1);
            this.MainPanel.Controls.Add(this.MainButtonPanel, 0, 1);
            this.MainPanel.Controls.Add(this.listView1, 1, 1);
            this.MainPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.MainPanel.Location = new System.Drawing.Point(0, 0);
            this.MainPanel.Name = "MainPanel";
            this.MainPanel.RowCount = 3;
            this.MainPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 27F));
            this.MainPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 444F));
            this.MainPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 12F));
            this.MainPanel.Size = new System.Drawing.Size(876, 494);
            this.MainPanel.TabIndex = 0;
            // 
            // statusStrip1
            // 
            this.MainPanel.SetColumnSpan(this.statusStrip1, 3);
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.aboutMainStatus});
            this.statusStrip1.Location = new System.Drawing.Point(0, 472);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(876, 22);
            this.statusStrip1.TabIndex = 5;
            // 
            // aboutMainStatus
            // 
            this.aboutMainStatus.Name = "aboutMainStatus";
            this.aboutMainStatus.Size = new System.Drawing.Size(31, 17);
            this.aboutMainStatus.Text = "상태";
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 6;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 142F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 125F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 21F));
            this.tableLayoutPanel2.Controls.Add(this.toHomeBtn, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.button2, 1, 0);
            this.tableLayoutPanel2.Controls.Add(this.button3, 2, 0);
            this.tableLayoutPanel2.Controls.Add(this.button4, 3, 0);
            this.tableLayoutPanel2.Location = new System.Drawing.Point(114, 3);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 1;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 21F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 21F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 21F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 21F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 21F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 21F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 21F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 21F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 21F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 21F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(492, 21);
            this.tableLayoutPanel2.TabIndex = 8;
            this.tableLayoutPanel2.Visible = false;
            // 
            // toHomeBtn
            // 
            this.toHomeBtn.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.toHomeBtn.Location = new System.Drawing.Point(0, 0);
            this.toHomeBtn.Margin = new System.Windows.Forms.Padding(0);
            this.toHomeBtn.Name = "toHomeBtn";
            this.toHomeBtn.Size = new System.Drawing.Size(92, 21);
            this.toHomeBtn.TabIndex = 0;
            this.toHomeBtn.Text = "목록으로";
            this.toHomeBtn.UseVisualStyleBackColor = true;
            this.toHomeBtn.Click += new System.EventHandler(this.toHomeBtn_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(92, 0);
            this.button2.Margin = new System.Windows.Forms.Padding(0);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(92, 21);
            this.button2.TabIndex = 1;
            this.button2.Text = "뒤로-미완";
            this.button2.UseVisualStyleBackColor = true;
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(184, 0);
            this.button3.Margin = new System.Windows.Forms.Padding(0);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(131, 21);
            this.button3.TabIndex = 2;
            this.button3.Text = "뒤로가기 시점 -미완";
            this.button3.UseVisualStyleBackColor = true;
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(326, 0);
            this.button4.Margin = new System.Windows.Forms.Padding(0);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(75, 21);
            this.button4.TabIndex = 3;
            this.button4.Text = "저장-미완";
            this.button4.UseVisualStyleBackColor = true;
            // 
            // tableLayoutPanel4
            // 
            this.tableLayoutPanel4.ColumnCount = 1;
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel4.Controls.Add(this.addColumnBtn, 0, 0);
            this.tableLayoutPanel4.Controls.Add(this.delColumnBtn, 0, 1);
            this.tableLayoutPanel4.Controls.Add(this.addDataBtn, 0, 2);
            this.tableLayoutPanel4.Controls.Add(this.delDataBtn, 0, 3);
            this.tableLayoutPanel4.Controls.Add(this.button10, 0, 5);
            this.tableLayoutPanel4.Controls.Add(this.chDataBtn, 0, 4);
            this.tableLayoutPanel4.Location = new System.Drawing.Point(768, 30);
            this.tableLayoutPanel4.Name = "tableLayoutPanel4";
            this.tableLayoutPanel4.RowCount = 9;
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 47F));
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 47F));
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 47F));
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 47F));
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 47F));
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 47F));
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 47F));
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 47F));
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel4.Size = new System.Drawing.Size(105, 438);
            this.tableLayoutPanel4.TabIndex = 10;
            this.tableLayoutPanel4.Visible = false;
            // 
            // addColumnBtn
            // 
            this.addColumnBtn.Location = new System.Drawing.Point(0, 0);
            this.addColumnBtn.Margin = new System.Windows.Forms.Padding(0);
            this.addColumnBtn.Name = "addColumnBtn";
            this.addColumnBtn.Size = new System.Drawing.Size(105, 45);
            this.addColumnBtn.TabIndex = 0;
            this.addColumnBtn.Text = "기준 추가";
            this.addColumnBtn.UseVisualStyleBackColor = true;
            this.addColumnBtn.Click += new System.EventHandler(this.addColumnBtn_Click);
            // 
            // delColumnBtn
            // 
            this.delColumnBtn.Location = new System.Drawing.Point(0, 47);
            this.delColumnBtn.Margin = new System.Windows.Forms.Padding(0);
            this.delColumnBtn.Name = "delColumnBtn";
            this.delColumnBtn.Size = new System.Drawing.Size(105, 45);
            this.delColumnBtn.TabIndex = 1;
            this.delColumnBtn.Text = "기준 삭제";
            this.delColumnBtn.UseVisualStyleBackColor = true;
            this.delColumnBtn.Click += new System.EventHandler(this.delColumnBtn_Click);
            // 
            // addDataBtn
            // 
            this.addDataBtn.Location = new System.Drawing.Point(0, 94);
            this.addDataBtn.Margin = new System.Windows.Forms.Padding(0);
            this.addDataBtn.Name = "addDataBtn";
            this.addDataBtn.Size = new System.Drawing.Size(105, 45);
            this.addDataBtn.TabIndex = 2;
            this.addDataBtn.Text = "데이터 추가";
            this.addDataBtn.UseVisualStyleBackColor = true;
            this.addDataBtn.Click += new System.EventHandler(this.addDataBtn_Click);
            // 
            // delDataBtn
            // 
            this.delDataBtn.Location = new System.Drawing.Point(0, 141);
            this.delDataBtn.Margin = new System.Windows.Forms.Padding(0);
            this.delDataBtn.Name = "delDataBtn";
            this.delDataBtn.Size = new System.Drawing.Size(105, 45);
            this.delDataBtn.TabIndex = 3;
            this.delDataBtn.Text = "데이터 삭제";
            this.delDataBtn.UseVisualStyleBackColor = true;
            this.delDataBtn.Click += new System.EventHandler(this.delDataBtn_Click);
            // 
            // button10
            // 
            this.button10.Location = new System.Drawing.Point(3, 238);
            this.button10.Name = "button10";
            this.button10.Size = new System.Drawing.Size(99, 41);
            this.button10.TabIndex = 5;
            this.button10.Text = "button10";
            this.button10.UseVisualStyleBackColor = true;
            // 
            // chDataBtn
            // 
            this.chDataBtn.Location = new System.Drawing.Point(0, 188);
            this.chDataBtn.Margin = new System.Windows.Forms.Padding(0);
            this.chDataBtn.Name = "chDataBtn";
            this.chDataBtn.Size = new System.Drawing.Size(105, 45);
            this.chDataBtn.TabIndex = 4;
            this.chDataBtn.Text = "데이터 수정";
            this.chDataBtn.UseVisualStyleBackColor = true;
            this.chDataBtn.Click += new System.EventHandler(this.chDataBtn_Click);
            // 
            // MainButtonPanel
            // 
            this.MainButtonPanel.ColumnCount = 1;
            this.MainButtonPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.MainButtonPanel.Controls.Add(this.addTableBtn, 0, 0);
            this.MainButtonPanel.Controls.Add(this.changeTNameBtn, 0, 2);
            this.MainButtonPanel.Controls.Add(this.delTableBtn, 0, 1);
            this.MainButtonPanel.Location = new System.Drawing.Point(3, 30);
            this.MainButtonPanel.Name = "MainButtonPanel";
            this.MainButtonPanel.RowCount = 6;
            this.MainButtonPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 47F));
            this.MainButtonPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 47F));
            this.MainButtonPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 47F));
            this.MainButtonPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.MainButtonPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.MainButtonPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.MainButtonPanel.Size = new System.Drawing.Size(105, 438);
            this.MainButtonPanel.TabIndex = 9;
            // 
            // changeTNameBtn
            // 
            this.changeTNameBtn.Location = new System.Drawing.Point(0, 94);
            this.changeTNameBtn.Margin = new System.Windows.Forms.Padding(0);
            this.changeTNameBtn.Name = "changeTNameBtn";
            this.changeTNameBtn.Size = new System.Drawing.Size(105, 45);
            this.changeTNameBtn.TabIndex = 7;
            this.changeTNameBtn.Text = "이름 변경";
            this.changeTNameBtn.UseVisualStyleBackColor = true;
            this.changeTNameBtn.Click += new System.EventHandler(this.changeTNameBtn_Click);
            // 
            // delTableBtn
            // 
            this.delTableBtn.Location = new System.Drawing.Point(0, 47);
            this.delTableBtn.Margin = new System.Windows.Forms.Padding(0);
            this.delTableBtn.Name = "delTableBtn";
            this.delTableBtn.Size = new System.Drawing.Size(105, 45);
            this.delTableBtn.TabIndex = 6;
            this.delTableBtn.Text = "테이블 삭제";
            this.delTableBtn.UseVisualStyleBackColor = true;
            this.delTableBtn.Click += new System.EventHandler(this.delTableBtn_Click);
            // 
            // listView1
            // 
            this.listView1.HideSelection = false;
            this.listView1.Location = new System.Drawing.Point(111, 31);
            this.listView1.Margin = new System.Windows.Forms.Padding(0, 4, 0, 0);
            this.listView1.Name = "listView1";
            this.listView1.OwnerDraw = true;
            this.listView1.Size = new System.Drawing.Size(654, 440);
            this.listView1.TabIndex = 3;
            this.listView1.UseCompatibleStateImageBehavior = false;
            this.listView1.View = System.Windows.Forms.View.Details;
            this.listView1.ColumnClick += new System.Windows.Forms.ColumnClickEventHandler(this.listView1_ColumnClick);
            this.listView1.Click += new System.EventHandler(this.listView1_Click);
            this.listView1.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.listView1_MouseDoubleClick);
            // 
            // Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(876, 494);
            this.Controls.Add(this.MainPanel);
            this.Location = new System.Drawing.Point(100, 100);
            this.Name = "Main";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Main_Load);
            this.LocationChanged += new System.EventHandler(this.Main_LocationChanged);
            this.MainPanel.ResumeLayout(false);
            this.MainPanel.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel4.ResumeLayout(false);
            this.MainButtonPanel.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button addTableBtn;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TableLayoutPanel MainPanel;
        private System.Windows.Forms.ListView listView1;
        private System.Windows.Forms.StatusStrip statusStrip1;
        public System.Windows.Forms.ToolStripStatusLabel aboutMainStatus;
        private System.Windows.Forms.Button delTableBtn;
        private System.Windows.Forms.Button changeTNameBtn;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel4;
        private System.Windows.Forms.TableLayoutPanel MainButtonPanel;
        private System.Windows.Forms.Button toHomeBtn;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.Button addColumnBtn;
        private System.Windows.Forms.Button delColumnBtn;
        private System.Windows.Forms.Button addDataBtn;
        private System.Windows.Forms.Button delDataBtn;
        private System.Windows.Forms.Button chDataBtn;
        private System.Windows.Forms.Button button10;
    }
}

