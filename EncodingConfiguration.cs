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


        private void DefaultBtn_CheckedChanged(object sender, EventArgs e)
        {
            var defaultCode = Constant.allCharCode;
            defaultCode.TryAdd('头', "-- ... --. -...-");
            defaultCode.TryAdd('尾', ".. .. ..");
            codeConfigTxb.Text = JsonConvert.SerializeObject(Constant.allCharCode
                , Formatting.Indented);
            EncodingType = 0;
        }
        private void Number5Btn_CheckedChanged(object sender, EventArgs e)
        {
            var defaultCode = new Dictionary<char, string>[] { Constant.shortNumber5, Constant.alphabet, Constant.symbol }.SelectMany(disc => disc).ToDictionary(
            group => group.Key,
            group => group.Value
    );
            defaultCode.TryAdd('头', "-- ... --. -...-");
            defaultCode.TryAdd('尾', ".. .. ..");
            codeConfigTxb.Text = JsonConvert.SerializeObject(defaultCode, Formatting.Indented);
            EncodingType = 1;
        }

        private void Number10Btn_CheckedChanged(object sender, EventArgs e)
        {
            var defaultCode = new Dictionary<char, string>[] { Constant.shortNumber10, Constant.alphabet, Constant.symbol }.SelectMany(disc => disc).ToDictionary(
            group => group.Key,
            group => group.Value
    );
            defaultCode.TryAdd('头', "-- ... --. -...-");
            defaultCode.TryAdd('尾', ".. .. ..");
            codeConfigTxb.Text = JsonConvert.SerializeObject(defaultCode, Formatting.Indented);
            EncodingType = 2;

        }

        private void CustomizeBtn_CheckedChanged(object sender, EventArgs e)
        {
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
            if (EncodingType == 0)
            {
                defaultBtn.Checked = true;

            }
            else if ((EncodingType == 1))
            {
                number5Btn.Checked = true;
            }
            else if ((EncodingType == 2))
            {
                number10Btn.Checked = true;
            }
            else if ((EncodingType == 3)) { 
            customizeBtn.Checked = true;
            }
            DefaultBtn_CheckedChanged(sender, e);

        }
    }
}
