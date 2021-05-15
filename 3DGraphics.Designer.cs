namespace _DGraphics
{
    partial class Graphics3D
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose ( bool disposing )
        {
            if ( disposing && ( components != null ) )
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent ()
        {
            this.SuspendLayout();
            // 
            // Graphics3D
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F , 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Name = "Graphics3D";
            this.Size = new System.Drawing.Size(298 , 305);
            this.MouseWheel += new System.Windows.Forms.MouseEventHandler(this.Graphics3D_MouseWheel);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Graphics3D_MouseDown);
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.Graphics3D_MouseMove);
            this.Resize += new System.EventHandler(this.Graphics3D_Resize);
            this.MouseUp += new System.Windows.Forms.MouseEventHandler(this.Graphics3D_MouseUp);
            this.ResumeLayout(false);

        }
       

        #endregion

    }
}
