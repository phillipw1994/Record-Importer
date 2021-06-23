using System;
using System.Windows.Forms;

namespace GUIObjectsLib.Input
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
