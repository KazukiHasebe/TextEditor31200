using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TextEditor31200
{
    public partial class VersionForm : Form
    {
        //インスタンス
        private static VersionForm _singleInstance;

        public static VersionForm GetInstance()
        {
            if (_singleInstance == null)
            {
                _singleInstance = new VersionForm();
            }
            return _singleInstance; //自分自身のオブジェクトを返す
        }

        //コンストラクタ
        private VersionForm()
        {
            InitializeComponent();
        }

        private void btOk_Click(object sender, EventArgs e)
        {
            this.Close();   //ダイアログを閉じる
        }

        //クローズしたら初期化
        private void VersionForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            _singleInstance = null;
        }
    }
}
