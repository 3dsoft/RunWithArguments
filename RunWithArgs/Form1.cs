using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RunWithArgs
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            string[] args = Environment.GetCommandLineArgs();
                   
            if (args.Length > 1)
            {
                if (args[1].ToUpper().EndsWith(".EXE"))
                {
                    cmbExePath.Items.Add(Path.GetFileName(args[1]));
                    cmbExePath.SelectedIndex = 0;
                }
                else if(Directory.Exists(args[1]))
                {
                    foreach (var item in Directory.GetFiles(args[1]))
                    {
                        cmbExePath.Items.Add(Path.GetFileName(item));
                    }
                    cmbExePath.SelectedIndex = 0;
                }

                txtArguments.Text = string.Empty;
                txtArguments.Focus();
            }
        }

        private void btnRun_Click(object sender, EventArgs e)
        {
            if (cmbExePath.Text.Trim() != string.Empty)
            {
                try
                {
                    Process.Start(cmbExePath.Text.Trim(), txtArguments.Text.Trim());
                }
                catch { }
            }
        }

        private void cmbExePath_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                if (cmbExePath.Text.Trim() != string.Empty)
                {
                    try
                    {
                        Process.Start(cmbExePath.Text.Trim(), txtArguments.Text.Trim());
                    }
                    catch { }
                }
            }
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void setMenuToolStripMenuItem_Click(object sender, EventArgs e)
        {
            RegisterContextMenu.RegisterContextMenu.Register(Application.ExecutablePath);

            MessageBox.Show("레지스트리에 Run With Args라는 이름으로 등록되었습니다.");

        }

        private void removeMenuToolStripMenuItem_Click(object sender, EventArgs e)
        {
            RegisterContextMenu.RegisterContextMenu.Unregister();

            MessageBox.Show("레지스트리에 등록된 Run With Args 키를 삭제하였습니다.");
        }

        private void btnSelectFile_Click(object sender, EventArgs e)
        {
            if(openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                txtArguments.AppendText(" \"" + openFileDialog1.FileName + "\" ");
            }
        }

        private void btnSelectFolder_Click(object sender, EventArgs e)
        {
            if(folderBrowserDialog1.ShowDialog() == DialogResult.OK)
            {
                txtArguments.AppendText(" \"" + folderBrowserDialog1.SelectedPath + "\" ");
            }
        }

        private void btnRunCMD_Click(object sender, EventArgs e)
        {
            if (cmbExePath.Text.Trim() == string.Empty) return;

            Process.Start("cmd.exe", $" /s /k \"{cmbExePath.Text.Trim()} {txtArguments.Text.Trim()}\"");
        }
    }
}
