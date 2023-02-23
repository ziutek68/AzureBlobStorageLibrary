
namespace AzureBlobStorageLibrary
{
    partial class Form1
    {
        /// <summary>
        /// Wymagana zmienna projektanta.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Wyczyść wszystkie używane zasoby.
        /// </summary>
        /// <param name="disposing">prawda, jeżeli zarządzane zasoby powinny zostać zlikwidowane; Fałsz w przeciwnym wypadku.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Kod generowany przez Projektanta formularzy systemu Windows

        /// <summary>
        /// Metoda wymagana do obsługi projektanta — nie należy modyfikować
        /// jej zawartości w edytorze kodu.
        /// </summary>
        private void InitializeComponent()
        {
            this.Button1 = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.paramsMemo = new System.Windows.Forms.RichTextBox();
            this.resultMemo = new System.Windows.Forms.RichTextBox();
            this.Button2 = new System.Windows.Forms.Button();
            this.Button3 = new System.Windows.Forms.Button();
            this.Button4 = new System.Windows.Forms.Button();
            this.Button5 = new System.Windows.Forms.Button();
            this.Button6 = new System.Windows.Forms.Button();
            this.Button7 = new System.Windows.Forms.Button();
            this.Button8 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // Button1
            // 
            this.Button1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.Button1.Location = new System.Drawing.Point(95, 265);
            this.Button1.Name = "Button1";
            this.Button1.Size = new System.Drawing.Size(87, 24);
            this.Button1.TabIndex = 0;
            this.Button1.Text = "Wyślij do Azure";
            this.Button1.UseVisualStyleBackColor = true;
            this.Button1.Click += new System.EventHandler(this.Button1_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 126);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(49, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Resultat:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(13, 9);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(57, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Parametry:";
            // 
            // paramsMemo
            // 
            this.paramsMemo.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.paramsMemo.Location = new System.Drawing.Point(68, 9);
            this.paramsMemo.Name = "paramsMemo";
            this.paramsMemo.Size = new System.Drawing.Size(399, 111);
            this.paramsMemo.TabIndex = 4;
            this.paramsMemo.Text = "connectionString=\ncontainerName=\nlocalFileName=";
            this.paramsMemo.WordWrap = false;
            // 
            // resultMemo
            // 
            this.resultMemo.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.resultMemo.AutoWordSelection = true;
            this.resultMemo.Location = new System.Drawing.Point(68, 126);
            this.resultMemo.Name = "resultMemo";
            this.resultMemo.Size = new System.Drawing.Size(399, 133);
            this.resultMemo.TabIndex = 5;
            this.resultMemo.Text = "";
            this.resultMemo.WordWrap = false;
            // 
            // Button2
            // 
            this.Button2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.Button2.Location = new System.Drawing.Point(188, 265);
            this.Button2.Name = "Button2";
            this.Button2.Size = new System.Drawing.Size(89, 24);
            this.Button2.TabIndex = 6;
            this.Button2.Text = "Pobierz z Azure";
            this.Button2.UseVisualStyleBackColor = true;
            this.Button2.Click += new System.EventHandler(this.Button2_Click);
            // 
            // Button3
            // 
            this.Button3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.Button3.Location = new System.Drawing.Point(283, 265);
            this.Button3.Name = "Button3";
            this.Button3.Size = new System.Drawing.Size(80, 24);
            this.Button3.TabIndex = 7;
            this.Button3.Text = "Kasuj z Azure";
            this.Button3.UseVisualStyleBackColor = true;
            this.Button3.Click += new System.EventHandler(this.Button3_Click);
            // 
            // Button4
            // 
            this.Button4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.Button4.Location = new System.Drawing.Point(10, 265);
            this.Button4.Name = "Button4";
            this.Button4.Size = new System.Drawing.Size(79, 24);
            this.Button4.TabIndex = 8;
            this.Button4.Text = "Pliki na Azure";
            this.Button4.UseVisualStyleBackColor = true;
            this.Button4.Click += new System.EventHandler(this.Button4_Click);
            // 
            // Button5
            // 
            this.Button5.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.Button5.Location = new System.Drawing.Point(369, 265);
            this.Button5.Name = "Button5";
            this.Button5.Size = new System.Drawing.Size(98, 24);
            this.Button5.TabIndex = 9;
            this.Button5.Text = "Pierwszy z Azure";
            this.Button5.UseVisualStyleBackColor = true;
            this.Button5.Click += new System.EventHandler(this.Button5_Click);
            // 
            // Button6
            // 
            this.Button6.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.Button6.Location = new System.Drawing.Point(95, 290);
            this.Button6.Name = "Button6";
            this.Button6.Size = new System.Drawing.Size(87, 24);
            this.Button6.TabIndex = 10;
            this.Button6.Text = "Daj do kolejki";
            this.Button6.UseVisualStyleBackColor = true;
            this.Button6.Click += new System.EventHandler(this.Button6_Click);
            // 
            // Button7
            // 
            this.Button7.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.Button7.Location = new System.Drawing.Point(188, 290);
            this.Button7.Name = "Button7";
            this.Button7.Size = new System.Drawing.Size(89, 24);
            this.Button7.TabIndex = 11;
            this.Button7.Text = "Weź z kolejki";
            this.Button7.UseVisualStyleBackColor = true;
            this.Button7.Click += new System.EventHandler(this.Button7_Click);
            // 
            // Button8
            // 
            this.Button8.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.Button8.Location = new System.Drawing.Point(283, 290);
            this.Button8.Name = "Button8";
            this.Button8.Size = new System.Drawing.Size(80, 24);
            this.Button8.TabIndex = 12;
            this.Button8.Text = "Co w kolejce";
            this.Button8.UseVisualStyleBackColor = true;
            this.Button8.Click += new System.EventHandler(this.Button8_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(479, 320);
            this.Controls.Add(this.Button8);
            this.Controls.Add(this.Button7);
            this.Controls.Add(this.Button6);
            this.Controls.Add(this.Button5);
            this.Controls.Add(this.Button4);
            this.Controls.Add(this.Button3);
            this.Controls.Add(this.Button2);
            this.Controls.Add(this.resultMemo);
            this.Controls.Add(this.paramsMemo);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.Button1);
            this.Name = "Form1";
            this.Text = "AzureBlobStorageLibrary ";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button Button1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.RichTextBox paramsMemo;
        private System.Windows.Forms.RichTextBox resultMemo;
        private System.Windows.Forms.Button Button2;
        private System.Windows.Forms.Button Button3;
        private System.Windows.Forms.Button Button4;
        private System.Windows.Forms.Button Button5;
        private System.Windows.Forms.Button Button6;
        private System.Windows.Forms.Button Button7;
        private System.Windows.Forms.Button Button8;
    }
}

