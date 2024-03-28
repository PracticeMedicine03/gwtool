// Decompiled with JetBrains decompiler
// Type: GWTool.Main
// Assembly: GWTool, Version=0.3.0.0, Culture=neutral, PublicKeyToken=null

// Modification of GWTool

using System;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.Remoting.Metadata.W3cXsd2001;
using System.Windows.Forms;

//#nullable disable
namespace GWTool
{
    public class Main : Form
    {
        private string[] args = Environment.GetCommandLineArgs();

        private IContainer components = (IContainer)null;
        private Label lblInfo;
        private Label lblResult;
        private Button btnOpenFile;
        private OpenFileDialog openFileDialog1;
        private Label lblVersion;

        public Main()
        {
            InitializeComponent();
        }

        private void Main_DragEnter(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.All;
        }

        private void Main_DragDrop(object sender, DragEventArgs e)
        {
            foreach (string file in (string[])e.Data.GetData(DataFormats.FileDrop))
                HandleFile(file);
        }

        private void HandleFile(string file)
        {
            Main.FileType type = this.AnalyzeFile(file);
            if (type == FileType.Unknown)
            {
                this.lblResult.ForeColor = Color.Red;
                this.lblResult.Text = "I don't know what type this file is :(";
            }
            else
            {
                this.lblResult.Text = "The file is a " + type.ToString() + " file!\r\n";
                string extension = this.GetExtension(type);
                if (new FileInfo(file).Extension != extension)
                {
                    string destFileName = file + extension;
                    File.Move(file, destFileName);
                    file = destFileName;
                    this.lblResult.ForeColor = Color.Green;
                    this.lblResult.Text += "Added proper extension to the file";
                }
                switch (type)
                {
                    case FileType.GMAD:
                        this.lblResult.Text = "Extracting GMAD file...";
                        try
                        {
                            GMADTool.Extract(file, Path.GetDirectoryName(file));
                        }
                        catch (Exception ex)
                        {
                            int num = (int)MessageBox.Show(ex.ToString(), "Exception occured");
                            this.lblResult.ForeColor = Color.Red;
                            this.lblResult.Text = "Failed to extract the GMAD file.";
                            break;
                        }
                        this.lblResult.ForeColor = Color.Green;
                        this.lblResult.Text = "Successfully extracted the GMAD file!";
                        break;
                    case FileType.LZMA:
                        this.lblResult.Text = "This file seems to be downloaded directly from the Steam server\nPlease extract this with 7-Zip.\n" +
                            "The archive's file name is the same as the .gma file that your trying to extract.";
                        break;
                    default:
                        this.lblResult.Text += "Now just put this file in the correct directory in GMOD!\n(ex. garrysmod/addons)";
                        break;
                }
            }
        }

        private string GetExtension(FileType type)
        {
            switch (type)
            {
                case FileType.GMAD:
                    return ".gma";
                case FileType.LZMA:
                    return ".7z";
                default:
                    return "." + type.ToString().ToLower();
            }
        }

        private Main.FileType AnalyzeFile(string file)
        {
            byte[] buffer = new byte[3];
            string str;
            using (FileStream fileStream = new FileStream(file, FileMode.Open, FileAccess.Read))
            {
                fileStream.Read(buffer, 0, buffer.Length);
                str = new SoapHexBinary(buffer).ToString();
            }
            switch (str)
            {
                case "5D0000":
                    return FileType.LZMA;
                case "474D41":
                    return FileType.GMAD;
                case "445550":
                    return FileType.DUPE;
                case "474D53":
                    return FileType.GMS;
                default:
                    return FileType.Unknown;
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && this.components != null)
                this.components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Main));
            this.lblInfo = new System.Windows.Forms.Label();
            this.lblResult = new System.Windows.Forms.Label();
            this.lblVersion = new System.Windows.Forms.Label();
            this.btnOpenFile = new System.Windows.Forms.Button();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.SuspendLayout();
            // 
            // lblInfo
            // 
            this.lblInfo.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.lblInfo.AutoSize = true;
            this.lblInfo.Location = new System.Drawing.Point(35, 65);
            this.lblInfo.Name = "lblInfo";
            this.lblInfo.Size = new System.Drawing.Size(143, 26);
            this.lblInfo.TabIndex = 0;
            this.lblInfo.Text = "Drop an addon file anywhere\r\non this window to extract it";
            this.lblInfo.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblResult
            // 
            this.lblResult.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)));
            this.lblResult.Location = new System.Drawing.Point(12, 108);
            this.lblResult.Name = "lblResult";
            this.lblResult.Size = new System.Drawing.Size(190, 48);
            this.lblResult.TabIndex = 1;
            this.lblResult.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblVersion
            // 
            this.lblVersion.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.lblVersion.AutoSize = true;
            this.lblVersion.Enabled = false;
            this.lblVersion.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblVersion.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lblVersion.Location = new System.Drawing.Point(93, 175);
            this.lblVersion.Name = "lblVersion";
            this.lblVersion.Size = new System.Drawing.Size(28, 13);
            this.lblVersion.TabIndex = 2;
            this.lblVersion.Text = "v0.4";
            // 
            // btnOpenFile
            // 
            this.btnOpenFile.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.btnOpenFile.Location = new System.Drawing.Point(49, 12);
            this.btnOpenFile.Name = "btnOpenFile";
            this.btnOpenFile.Size = new System.Drawing.Size(105, 23);
            this.btnOpenFile.TabIndex = 3;
            this.btnOpenFile.Text = "Open GMAD file";
            this.btnOpenFile.UseVisualStyleBackColor = true;
            this.btnOpenFile.Click += new System.EventHandler(this.btnOpenFile_Click);
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.Filter = "GMOD addon files|*.gma";
            this.openFileDialog1.Title = "Select an GMAD file";
            // 
            // Main
            // 
            this.AllowDrop = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(214, 196);
            this.Controls.Add(this.btnOpenFile);
            this.Controls.Add(this.lblVersion);
            this.Controls.Add(this.lblResult);
            this.Controls.Add(this.lblInfo);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Main";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "GWTool";
            this.Load += new System.EventHandler(this.Main_Load);
            this.DragDrop += new System.Windows.Forms.DragEventHandler(this.Main_DragDrop);
            this.DragEnter += new System.Windows.Forms.DragEventHandler(this.Main_DragEnter);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        private enum FileType
        {
            GMAD,
            GMS,
            DUPE,
            LZMA,
            Unknown,
        }

        private void btnOpenFile_Click(object sender, EventArgs e)
        {
            this.openFileDialog1.ShowDialog();
            if(this.openFileDialog1.FileName.Any())
            {
                if(File.Exists(this.openFileDialog1.FileName))
                {
                    foreach (string file in (string[])this.openFileDialog1.FileNames)
                    {
                        HandleFile(file);
                    }
                }
            }
        }

        private void Main_Load(object sender, EventArgs e)
        {
            if(args.Length < 2)
            {
                return;
            }

            if (File.Exists(args[1]))
            {
                HandleFile(args[1]);
            }
        }
    }
}
