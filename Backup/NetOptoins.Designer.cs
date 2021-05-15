namespace Neuron
{
    partial class NetOptions
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
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.inputsCount = new System.Windows.Forms.NumericUpDown();
            this.groupsCount = new System.Windows.Forms.NumericUpDown();
            this.neuronsCount = new System.Windows.Forms.NumericUpDown();
            this.groupNumber = new System.Windows.Forms.NumericUpDown();
            this.label5 = new System.Windows.Forms.Label();
            this.eraCount = new System.Windows.Forms.NumericUpDown();
            this.label6 = new System.Windows.Forms.Label();
            this.StudyStepText = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.IntervalFrom = new System.Windows.Forms.TextBox();
            this.IntervalTo = new System.Windows.Forms.TextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.button1 = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.inputsCount)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupsCount)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.neuronsCount)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupNumber)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.eraCount)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(7, 25);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(113, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Количество входов : ";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(7, 51);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(108, 13);
            this.label2.TabIndex = 0;
            this.label2.Text = "Количество слоев : ";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(7, 79);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(126, 13);
            this.label3.TabIndex = 0;
            this.label3.Text = "Количество нейронов : ";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(214, 77);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(49, 13);
            this.label4.TabIndex = 0;
            this.label4.Text = "в слое : ";
            // 
            // inputsCount
            // 
            this.inputsCount.Location = new System.Drawing.Point(135, 23);
            this.inputsCount.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.inputsCount.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.inputsCount.Name = "inputsCount";
            this.inputsCount.Size = new System.Drawing.Size(68, 20);
            this.inputsCount.TabIndex = 1;
            this.inputsCount.Value = new decimal(new int[] {
            2,
            0,
            0,
            0});
            this.inputsCount.ValueChanged += new System.EventHandler(this.inputsCount_ValueChanged);
            // 
            // groupsCount
            // 
            this.groupsCount.Location = new System.Drawing.Point(135, 49);
            this.groupsCount.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.groupsCount.Name = "groupsCount";
            this.groupsCount.Size = new System.Drawing.Size(68, 20);
            this.groupsCount.TabIndex = 1;
            this.groupsCount.Value = new decimal(new int[] {
            2,
            0,
            0,
            0});
            this.groupsCount.ValueChanged += new System.EventHandler(this.groupsCount_ValueChanged);
            // 
            // neuronsCount
            // 
            this.neuronsCount.Location = new System.Drawing.Point(135, 75);
            this.neuronsCount.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.neuronsCount.Name = "neuronsCount";
            this.neuronsCount.Size = new System.Drawing.Size(68, 20);
            this.neuronsCount.TabIndex = 1;
            this.neuronsCount.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.neuronsCount.ValueChanged += new System.EventHandler(this.neuronsCount_ValueChanged);
            // 
            // groupNumber
            // 
            this.groupNumber.Location = new System.Drawing.Point(269, 75);
            this.groupNumber.Maximum = new decimal(new int[] {
            2,
            0,
            0,
            0});
            this.groupNumber.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.groupNumber.Name = "groupNumber";
            this.groupNumber.Size = new System.Drawing.Size(40, 20);
            this.groupNumber.TabIndex = 1;
            this.groupNumber.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.groupNumber.ValueChanged += new System.EventHandler(this.groupNumber_ValueChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(7, 52);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(101, 13);
            this.label5.TabIndex = 0;
            this.label5.Text = "Количество эпох : ";
            // 
            // eraCount
            // 
            this.eraCount.Location = new System.Drawing.Point(135, 50);
            this.eraCount.Maximum = new decimal(new int[] {
            10000000,
            0,
            0,
            0});
            this.eraCount.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.eraCount.Name = "eraCount";
            this.eraCount.Size = new System.Drawing.Size(68, 20);
            this.eraCount.TabIndex = 1;
            this.eraCount.Value = new decimal(new int[] {
            3000,
            0,
            0,
            0});
            this.eraCount.ValueChanged += new System.EventHandler(this.eraCount_ValueChanged);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(7, 27);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(85, 13);
            this.label6.TabIndex = 0;
            this.label6.Text = "Шаг обучения : ";
            // 
            // StudyStepText
            // 
            this.StudyStepText.Location = new System.Drawing.Point(135, 24);
            this.StudyStepText.Name = "StudyStepText";
            this.StudyStepText.Size = new System.Drawing.Size(68, 20);
            this.StudyStepText.TabIndex = 2;
            this.StudyStepText.Text = "0,9";
            this.StudyStepText.TextChanged += new System.EventHandler(this.StudyStepText_TextChanged);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(7, 79);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(98, 13);
            this.label7.TabIndex = 0;
            this.label7.Text = "Интервал весов : ";
            // 
            // IntervalFrom
            // 
            this.IntervalFrom.Location = new System.Drawing.Point(135, 76);
            this.IntervalFrom.Name = "IntervalFrom";
            this.IntervalFrom.Size = new System.Drawing.Size(68, 20);
            this.IntervalFrom.TabIndex = 2;
            this.IntervalFrom.Text = "-0,5";
            this.IntervalFrom.TextChanged += new System.EventHandler(this.StudyStepText_TextChanged);
            // 
            // IntervalTo
            // 
            this.IntervalTo.Location = new System.Drawing.Point(209, 76);
            this.IntervalTo.Name = "IntervalTo";
            this.IntervalTo.Size = new System.Drawing.Size(68, 20);
            this.IntervalTo.TabIndex = 2;
            this.IntervalTo.Text = "0,5";
            this.IntervalTo.TextChanged += new System.EventHandler(this.StudyStepText_TextChanged);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.neuronsCount);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.groupNumber);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.inputsCount);
            this.groupBox1.Controls.Add(this.groupsCount);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(326, 113);
            this.groupBox1.TabIndex = 3;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Архитектура сети";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.button1);
            this.groupBox2.Controls.Add(this.label6);
            this.groupBox2.Controls.Add(this.label5);
            this.groupBox2.Controls.Add(this.IntervalTo);
            this.groupBox2.Controls.Add(this.label7);
            this.groupBox2.Controls.Add(this.IntervalFrom);
            this.groupBox2.Controls.Add(this.eraCount);
            this.groupBox2.Controls.Add(this.StudyStepText);
            this.groupBox2.Location = new System.Drawing.Point(12, 131);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(326, 142);
            this.groupBox2.TabIndex = 4;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Параметры обучения";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(217, 113);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(103, 23);
            this.button1.TabIndex = 3;
            this.button1.Text = "Применить";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // NetOptions
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(352, 282);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Name = "NetOptions";
            this.Text = "Архитектура сети";
            this.Load += new System.EventHandler(this.NetOptions_Load);
            ((System.ComponentModel.ISupportInitialize)(this.inputsCount)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupsCount)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.neuronsCount)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupNumber)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.eraCount)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.NumericUpDown inputsCount;
        private System.Windows.Forms.NumericUpDown groupsCount;
        private System.Windows.Forms.NumericUpDown neuronsCount;
        private System.Windows.Forms.NumericUpDown groupNumber;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.NumericUpDown eraCount;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox StudyStepText;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox IntervalFrom;
        private System.Windows.Forms.TextBox IntervalTo;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button button1;
    }
}