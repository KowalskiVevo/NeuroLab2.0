namespace Neuron
{
    partial class RegressionForm
    {
        /// <summary>
        /// Требуется переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Обязательный метод для поддержки конструктора - не изменяйте
        /// содержимое данного метода при помощи редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.Container = new System.Windows.Forms.SplitContainer();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.button2 = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.label8 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.ZN = new System.Windows.Forms.TextBox();
            this.Z0 = new System.Windows.Forms.TextBox();
            this.PointCountZ = new System.Windows.Forms.TextBox();
            this.ShiftValue = new System.Windows.Forms.TextBox();
            this.PointCountX = new System.Windows.Forms.TextBox();
            this.XN = new System.Windows.Forms.TextBox();
            this.X0 = new System.Windows.Forms.TextBox();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.функцияToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.построитьПоВыборкеToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.нейроннаяСетьToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.построитьToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.обучитьToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.фильтрГрафиковToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.SourceFunction = new System.Windows.Forms.ToolStripMenuItem();
            this.RandomFunction = new System.Windows.Forms.ToolStripMenuItem();
            this.NetOutput = new System.Windows.Forms.ToolStripMenuItem();
            this.label10 = new System.Windows.Forms.Label();
            this.FunctionTxt = new System.Windows.Forms.TextBox();
            this.DrawerContainer = new System.Windows.Forms.SplitContainer();
            this.StudyProgress = new System.Windows.Forms.ProgressBar();
            this.Container.Panel1.SuspendLayout();
            this.Container.Panel2.SuspendLayout();
            this.Container.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.DrawerContainer.Panel2.SuspendLayout();
            this.DrawerContainer.SuspendLayout();
            this.SuspendLayout();
            // 
            // Container
            // 
            this.Container.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Container.Location = new System.Drawing.Point(0, 0);
            this.Container.Name = "Container";
            this.Container.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // Container.Panel1
            // 
            this.Container.Panel1.Controls.Add(this.groupBox1);
            this.Container.Panel1.Controls.Add(this.menuStrip1);
            this.Container.Panel1.Controls.Add(this.label10);
            this.Container.Panel1.Controls.Add(this.FunctionTxt);
            // 
            // Container.Panel2
            // 
            this.Container.Panel2.Controls.Add(this.DrawerContainer);
            this.Container.Size = new System.Drawing.Size(628, 582);
            this.Container.SplitterDistance = 187;
            this.Container.TabIndex = 0;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.button2);
            this.groupBox1.Controls.Add(this.button1);
            this.groupBox1.Controls.Add(this.label8);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label9);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.ZN);
            this.groupBox1.Controls.Add(this.Z0);
            this.groupBox1.Controls.Add(this.PointCountZ);
            this.groupBox1.Controls.Add(this.ShiftValue);
            this.groupBox1.Controls.Add(this.PointCountX);
            this.groupBox1.Controls.Add(this.XN);
            this.groupBox1.Controls.Add(this.X0);
            this.groupBox1.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.groupBox1.Location = new System.Drawing.Point(12, 70);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(604, 115);
            this.groupBox1.TabIndex = 4;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Параметры регресии";
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(492, 50);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(95, 23);
            this.button2.TabIndex = 2;
            this.button2.Text = "Применить";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.PointsCountChanged);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(492, 24);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(95, 23);
            this.button1.TabIndex = 2;
            this.button1.Text = "Применить";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.ChangeIntervals);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(338, 56);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(20, 13);
            this.label8.TabIndex = 1;
            this.label8.Text = "Z0";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(184, 56);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(20, 13);
            this.label7.TabIndex = 1;
            this.label7.Text = "X0";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(184, 30);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(20, 13);
            this.label3.TabIndex = 1;
            this.label3.Text = "X0";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(414, 30);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(22, 13);
            this.label6.TabIndex = 1;
            this.label6.Text = "ZN";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(338, 30);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(20, 13);
            this.label5.TabIndex = 1;
            this.label5.Text = "Z0";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(260, 30);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(22, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "XN";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(6, 83);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(104, 13);
            this.label9.TabIndex = 1;
            this.label9.Text = "Величина помехи : ";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(6, 56);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(102, 13);
            this.label4.TabIndex = 1;
            this.label4.Text = "Размер выборки : ";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 30);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(174, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Область определения функции : ";
            // 
            // ZN
            // 
            this.ZN.Location = new System.Drawing.Point(442, 27);
            this.ZN.Name = "ZN";
            this.ZN.Size = new System.Drawing.Size(44, 20);
            this.ZN.TabIndex = 0;
            this.ZN.Text = "5";
            // 
            // Z0
            // 
            this.Z0.Location = new System.Drawing.Point(364, 27);
            this.Z0.Name = "Z0";
            this.Z0.Size = new System.Drawing.Size(44, 20);
            this.Z0.TabIndex = 0;
            this.Z0.Text = "-5";
            // 
            // PointCountZ
            // 
            this.PointCountZ.Location = new System.Drawing.Point(364, 53);
            this.PointCountZ.Name = "PointCountZ";
            this.PointCountZ.Size = new System.Drawing.Size(122, 20);
            this.PointCountZ.TabIndex = 0;
            this.PointCountZ.Text = "30";
            // 
            // ShiftValue
            // 
            this.ShiftValue.Location = new System.Drawing.Point(210, 80);
            this.ShiftValue.Name = "ShiftValue";
            this.ShiftValue.Size = new System.Drawing.Size(122, 20);
            this.ShiftValue.TabIndex = 0;
            this.ShiftValue.Text = "0,5";
            // 
            // PointCountX
            // 
            this.PointCountX.Location = new System.Drawing.Point(210, 53);
            this.PointCountX.Name = "PointCountX";
            this.PointCountX.Size = new System.Drawing.Size(122, 20);
            this.PointCountX.TabIndex = 0;
            this.PointCountX.Text = "30";
            // 
            // XN
            // 
            this.XN.Location = new System.Drawing.Point(288, 27);
            this.XN.Name = "XN";
            this.XN.Size = new System.Drawing.Size(44, 20);
            this.XN.TabIndex = 0;
            this.XN.Text = "5";
            // 
            // X0
            // 
            this.X0.Location = new System.Drawing.Point(210, 27);
            this.X0.Name = "X0";
            this.X0.Size = new System.Drawing.Size(44, 20);
            this.X0.TabIndex = 0;
            this.X0.Text = "-5";
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.функцияToolStripMenuItem,
            this.нейроннаяСетьToolStripMenuItem,
            this.фильтрГрафиковToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(628, 24);
            this.menuStrip1.TabIndex = 3;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // функцияToolStripMenuItem
            // 
            this.функцияToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.построитьПоВыборкеToolStripMenuItem});
            this.функцияToolStripMenuItem.Name = "функцияToolStripMenuItem";
            this.функцияToolStripMenuItem.Size = new System.Drawing.Size(63, 20);
            this.функцияToolStripMenuItem.Text = "Функция";
            // 
            // построитьПоВыборкеToolStripMenuItem
            // 
            this.построитьПоВыборкеToolStripMenuItem.Name = "построитьПоВыборкеToolStripMenuItem";
            this.построитьПоВыборкеToolStripMenuItem.Size = new System.Drawing.Size(201, 22);
            this.построитьПоВыборкеToolStripMenuItem.Text = "Построить по выборке";
            this.построитьПоВыборкеToolStripMenuItem.Click += new System.EventHandler(this.BuildByPOints);
            // 
            // нейроннаяСетьToolStripMenuItem
            // 
            this.нейроннаяСетьToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.построитьToolStripMenuItem,
            this.обучитьToolStripMenuItem});
            this.нейроннаяСетьToolStripMenuItem.Name = "нейроннаяСетьToolStripMenuItem";
            this.нейроннаяСетьToolStripMenuItem.Size = new System.Drawing.Size(100, 20);
            this.нейроннаяСетьToolStripMenuItem.Text = "Нейронная сеть";
            // 
            // построитьToolStripMenuItem
            // 
            this.построитьToolStripMenuItem.Name = "построитьToolStripMenuItem";
            this.построитьToolStripMenuItem.Size = new System.Drawing.Size(139, 22);
            this.построитьToolStripMenuItem.Text = "Построить";
            this.построитьToolStripMenuItem.Click += new System.EventHandler(this.BuildNet);
            // 
            // обучитьToolStripMenuItem
            // 
            this.обучитьToolStripMenuItem.Name = "обучитьToolStripMenuItem";
            this.обучитьToolStripMenuItem.Size = new System.Drawing.Size(139, 22);
            this.обучитьToolStripMenuItem.Text = "Обучить";
            this.обучитьToolStripMenuItem.Click += new System.EventHandler(this.StudyNet);
            // 
            // фильтрГрафиковToolStripMenuItem
            // 
            this.фильтрГрафиковToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.SourceFunction,
            this.RandomFunction,
            this.NetOutput});
            this.фильтрГрафиковToolStripMenuItem.Name = "фильтрГрафиковToolStripMenuItem";
            this.фильтрГрафиковToolStripMenuItem.Size = new System.Drawing.Size(109, 20);
            this.фильтрГрафиковToolStripMenuItem.Text = "Фильтр графиков";
            // 
            // SourceFunction
            // 
            this.SourceFunction.Checked = true;
            this.SourceFunction.CheckOnClick = true;
            this.SourceFunction.CheckState = System.Windows.Forms.CheckState.Checked;
            this.SourceFunction.Name = "SourceFunction";
            this.SourceFunction.Size = new System.Drawing.Size(181, 22);
            this.SourceFunction.Text = "Исходная функция";
            this.SourceFunction.Click += new System.EventHandler(this.FilterChanged);
            // 
            // RandomFunction
            // 
            this.RandomFunction.Checked = true;
            this.RandomFunction.CheckOnClick = true;
            this.RandomFunction.CheckState = System.Windows.Forms.CheckState.Checked;
            this.RandomFunction.Name = "RandomFunction";
            this.RandomFunction.Size = new System.Drawing.Size(181, 22);
            this.RandomFunction.Text = "Выборка";
            this.RandomFunction.Click += new System.EventHandler(this.FilterChanged);
            // 
            // NetOutput
            // 
            this.NetOutput.Checked = true;
            this.NetOutput.CheckOnClick = true;
            this.NetOutput.CheckState = System.Windows.Forms.CheckState.Checked;
            this.NetOutput.Name = "NetOutput";
            this.NetOutput.Size = new System.Drawing.Size(181, 22);
            this.NetOutput.Text = "Выход сети";
            this.NetOutput.Click += new System.EventHandler(this.FilterChanged);
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(18, 37);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(90, 13);
            this.label10.TabIndex = 1;
            this.label10.Text = "Функция F(X) =  ";
            // 
            // FunctionTxt
            // 
            this.FunctionTxt.Font = new System.Drawing.Font("Lucida Console", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FunctionTxt.Location = new System.Drawing.Point(114, 34);
            this.FunctionTxt.Name = "FunctionTxt";
            this.FunctionTxt.Size = new System.Drawing.Size(485, 20);
            this.FunctionTxt.TabIndex = 0;
            this.FunctionTxt.Text = "sin(x)";
            this.FunctionTxt.KeyDown += new System.Windows.Forms.KeyEventHandler(this.FunctionText_KeyDown);
            // 
            // DrawerContainer
            // 
            this.DrawerContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.DrawerContainer.Location = new System.Drawing.Point(0, 0);
            this.DrawerContainer.Name = "DrawerContainer";
            this.DrawerContainer.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // DrawerContainer.Panel2
            // 
            this.DrawerContainer.Panel2.Controls.Add(this.StudyProgress);
            this.DrawerContainer.Size = new System.Drawing.Size(628, 391);
            this.DrawerContainer.SplitterDistance = 360;
            this.DrawerContainer.TabIndex = 0;
            // 
            // StudyProgress
            // 
            this.StudyProgress.Dock = System.Windows.Forms.DockStyle.Fill;
            this.StudyProgress.Location = new System.Drawing.Point(0, 0);
            this.StudyProgress.Name = "StudyProgress";
            this.StudyProgress.Size = new System.Drawing.Size(628, 27);
            this.StudyProgress.TabIndex = 0;
            // 
            // RegressionForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(628, 582);
            this.Controls.Add(this.Container);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "RegressionForm";
            this.Text = "RegressionForm";
            this.Container.Panel1.ResumeLayout(false);
            this.Container.Panel1.PerformLayout();
            this.Container.Panel2.ResumeLayout(false);
            this.Container.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.DrawerContainer.Panel2.ResumeLayout(false);
            this.DrawerContainer.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer Container;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem функцияToolStripMenuItem;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox PointCountX;
        private System.Windows.Forms.TextBox X0;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox ZN;
        private System.Windows.Forms.TextBox Z0;
        private System.Windows.Forms.TextBox XN;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox PointCountZ;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.ToolStripMenuItem построитьПоВыборкеToolStripMenuItem;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox ShiftValue;
        private System.Windows.Forms.ToolStripMenuItem нейроннаяСетьToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem построитьToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem обучитьToolStripMenuItem;
        private System.Windows.Forms.SplitContainer DrawerContainer;
        private System.Windows.Forms.ProgressBar StudyProgress;
        private System.Windows.Forms.ToolStripMenuItem фильтрГрафиковToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem SourceFunction;
        private System.Windows.Forms.ToolStripMenuItem RandomFunction;
        private System.Windows.Forms.ToolStripMenuItem NetOutput;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox FunctionTxt;
    }
}