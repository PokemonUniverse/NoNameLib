using System.ComponentModel;
using System.Windows.Forms;
using NoNameLib.Extension;

namespace NoNameLib.UI.Controls.Forms
{
    public class NumericUpDownExt : NumericUpDown
    {
        private string postfixText = "";

        /// <summary>
        /// Specify text which is printed after the NumericUpDown value
        /// </summary>
        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        [TypeConverter(typeof(StringConverter))]
        public string PostfixText
        {
            get { return this.postfixText; }
            set 
            { 
                this.postfixText = value;
                UpdateEditText();
            }
        }

        protected override void UpdateEditText()
        {
            if (PostfixText.IsNullOrWhiteSpace())
                base.UpdateEditText();
            else
                this.Text = string.Format("{0} {1}", this.Value, PostfixText);
        }
    }
}
