using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ScreenShot
{
    public partial class Data2Word : Form
    {
        private enum ConvertType
        {
            Unknown,
            Data2Word,
            Word2Data
        }
        private ConvertType _convertType = ConvertType.Unknown;
        public Data2Word()
        {
            InitializeComponent();
        }
        private void setConvertType(ConvertType type)
        {
            _convertType = type;
            switch(_convertType)
            {
                case ConvertType.Word2Data:
                    panelInfo.Hide();
                    panelSaveFile.Show();
                    radioBtnW2D.Checked = true;
                    break;
                case ConvertType.Data2Word:
                    panelInfo.Show();
                    panelSaveFile.Hide();
                    radioBtnD2W.Checked = true;
                    break;
            }
        }
        private void Data2Word_Load(object sender, EventArgs e)
        {

        }

        private void resultTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 22)//ctrl+v
            {
                setConvertType(ConvertType.Word2Data);
            }
        }
        private void CtrlVReceiver_KeyPress(object sender, KeyPressEventArgs e)
        {
            if(e.KeyChar == 22)//ctrl+v
            {
                setConvertType(ConvertType.Data2Word);
                //file
                if (Clipboard.ContainsFileDropList())
                {
                    StringCollection files = Clipboard.GetFileDropList();
                    if(files.Count > 0)
                    {
                        string name = files[0];
                        FileStream fs = new FileStream(name, FileMode.Open, FileAccess.Read);
                        byte[] data = new byte[fs.Length];
                        fs.Read(data, 0, (int)fs.Length);
                        fs.Close();
                        this.resultTextBox.Text = Binary2Chinese.Bin2Chinese(data);
                    }
                }
                else if(Clipboard.ContainsImage())
                {

                }

            }
        }

        private void resultTextBox_TextChanged(object sender, EventArgs e)
        {
            if (_convertType != ConvertType.Word2Data)
                return;
            string s = resultTextBox.Text;
            byte[] data = Binary2Chinese.Chinese2Bin(s);
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            SaveFileDialog dia = new SaveFileDialog();
            dia.Filter = "*.*|*.*";
            DialogResult result = dia.ShowDialog();
            if (result == DialogResult.OK && dia.FileName != "")
            {
                byte[] data = Binary2Chinese.Chinese2Bin(resultTextBox.Text);
                FileStream fs = new FileStream(dia.FileName, FileMode.OpenOrCreate);
                if(fs != null)
                {
                    fs.Write(data, 0, data.Length);
                    fs.Close();
                }
            }
        }
    }
}
