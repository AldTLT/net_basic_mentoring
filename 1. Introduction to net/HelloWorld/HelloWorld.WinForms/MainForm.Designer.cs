
namespace HelloWorld.WinForms
{
    partial class MainForm
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
            this.textName = new System.Windows.Forms.TextBox();
            this.labelHelloMessage = new System.Windows.Forms.Label();
            this.labelEnterName = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // textName
            // 
            this.textName.Location = new System.Drawing.Point(173, 46);
            this.textName.MaxLength = 20;
            this.textName.Name = "textName";
            this.textName.Size = new System.Drawing.Size(222, 26);
            this.textName.TabIndex = 0;
            this.textName.TextChanged += new System.EventHandler(this.TextChangedHandler);
            // 
            // labelHelloMessage
            // 
            this.labelHelloMessage.AutoSize = true;
            this.labelHelloMessage.Location = new System.Drawing.Point(43, 106);
            this.labelHelloMessage.Name = "labelHelloMessage";
            this.labelHelloMessage.Size = new System.Drawing.Size(0, 20);
            this.labelHelloMessage.TabIndex = 1;
            // 
            // labelEnterName
            // 
            this.labelEnterName.AutoSize = true;
            this.labelEnterName.Location = new System.Drawing.Point(32, 49);
            this.labelEnterName.Name = "labelEnterName";
            this.labelEnterName.Size = new System.Drawing.Size(126, 20);
            this.labelEnterName.TabIndex = 2;
            this.labelEnterName.Text = "Enter your name";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(478, 166);
            this.Controls.Add(this.labelEnterName);
            this.Controls.Add(this.labelHelloMessage);
            this.Controls.Add(this.textName);
            this.Name = "MainForm";
            this.Text = "HelloWorld";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox textName;
        private System.Windows.Forms.Label labelHelloMessage;
        private System.Windows.Forms.Label labelEnterName;
    }
}

