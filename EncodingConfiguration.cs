using AngleSharp.Dom;
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
        //这个是编码的类型，默认是正常编码
        //0默认编码
        //1 短5改
        //2 短10改
        //3 自定义编码
        public int EncodingType = 0;
        public EncodingConfiguration()
        {
            InitializeComponent();            
        }

        public EncodingConfiguration(int EncodingType, Dictionary<char, string>  codes)
        {
            InitializeComponent();
            this.EncodingType = EncodingType;
            this.Code = codes;
        }


        private bool loadingPreset;

        private void DefaultBtn_CheckedChanged(object sender, EventArgs e)
        {
            if (loadingPreset || sender is RadioButton radio && !radio.Checked)
                return;
            var defaultCode =  new Dictionary<char, string>[] { Constant.header, Constant.allCharCode }.SelectMany(disc => disc).ToDictionary(
            group => group.Key,
            group => group.Value
    );
            codeConfigTxb.Text = JsonConvert.SerializeObject(defaultCode
                , Formatting.Indented);
            EncodingType = 0;
        }
        private void Number5Btn_CheckedChanged(object sender, EventArgs e)
        {
            if (loadingPreset || sender is RadioButton radio && !radio.Checked)
                return;
            var defaultCode = new Dictionary<char, string>[] { Constant.header, Constant.shortNumber5, Constant.alphabet, Constant.symbol }.SelectMany(disc => disc).ToDictionary(
            group => group.Key,
            group => group.Value
    );
            codeConfigTxb.Text = JsonConvert.SerializeObject(defaultCode, Formatting.Indented);
            EncodingType = 1;
        }

        private void Number10Btn_CheckedChanged(object sender, EventArgs e)
        {
            if (loadingPreset || sender is RadioButton radio && !radio.Checked)
                return;
            var defaultCode = new Dictionary<char, string>[] { Constant.header, Constant.shortNumber10, Constant.alphabet, Constant.symbol }.SelectMany(disc => disc).ToDictionary(
            group => group.Key,
            group => group.Value
    );
            codeConfigTxb.Text = JsonConvert.SerializeObject(defaultCode, Formatting.Indented);
            EncodingType = 2;

        }

        private void CustomizeBtn_CheckedChanged(object sender, EventArgs e)
        {
            if (loadingPreset || sender is RadioButton radio && !radio.Checked)
                return;
            DefaultBtn_CheckedChanged(sender, e);
            EncodingType = 3;
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
            loadingPreset = true;
            defaultBtn.Checked = EncodingType == 0;
            number5Btn.Checked = EncodingType == 1;
            number10Btn.Checked = EncodingType == 2;
            customizeBtn.Checked = EncodingType == 3;
            loadingPreset = false;

            // 打开已有自定义编码时保留传入的码表，不要被默认模板覆盖。
            if (EncodingType == 3)
            {
                codeConfigTxb.Text = JsonConvert.SerializeObject(Code, Formatting.Indented);
                return;
            }

            if (EncodingType == 1)
                Number5Btn_CheckedChanged(number5Btn, EventArgs.Empty);
            else if (EncodingType == 2)
                Number10Btn_CheckedChanged(number10Btn, EventArgs.Empty);
            else
                DefaultBtn_CheckedChanged(defaultBtn, EventArgs.Empty);
        }
    }
}
