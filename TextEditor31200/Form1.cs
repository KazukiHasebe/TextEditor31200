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

        //新規作成
        private void NewNToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (rtTextArea.Text != "")
            {
                DialogResult result = MessageBox.Show("ファイルを保存しますか？", "質問",
                                  MessageBoxButtons.YesNoCancel,
                                  MessageBoxIcon.Exclamation,
                                  MessageBoxDefaultButton.Button2);

                //何が選択されたか調べる
                if (result == DialogResult.Yes)
                {
                    //「はい」が選択された時
                    SaveToolStripMenuItem_Click(sender, e);

                    rtTextArea.Text = "";
                }
                else if (result == DialogResult.No)
                {
                    //「いいえ」が選択された時
                    rtTextArea.Text = "";
                }
                else if (result == DialogResult.Cancel)
                {
                    //「キャンセル」が選択された時
                    return;
                }
            }

            this.fileName = "";
            this.Text = "テキストエディタ";
        }

        //終了
        private void ExitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (rtTextArea.Text != "")
            {
                DialogResult result = MessageBox.Show("ファイルを保存しますか？", "質問",
                                  MessageBoxButtons.YesNoCancel,
                                  MessageBoxIcon.Exclamation,
                                  MessageBoxDefaultButton.Button2);

                //何が選択されたか調べる
                if (result == DialogResult.Yes)
                {
                    //「はい」が選択された時
                    SaveToolStripMenuItem_Click(sender, e);
                }
                else if (result == DialogResult.Cancel)
                {
                    //「キャンセル」が選択された時
                    return;
                }
            }

            Application.Exit();
        }

        //名前を付けて保存
        private void SaveNameToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //[名前を付けて保存] ダイアログを表示
            if (sfdFileSave.ShowDialog() == DialogResult.OK)
            {
                FileSave(sfdFileSave.FileName);
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
                FileSave(fileName);
            }
            //ファイルがない場合
            else
            {
                SaveNameToolStripMenuItem_Click(sender, e);
            }
        }

        private void FileSave(string fileName)
        {
            //SteamReaderクラスを使用してファイルを読み込み
            using (StreamWriter sw = new StreamWriter(fileName, false, Encoding.GetEncoding("utf-8")))
            {
                sw.WriteLine(rtTextArea.Text);
                this.fileName = fileName;
                this.Text = this.fileName;
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

        //切り取り
        private void CutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            rtTextArea.Cut();
        }

        //コピー
        private void CopyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            rtTextArea.Copy();
        }

        //貼り付け
        private void PasteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            rtTextArea.Paste();
        }

        //削除
        private void DeleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            rtTextArea.SelectedText = "";
        }

        //編集
        private void EditToolStripMenuItem_Click(object sender, EventArgs e)
        {
            EditMenuMaskCheck();
        }

        //マスク機能
        private void EditMenuMaskCheck()
        {
            //元に戻すマスク
            UndoToolStripMenuItem.Enabled = rtTextArea.CanUndo ? true : false;

            //やり直しマスク
            RedoToolStripMenuItem.Enabled = rtTextArea.CanRedo ? true : false;

            //削除、切り取り、コピーマスク
            if (rtTextArea.SelectedText != "")
            {
                CutToolStripMenuItem.Enabled = true;
                CopyToolStripMenuItem.Enabled = true;
                DeleteToolStripMenuItem.Enabled = true;
            }
            else
            {
                CutToolStripMenuItem.Enabled = false;
                CopyToolStripMenuItem.Enabled = false;
                DeleteToolStripMenuItem.Enabled = false;
            }

            //貼り付けマスク
            PasteToolStripMenuItem.Enabled = Clipboard.GetDataObject().GetDataPresent(DataFormats.Text) ? true : false;
        }

        //色
        private void ColorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DialogResult dr = cdColor.ShowDialog();
            if (dr == System.Windows.Forms.DialogResult.OK)
            {
                rtTextArea.ForeColor = cdColor.Color;
            }
        }

        //フォント
        private void FontToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DialogResult dr = fdFont.ShowDialog();
            if (dr == System.Windows.Forms.DialogResult.OK)
            {
                rtTextArea.Font = fdFont.Font;
            }
        }
    }
}
