using System;
using System.ComponentModel;
using System.Windows.Forms;

namespace GUIObjectsLib
{
    public partial class NullableNumericUpDown : NumericUpDown
    {
        public NullableNumericUpDown()
        {          
        }

        /// <summary>
        /// Read only property doesn't work on default control, this fixes thisw
        /// </summary>
        public override void DownButton()
        {
            if (ReadOnly)
                return;
            base.DownButton();
        }

        /// <summary>
        /// Read only property doesn't work on default control, this fixes thisw
        /// </summary>
        public override void UpButton()
        {
            if (ReadOnly)
                return;
            base.UpButton();
        }


        private double? _value;
        [
      Bindable(true)]
        public new double? Value
        {
            get
            {
                return _value;
            }
            set
            {
                _value = value;
                if (value != null)
                {
                    base.Value = (decimal)value;
                    Text = base.Value.ToString();
                }
                else
                {
                    base.Value = 0;
                    Text = "";
                }
            }
        }

        public double getValue
        {
            get
            {
                if (base.Value == null)
                {
                    if (_value != null)
                        return (double)_value;
                    else
                        return 0;
                }
                return (double)base.Value;
            }
        }

        private void NullableNumericUpDown_ValueChanged(object sender, EventArgs e)
        {
            _value = (double)base.Value;
        }

        void NullableNumericUpDown_TextChanged(object sender, System.EventArgs e)
        {
            if (string.IsNullOrEmpty(Text ))
            {
                _value = null;
            }
        }
    }
}



 
