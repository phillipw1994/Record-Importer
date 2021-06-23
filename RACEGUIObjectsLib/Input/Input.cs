using System;
using System.Collections.Generic;
using System.Text;

namespace RaceGUIObjectsLib.Input
{
    public static class Input
    {
        public static string GetUserInput(string question, string defaultAnswer)
        {
            InputForm inputForm = new InputForm(question, defaultAnswer);
            inputForm.ShowDialog();
            string userInput = inputForm.UserInput;
            inputForm.Dispose();
            return userInput;
        }
        
    }
}
