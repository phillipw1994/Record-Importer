using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace RaceGUIObjectsLib.Input
{
    public partial class InputForm : Form
    {
        private string _userInput;

        public InputForm(string question, string defaultAnswer)
        {
            InitializeComponent();
            _userInput = "";
            lblQuestion.Text = question == null ? "" : question;
            tbxAnswer.Text = defaultAnswer == null ? "" : defaultAnswer;
            tbxAnswer.Focus();
            tbxAnswer.SelectAll();
        }

        public string UserInput
        {
            get
            {
                return _userInput;
            }
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            _userInput = tbxAnswer.Text;
            this.Hide();
        }
    }
}
