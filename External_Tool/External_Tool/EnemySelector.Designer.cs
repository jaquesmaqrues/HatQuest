namespace External_Tool
{
    partial class CombatCreator
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
            this.groupBoxEnemSource = new System.Windows.Forms.GroupBox();
            this.groupBoxEnemResult = new System.Windows.Forms.GroupBox();
            this.pictureBoxEnemResult2 = new System.Windows.Forms.PictureBox();
            this.pictureBoxEnemResult3 = new System.Windows.Forms.PictureBox();
            this.pictureBoxEnemResult4 = new System.Windows.Forms.PictureBox();
            this.pictureBoxEnemResult5 = new System.Windows.Forms.PictureBox();
            this.pictureBoxEnemResult1 = new System.Windows.Forms.PictureBox();
            this.comboBoxCombats = new System.Windows.Forms.ComboBox();
            this.groupBoxSettings = new System.Windows.Forms.GroupBox();
            this.buttonSave = new System.Windows.Forms.Button();
            this.buttonLoad = new System.Windows.Forms.Button();
            this.buttonRemoveCombat = new System.Windows.Forms.Button();
            this.buttonAddCombat = new System.Windows.Forms.Button();
            this.hScrollBar = new System.Windows.Forms.HScrollBar();
            this.groupBoxEnemResult.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxEnemResult2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxEnemResult3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxEnemResult4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxEnemResult5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxEnemResult1)).BeginInit();
            this.groupBoxSettings.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBoxEnemSource
            // 
            this.groupBoxEnemSource.BackColor = System.Drawing.Color.Gray;
            this.groupBoxEnemSource.Location = new System.Drawing.Point(12, 12);
            this.groupBoxEnemSource.Name = "groupBoxEnemSource";
            this.groupBoxEnemSource.Size = new System.Drawing.Size(760, 136);
            this.groupBoxEnemSource.TabIndex = 0;
            this.groupBoxEnemSource.TabStop = false;
            this.groupBoxEnemSource.Text = "Choose an Enemy";
            // 
            // groupBoxEnemResult
            // 
            this.groupBoxEnemResult.BackColor = System.Drawing.Color.Gray;
            this.groupBoxEnemResult.Controls.Add(this.pictureBoxEnemResult2);
            this.groupBoxEnemResult.Controls.Add(this.pictureBoxEnemResult3);
            this.groupBoxEnemResult.Controls.Add(this.pictureBoxEnemResult4);
            this.groupBoxEnemResult.Controls.Add(this.pictureBoxEnemResult5);
            this.groupBoxEnemResult.Controls.Add(this.pictureBoxEnemResult1);
            this.groupBoxEnemResult.Location = new System.Drawing.Point(12, 187);
            this.groupBoxEnemResult.Name = "groupBoxEnemResult";
            this.groupBoxEnemResult.Size = new System.Drawing.Size(760, 147);
            this.groupBoxEnemResult.TabIndex = 1;
            this.groupBoxEnemResult.TabStop = false;
            this.groupBoxEnemResult.Text = "Current Enemies";
            // 
            // pictureBoxEnemResult2
            // 
            this.pictureBoxEnemResult2.Location = new System.Drawing.Point(176, 19);
            this.pictureBoxEnemResult2.Name = "pictureBoxEnemResult2";
            this.pictureBoxEnemResult2.Size = new System.Drawing.Size(100, 100);
            this.pictureBoxEnemResult2.TabIndex = 4;
            this.pictureBoxEnemResult2.TabStop = false;
            // 
            // pictureBoxEnemResult3
            // 
            this.pictureBoxEnemResult3.Location = new System.Drawing.Point(346, 19);
            this.pictureBoxEnemResult3.Name = "pictureBoxEnemResult3";
            this.pictureBoxEnemResult3.Size = new System.Drawing.Size(100, 100);
            this.pictureBoxEnemResult3.TabIndex = 3;
            this.pictureBoxEnemResult3.TabStop = false;
            // 
            // pictureBoxEnemResult4
            // 
            this.pictureBoxEnemResult4.Location = new System.Drawing.Point(497, 19);
            this.pictureBoxEnemResult4.Name = "pictureBoxEnemResult4";
            this.pictureBoxEnemResult4.Size = new System.Drawing.Size(100, 100);
            this.pictureBoxEnemResult4.TabIndex = 2;
            this.pictureBoxEnemResult4.TabStop = false;
            // 
            // pictureBoxEnemResult5
            // 
            this.pictureBoxEnemResult5.Location = new System.Drawing.Point(635, 19);
            this.pictureBoxEnemResult5.Name = "pictureBoxEnemResult5";
            this.pictureBoxEnemResult5.Size = new System.Drawing.Size(100, 100);
            this.pictureBoxEnemResult5.TabIndex = 1;
            this.pictureBoxEnemResult5.TabStop = false;
            // 
            // pictureBoxEnemResult1
            // 
            this.pictureBoxEnemResult1.Location = new System.Drawing.Point(26, 19);
            this.pictureBoxEnemResult1.Name = "pictureBoxEnemResult1";
            this.pictureBoxEnemResult1.Size = new System.Drawing.Size(100, 100);
            this.pictureBoxEnemResult1.TabIndex = 0;
            this.pictureBoxEnemResult1.TabStop = false;
            // 
            // comboBoxCombats
            // 
            this.comboBoxCombats.FormattingEnabled = true;
            this.comboBoxCombats.Location = new System.Drawing.Point(26, 53);
            this.comboBoxCombats.Name = "comboBoxCombats";
            this.comboBoxCombats.Size = new System.Drawing.Size(121, 21);
            this.comboBoxCombats.TabIndex = 4;
            this.comboBoxCombats.SelectedIndexChanged += new System.EventHandler(this.comboBoxCombats_SelectedIndexChanged);
            // 
            // groupBoxSettings
            // 
            this.groupBoxSettings.BackColor = System.Drawing.Color.Gray;
            this.groupBoxSettings.Controls.Add(this.buttonSave);
            this.groupBoxSettings.Controls.Add(this.buttonLoad);
            this.groupBoxSettings.Controls.Add(this.buttonRemoveCombat);
            this.groupBoxSettings.Controls.Add(this.buttonAddCombat);
            this.groupBoxSettings.Controls.Add(this.comboBoxCombats);
            this.groupBoxSettings.Location = new System.Drawing.Point(15, 351);
            this.groupBoxSettings.Name = "groupBoxSettings";
            this.groupBoxSettings.Size = new System.Drawing.Size(759, 100);
            this.groupBoxSettings.TabIndex = 5;
            this.groupBoxSettings.TabStop = false;
            this.groupBoxSettings.Text = "Settings";
            // 
            // buttonSave
            // 
            this.buttonSave.Location = new System.Drawing.Point(649, 53);
            this.buttonSave.Name = "buttonSave";
            this.buttonSave.Size = new System.Drawing.Size(75, 23);
            this.buttonSave.TabIndex = 8;
            this.buttonSave.Text = "Save";
            this.buttonSave.UseVisualStyleBackColor = true;
            this.buttonSave.Click += new System.EventHandler(this.buttonSave_Click);
            // 
            // buttonLoad
            // 
            this.buttonLoad.Location = new System.Drawing.Point(497, 53);
            this.buttonLoad.Name = "buttonLoad";
            this.buttonLoad.Size = new System.Drawing.Size(75, 23);
            this.buttonLoad.TabIndex = 7;
            this.buttonLoad.Text = "Load";
            this.buttonLoad.UseVisualStyleBackColor = true;
            this.buttonLoad.Click += new System.EventHandler(this.buttonLoad_Click);
            // 
            // buttonRemoveCombat
            // 
            this.buttonRemoveCombat.Location = new System.Drawing.Point(323, 53);
            this.buttonRemoveCombat.Name = "buttonRemoveCombat";
            this.buttonRemoveCombat.Size = new System.Drawing.Size(75, 23);
            this.buttonRemoveCombat.TabIndex = 6;
            this.buttonRemoveCombat.Text = "Remove Floor";
            this.buttonRemoveCombat.UseVisualStyleBackColor = true;
            this.buttonRemoveCombat.Click += new System.EventHandler(this.buttonRemoveCombat_Click);
            // 
            // buttonAddCombat
            // 
            this.buttonAddCombat.Location = new System.Drawing.Point(195, 53);
            this.buttonAddCombat.Name = "buttonAddCombat";
            this.buttonAddCombat.Size = new System.Drawing.Size(75, 23);
            this.buttonAddCombat.TabIndex = 5;
            this.buttonAddCombat.Text = "Add Combat";
            this.buttonAddCombat.UseVisualStyleBackColor = true;
            this.buttonAddCombat.Click += new System.EventHandler(this.buttonAddCombat_Click);
            // 
            // hScrollBar
            // 
            this.hScrollBar.Location = new System.Drawing.Point(12, 151);
            this.hScrollBar.Name = "hScrollBar";
            this.hScrollBar.Size = new System.Drawing.Size(760, 17);
            this.hScrollBar.TabIndex = 0;
            this.hScrollBar.Scroll += new System.Windows.Forms.ScrollEventHandler(this.hScrollBar_Scroll);
            // 
            // CombatCreator
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.DimGray;
            this.ClientSize = new System.Drawing.Size(784, 509);
            this.Controls.Add(this.hScrollBar);
            this.Controls.Add(this.groupBoxSettings);
            this.Controls.Add(this.groupBoxEnemResult);
            this.Controls.Add(this.groupBoxEnemSource);
            this.Name = "CombatCreator";
            this.Text = "Combat Creator";
            this.groupBoxEnemResult.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxEnemResult2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxEnemResult3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxEnemResult4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxEnemResult5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxEnemResult1)).EndInit();
            this.groupBoxSettings.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBoxEnemSource;
        private System.Windows.Forms.GroupBox groupBoxEnemResult;
        private System.Windows.Forms.PictureBox pictureBoxEnemResult2;
        private System.Windows.Forms.PictureBox pictureBoxEnemResult3;
        private System.Windows.Forms.PictureBox pictureBoxEnemResult4;
        private System.Windows.Forms.PictureBox pictureBoxEnemResult5;
        private System.Windows.Forms.PictureBox pictureBoxEnemResult1;
        private System.Windows.Forms.ComboBox comboBoxCombats;
        private System.Windows.Forms.GroupBox groupBoxSettings;
        private System.Windows.Forms.Button buttonSave;
        private System.Windows.Forms.Button buttonLoad;
        private System.Windows.Forms.Button buttonRemoveCombat;
        private System.Windows.Forms.Button buttonAddCombat;
        private System.Windows.Forms.HScrollBar hScrollBar;
    }
}