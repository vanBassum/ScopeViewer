﻿namespace ScopeViewer
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
            FRMLib.Scope.Controls.ScopeViewSettings scopeViewSettings1 = new FRMLib.Scope.Controls.ScopeViewSettings();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.markerView1 = new FRMLib.Scope.Controls.MarkerView();
            this.scopeView1 = new FRMLib.Scope.Controls.ScopeView();
            this.traceView1 = new FRMLib.Scope.Controls.TraceView();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(12, 12);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 1;
            this.button1.Text = "button1";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(93, 12);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 2;
            this.button2.Text = "button2";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(314, 12);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(75, 23);
            this.button3.TabIndex = 4;
            this.button3.Text = "button3";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // markerView1
            // 
            this.markerView1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.markerView1.DataSource = null;
            this.markerView1.Location = new System.Drawing.Point(12, 546);
            this.markerView1.Name = "markerView1";
            this.markerView1.Size = new System.Drawing.Size(1334, 83);
            this.markerView1.TabIndex = 8;
            // 
            // scopeView1
            // 
            this.scopeView1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.scopeView1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.scopeView1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.scopeView1.DataSource = null;
            this.scopeView1.Location = new System.Drawing.Point(418, 41);
            this.scopeView1.Name = "scopeView1";
            scopeViewSettings1.BackgroundColor = System.Drawing.Color.Black;
            scopeViewSettings1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            scopeViewSettings1.HorizontalDivisions = 10;
            scopeViewSettings1.HorOffset = 0D;
            scopeViewSettings1.HorScale = 10D;
            scopeViewSettings1.VerticalDivisions = 8;
            this.scopeView1.Settings = scopeViewSettings1;
            this.scopeView1.Size = new System.Drawing.Size(928, 499);
            this.scopeView1.TabIndex = 7;
            // 
            // traceView1
            // 
            this.traceView1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.traceView1.DataSource = null;
            this.traceView1.Location = new System.Drawing.Point(12, 41);
            this.traceView1.Name = "traceView1";
            this.traceView1.Size = new System.Drawing.Size(400, 499);
            this.traceView1.TabIndex = 6;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1358, 641);
            this.Controls.Add(this.markerView1);
            this.Controls.Add(this.scopeView1);
            this.Controls.Add(this.traceView1);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button3;
        private FRMLib.Scope.Controls.TraceView traceView1;
        private FRMLib.Scope.Controls.ScopeView scopeView1;
        private FRMLib.Scope.Controls.MarkerView markerView1;
    }
}

