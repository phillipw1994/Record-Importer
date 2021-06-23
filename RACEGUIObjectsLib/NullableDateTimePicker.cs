using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace RACEGUIObjectsLib
{
    public partial class NullableDateTimePicker: DateTimePicker
    {
        public NullableDateTimePicker()
        {
            base.Checked = false;
            base.ShowCheckBox = true;
            base.CustomFormat = " ";
            this.Format = DateTimePickerFormat.Custom;
            this.Value = DateTime.Now;
        }

        protected override void OnValueChanged(EventArgs eventargs)
        {
            if (!this.Checked)
            {
                // hide date value since it's not set
                this.CustomFormat = " ";
                this.Format = DateTimePickerFormat.Custom;
            }
            else
            {
                this.CustomFormat = null;
                this.Format = DateTimePickerFormat.Short;       //could be improved by remembering last format
            }

            base.OnValueChanged(eventargs);
        }

    }
}
