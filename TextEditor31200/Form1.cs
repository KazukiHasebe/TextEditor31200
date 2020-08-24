using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TextEditor31200
{
    public partial class Form1 : Form
    {
        private string fileName = "";

        public Form1()
        {
            InitializeComponent();
        }

        //終了
        private void ExitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        //名前を付けて保存
        private void SaveNameToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //[名前を付けて保存] ダイアログを表示
            if (sfdFileSave.ShowDialog() == DialogResult.OK)
            {
                using (StreamWriter sw = new StreamWriter(sfdFileSave.FileName, false, Encoding.GetEncoding("utf-8")))
                {
                    sw.WriteLine(rtTextArea.Text);
                    this.fileName = sfdFileSave.FileName;
                    this.Text = fileName;
                }
            }
        }

        //開く
        private void OpenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //[開く] ダイアログを表示
            if (ofdFileOpen.ShowDialog() == DialogResult.OK)
            {
                //SteamReaderクラスを使用してファイルを読み込み
                using (StreamReader sr = new StreamReader(ofdFileOpen.FileName, Encoding.GetEncoding("utf-8"), false))
                {
                    rtTextArea.Text = sr.ReadToEnd();
                    this.fileName = ofdFileOpen.FileName;
                    this.Text = fileName;
                }

            }
        }

        //上書き保存
        private void SaveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //ファイルがある場合
            if (File.Exists(fileName))
            {
                using (StreamWriter sw = new StreamWriter(fileName, false, Encoding.GetEncoding("utf-8")))
                {
                    sw.WriteLine(rtTextArea.Text);
                    this.Text = fileName;
                }

            }
            //ファイルがない場合
            else
            {
                SaveNameToolStripMenuItem_Click(sender, e);
            }

        }

        //元に戻す
        private void UndoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            rtTextArea.Undo();
        }

        //やり直し
        private void RedoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            rtTextArea.Redo();
        }
    }
}
