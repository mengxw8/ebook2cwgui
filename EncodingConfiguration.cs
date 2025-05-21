using AngleSharp.Text;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace CW
{
    public partial class EncodingConfiguration : Form
    {

        //编码方式，默认为正常编码
        public Dictionary<char, string> Code { get; set; } = Constant.allCharCode;
        public EncodingConfiguration()
        {
            InitializeComponent();
        }

        private void DefaultBtn_CheckedChanged(object sender, EventArgs e)
        {
            codeConfigTxb.Text = JsonConvert.SerializeObject(Code, Formatting.Indented);
        }
        private void Number5Btn_CheckedChanged(object sender, EventArgs e)
        {
            codeConfigTxb.Text = JsonConvert.SerializeObject(new Dictionary<char, string>[] { Constant.shortNumber5, Constant.alphabet, Constant.symbol }.SelectMany(disc => disc).ToDictionary(
            group => group.Key,
            group => group.Value
    ), Formatting.Indented);
        }

        private void Number10Btn_CheckedChanged(object sender, EventArgs e)
        {
            codeConfigTxb.Text = JsonConvert.SerializeObject(new Dictionary<char, string>[] { Constant.shortNumber10, Constant.alphabet, Constant.symbol }.SelectMany(disc => disc).ToDictionary(
            group => group.Key,
            group => group.Value
    ), Formatting.Indented);

        }

        private void CustomizeBtn_CheckedChanged(object sender, EventArgs e)
        {
            DefaultBtn_CheckedChanged(sender, e);
        }

        private void SaveBtn_Click(object sender, EventArgs e)
        {

            var dict = JsonConvert.DeserializeObject<Dictionary<char, string>>(codeConfigTxb.Text!);
            if (dict != null)
            {
                Code = dict;
                this.DialogResult = DialogResult.OK;
            }
        }

        private void EncodingConfiguration_Load(object sender, EventArgs e)
        {
            defaultBtn.Checked = true;
        }
    }
}
