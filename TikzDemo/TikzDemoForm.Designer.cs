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
            this.btnExportText = new System.Windows.Forms.Button();
            this.btnExportFile = new System.Windows.Forms.Button();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.ilPanel = new ILNumerics.Drawing.Panel();
            this.textBoxTikz = new System.Windows.Forms.TextBox();
            this.comboBoxScene = new System.Windows.Forms.ComboBox();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer)).BeginInit();
            this.splitContainer.Panel1.SuspendLayout();
            this.splitContainer.Panel2.SuspendLayout();
            this.splitContainer.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
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
            this.splitContainer.Panel1.Controls.Add(this.comboBoxScene);
            this.splitContainer.Panel1.Controls.Add(this.btnExportText);
            this.splitContainer.Panel1.Controls.Add(this.btnExportFile);
            this.splitContainer.Panel1.RightToLeft = System.Windows.Forms.RightToLeft.No;
            // 
            // splitContainer.Panel2
            // 
            this.splitContainer.Panel2.Controls.Add(this.splitContainer1);
            this.splitContainer.Panel2.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.splitContainer.Size = new System.Drawing.Size(1022, 664);
            this.splitContainer.SplitterDistance = 32;
            this.splitContainer.SplitterWidth = 1;
            this.splitContainer.TabIndex = 0;
            // 
            // btnExportText
            // 
            this.btnExportText.AutoSize = true;
            this.btnExportText.Location = new System.Drawing.Point(264, 5);
            this.btnExportText.Name = "btnExportText";
            this.btnExportText.Size = new System.Drawing.Size(83, 23);
            this.btnExportText.TabIndex = 1;
            this.btnExportText.Text = "Export to Text";
            this.btnExportText.UseVisualStyleBackColor = true;
            this.btnExportText.Click += new System.EventHandler(this.btnExportText_Click);
            // 
            // btnExportFile
            // 
            this.btnExportFile.AutoSize = true;
            this.btnExportFile.Location = new System.Drawing.Point(180, 5);
            this.btnExportFile.Name = "btnExportFile";
            this.btnExportFile.Size = new System.Drawing.Size(78, 23);
            this.btnExportFile.TabIndex = 1;
            this.btnExportFile.Text = "Export to File";
            this.btnExportFile.UseVisualStyleBackColor = true;
            this.btnExportFile.Click += new System.EventHandler(this.btnExportFile_Click);
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.ilPanel);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.textBoxTikz);
            this.splitContainer1.Size = new System.Drawing.Size(1022, 631);
            this.splitContainer1.SplitterDistance = 520;
            this.splitContainer1.TabIndex = 1;
            // 
            // ilPanel
            // 
            this.ilPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ilPanel.RendererType = ILNumerics.Drawing.RendererTypes.OpenGL;
            this.ilPanel.Editor = null;
            this.ilPanel.Location = new System.Drawing.Point(0, 0);
            this.ilPanel.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.ilPanel.Name = "ilPanel";
            this.ilPanel.ShowUIControls = false;
            this.ilPanel.Size = new System.Drawing.Size(520, 631);
            this.ilPanel.TabIndex = 1;
            this.ilPanel.Timeout = ((uint)(0u));
            // 
            // textBoxTikz
            // 
            this.textBoxTikz.BackColor = System.Drawing.SystemColors.Window;
            this.textBoxTikz.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textBoxTikz.Location = new System.Drawing.Point(0, 0);
            this.textBoxTikz.Multiline = true;
            this.textBoxTikz.Name = "textBoxTikz";
            this.textBoxTikz.ReadOnly = true;
            this.textBoxTikz.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.textBoxTikz.Size = new System.Drawing.Size(498, 631);
            this.textBoxTikz.TabIndex = 0;
            // 
            // comboBoxScene
            // 
            this.comboBoxScene.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxScene.FormattingEnabled = true;
            this.comboBoxScene.Location = new System.Drawing.Point(12, 6);
            this.comboBoxScene.Name = "comboBoxScene";
            this.comboBoxScene.Size = new System.Drawing.Size(162, 21);
            this.comboBoxScene.TabIndex = 2;
            this.comboBoxScene.SelectedIndexChanged += new System.EventHandler(this.comboBoxScene_SelectedIndexChanged);
            // 
            // TikzDemoForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1022, 664);
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
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer;
        private System.Windows.Forms.Button btnExportFile;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private ILNumerics.Drawing.Panel ilPanel;
        private System.Windows.Forms.TextBox textBoxTikz;
        private System.Windows.Forms.Button btnExportText;
        private System.Windows.Forms.ComboBox comboBoxScene;
    }
}

