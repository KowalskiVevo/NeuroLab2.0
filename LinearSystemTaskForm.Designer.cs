namespace Neuron
{
    partial class LinearSystemTaskForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.DataGridMain = new System.Windows.Forms.DataGridView();
            this.DataGridInput = new System.Windows.Forms.DataGridView();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.файлToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.сохранитьЗадачуToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.загрузитьЗадачуToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.нейроннаяСетьToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.построитьСетьToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.начатьРешениеToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.button1 = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.textBoxFactEpoch = new System.Windows.Forms.TextBox();
            this.textBoxFactError = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.textBoxMaxEpoch = new System.Windows.Forms.TextBox();
            this.textBoxMaxError = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.textBoxVarNum = new System.Windows.Forms.TextBox();
            this.textBoxEqvNum = new System.Windows.Forms.TextBox();
            this.импортВБазуДанныхToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.экспортИзБазыДанныхToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)(this.DataGridMain)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.DataGridInput)).BeginInit();
            this.menuStrip1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // DataGridMain
            // 
            this.DataGridMain.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.DataGridMain.Location = new System.Drawing.Point(31, 33);
            this.DataGridMain.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.DataGridMain.Name = "DataGridMain";
            this.DataGridMain.Size = new System.Drawing.Size(548, 156);
            this.DataGridMain.TabIndex = 0;
            this.DataGridMain.UserAddedRow += new System.Windows.Forms.DataGridViewRowEventHandler(this.dataGridView1_UserAddedRow);
            this.DataGridMain.UserDeletedRow += new System.Windows.Forms.DataGridViewRowEventHandler(this.dataGridView1_UserDeletedRow);
            // 
            // DataGridInput
            // 
            this.DataGridInput.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.DataGridInput.Location = new System.Drawing.Point(31, 207);
            this.DataGridInput.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.DataGridInput.Name = "DataGridInput";
            this.DataGridInput.Size = new System.Drawing.Size(548, 143);
            this.DataGridInput.TabIndex = 5;
            // 
            // menuStrip1
            // 
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.файлToolStripMenuItem,
            this.нейроннаяСетьToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Padding = new System.Windows.Forms.Padding(8, 2, 0, 2);
            this.menuStrip1.Size = new System.Drawing.Size(1008, 28);
            this.menuStrip1.TabIndex = 6;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // файлToolStripMenuItem
            // 
            this.файлToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.импортВБазуДанныхToolStripMenuItem,
            this.экспортИзБазыДанныхToolStripMenuItem,
            this.сохранитьЗадачуToolStripMenuItem,
            this.загрузитьЗадачуToolStripMenuItem});
            this.файлToolStripMenuItem.Name = "файлToolStripMenuItem";
            this.файлToolStripMenuItem.Size = new System.Drawing.Size(57, 24);
            this.файлToolStripMenuItem.Text = "Файл";
            // 
            // сохранитьЗадачуToolStripMenuItem
            // 
            this.сохранитьЗадачуToolStripMenuItem.Name = "сохранитьЗадачуToolStripMenuItem";
            this.сохранитьЗадачуToolStripMenuItem.Size = new System.Drawing.Size(219, 26);
            this.сохранитьЗадачуToolStripMenuItem.Text = "Сохранить в файл";
            this.сохранитьЗадачуToolStripMenuItem.Click += new System.EventHandler(this.сохранитьЗадачуToolStripMenuItem_Click);
            // 
            // загрузитьЗадачуToolStripMenuItem
            // 
            this.загрузитьЗадачуToolStripMenuItem.Name = "загрузитьЗадачуToolStripMenuItem";
            this.загрузитьЗадачуToolStripMenuItem.Size = new System.Drawing.Size(219, 26);
            this.загрузитьЗадачуToolStripMenuItem.Text = "Загрузить из файла";
            this.загрузитьЗадачуToolStripMenuItem.Click += new System.EventHandler(this.загрузитьЗадачуToolStripMenuItem_Click);
            // 
            // нейроннаяСетьToolStripMenuItem
            // 
            this.нейроннаяСетьToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.построитьСетьToolStripMenuItem,
            this.начатьРешениеToolStripMenuItem});
            this.нейроннаяСетьToolStripMenuItem.Name = "нейроннаяСетьToolStripMenuItem";
            this.нейроннаяСетьToolStripMenuItem.Size = new System.Drawing.Size(134, 24);
            this.нейроннаяСетьToolStripMenuItem.Text = "Нейронная сеть";
            // 
            // построитьСетьToolStripMenuItem
            // 
            this.построитьСетьToolStripMenuItem.Name = "построитьСетьToolStripMenuItem";
            this.построитьСетьToolStripMenuItem.Size = new System.Drawing.Size(191, 26);
            this.построитьСетьToolStripMenuItem.Text = "Построить сеть";
            this.построитьСетьToolStripMenuItem.Click += new System.EventHandler(this.построитьСетьToolStripMenuItem_Click);
            // 
            // начатьРешениеToolStripMenuItem
            // 
            this.начатьРешениеToolStripMenuItem.Name = "начатьРешениеToolStripMenuItem";
            this.начатьРешениеToolStripMenuItem.Size = new System.Drawing.Size(191, 26);
            this.начатьРешениеToolStripMenuItem.Text = "Решить задачу";
            this.начатьРешениеToolStripMenuItem.Click += new System.EventHandler(this.начатьРешениеToolStripMenuItem_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.button1);
            this.groupBox1.Controls.Add(this.groupBox2);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.textBoxVarNum);
            this.groupBox1.Controls.Add(this.textBoxEqvNum);
            this.groupBox1.Location = new System.Drawing.Point(600, 33);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.groupBox1.Size = new System.Drawing.Size(372, 316);
            this.groupBox1.TabIndex = 7;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Параметры задачи";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(116, 281);
            this.button1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(163, 28);
            this.button1.TabIndex = 5;
            this.button1.Text = "Решить задачу";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.textBoxFactEpoch);
            this.groupBox2.Controls.Add(this.textBoxFactError);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Controls.Add(this.textBoxMaxEpoch);
            this.groupBox2.Controls.Add(this.textBoxMaxError);
            this.groupBox2.Location = new System.Drawing.Point(12, 143);
            this.groupBox2.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.groupBox2.Size = new System.Drawing.Size(333, 124);
            this.groupBox2.TabIndex = 4;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Ограничения";
            // 
            // textBoxFactEpoch
            // 
            this.textBoxFactEpoch.Location = new System.Drawing.Point(244, 75);
            this.textBoxFactEpoch.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.textBoxFactEpoch.Name = "textBoxFactEpoch";
            this.textBoxFactEpoch.ReadOnly = true;
            this.textBoxFactEpoch.Size = new System.Drawing.Size(80, 22);
            this.textBoxFactEpoch.TabIndex = 5;
            this.textBoxFactEpoch.Text = "0";
            // 
            // textBoxFactError
            // 
            this.textBoxFactError.Location = new System.Drawing.Point(244, 37);
            this.textBoxFactError.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.textBoxFactError.Name = "textBoxFactError";
            this.textBoxFactError.ReadOnly = true;
            this.textBoxFactError.Size = new System.Drawing.Size(80, 22);
            this.textBoxFactError.TabIndex = 4;
            this.textBoxFactError.Text = "0,0";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(8, 84);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(107, 17);
            this.label4.TabIndex = 3;
            this.label4.Text = "Максимум эпох";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(8, 46);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(61, 17);
            this.label3.TabIndex = 2;
            this.label3.Text = "Ошибка";
            // 
            // textBoxMaxEpoch
            // 
            this.textBoxMaxEpoch.Location = new System.Drawing.Point(157, 75);
            this.textBoxMaxEpoch.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.textBoxMaxEpoch.Name = "textBoxMaxEpoch";
            this.textBoxMaxEpoch.Size = new System.Drawing.Size(80, 22);
            this.textBoxMaxEpoch.TabIndex = 1;
            this.textBoxMaxEpoch.Text = "5000";
            this.textBoxMaxEpoch.TextChanged += new System.EventHandler(this.textBoxMaxEpoch_TextChanged);
            // 
            // textBoxMaxError
            // 
            this.textBoxMaxError.Location = new System.Drawing.Point(157, 37);
            this.textBoxMaxError.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.textBoxMaxError.Name = "textBoxMaxError";
            this.textBoxMaxError.Size = new System.Drawing.Size(80, 22);
            this.textBoxMaxError.TabIndex = 0;
            this.textBoxMaxError.Text = "0,0005";
            this.textBoxMaxError.TextChanged += new System.EventHandler(this.textBoxMaxError_TextChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(8, 80);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(138, 17);
            this.label2.TabIndex = 3;
            this.label2.Text = "Кол-во переменных";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(8, 48);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(127, 17);
            this.label1.TabIndex = 2;
            this.label1.Text = "Кол-во уравнений";
            // 
            // textBoxVarNum
            // 
            this.textBoxVarNum.Location = new System.Drawing.Point(259, 71);
            this.textBoxVarNum.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.textBoxVarNum.Name = "textBoxVarNum";
            this.textBoxVarNum.Size = new System.Drawing.Size(85, 22);
            this.textBoxVarNum.TabIndex = 1;
            this.textBoxVarNum.Text = "2";
            this.textBoxVarNum.TextChanged += new System.EventHandler(this.textBoxVarNum_TextChanged);
            // 
            // textBoxEqvNum
            // 
            this.textBoxEqvNum.Location = new System.Drawing.Point(259, 39);
            this.textBoxEqvNum.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.textBoxEqvNum.Name = "textBoxEqvNum";
            this.textBoxEqvNum.Size = new System.Drawing.Size(85, 22);
            this.textBoxEqvNum.TabIndex = 0;
            this.textBoxEqvNum.Text = "2";
            this.textBoxEqvNum.TextChanged += new System.EventHandler(this.textBoxEqvNum_TextChanged);
            // 
            // импортВБазуДанныхToolStripMenuItem
            // 
            this.импортВБазуДанныхToolStripMenuItem.Name = "импортВБазуДанныхToolStripMenuItem";
            this.импортВБазуДанныхToolStripMenuItem.Size = new System.Drawing.Size(255, 26);
            this.импортВБазуДанныхToolStripMenuItem.Text = "Импорт в базу данных";
            this.импортВБазуДанныхToolStripMenuItem.Click += new System.EventHandler(this.импортВБазуДанныхToolStripMenuItem_Click);
            // 
            // экспортИзБазыДанныхToolStripMenuItem
            // 
            this.экспортИзБазыДанныхToolStripMenuItem.Name = "экспортИзБазыДанныхToolStripMenuItem";
            this.экспортИзБазыДанныхToolStripMenuItem.Size = new System.Drawing.Size(255, 26);
            this.экспортИзБазыДанныхToolStripMenuItem.Text = "Экспорт из базы данных";
            this.экспортИзБазыДанныхToolStripMenuItem.Click += new System.EventHandler(this.экспортИзБазыДанныхToolStripMenuItem_Click);
            // 
            // LinearSystemTaskForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1008, 358);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.DataGridInput);
            this.Controls.Add(this.DataGridMain);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Name = "LinearSystemTaskForm";
            this.Text = "Решение систем линейных  уравнений";
            ((System.ComponentModel.ISupportInitialize)(this.DataGridMain)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.DataGridInput)).EndInit();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView DataGridMain;
        private System.Windows.Forms.DataGridView DataGridInput;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem файлToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem нейроннаяСетьToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem построитьСетьToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem начатьРешениеToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem сохранитьЗадачуToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem загрузитьЗадачуToolStripMenuItem;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBoxVarNum;
        private System.Windows.Forms.TextBox textBoxEqvNum;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox textBoxMaxEpoch;
        private System.Windows.Forms.TextBox textBoxMaxError;
        private System.Windows.Forms.TextBox textBoxFactEpoch;
        private System.Windows.Forms.TextBox textBoxFactError;
        private System.Windows.Forms.ToolStripMenuItem импортВБазуДанныхToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem экспортИзБазыДанныхToolStripMenuItem;
    }
}