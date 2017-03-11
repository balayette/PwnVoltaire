namespace PwnVoltaire
{
    partial class Form1
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
            this.richTextBox1 = new System.Windows.Forms.RichTextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.webKitBrowser1 = new WebKit.WebKitBrowser();
            this.SuspendLayout();
            // 
            // richTextBox1
            // 
            this.richTextBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.richTextBox1.Location = new System.Drawing.Point(13, 779);
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.Size = new System.Drawing.Size(1251, 117);
            this.richTextBox1.TabIndex = 1;
            this.richTextBox1.Text = "";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(1270, 779);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(228, 117);
            this.button1.TabIndex = 2;
            this.button1.Text = "Check";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // webKitBrowser1
            // 
            this.webKitBrowser1.BackColor = System.Drawing.Color.White;
            this.webKitBrowser1.Location = new System.Drawing.Point(13, 12);
            this.webKitBrowser1.Name = "webKitBrowser1";
            this.webKitBrowser1.Size = new System.Drawing.Size(1485, 761);
            this.webKitBrowser1.TabIndex = 3;
            this.webKitBrowser1.Url = null;
            this.webKitBrowser1.Load += new System.EventHandler(this.webKitBrowser1_Load);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1510, 908);
            this.Controls.Add(this.webKitBrowser1);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.richTextBox1);
            this.Name = "Form1";
            this.Text = "PwnVoltaire";
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.RichTextBox richTextBox1;
        private System.Windows.Forms.Button button1;
        private WebKit.WebKitBrowser webKitBrowser1;
    }
}

