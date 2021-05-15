namespace Neuron
{
    partial class StudyPairModifier
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
            this.label1 = new System.Windows.Forms.Label();
            this.studyPairsCount = new System.Windows.Forms.NumericUpDown();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.studyPairsBox = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.studyPairsCount)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(10, 14);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(155, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Количество обучающих пар : ";
            // 
            // studyPairsCount
            // 
            this.studyPairsCount.Location = new System.Drawing.Point(175, 12);
            this.studyPairsCount.Maximum = new decimal(new int[] {
            2000,
            0,
            0,
            0});
            this.studyPairsCount.Name = "studyPairsCount";
            this.studyPairsCount.Size = new System.Drawing.Size(148, 20);
            this.studyPairsCount.TabIndex = 1;
            this.studyPairsCount.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.studyPairsCount.ValueChanged += new System.EventHandler(this.studyPairsCount_ValueChanged);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(12, 260);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(149, 23);
            this.button1.TabIndex = 3;
            this.button1.Text = "Применить";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(167, 260);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(156, 23);
            this.button2.TabIndex = 4;
            this.button2.Text = "Загрузить";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.LoadPairs);
            // 
            // studyPairsBox
            // 
            this.studyPairsBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.studyPairsBox.Location = new System.Drawing.Point(12, 38);
            this.studyPairsBox.Multiline = true;
            this.studyPairsBox.Name = "studyPairsBox";
            this.studyPairsBox.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.studyPairsBox.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.studyPairsBox.Size = new System.Drawing.Size(311, 216);
            this.studyPairsBox.TabIndex = 5;
            this.studyPairsBox.WordWrap = false;
            // 
            // StudyPairModifier
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(340, 292);
            this.Controls.Add(this.studyPairsBox);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.studyPairsCount);
            this.Controls.Add(this.label1);
            this.Name = "StudyPairModifier";
            this.Text = "Изменение обучающих пар";
            ((System.ComponentModel.ISupportInitialize)(this.studyPairsCount)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.NumericUpDown studyPairsCount;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.TextBox studyPairsBox;
    }
}