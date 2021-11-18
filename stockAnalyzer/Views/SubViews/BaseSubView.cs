using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace stockAnalyzer
{
    public partial class BaseSubView : UserControl
    {
        private BaseInvestmentType baseData;

        //
        // Constructor
        //
        #region CONSTRUCTOR_DESTRUCTOR
        public BaseSubView(BaseInvestmentType baseData)
        {
            InitializeComponent();
            this.baseData = baseData;
        } // Constructor
        #endregion CONSTRUCTOR_DESTRUCTOR

        //
        // UI Events
        //
        #region UI_EVENTS
        private void BaseSubView_Load(object sender, EventArgs e)
        {
            this.lvDetails.MultiSelect = true;
            this.lvDetails.FullRowSelect = true;

            ColumnHeader chDate = new ColumnHeader();
            chDate.Text = "Date-Time";
            chDate.Name = "chDateTime";
            chDate.Width = 100;
            this.lvDetails.Columns.Add(chDate);

            ColumnHeader chPrice = new ColumnHeader();
            chPrice.Text = "Price";
            chPrice.Name = "chPrice";
            chPrice.Width = 100;
            this.lvDetails.Columns.Add(chPrice);

            ColumnHeader chVolume = new ColumnHeader();
            chVolume.Text = "Volume";
            chVolume.Name = "chVolume";
            chVolume.Width = 150;
            this.lvDetails.Columns.Add(chVolume);
        } // BaseSubView_Load()
        #endregion UI_EVENTS

        //
        // Protected methods for children classes
        //
        #region PROTECTED_METHODS
        protected void AddColumnHeader(ColumnHeader ch)
        {
            this.lvDetails.Columns.Add(ch);
        } // AddColumnHeader()

        protected void AddDataRows(List<List<string>> listRows)
        {
            foreach(List<string> row in listRows)
            {
                ListViewItem item = new ListViewItem();
                for(int i = 0; i < row.Count; i++)
                {
                    if (i == 0)
                    {
                        item.Text = row[i];
                    }
                    else
                    {
                        item.SubItems.Add(row[i]);
                    }
                }
                this.lvDetails.Items.Add(item);
            }
        } // AddDataRows()

        protected string GetNumDecimalPlaces(double value)
        {
            string strDecimalPlaces = "0";
            string strValue = value.ToString();

            if (strValue.Contains("."))
            {
                string afterDec = strValue.Substring(strValue.IndexOf('.') + 1);
                strDecimalPlaces = afterDec.Length.ToString();
            }

            return strDecimalPlaces;
        } // GetNumDecimalPlaces()
        #endregion PROTECTED_METHODS
    }
}
