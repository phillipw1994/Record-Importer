using System.Collections;
using System.Data;

namespace GUIObjectsLib.Option
{
    public static class Selection
    {
        public static string GetUserSelectedValue(DataTable options)
        {
            SelectionForm selectionForm = new SelectionForm(options);
            string selectedValue = selectionForm.SelectedValue;
            selectionForm.Close();
            return selectedValue;
        }

        public static string GetUserSelectedValue(ArrayList options)
        {
            SelectionForm selectionForm = new SelectionForm(options);
            string selectedValue = selectionForm.SelectedValue;
            selectionForm.Close();
            return selectedValue;
        }
    }
}
