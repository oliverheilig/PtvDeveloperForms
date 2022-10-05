
namespace PtvDeveloperForms
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.formsMap1 = new Ptv.XServer.Controls.Map.FormsMap();
            this.panel1 = new System.Windows.Forms.Panel();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // formsMap1
            // 
            this.formsMap1.Center = ((System.Windows.Point)(resources.GetObject("formsMap1.Center")));
            this.formsMap1.CoordinateDiplayFormat = Ptv.XServer.Controls.Map.CoordinateDiplayFormat.Degree;
            this.formsMap1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.formsMap1.FitInWindow = false;
            this.formsMap1.InvertMouseWheel = false;
            this.formsMap1.Location = new System.Drawing.Point(0, 0);
            this.formsMap1.MaxZoom = 19;
            this.formsMap1.MinZoom = 0;
            this.formsMap1.MouseDoubleClickZoom = true;
            this.formsMap1.MouseDragMode = Ptv.XServer.Controls.Map.Gadgets.DragMode.SelectOnShift;
            this.formsMap1.MouseWheelSpeed = 0.5D;
            this.formsMap1.Name = "formsMap1";
            this.formsMap1.ShowCoordinates = true;
            this.formsMap1.ShowLayers = true;
            this.formsMap1.ShowMagnifier = true;
            this.formsMap1.ShowNavigation = true;
            this.formsMap1.ShowOverview = true;
            this.formsMap1.ShowScale = true;
            this.formsMap1.ShowZoomSlider = true;
            this.formsMap1.Size = new System.Drawing.Size(1429, 845);
            this.formsMap1.TabIndex = 0;
            this.formsMap1.UseAnimation = true;
            this.formsMap1.UseDefaultTheme = true;
            this.formsMap1.UseMiles = false;
            this.formsMap1.XMapCopyright = "Please configure a valid copyright text!";
            this.formsMap1.XMapCredentials = "";
            this.formsMap1.XMapStyle = "";
            this.formsMap1.XMapUrl = "";
            this.formsMap1.ZoomLevel = 1D;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.comboBox1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 845);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1429, 57);
            this.panel1.TabIndex = 1;
            // 
            // comboBox1
            // 
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Items.AddRange(new object[] {
            "Road",
            "Satellite",
            "Hybrid"});
            this.comboBox1.Location = new System.Drawing.Point(126, 12);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(284, 33);
            this.comboBox1.TabIndex = 0;
            this.comboBox1.SelectedIndexChanged += new System.EventHandler(this.comboBox1_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(108, 25);
            this.label1.TabIndex = 1;
            this.label1.Text = "Map Style";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1429, 902);
            this.Controls.Add(this.formsMap1);
            this.Controls.Add(this.panel1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private Ptv.XServer.Controls.Map.FormsMap formsMap1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.Label label1;
    }
}

