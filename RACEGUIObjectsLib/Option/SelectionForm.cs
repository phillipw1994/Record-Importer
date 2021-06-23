﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Collections;

namespace RaceGUIObjectsLib.Option
{
    public partial class SelectionForm : Form
    {
        private string _selectedValue = "";

        public SelectionForm(DataTable options)
        {
            InitializeComponent();

            lbxOption.Items.Clear();
            foreach (DataRow option in options.Rows)
            {
                if (!option[0].Equals(DBNull.Value))
                {
                    lbxOption.Items.Add(option[0].ToString());
                }
            }

            lbxOption.SelectedValueChanged += new EventHandler(ValueSelectedHandler);
        }

        public SelectionForm(ArrayList options)
        {
            InitializeComponent();

            lbxOption.Items.Clear();
            foreach (string option in options)
            {
                if (!(option == null || option.Equals("")))
                {
                    lbxOption.Items.Add(option);
                }
            }

            lbxOption.SelectedValueChanged += new EventHandler(ValueSelectedHandler);
        }

        public string SelectedValue
        {
            get
            {
                return _selectedValue;
            }
        }

        void ValueSelectedHandler(object sender, EventArgs e)
        {
            _selectedValue = lbxOption.SelectedValue.ToString();
            this.Hide();
        }
    }
}
