namespace TikzDemo
{
    partial class TikzDemoForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TikzDemoForm));
            this.splitContainer = new System.Windows.Forms.SplitContainer();
            this.label1 = new System.Windows.Forms.Label();
            this.buttonExport = new System.Windows.Forms.Button();
            this.textBoxExportPath = new System.Windows.Forms.TextBox();
            this.ilPanel = new ILNumerics.Drawing.ILPanel();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer)).BeginInit();
            this.splitContainer.Panel1.SuspendLayout();
            this.splitContainer.Panel2.SuspendLayout();
            this.splitContainer.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitContainer
            // 
            this.splitContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer.IsSplitterFixed = true;
            this.splitContainer.Location = new System.Drawing.Point(0, 0);
            this.splitContainer.Name = "splitContainer";
            this.splitContainer.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer.Panel1
            // 
            this.splitContainer.Panel1.Controls.Add(this.label1);
            this.splitContainer.Panel1.Controls.Add(this.buttonExport);
            this.splitContainer.Panel1.Controls.Add(this.textBoxExportPath);
            this.splitContainer.Panel1.RightToLeft = System.Windows.Forms.RightToLeft.No;
            // 
            // splitContainer.Panel2
            // 
            this.splitContainer.Panel2.Controls.Add(this.ilPanel);
            this.splitContainer.Panel2.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.splitContainer.Size = new System.Drawing.Size(624, 441);
            this.splitContainer.SplitterDistance = 31;
            this.splitContainer.SplitterWidth = 1;
            this.splitContainer.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(61, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Export path";
            // 
            // buttonExport
            // 
            this.buttonExport.Location = new System.Drawing.Point(408, 4);
            this.buttonExport.Name = "buttonExport";
            this.buttonExport.Size = new System.Drawing.Size(75, 23);
            this.buttonExport.TabIndex = 1;
            this.buttonExport.Text = "Export";
            this.buttonExport.UseVisualStyleBackColor = true;
            this.buttonExport.Click += new System.EventHandler(this.buttonExport_Click);
            // 
            // textBoxExportPath
            // 
            this.textBoxExportPath.Location = new System.Drawing.Point(79, 6);
            this.textBoxExportPath.Name = "textBoxExportPath";
            this.textBoxExportPath.Size = new System.Drawing.Size(323, 20);
            this.textBoxExportPath.TabIndex = 0;
            // 
            // ilPanel
            // 
            this.ilPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ilPanel.Driver = ILNumerics.Drawing.RendererTypes.OpenGL;
            this.ilPanel.Editor = null;
            this.ilPanel.Location = new System.Drawing.Point(0, 0);
            this.ilPanel.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.ilPanel.Name = "ilPanel";
            this.ilPanel.Rectangle = ((System.Drawing.RectangleF)(resources.GetObject("ilPanel.Rectangle")));
            this.ilPanel.ShowUIControls = false;
            this.ilPanel.Size = new System.Drawing.Size(624, 409);
            this.ilPanel.TabIndex = 0;
            this.ilPanel.Timeout = ((uint)(0u));
            // 
            // TikzDemoForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(624, 441);
            this.Controls.Add(this.splitContainer);
            this.MinimumSize = new System.Drawing.Size(640, 480);
            this.Name = "TikzDemoForm";
            this.Text = "Tikz Export Demo";
            this.Load += new System.EventHandler(this.TikzDemoForm_Load);
            this.splitContainer.Panel1.ResumeLayout(false);
            this.splitContainer.Panel1.PerformLayout();
            this.splitContainer.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer)).EndInit();
            this.splitContainer.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button buttonExport;
        private System.Windows.Forms.TextBox textBoxExportPath;
        private ILNumerics.Drawing.ILPanel ilPanel;
    }
}

