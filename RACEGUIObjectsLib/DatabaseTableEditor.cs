using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using System.Data.SqlClient;

namespace RaceGUIObjectsLib
{
    public partial class DatabaseTableEditor : Form
    {
        private bool duplicatesAllowed = false;   
        private bool blnNewRow = false;    
        private string tableName = "";  
        private Dictionary<String,String> fieldMapping; 
        private ArrayList aOrderBy = null;  
        private ArrayList aPrimaryKeys = null;   
        private bool blnAutoPrimaryKey = false;  
        private string primaryKeyValue = "";  
        private string primaryKeyColumnName = "";
        SqlConnection conn = null;

        public DatabaseTableEditor()
        {
            InitializeComponent();
        }

        public DatabaseTableEditor
            (
            SqlConnection conn,
            string TableName,
            Dictionary<String,String> FieldMapping,
            ArrayList PrimaryKeys,
            Boolean AutoPrimaryKey = false,
            ArrayList OrderBy = null,
            String Title = "Editor",
            Boolean DuplicatesAllowed = false
            )
        {
            //  This call is required by the designer.
            InitializeComponent();

            //  Add any initialization after the InitializeComponent() call.
            this.conn = conn;
            this.Text = Title;
            this.duplicatesAllowed = DuplicatesAllowed;
            this.fieldMapping = FieldMapping;
            this.tableName = TableName;
            this.aOrderBy = OrderBy;
            this.aPrimaryKeys = PrimaryKeys;
            this.blnAutoPrimaryKey = AutoPrimaryKey;
            this.lblTitle.Text = Title;

            // currently only using 1 primary key value
            primaryKeyColumnName = aPrimaryKeys[0].ToString();

            // Set which button to use when the escape key is pressed
            this.CancelButton = btnClose;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            if (blnNewRow)
            {
                MessageBox.Show("You haven\'t finished enter values for the record!");
            }
            else
            {
                this.Close();
            }
        }



    
    // '' <summary>
    // '' construct sql statement to get data from table and field mappings
    // '' </summary>
    // '' <remarks></remarks>
    private void GetData() 
    {
        string sql = "";
        string sOrderBy = "";
        try {
            // loop through fields 
            foreach (KeyValuePair<String,String> pair in fieldMapping) 
            {
                // if this isn't the first field, add a column
                if ((sql.Length > 0)) 
                    sql += ",";

                // add field - handling psuedonyms with an AS statement
                if ((pair.Key == pair.Value)) 
                    sql = (sql + pair.Key);
                else 
                    sql = (sql + ("[" 
                                + (pair.Value + ("] AS [" 
                                + (pair.Key + "]")))));
                
            }
            // Order by what has been specified
            foreach (string value in aOrderBy) 
            {
                // if it isn't the first field
                if ((sOrderBy.Length > 0)) 
                    sOrderBy += ",";
                
                // add field
                sOrderBy = (sOrderBy + ("[" 
                            + (value + "]")));
            }
            sql = ("SELECT " 
                        + (sql + (" FROM [" 
                        + (tableName + "]"))));
            if ((sOrderBy.Length > 0)) 
                sql = (sql + (" ORDER BY " + sOrderBy));
            
            PopulateFormData(sql);
        }
        catch (Exception ex) 
        {
        }
    }
    
    // '' <summary>
    // '' Go get the data to display on this form
    // '' </summary>
    // '' <param name="sql">SQL Server sql statement for getting the data out we want</param>
    // '' <remarks></remarks>
    private void PopulateFormData(string sql) {
        try {
            PopulateControl(sql, this.DataGridView1, true);
        }
        catch (Exception ex) {
            //MessageBox.Show(("We had a problem getting what you wanted!  " + ex.Message));
        }
    }
    
    private void DataGridView1_CellEndEdit(object sender, System.Windows.Forms.DataGridViewCellEventArgs e) {
        try {
            bool bOK;
            string sql = "";
            DataTable rst = null;
            DataGridViewCell cell = DataGridView1.CurrentCell;
            string columnName = GetTableFieldName(DataGridView1.Columns[cell.ColumnIndex].HeaderText);
            bOK = true;
            if (bOK) {
                // Stop dulicates if required
                if (!duplicatesAllowed) 
                {
                    // only if the current cell is the primary index
                    if ((columnName == primaryKeyColumnName)) 
                    {
                        // check if there is a primarykey value
                        if (cell.Value==DBNull.Value) 
                        {
                            bOK = false;
                            MessageBox.Show(("There must be a value in column: " + DataGridView1.Columns[cell.ColumnIndex].HeaderText));
                            if (blnNewRow) 
                                GetData();
                            else 
                                cell.Value = cell.Tag;
                        }
                        if (bOK) 
                        {
                            sql = ("SELECT Count([" 
                                        + (primaryKeyColumnName + ("]) AS RecordCount FROM " 
                                        + (tableName + (" WHERE [" 
                                        + (primaryKeyColumnName + ("] = \'" 
                                        + (cell.Value.ToString() + "\'"))))))));
                            rst = QueryDataTable(sql);
                            
                            foreach (DataRow row in rst.Rows) 
                            {
                                if ((blnNewRow == true)) 
                                {
                                    // If its a new record then there shouldn't already be one
                                    if (Convert.ToInt32(row["RecordCount"]) > 0) 
                                    {
                                        bOK = false;
                                        MessageBox.Show("Cannot insert duplicate record!");
                                        // If there was an error then repopulate the datagrid 
                                        DataGridView1.CancelEdit();
                                        GetData();
                                    }
                                }
                                else 
                                {
                                    // If they are editing then there could be one 
                                    if (Convert.ToInt32(row["RecordCount"]) > 0)
                                    {
                                        // Do nothing if they have entered the same original value into the same cell
                                        if ((cell.Value.ToString().ToUpper() != cell.Tag.ToString().ToUpper())) 
                                        {
                                            bOK = false;
                                            MessageBox.Show("Cannot insert duplicate record!");
                                            cell.Value = cell.Tag;
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }

            if (bOK) 
            {
                try
                {
                    sql = "";
                    if (blnNewRow)
                    {
                        if (blnAutoPrimaryKey || (columnName == primaryKeyColumnName)) 
                        {
                            // new row being inserted
                            sql = BuildMultipalColumnInsertQuery(cell.RowIndex);
                        }
                    }
                    else if (!string.IsNullOrEmpty(primaryKeyValue ))
                    {
                        // Update the current row being edited
                        sql = BuildMultipalColumnUpdateQuery(cell.RowIndex);
                    }
                    if (sql.Length > 0)
                    {
                        ExecuteCommand(sql);
                        if (blnNewRow) 
                        {
                            if (blnAutoPrimaryKey)
                            {
                                //get generated primary key
                                int pk = 0;
                                pk = getLastPrimaryKeyGenerated(tableName);
                                //find PK field
                                for (int intX = 0; (intX <= (DataGridView1.Columns.Count - 1)); intX++)
                                {
                                    if ((GetTableFieldName(DataGridView1.Columns[intX].HeaderText) == primaryKeyColumnName))
                                    {
                                        DataGridView1.CurrentRow.Cells[intX].Value = pk.ToString();
                                        break;
                                    }
                                }
                                //DataGridView1.CurrentRow.Cells[primaryKeyColumnName].Value = pk.ToString();
                            }

                            // reset the new row flag
                            blnNewRow = false;
                            GetData();
                        }
                    }
                }
                catch (Exception ex) 
                {
                    MessageBox.Show(("Error updating data: " + ex.Message));
                }
            }
        }
        catch (Exception ex) {
            MessageBox.Show(ex.Message);
        }
    }
    
    private void DataGridView1_UserAddedRow(object sender, System.Windows.Forms.DataGridViewRowEventArgs e) {
        blnNewRow = true;
    }
    
    private void DataGridView1_CellEnter(object sender, System.Windows.Forms.DataGridViewCellEventArgs e) {
        try {
            // turn off new row only if they have entered the primary key value
            if (!string.IsNullOrEmpty(primaryKeyValue ))
                blnNewRow = false;
            
            // remember starting value of cell
            DataGridView1.CurrentCell.Tag = DataGridView1.CurrentCell.Value;
            // remember primary key 
            primaryKeyValue = GetCurrentRowsPrimaryKeyValue(primaryKeyColumnName, e.RowIndex);
        }
        catch (Exception ex) {
        }
    }
    
    private string GetTableFieldName(string _CellHeading) 
    {
        // Gets the table field name by looking up the datagrid column name in the fieldmapping dictonary
        string sColumnHeading = "";
        try 
        {
            if (fieldMapping.ContainsKey(_CellHeading)) 
            {
                sColumnHeading = fieldMapping[_CellHeading].ToString(); //.Item[_CellHeading];
            }
        }
        catch (Exception ex) 
        {
            MessageBox.Show("Error getting column name: " + ex.Message);
        }
        return sColumnHeading;
    }
    
    private string GetCurrentRowsPrimaryKeyValue(string _primaryKeyColumnName, int _RowNumber) 
    {
        string sPrimaryKeyValue = "";
        try {
            for (int intX = 0; (intX <= (DataGridView1.Columns.Count - 1)); intX++) 
            {
                if ((GetTableFieldName(DataGridView1.Columns[intX].HeaderText) == _primaryKeyColumnName)) 
                {
                    if (DataGridView1.Rows[_RowNumber].Cells[intX].Value != DBNull.Value) 
                    {
                        sPrimaryKeyValue = DataGridView1.Rows[_RowNumber].Cells[intX].Value.ToString();
                        break;
                    }
                }
            }
        }
        catch (Exception ex) 
        {
            MessageBox.Show(("Error getting primary key value! " + ex.Message));
        }
        return sPrimaryKeyValue;
    }
    
    private bool DeleteRecord() 
    {
        bool bOK;
        string sql = "";
        bOK = true;
        try 
        {
            sql = ("DELETE FROM [" 
                        + (tableName + ("] WHERE [" 
                        + (primaryKeyColumnName + ("] = \'" 
                        + (primaryKeyValue + "\'"))))));
            ExecuteCommand(sql);
        }
        catch (Exception ex) 
        {
            MessageBox.Show(("Error in procedure DeleteRecord: " + ex.Message));
            bOK = false;
        }
        return bOK;
    }
    
    private void DataGridView1_UserDeletingRow(object sender, System.Windows.Forms.DataGridViewRowCancelEventArgs e) {
        // Use this event instead of UserDeletedRow so it doesn't delete the record it moved to :-)
        try {
            if ((DeleteRecord() == false)) {
                MessageBox.Show("Error deleting record! ");
                GetData();
            }
        }
        catch (Exception ex) {
            MessageBox.Show(("Error in procedure DataGridView1_UserDeletedRow! " + ex.Message));
        }
    }
    
    private void frmGenericDescription_Load(object sender, System.EventArgs e) 
    {
        GetData();
        SetUpDataGridView();
        
        MakeReadOnly();
    }
    
    private void MakeReadOnly() 
    {
        DataGridView1.AllowUserToAddRows = false;
        DataGridView1.AllowUserToDeleteRows = false;
        DataGridView1.ReadOnly = true;
    }
    
    private void AllowEdits() 
    {
        DataGridView1.AllowUserToDeleteRows = false ;
        DataGridView1.SelectionMode = DataGridViewSelectionMode.CellSelect;
        DataGridView1.AllowUserToAddRows = true;
        DataGridView1.ReadOnly = false;
    }
    
    private void AllowDeletes() 
    {
        this.BackColor = Color.Salmon;
        DataGridView1.AllowUserToDeleteRows = true;
        DataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        DataGridView1.AllowUserToAddRows = false;
        DataGridView1.ReadOnly = false;
    }
    
    private void btnEdit_Click(object sender, System.EventArgs e) {
        this.BackColor = Color.LawnGreen;
        AllowEdits();
        // if the primary key is automatic then lock the column
        if (blnAutoPrimaryKey) {
            LockPrimaryKeyColumn();
        }
    }
    
    private void btnDelete_Click(object sender, System.EventArgs e) 
    {
        AllowDeletes();
        // if the primary key is automatic then lock the column
        if (blnAutoPrimaryKey) {
            LockPrimaryKeyColumn();
        }
    }
    
    private bool LockPrimaryKeyColumn() 
    {
        // The supplied primary key is the table column name not the Datagrid column heading
        // This will go through using the dictionary and find the primary key column
        bool bOk;
        // Start with false and set to true when it is found
        bOk = false;
        try {
            for (int intX = 0; (intX 
                        <= (DataGridView1.Columns.Count - 1)); intX++) {
                if ((GetTableFieldName(DataGridView1.Columns[intX].HeaderText) == primaryKeyColumnName)) {
                    // found it so lock the column
                    DataGridView1.Columns[intX].ReadOnly = true;
                    bOk = true;
                }
            }
        }
        catch (Exception ex) {
            MessageBox.Show(("Error locking primary key column! " + ex.Message));
        }
        return bOk;
    }
    
    private void SetUpDataGridView() 
    {
        DataGridView1.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.DisplayedCellsExceptHeaders;
        DataGridView1.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.Raised;
        DataGridView1.CellBorderStyle = DataGridViewCellBorderStyle.Single;
        DataGridView1.GridColor = SystemColors.ActiveBorder;
        //DataGridView1.MultiSelect = false;
    }
    
    private string BuildMultipalColumnInsertQuery(int _RowIndex) 
    {
        // Builds the insert query from the columns in the datagridview
        // Takes into account if the primary key is an auto field
        string sInsert = "";
        string sValues = "";
        string sColumnName = "";
        string sColumnValue = "";

        try 
        {
            // go through each column in the grid
            for (int intA = 0; (intA 
                        <= (DataGridView1.Columns.Count - 1)); intA++) 
                        {
                //  zero based 
                sColumnName = GetTableFieldName(DataGridView1.Columns[intA].HeaderText);
                // Handle if this is the primary key field and only add if its required
                if (((sColumnName == primaryKeyColumnName) 
                            && blnAutoPrimaryKey)) {
                    // do nothing
                }
                else 
                {
                    if (DataGridView1.Rows[_RowIndex].Cells[intA].Value==DBNull.Value) 
                    {
                        sColumnValue = "";
                    }
                    else {
                        sColumnValue = DataGridView1.Rows[_RowIndex].Cells[intA].Value.ToString().Replace("'","''");
                    }
                    // if there is a value then add the field and value to the strings
                    if ((sColumnValue.Length > 0)) 
                    {
                        if ((sInsert.Length > 0)) 
                        {
                            sInsert += ",";
                        }
                        sInsert = (sInsert + ("[" 
                                    + (sColumnName + "]")));
                        if ((sValues.Length > 0)) 
                        {
                            sValues += ",";
                        }
                        sValues = (sValues + ("\'" 
                                    + (sColumnValue + "\'")));
                    }
                }
            }
            return ("INSERT INTO [" 
                        + (tableName + ("] (" 
                        + (sInsert + (") SELECT " + sValues)))));
        }
        catch (Exception ex) 
        {
            MessageBox.Show(("Error generating insert query! " + ex.Message));
        }

        return "";
    }
    
    private string BuildMultipalColumnUpdateQuery(int _RowIndex) {
        // Builds the update query from the columns in the datagridview
        string sUpdate = "";
        string sColumnName = "";
        string sColumnValue = "";

        try {
            // go through each column in the grid
            for (int intA = 0; (intA 
                        <= (DataGridView1.Columns.Count - 1)); intA++) {
                //  zero based 
                sColumnName = GetTableFieldName(DataGridView1.Columns[intA].HeaderText);
                // Handle if this is the auto primary key field 
                if ((blnAutoPrimaryKey 
                            && (sColumnName == primaryKeyColumnName))) {
                    // do nothing
                }
                else {
                    if (DataGridView1.Rows[_RowIndex].Cells[intA].Value == DBNull.Value)
                        sColumnValue = "NULL";
                    else 
                        sColumnValue = ("\'" + (DataGridView1.Rows[_RowIndex].Cells[intA].Value.ToString().Replace("'","''") + "\'"));
                    
                    // set all fields even if they are empty. The user may be clearing a field
                    if (sUpdate.Length > 0) 
                        sUpdate += ",";
                    
                    sUpdate = (sUpdate + ("[" + (sColumnName + ("] = " + sColumnValue))));
                }
            }
            return ("UPDATE [" 
                        + (tableName + ("] SET " 
                        + (sUpdate + (" WHERE [" 
                        + (primaryKeyColumnName + ("] = \'" 
                        + (primaryKeyValue + "\'"))))))));
        }
        catch (Exception ex) {
            MessageBox.Show(("Error generating Update query! " + ex.Message));
        }

        return "";
    }


    /// <summary>
    /// Populates a ComboBox, ListBox, or Listview with data from the database
    /// </summary>
    /// <param name="sql"></param>
    /// <param name="myCtl"></param>
    public void PopulateControl(string sql, System.Windows.Forms.Control myCtl, bool bClearCtl = true)
    {
        //clear control
        switch (myCtl.GetType().Name.ToUpper())
        {
            case "COMBOBOX":
                ((ComboBox)myCtl).Items.Clear();
                break;
            case "LISTBOX":
            case "CHECKEDLISTBOX":
                ((ListBox)myCtl).Items.Clear();
                break;
            case "LISTVIEW":
                ((ListView)myCtl).Items.Clear();
                break;
            case "TEXTBOX":
                ((TextBox)myCtl).Text = "";
                break;
            case "DATAGRIDVIEW":
                break;
            default:
                throw new Exception(myCtl.GetType().Name + "is unsupported control in PopulateControl");
            //break;
        }

        //get data
        DataTable rst = QueryDataTable(sql);

        //populate control
        switch (myCtl.GetType().Name.ToUpper())
        {
            case "COMBOBOX":
                foreach (DataRow row in rst.Rows)
                {
                    if (row.ItemArray.Length > 1)
                        ((ComboBox)myCtl).Items.Add(new DataPair(row[1].ToString(), row[0].ToString()));  //unique identifier and item provided
                    else
                        ((ComboBox)myCtl).Items.Add(row[0]);   //just item provided - use item itself as unique identifier  
                }
                break;
            case "LISTBOX":
            case "CHECKEDLISTBOX":
                foreach (DataRow row in rst.Rows)
                {
                    if (row.ItemArray.Length > 1)
                        ((ListBox)myCtl).Items.Add(new DataPair(row[1].ToString(), row[0].ToString()));
                    else
                        ((ListBox)myCtl).Items.Add(row[0]);
                }
                break;
            case "TEXTBOX":
                foreach (DataRow row in rst.Rows)
                    ((TextBox)myCtl).Text += row[0] + Environment.NewLine;
                break;
            case "LISTVIEW":
                //setup headers
                ((ListView)myCtl).Columns.Clear();
                foreach (DataColumn col in rst.Columns)
                {
                    ((ListView)myCtl).Columns.Add(col.ColumnName);
                    ((ListView)myCtl).Columns[((ListView)myCtl).Columns.Count - 1].Name = col.ColumnName;
                }

                //populate data
                foreach (DataRow row in rst.Rows)
                {
                    System.Windows.Forms.ListViewItem item = new System.Windows.Forms.ListViewItem(row[0].ToString());

                    foreach (DataColumn col in rst.Columns)
                    {
                        if (col.Ordinal > 0)
                            item.SubItems.Add(row[col.Ordinal].ToString());
                    }

                    ((ListView)myCtl).Items.Add(item);
                }

                //autofit columns
                for (int i = 0; i < ((ListView)myCtl).Columns.Count; i++)
                    ((ListView)myCtl).Columns[i].Width = -2;

                break;
            case "DATAGRIDVIEW":
                ((DataGridView)myCtl).DataSource = rst;
                //BindingSource source = (BindingSource)((DataGridView)myCtl).DataSource;
                //source.DataSource = rst;
                break;
            default:
                throw new Exception(myCtl.GetType().Name + "is unsupported control in PopulateControl");
        }
        return;
    }

    //query a data table from the database
    public DataTable QueryDataTable(string selectCommand, Boolean editDataGrid = false, Boolean ReadOnly = false, string name = "")
    {
        try
        {
            if (conn.State != System.Data.ConnectionState.Open)
                conn.Open();

            return QueryDataTable(new SqlCommand(selectCommand), editDataGrid, name);

                
        }
        catch (Exception ex)
        {
            throw ex;
        }
        finally
        {
            //Disconnect();
        }
    }

    //query a data table from the database
    // the editDataGrid Boolean is used for storing the datatable into a sql data adapter, so that the data can be saved in the data grid on the fly.
    //the optional parameter name is used to name the DataTable for serialization purposes
    public DataTable QueryDataTable(SqlCommand selectCommand, Boolean editDataGrid = false, string name = "")
    {
        try
        {
            //if there is a ";" in the middle of the command text, do nothing,
            //because it may be sql injection
            if (selectCommand.CommandText.IndexOf(";") > 0 && selectCommand.CommandText.IndexOf(";") != selectCommand.CommandText.Length - 1)
            {
                return null;
            }

            if (conn.State!=System.Data.ConnectionState.Open)
                conn.Open();
            selectCommand.Connection = conn;
            selectCommand.CommandTimeout = 300000;

            SqlDataAdapter adapter = new SqlDataAdapter(selectCommand);

            DataTable table = null;
            if (name.Length > 0)
                table = new DataTable(name);
            else
                table = new DataTable(); //new DataTable("Data");
            table.Locale = System.Globalization.CultureInfo.InvariantCulture;

            adapter.Fill(table);
            adapter.Dispose();

            return table;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public int ExecuteCommand(String sql)
    {
        try
        {

            SqlCommand command = new SqlCommand();
            command.CommandText = sql;

            //if there is a ";" in the middle of the command text, do nothing,
            //because it may be sql injection
            if (command.CommandText.IndexOf(";") > 0 && command.CommandText.IndexOf(";") != command.CommandText.Length - 1)
            {
                return 0;
            }

            if (conn.State != System.Data.ConnectionState.Open)
                conn.Open();
            command.Connection = conn;
            command.CommandTimeout = 300000;

            int affected = command.ExecuteNonQuery();
            return affected;
        }
        catch (Exception ex)
        {
            throw ex;
        }
        finally
        {
            //if (_mySqlTransaction == null)
            //    _connection.Close();
        }
    }

    public int getLastPrimaryKeyGenerated(string tableName)
    {
        try
        {
            string sql = "SELECT IDENT_CURRENT('" + tableName + "')";
            DataTable rst = QueryDataTable(sql); 
            return Convert.ToInt32(rst.Rows[0][0]);
        }
        catch
        {
            return 0;
        }
    }

    }

    // Content item for the combo box
    public class DataPair
    {
        public string Name;
        public string Key;

        public DataPair()
        { }

        public DataPair(string name, string key)
        {
            Name = name;
            Key = key;
            return;
        }

        public override string ToString()
        {
            // Generates the text shown in the combo box
            return Name;
        }

        public string getKey()
        {
            return Key.Trim();
        }

        public string getName()
        {
            return Name.Trim();
        }
    }
}
