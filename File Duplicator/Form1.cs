using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace File_Duplicator
{
    public partial class Form1 : Form
    {
        // Fields
        // Keep track of the source file path
        private string strSourcePath = null;
        // Keep track of destination path
        private string strDestPath = null;

        public Form1()
        {
            InitializeComponent();
        }

        private void btnSourceBrowse_Click(object sender, EventArgs e)
        {
            // Show the open file dialog and store the result it returns
            DialogResult drResult = ofdOpenFile.ShowDialog();
            // Only open and display the file if the user clicked open which returns a dialog result "OK"
            if (drResult == DialogResult.OK)
            {
                // Display file path in textbox
                txtSource.Text = ofdOpenFile.FileName;
                strSourcePath = ofdOpenFile.FileName;
            }
        }

        private void btnDestBrowse_Click(object sender, EventArgs e)
        {
            if (strSourcePath != null)
            {
                // Seperate the directory name from the file name
                string strDirName = Path.GetDirectoryName(strSourcePath);
                string strFileName = Path.GetFileName(strSourcePath);
                sfdSaveFile.InitialDirectory = strDirName;
                sfdSaveFile.FileName = strFileName;
            }
            // Only open and display the file if the user clicked open which returns a dialog result "OK"
            if (sfdSaveFile.ShowDialog() == DialogResult.OK)
            {
                // Display file path in textbox
                txtDestination.Text = sfdSaveFile.FileName;
                strDestPath = sfdSaveFile.FileName;
            }
        }

        private void btnDuplicate_Click(object sender, EventArgs e)
        {
            // Could use this, but assignment asks for the other way
            // File.Copy(strSourcePath, strDestPath);

            // Open source file to read
            using (FileStream fsSourceFile = File.Open(strSourcePath, FileMode.Open, FileAccess.Read))
            {
                // Open/Create destination file to write 
                using (FileStream fsDestFile = File.Open(strDestPath, FileMode.Create, FileAccess.Write))
                {
                    int iBytesRead;
                    byte[] byBuffer = new byte[4096];
                    while ((iBytesRead = fsSourceFile.Read(byBuffer, 0, 4096)) > 0)
                    {
                        fsDestFile.Write(byBuffer, 0, iBytesRead);
                    }
                }
            }
            MessageBox.Show("File has been duplicated.");
        }
    }
}
