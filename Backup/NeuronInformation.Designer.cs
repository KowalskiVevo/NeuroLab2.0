namespace Neuron
{
    partial class NeuronInformation
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
            this.functionBox = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // functionBox
            // 
            this.functionBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.functionBox.FormattingEnabled = true;
            this.functionBox.Items.AddRange(new object[] {
            "Пороговая",
            "Логистическая",
            "Гиперболический тангенс",
            "Радиальная базисная функция",
            "Горбаня",
            "Линейная",
            "Радиальная базисная функция - 2",
            "Softmax"});
            this.functionBox.Location = new System.Drawing.Point(181, 17);
            this.functionBox.Name = "functionBox";
            this.functionBox.Size = new System.Drawing.Size(249, 21);
            this.functionBox.TabIndex = 0;
            this.functionBox.SelectedIndexChanged += new System.EventHandler(this.functionBox_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 20);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(163, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Функция активации нейрона : ";
            // 
            // NeuronInformation
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(442, 345);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.functionBox);
            this.Name = "NeuronInformation";
            this.Text = "Настройки нейрона";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox functionBox;
        private System.Windows.Forms.Label label1;
    }
}