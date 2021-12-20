using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Text.RegularExpressions;

namespace Transight.Interface.Config
{
    public partial class UserControlConfig : UserControl
    {
        private XMLConfig XmlConfig;

        public UserControlConfig(XMLConfig Config)
        {
            InitializeComponent();

            XmlConfig = Config;

            switch (XmlConfig.CtrlSetting.Type)
            { 
                case ControlType.None:
                    lblTitle.Text = XmlConfig.CtrlSetting.Caption;
                    lblTitle.Visible = true;
                    break;

                case ControlType.TxtBox:
                    lbl1.Text = XmlConfig.CtrlSetting.Caption + ":";
                    txtBox.Text = XmlConfig.StringValue;

                    txtBox.MaxLength = XmlConfig.CtrlSetting.Size;

                    if (XmlConfig.CtrlSetting.Size > 20)
                        txtBox.Size = new System.Drawing.Size(200, 20);
                    else
                        txtBox.Size = new System.Drawing.Size(100, 20);

                    if (XmlConfig.CtrlSetting.Desc != null)
                    {
                        lbl2.Text = XmlConfig.CtrlSetting.Desc;

                        if (XmlConfig.CtrlSetting.Size <= 20)
                            lbl2.Location = new System.Drawing.Point(lbl2.Location.X - 100, lbl2.Location.Y);

                        lbl2.Visible = true;
                    }

                    lbl1.Visible = true;
                    txtBox.Visible = true;
                    break;

                case ControlType.Path:
                    lbl1.Text = XmlConfig.CtrlSetting.Caption + ":";
                    txtBox.Text = XmlConfig.StringValue;

                    txtBox.MaxLength = 1000;
                    txtBox.ReadOnly = true;

                    lbl1.Visible = true;
                    txtBox.Visible = true;
                    btnBrowse.Visible = true;
                    break;

                case ControlType.File:
                    lbl1.Text = XmlConfig.CtrlSetting.Caption + ":";
                    txtBox.Text = XmlConfig.StringValue;

                    txtBox.MaxLength = 1000;
                    txtBox.ReadOnly = true;

                    lbl1.Visible = true;
                    txtBox.Visible = true;
                    btnBrowse.Visible = true;
                    break;

                case ControlType.DtTmPicker:
                    lbl1.Text = XmlConfig.CtrlSetting.Caption + ":";

                    dtPicker.CustomFormat = XmlConfig.CtrlSetting.Desc;
                    dtPicker.MinDate = XmlConfig.CtrlSetting.MinDate;
                    dtPicker.MaxDate = XmlConfig.CtrlSetting.MaxDate;

                    if (XmlConfig.DateTimeValue < dtPicker.MinDate)
                        dtPicker.Value = dtPicker.MinDate;
                    else if (XmlConfig.DateTimeValue > dtPicker.MaxDate)
                        dtPicker.Value = dtPicker.MaxDate;
                    else
                        dtPicker.Value = XmlConfig.DateTimeValue;

                    lbl2.Text = XmlConfig.CtrlSetting.Desc;
                    lbl2.Location = new System.Drawing.Point(lbl2.Location.X -50, lbl2.Location.Y);

                    lbl2.Visible = true;
                    lbl1.Visible = true;
                    dtPicker.Visible = true;
                    break;

                case ControlType.NmrcUpDown:
                    lbl1.Text = XmlConfig.CtrlSetting.Caption + ":";
                    
                    nudNumeric.Minimum = XmlConfig.CtrlSetting.MinValue;
                    nudNumeric.Maximum = XmlConfig.CtrlSetting.MaxValue;
                    nudNumeric.Increment = XmlConfig.CtrlSetting.Interval;
                    nudNumeric.Value = XmlConfig.IntValue;

                    if (XmlConfig.CtrlSetting.Desc != null)
                    {
                        lbl2.Text = XmlConfig.CtrlSetting.Desc;

                        if (XmlConfig.CtrlSetting.Size < 20)
                            lbl2.Location = new System.Drawing.Point(lbl2.Location.X - 100, lbl2.Location.Y);

                        lbl2.Visible = true;
                    }

                    lbl1.Visible = true;
                    nudNumeric.Visible = true;
                    break;

                case ControlType.Decimal:
                    lbl1.Text = XmlConfig.CtrlSetting.Caption + ":";
                    txtBox.Text = XmlConfig.DecimalValue.ToString("0.".PadRight(XmlConfig.CtrlSetting.Size + 2, '0'));

                    txtBox.MaxLength = 15;
                    txtBox.Size = new System.Drawing.Size(100, 20);

                    if (XmlConfig.CtrlSetting.Desc != null)
                    {
                        lbl2.Text = XmlConfig.CtrlSetting.Desc;
                        lbl2.Location = new System.Drawing.Point(lbl2.Location.X - 100, lbl2.Location.Y);
                        lbl2.Visible = true;
                    }

                    lbl1.Visible = true;
                    txtBox.Visible = true;
                    break;

                case ControlType.ChkBox:
                    lbl1.Text = XmlConfig.CtrlSetting.Caption + ":";

                    if (XmlConfig.BoolValue)
                        chkBox.Checked = true;

                    if (XmlConfig.CtrlSetting.Desc != null)
                        chkBox.Text = XmlConfig.CtrlSetting.Desc;

                    chkBox.Visible = true;
                    lbl1.Visible = true;
                    break;

                case ControlType.DropDown:
                    lbl1.Text = XmlConfig.CtrlSetting.Caption + ":";

                    List<ComboItem> comboItemList = new List<ComboItem> { };

                    foreach (string s in XmlConfig.CtrlSetting.Values)
                    {
                        comboItemList.Add(new ComboItem(s.Split(',')[0], s.Split(',')[1]));
                    }

                    cmbBox.DataSource = comboItemList;
                    cmbBox.DisplayMember = "Display";
                    cmbBox.ValueMember = "Value";

                    cmbBox.SelectedValue = XmlConfig.StringValue;

                    cmbBox.Visible = true;
                    lbl1.Visible = true;
                    break;

                case ControlType.RdButton:
                    lbl1.Text = XmlConfig.CtrlSetting.Caption + ":";

                    int i = 0;

                    foreach (string s in XmlConfig.CtrlSetting.Values)
                    {
                        if (i == 0)
                        {
                            rdButton1.Text = s.Split(',')[0];
                            rdButton1.Tag = s.Split(',')[1];
                        }
                        else if (i == 1)
                        {
                            rdButton2.Text = s.Split(',')[0];
                            rdButton2.Tag = s.Split(',')[1];
                        }
                        i++;
                    }

                    if (XmlConfig.StringValue == string.Empty)
                        rdButton1.Checked = true;
                    else
                    {
                        if (XmlConfig.StringValue == rdButton1.Tag.ToString())
                            rdButton1.Checked = true;
                        else
                            rdButton2.Checked = true;
                    }

                    rdButton1.Visible = true;
                    rdButton2.Visible = true;
                    lbl1.Visible = true;
                    break;
            }
        }

        public string ValidateInput()
        {
            switch (XmlConfig.CtrlSetting.Type)
            {
                case ControlType.TxtBox:
                    txtBox.Text = txtBox.Text.Trim();

                    if (XmlConfig.CtrlSetting.Default == "N")
                    {
                        if (txtBox.Text == string.Empty)
                        {
                            txtBox.Focus();
                            return "Please enter " + XmlConfig.CtrlSetting.Caption + ".";
                        }
                    }

                    if (txtBox.Text != string.Empty)
                    {
                        if (XmlConfig.CtrlSetting.RegEx != null)
                        {
                            if (!Regex.IsMatch(txtBox.Text, XmlConfig.CtrlSetting.RegEx))
                            {
                                txtBox.SelectAll();
                                txtBox.Focus();
                                return "Invalid " + XmlConfig.CtrlSetting.Caption + " format.";
                            }
                        }
                    }
                    break;

                case ControlType.Path:
                    if (txtBox.Text == string.Empty)
                    {
                        btnBrowse.Focus();
                        return "Please select a folder for " + XmlConfig.CtrlSetting.Caption + ".";
                    }
                    break;

                case ControlType.File:
                    if (txtBox.Text == string.Empty)
                    {
                        btnBrowse.Focus();
                        return "Please select a file for " + XmlConfig.CtrlSetting.Caption + ".";
                    }
                    break;

                case ControlType.Decimal:
                    txtBox.Text = txtBox.Text.Trim();

                    if (txtBox.Text == string.Empty)
                    {
                        txtBox.Focus();
                        return "Please enter " + XmlConfig.CtrlSetting.Caption + ".";
                    }

                    if (!Regex.IsMatch(txtBox.Text, @"^[0-9]+(\.[0-9]{1,X})?$".Replace("X", XmlConfig.CtrlSetting.Size.ToString())))
                    {
                        txtBox.SelectAll();
                        txtBox.Focus();
                        return "Invalid " + XmlConfig.CtrlSetting.Caption + " format.";
                    }
                    break;

                case ControlType.DropDown:
                    if (cmbBox.Text == string.Empty)
                    {
                        cmbBox.Focus();
                        return "Please select a " + XmlConfig.CtrlSetting.Caption + ".";
                    }
                    break;
            }

            return string.Empty;
        }

        public bool Changed()
        {
            switch (XmlConfig.CtrlSetting.Type)
            {
                case ControlType.TxtBox:
                    txtBox.Text = txtBox.Text.Trim();

                    if (txtBox.Text != XmlConfig.StringValue)
                        XmlConfig.Changed = true;
                    break;

                case ControlType.Path:
                case ControlType.File:
                    if (txtBox.Text != XmlConfig.StringValue)
                        XmlConfig.Changed = true;
                    break;

                case ControlType.DtTmPicker:
                    if (dtPicker.Value != XmlConfig.DateTimeValue)
                        XmlConfig.Changed = true;
                    break;

                case ControlType.NmrcUpDown:
                    if (nudNumeric.Value != XmlConfig.IntValue)
                        XmlConfig.Changed = true;
                    break;

                case ControlType.Decimal:
                    if (Convert.ToDecimal(txtBox.Text) != XmlConfig.DecimalValue)
                        XmlConfig.Changed = true;
                    break;

                case ControlType.ChkBox:
                    if (chkBox.Checked != XmlConfig.BoolValue)
                        XmlConfig.Changed = true;
                    break;

                case ControlType.DropDown:
                    if (cmbBox.SelectedValue.ToString() != XmlConfig.StringValue)
                        XmlConfig.Changed = true;
                    break;

                case ControlType.RdButton:
                    if ((rdButton1.Checked && rdButton1.Tag.ToString() != XmlConfig.StringValue) || (rdButton2.Checked && rdButton2.Tag.ToString() != XmlConfig.StringValue))
                        XmlConfig.Changed = true;
                    break;
            }

            if (XmlConfig.Changed == true)
                return true;
            else
                return false;
        }

        public void SetChanges()
        {
            Changed();

            if (XmlConfig.Changed)
            {
                switch (XmlConfig.CtrlSetting.Type)
                {
                    case ControlType.TxtBox:
                    case ControlType.Path:
                    case ControlType.File:
                        XmlConfig.SetValue(txtBox.Text.Trim());
                        break;

                    case ControlType.DtTmPicker:
                        XmlConfig.SetValue(dtPicker.Value);
                        break;

                    case ControlType.NmrcUpDown:
                        XmlConfig.SetValue(Convert.ToInt32(nudNumeric.Value));
                        break;

                    case ControlType.Decimal:
                        XmlConfig.SetValue(Convert.ToDecimal(txtBox.Text.Trim()));
                        break;

                    case ControlType.ChkBox:
                        XmlConfig.SetValue(chkBox.Checked);
                        break;

                    case ControlType.DropDown:
                        XmlConfig.SetValue(cmbBox.SelectedValue.ToString());
                        break;

                    case ControlType.RdButton:
                        if (rdButton1.Checked)
                            XmlConfig.SetValue(rdButton1.Tag.ToString());
                        else
                            XmlConfig.SetValue(rdButton2.Tag.ToString());
                        break;
                }
            }
        }

        public XMLConfig GetConfig()
        {
            return XmlConfig;
        }

        private void btnBrowse_Click(object sender, EventArgs e)
        {
            switch (XmlConfig.CtrlSetting.Type)
            {
                case ControlType.Path:
                    if (XmlConfig.CtrlSetting.Desc == null)
                        folderBrowserDialog.Description = "Browse For Folder";
                    else
                        folderBrowserDialog.Description = XmlConfig.CtrlSetting.Desc;

                    if (folderBrowserDialog.ShowDialog() == DialogResult.OK)
                        txtBox.Text = folderBrowserDialog.SelectedPath;

                    break;
                case ControlType.File:
                    if (XmlConfig.CtrlSetting.Desc == null)
                        openFileDialog.Title = "Open";
                    else
                        openFileDialog.Title = XmlConfig.CtrlSetting.Desc;

                    openFileDialog.Filter = XmlConfig.CtrlSetting.Default;

                    if (openFileDialog.ShowDialog() == DialogResult.OK)
                        txtBox.Text = openFileDialog.FileName;

                    break;
            }
        }
    }

    class ComboItem
    {
        private string di;
        private string vi;

        public string Display
        {
            get
            {
                return di;
            }
        }

        public string Value
        {
            get
            {
                return vi;
            }
        }

        public ComboItem(string Display, string Value)
        {
            di = Display;
            vi = Value;
        }
    }
}
