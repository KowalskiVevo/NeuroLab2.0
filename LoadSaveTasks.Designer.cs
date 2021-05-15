namespace Neuron
{
    partial class LoadSaveTasks
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
            this.InputsCount = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.OutputsCount = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.StudyPairsCount = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.TaskDescription = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.button2 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.button4 = new System.Windows.Forms.Button();
            this.button5 = new System.Windows.Forms.Button();
            this.TaskBox = new System.Windows.Forms.ComboBox();
            this.NetTypeBox = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // InputsCount
            // 
            this.InputsCount.Location = new System.Drawing.Point(140, 69);
            this.InputsCount.Name = "InputsCount";
            this.InputsCount.ReadOnly = true;
            this.InputsCount.Size = new System.Drawing.Size(155, 20);
            this.InputsCount.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(73, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Тип задачи : ";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 42);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(61, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Тип сети : ";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 72);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(122, 13);
            this.label3.TabIndex = 1;
            this.label3.Text = "Размерность задачи : ";
            // 
            // OutputsCount
            // 
            this.OutputsCount.Location = new System.Drawing.Point(140, 95);
            this.OutputsCount.Name = "OutputsCount";
            this.OutputsCount.ReadOnly = true;
            this.OutputsCount.Size = new System.Drawing.Size(155, 20);
            this.OutputsCount.TabIndex = 0;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(12, 98);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(121, 13);
            this.label4.TabIndex = 1;
            this.label4.Text = "Количество выходов : ";
            // 
            // StudyPairsCount
            // 
            this.StudyPairsCount.Location = new System.Drawing.Point(180, 121);
            this.StudyPairsCount.Name = "StudyPairsCount";
            this.StudyPairsCount.ReadOnly = true;
            this.StudyPairsCount.Size = new System.Drawing.Size(115, 20);
            this.StudyPairsCount.TabIndex = 0;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(12, 124);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(162, 13);
            this.label5.TabIndex = 1;
            this.label5.Text = "Размер обучающей выборки : ";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(15, 264);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(280, 23);
            this.button1.TabIndex = 3;
            this.button1.Text = "Просмотр\\изменение параметров задачи";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.ViewParameters);
            // 
            // TaskDescription
            // 
            this.TaskDescription.Location = new System.Drawing.Point(15, 166);
            this.TaskDescription.Multiline = true;
            this.TaskDescription.Name = "TaskDescription";
            this.TaskDescription.Size = new System.Drawing.Size(280, 92);
            this.TaskDescription.TabIndex = 0;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(12, 150);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(104, 13);
            this.label6.TabIndex = 1;
            this.label6.Text = "Описание задачи : ";
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(15, 293);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(99, 23);
            this.button2.TabIndex = 4;
            this.button2.Text = "Загрузить";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.LoadCurrentExample);
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(120, 293);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(83, 23);
            this.button3.TabIndex = 4;
            this.button3.Text = "Сохранить";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.SaveTask);
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(209, 293);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(40, 23);
            this.button4.TabIndex = 4;
            this.button4.Text = "<";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.PrevTask);
            // 
            // button5
            // 
            this.button5.Location = new System.Drawing.Point(255, 293);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(40, 23);
            this.button5.TabIndex = 4;
            this.button5.Text = ">";
            this.button5.UseVisualStyleBackColor = true;
            this.button5.Click += new System.EventHandler(this.NextTask);
            // 
            // TaskBox
            // 
            this.TaskBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.TaskBox.FormattingEnabled = true;
            this.TaskBox.Location = new System.Drawing.Point(91, 12);
            this.TaskBox.Name = "TaskBox";
            this.TaskBox.Size = new System.Drawing.Size(204, 21);
            this.TaskBox.TabIndex = 5;
            this.TaskBox.SelectedIndexChanged += new System.EventHandler(this.TaskBox_SelectedIndexChanged);
            // 
            // NetTypeBox
            // 
            this.NetTypeBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.NetTypeBox.FormattingEnabled = true;
            this.NetTypeBox.Location = new System.Drawing.Point(91, 39);
            this.NetTypeBox.Name = "NetTypeBox";
            this.NetTypeBox.Size = new System.Drawing.Size(204, 21);
            this.NetTypeBox.TabIndex = 6;
            // 
            // LoadSaveTasks
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(311, 326);
            this.Controls.Add(this.NetTypeBox);
            this.Controls.Add(this.TaskBox);
            this.Controls.Add(this.button5);
            this.Controls.Add(this.button4);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.TaskDescription);
            this.Controls.Add(this.StudyPairsCount);
            this.Controls.Add(this.OutputsCount);
            this.Controls.Add(this.InputsCount);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "LoadSaveTasks";
            this.Text = "LoadSaveTasks";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.LoadSaveTasks_FormClosing);
            this.Load += new System.EventHandler(this.LoadSaveTasks_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox InputsCount;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox OutputsCount;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox StudyPairsCount;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TextBox TaskDescription;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.Button button5;
        private System.Windows.Forms.ComboBox TaskBox;
        private System.Windows.Forms.ComboBox NetTypeBox;
    }
}