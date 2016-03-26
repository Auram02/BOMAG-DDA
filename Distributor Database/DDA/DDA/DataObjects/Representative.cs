using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Windows.Forms;
using System.Data;
using DDA.DataAccess;

using System.ComponentModel;

namespace DDA.DataObjects
{
    class Representative
    {

        #region Private Variables
        private  int _RepID;
        private  string _Name;
        private  string _Address;
        private  string _City;
        private  string _State;
        private  int _Zip;
        private  string _Country;
        private  string _Phone;
        private  string _Fax;
        private  string _Email;
        private  string _RepType;
        private string _MobilePhone;

        #endregion

        #region Properties

        public string MobilePhone
        {
            get { return _MobilePhone; }
            set { _MobilePhone = value; }
        }

        public int RepID
        {
            get { return _RepID; }
            set { _RepID = value; }
        }

        public string Name
        {
            get { return _Name; }
            set { _Name = value; }
        }

        public  string Address
        {
            get { return _Address; }
            set { _Address = value; }
        }

        public  string City
        {
            get { return _City; }
            set { _City = value; }
        }

        public  string State
        {
            get { return _State; }
            set { _State = value; }
        }

        public  int Zip
        {
            get { return _Zip; }
            set { _Zip = value; }
        }

        public  string Country
        {
            get { return _Country; }
            set { _Country = value; }
        }

        public  string Phone
        {
            get { return _Phone; }
            set { _Phone = value; }
        }

        public  string Fax
        {
            get { return _Fax; }
            set { _Fax = value; }
        }


        public  string Email
        {
            get { return _Email; }
            set { _Email = value; }
        }

        public  string RepType
        {
            get { return _RepType; }
            set { _RepType = value; }
        }

        #endregion


        #region Methods
        public Representative()
        {
            // Constructor
        }

        public Representative(int p_RepID)
        {
            // Get rep data
            // Instantiate it in the class object.
            DataSet dsRep = new DataSet();
            dsRep = Representative_da.GetRepresentativeInformation(p_RepID);

            _RepID = Convert.ToInt32(dsRep.Tables[0].Rows[0]["RepID"]);
            _Name = dsRep.Tables[0].Rows[0]["RepName"].ToString();
            _Address = dsRep.Tables[0].Rows[0]["Address"].ToString();
            _City = dsRep.Tables[0].Rows[0]["CityName"].ToString();
            _State = dsRep.Tables[0].Rows[0]["FullName"].ToString();
            _Zip = Convert.ToInt32(dsRep.Tables[0].Rows[0]["fk_ZipID"]);
            _Country = dsRep.Tables[0].Rows[0]["CountryName"].ToString();
            _Phone = dsRep.Tables[0].Rows[0]["Phone"].ToString();
            _Fax = dsRep.Tables[0].Rows[0]["Fax"].ToString();
            _Email = dsRep.Tables[0].Rows[0]["Email"].ToString();
            _RepType = dsRep.Tables[0].Rows[0]["Description"].ToString();
            _MobilePhone = dsRep.Tables[0].Rows[0]["MobilePhone"].ToString();
            
        }


        public static DataSet LoadRepList(int p_distributorID, string repType, ref System.Windows.Forms.DataGridView dgRepList)
        {
                 BindingSource bindingSource1 = new BindingSource();
            DataSet dsRepList = new DataSet();

            dsRepList = DDA.DataAccess.Representative_da.GetRepresentativeList(repType);

            DataGridViewCheckBoxColumn myDataCol3 = new DataGridViewCheckBoxColumn();
            myDataCol3.HeaderText = "ASSOCIATE";
            myDataCol3.ThreeState = true;


            int i;

            for (i = 0; i < dsRepList.Tables[0].Rows.Count; i++)
            {
                bindingSource1.Add(new DG_Rep(dsRepList.Tables[0].Rows[i]));
            }

            // Initialize the DataGridView.
            dgRepList.AutoGenerateColumns = false;
            dgRepList.AutoSize = true;
            dgRepList.DataSource = bindingSource1;

            // Initialize and add a text box column.
            DataGridViewTextBoxColumn column = new DataGridViewTextBoxColumn();
            column.DataPropertyName = "RepID";
            column.Name = "RepID";
            column.Visible = false;
            dgRepList.Columns.Add(column);

            // Initialize and add a text box column.
             column = new DataGridViewTextBoxColumn();
            column.DataPropertyName = "RepName";
            column.Name = "REP NAME";
            dgRepList.Columns.Add(column);

            // Initialize and add a text box column.
             column = new DataGridViewTextBoxColumn();
            column.DataPropertyName = "RepCity";
            column.Name = "CITY";
            dgRepList.Columns.Add(column);

            // Initialize and add a text box column.
             column = new DataGridViewTextBoxColumn();
            column.DataPropertyName = "RepState";
            column.Name = "STATE";
            dgRepList.Columns.Add(column);

            // Initialize and add a text box column.
             column = new DataGridViewTextBoxColumn();
            column.DataPropertyName = "RepPhone";
            column.Name = "PHONE";
            dgRepList.Columns.Add(column);


            // Initialize and add a check box column.
            DataGridViewCheckBoxColumn column2 = new DataGridViewCheckBoxColumn();
            column2.ThreeState = false;
            column2.DataPropertyName = "RepAssociate";
            column2.Name = "Associate";

            if (DDA.DataObjects.AppData.FromMainDistList == true)
            {
                column2.Visible = false;
            }
            
            dgRepList.Columns.Add(column2);

            // Initialize and add a text box column.
            column = new DataGridViewTextBoxColumn();
            column.DataPropertyName = "DistID";
            column.Name = "DISTRIBUTOR ID";
            column.Visible = false;
            dgRepList.Columns.Add(column);

            DataGridViewButtonColumn dgbc1 = new DataGridViewButtonColumn();
            dgbc1.HeaderText = "ACTION";
            dgbc1.Text = "Edit";
            dgbc1.UseColumnTextForButtonValue = true;
            dgRepList.Columns.Add(dgbc1);

            DataGridViewButtonColumn dgbc2 = new DataGridViewButtonColumn();
            dgbc2.Text = "Delete";
            dgbc2.UseColumnTextForButtonValue = true;
            dgRepList.Columns.Add(dgbc2);

            dgRepList.ScrollBars = ScrollBars.Both;

            return dsRepList;
            // Initialize the form.
        }

        private class DG_Rep{

            private string id;
            private string name;
            private string city;
            private string state;
            private string phone;
            private int associate;
            private int distID;

            public DG_Rep(DataRow dr)
            {
                this.id = Convert.ToString(dr["RepID"]);
                this.name = Convert.ToString(dr["REP NAME"]);
                this.city = Convert.ToString(dr["CITY"]);
                this.state = Convert.ToString(dr["STATE"]);
                this.phone = Convert.ToString(dr["PHONE"]);
                this.distID = Convert.ToInt32(dr["DistID"]);

                // custom logic to get the values
                if (Convert.ToInt32(dr["DistID"]) == DDA.DataObjects.AppData.DistributorID)
                {
                    this.associate = 1;
                }
                else if (Convert.ToInt32(dr["DistID"]) != 0)
                {
                    this.associate = 2;
                }
                else
                {
                    this.associate = 0;
                }
            }

            public string RepName
            {
                get { return name; }
                set { name = value; }
            }

            public string RepCity
            {
                get { return city; }
                set { city = value; }
            }

            public string RepState
            {
                get { return state; }
                set { state = value; }
            }

            public string RepPhone
            {
                get { return phone; }
                set { phone = value; }
            }

            public string RepID
            {
                get { return id; }
                set { id = value; }
            }

            public int RepAssociate
            {
                get { return associate; }
                set { associate = value; }
            }

            public int DistID
            {
                get { return distID; }
                set { distID = value; }
            }

        }

        public enum Title
        {
            King,
            Sir
        };

        #endregion

    }
}
