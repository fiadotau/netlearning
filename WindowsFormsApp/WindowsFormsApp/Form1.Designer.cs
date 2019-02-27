namespace WindowsFormsApp
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
			this.helloButton = new System.Windows.Forms.Button();
			this.nameTextBox = new System.Windows.Forms.TextBox();
			this.SuspendLayout();
			// 
			// helloButton
			// 
			this.helloButton.Location = new System.Drawing.Point(99, 44);
			this.helloButton.Name = "helloButton";
			this.helloButton.Size = new System.Drawing.Size(75, 23);
			this.helloButton.TabIndex = 0;
			this.helloButton.Text = "Hello!";
			this.helloButton.UseVisualStyleBackColor = true;
			this.helloButton.Click += new System.EventHandler(this.helloButton_Click);
			// 
			// nameTextBox
			// 
			this.nameTextBox.Location = new System.Drawing.Point(12, 12);
			this.nameTextBox.Name = "nameTextBox";
			this.nameTextBox.Size = new System.Drawing.Size(162, 20);
			this.nameTextBox.TabIndex = 1;
			// 
			// Form1
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(186, 79);
			this.Controls.Add(this.nameTextBox);
			this.Controls.Add(this.helloButton);
			this.Name = "Form1";
			this.Text = "Hello Form";
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Button helloButton;
		private System.Windows.Forms.TextBox nameTextBox;
	}
}

