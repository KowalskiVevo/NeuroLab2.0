namespace Neuron
{
    partial class ParameterListForm
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
            this.ParametrGrid = new System.Windows.Forms.DataGridView();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.обучающаяВыборкаToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.добавитьТекущийНаборПараметровToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.OutputGridView = new System.Windows.Forms.DataGridView();
            this.Column4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.посчитатьВыходСетиToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)(this.ParametrGrid)).BeginInit();
            this.menuStrip1.SuspendLayout();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.OutputGridView)).BeginInit();
            this.SuspendLayout();
            // 
            // ParametrGrid
            // 
            this.ParametrGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.ParametrGrid.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column1,
            this.Column2,
            this.Column3});
            this.ParametrGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ParametrGrid.Location = new System.Drawing.Point(0, 0);
            this.ParametrGrid.Name = "ParametrGrid";
            this.ParametrGrid.Size = new System.Drawing.Size(565, 189);
            this.ParametrGrid.TabIndex = 0;
            this.ParametrGrid.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.ParametrGrid_CellValueChanged);
            // 
            // Column1
            // 
            this.Column1.HeaderText = "N";
            this.Column1.Name = "Column1";
            this.Column1.ReadOnly = true;
            // 
            // Column2
            // 
            this.Column2.HeaderText = "Название параметра";
            this.Column2.Name = "Column2";
            // 
            // Column3
            // 
            this.Column3.HeaderText = "Значение параметра";
            this.Column3.Name = "Column3";
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.обучающаяВыборкаToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(565, 24);
            this.menuStrip1.TabIndex = 1;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // обучающаяВыборкаToolStripMenuItem
            // 
            this.обучающаяВыборкаToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.добавитьТекущийНаборПараметровToolStripMenuItem,
            this.посчитатьВыходСетиToolStripMenuItem});
            this.обучающаяВыборкаToolStripMenuItem.Name = "обучающаяВыборкаToolStripMenuItem";
            this.обучающаяВыборкаToolStripMenuItem.Size = new System.Drawing.Size(138, 20);
            this.обучающаяВыборкаToolStripMenuItem.Text = "Обучающая выборка";
            // 
            // добавитьТекущийНаборПараметровToolStripMenuItem
            // 
            this.добавитьТекущийНаборПараметровToolStripMenuItem.Name = "добавитьТекущийНаборПараметровToolStripMenuItem";
            this.добавитьТекущийНаборПараметровToolStripMenuItem.Size = new System.Drawing.Size(283, 22);
            this.добавитьТекущийНаборПараметровToolStripMenuItem.Text = "Добавить текущий набор параметров";
            this.добавитьТекущийНаборПараметровToolStripMenuItem.Click += new System.EventHandler(this.AddStudyPair);
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 24);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.ParametrGrid);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.OutputGridView);
            this.splitContainer1.Size = new System.Drawing.Size(565, 378);
            this.splitContainer1.SplitterDistance = 189;
            this.splitContainer1.TabIndex = 2;
            // 
            // OutputGridView
            // 
            this.OutputGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.OutputGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column4,
            this.Column5});
            this.OutputGridView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.OutputGridView.Location = new System.Drawing.Point(0, 0);
            this.OutputGridView.Name = "OutputGridView";
            this.OutputGridView.Size = new System.Drawing.Size(565, 185);
            this.OutputGridView.TabIndex = 0;
            this.OutputGridView.RowsAdded += new System.Windows.Forms.DataGridViewRowsAddedEventHandler(this.OutputGridView_RowsAdded);
            // 
            // Column4
            // 
            this.Column4.HeaderText = "Название выхода сети";
            this.Column4.Name = "Column4";
            // 
            // Column5
            // 
            this.Column5.HeaderText = "Значение выхода сети";
            this.Column5.Name = "Column5";
            // 
            // посчитатьВыходСетиToolStripMenuItem
            // 
            this.посчитатьВыходСетиToolStripMenuItem.Name = "посчитатьВыходСетиToolStripMenuItem";
            this.посчитатьВыходСетиToolStripMenuItem.Size = new System.Drawing.Size(283, 22);
            this.посчитатьВыходСетиToolStripMenuItem.Text = "Посчитать выход сети";
            this.посчитатьВыходСетиToolStripMenuItem.Click += new System.EventHandler(this.посчитатьВыходСетиToolStripMenuItem_Click);
            // 
            // ParameterListForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(565, 402);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "ParameterListForm";
            this.Text = "ParameterListForm";
            this.Load += new System.EventHandler(this.ParameterListForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.ParametrGrid)).EndInit();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.OutputGridView)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView ParametrGrid;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column2;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column3;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem обучающаяВыборкаToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem добавитьТекущийНаборПараметровToolStripMenuItem;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.DataGridView OutputGridView;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column4;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column5;
        private System.Windows.Forms.ToolStripMenuItem посчитатьВыходСетиToolStripMenuItem;
    }
}