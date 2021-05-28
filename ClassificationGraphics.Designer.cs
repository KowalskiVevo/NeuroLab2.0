namespace Neuron
{
    partial class ClassificationGraphics
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

        #region Код, автоматически созданный конструктором компонентов

        /// <summary> 
        /// Обязательный метод для поддержки конструктора - не изменяйте 
        /// содержимое данного метода при помощи редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.импортВБазуДанныхToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.экспортИзБазыДанныхToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.операцииToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.добавитьToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.удалитьВсеToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.нейроннаяСетьToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.построитьToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.обучитьToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.IsClassify = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem1,
            this.операцииToolStripMenuItem,
            this.нейроннаяСетьToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Padding = new System.Windows.Forms.Padding(8, 2, 0, 2);
            this.menuStrip1.Size = new System.Drawing.Size(372, 28);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.импортВБазуДанныхToolStripMenuItem,
            this.экспортИзБазыДанныхToolStripMenuItem});
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(57, 24);
            this.toolStripMenuItem1.Text = "Файл";
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
            // операцииToolStripMenuItem
            // 
            this.операцииToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.добавитьToolStripMenuItem,
            this.удалитьВсеToolStripMenuItem});
            this.операцииToolStripMenuItem.Name = "операцииToolStripMenuItem";
            this.операцииToolStripMenuItem.Size = new System.Drawing.Size(129, 24);
            this.операцииToolStripMenuItem.Text = "Класс объектов";
            // 
            // добавитьToolStripMenuItem
            // 
            this.добавитьToolStripMenuItem.Name = "добавитьToolStripMenuItem";
            this.добавитьToolStripMenuItem.Size = new System.Drawing.Size(167, 26);
            this.добавитьToolStripMenuItem.Text = "Добавить";
            this.добавитьToolStripMenuItem.Click += new System.EventHandler(this.AddingClass);
            // 
            // удалитьВсеToolStripMenuItem
            // 
            this.удалитьВсеToolStripMenuItem.Name = "удалитьВсеToolStripMenuItem";
            this.удалитьВсеToolStripMenuItem.Size = new System.Drawing.Size(167, 26);
            this.удалитьВсеToolStripMenuItem.Text = "Удалить все";
            this.удалитьВсеToolStripMenuItem.Click += new System.EventHandler(this.DeleteAll);
            // 
            // нейроннаяСетьToolStripMenuItem
            // 
            this.нейроннаяСетьToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.построитьToolStripMenuItem,
            this.обучитьToolStripMenuItem,
            this.IsClassify});
            this.нейроннаяСетьToolStripMenuItem.Name = "нейроннаяСетьToolStripMenuItem";
            this.нейроннаяСетьToolStripMenuItem.Size = new System.Drawing.Size(134, 24);
            this.нейроннаяСетьToolStripMenuItem.Text = "Нейронная сеть";
            // 
            // построитьToolStripMenuItem
            // 
            this.построитьToolStripMenuItem.Name = "построитьToolStripMenuItem";
            this.построитьToolStripMenuItem.Size = new System.Drawing.Size(217, 26);
            this.построитьToolStripMenuItem.Text = "Построить";
            this.построитьToolStripMenuItem.Click += new System.EventHandler(this.BuildNet);
            // 
            // обучитьToolStripMenuItem
            // 
            this.обучитьToolStripMenuItem.Name = "обучитьToolStripMenuItem";
            this.обучитьToolStripMenuItem.Size = new System.Drawing.Size(217, 26);
            this.обучитьToolStripMenuItem.Text = "Обучить";
            this.обучитьToolStripMenuItem.Click += new System.EventHandler(this.Study);
            // 
            // IsClassify
            // 
            this.IsClassify.CheckOnClick = true;
            this.IsClassify.Name = "IsClassify";
            this.IsClassify.Size = new System.Drawing.Size(217, 26);
            this.IsClassify.Text = "Классифицировать";
            // 
            // ClassificationGraphics
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.menuStrip1);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "ClassificationGraphics";
            this.Size = new System.Drawing.Size(372, 300);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem операцииToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem добавитьToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem нейроннаяСетьToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem построитьToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem обучитьToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem IsClassify;
        private System.Windows.Forms.ToolStripMenuItem удалитьВсеToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem импортВБазуДанныхToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem экспортИзБазыДанныхToolStripMenuItem;
    }
}
