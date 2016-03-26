using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Windows.Forms;
using System.Data;
using DDA.DataAccess;
using System.Collections;
using System.ComponentModel;


namespace DDA.DataObjects
{
    class Distributor
    {

        #region Private Variables
        
        // Variables with properties
        static string _Name;
        static string _BillingAddress;
        static string _ShippingAddress;
        static string _City;
        static string _BillingCity;
        static string _BillingState;
        static string _BillingZip;
        static string _BillingCountry;
        static string _Node;
        static string _SAP;
        static string _Email;
        static string _State;
        static string _Zip;
        static string _Country;
        static string _Phone;
        static string _Fax;
        static string _Contact;  // Should be a dropdown
        static int _PartsOnly;
        static int _manufacturerRep;
        static int _mainDistributor;

        private static ArrayList _emails;
        private static ArrayList _contacts;

        // TODO: Add in service rep list array, territory rep list array, contracts array
        // TODO: Add in counties they are in with which product lines (for lookups later)
        #endregion

        #region Properties

        public static int MainDistributor
        {
            get { return _mainDistributor; }
            set { _mainDistributor = value; }
        }

        public static int ManufacturerRep
        {
            get { return _manufacturerRep; }
            set { _manufacturerRep = value; }
        }
   
        public static int PartsOnly
        {
            get { return _PartsOnly; }
            set { _PartsOnly = value; }
        }

        public static string Name
        {
            get { return _Name; }
            set { _Name = value; }
        }
        public static string BillingAddress
        {
            get { return _BillingAddress; }
            set { _BillingAddress = value; }
        }

        public static string ShippingAddress
        {
            get { return _ShippingAddress; }
            set { _ShippingAddress = value; }
        }

        public static string City
        {
            get { return _City; }
            set { _City = value; }
        }

        public static string State
        {
            get { return _State; }
            set { _State = value; }
        }

        public static string Zip
        {
            get { return _Zip; }
            set { _Zip = value; }
        }
        public static string Country
        {
            get { return _Country; }
            set { _Country = value; }
        }
        public static string Phone
        {
            get { return _Phone; }
            set { _Phone = value; }
        }
        public static string Fax
        {
            get { return _Fax; }
            set { _Fax = value; }
        }

        public static string Contact
        {
            get { return _Contact; }
            set { _Contact = value; }
        }

        public static string BillingCity
        {
        get { return _BillingCity ;}
        set { _BillingCity = value;}
    }
        public static string BillingState
        {
        get { return _BillingState;}
        set { _BillingState = value;}
    }
        public static string BillingZip
        {
        get { return _BillingZip;}
        set { _BillingZip = value;}
    }
        public static string BillingCountry
        {
        get { return _BillingCountry;}
        set { _BillingCountry = value;}
    }
        public static string Node
        {
        get { return _Node;}
        set { _Node = value;}
    }
        public static string SAP
        {
        get { return _SAP;}
        set { _SAP = value;}
    }
        public static string Email
        {
        get { return _Email;}
        set { _Email = value;}


    }

        public static ArrayList Emails
        {
            get { return _emails; }
        }

        public static ArrayList Contacts
        {
            get { return _contacts; }
        }

        #endregion

        #region Methods

        public static void AddEmail(string p_email)
        {
            try
            {

                _emails.Add(p_email);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public static void AddContact(string p_contact)
        {
            try
            {

                _contacts.Add(p_contact);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        /// <summary>
        /// Creates a new instance of a distributor
        /// </summary>
        /// <param name="p_distID">Distributor ID to fetch the data with</param>
        public static void NewDistributor(int p_distID)
        {
            DataSet dsDI = DDA.DataAccess.Distributor_da.GetDistributorInformation(p_distID);

            _Name = dsDI.Tables[0].Rows[0]["DistName"].ToString();
            _BillingAddress = dsDI.Tables[0].Rows[0]["BillingAddress"].ToString();
            _ShippingAddress = dsDI.Tables[0].Rows[0]["ShippingAddress"].ToString();
            _City = dsDI.Tables[0].Rows[0]["CityName"].ToString();
            _State = dsDI.Tables[0].Rows[0]["FullName"].ToString();
            _Zip = dsDI.Tables[0].Rows[0]["fk_ZipID"].ToString();
            _Country = dsDI.Tables[0].Rows[0]["CountryName"].ToString();
            _Phone = dsDI.Tables[0].Rows[0]["Phone"].ToString();
            _Fax = dsDI.Tables[0].Rows[0]["Fax"].ToString();
            _Contact = dsDI.Tables[0].Rows[0]["Contacts"].ToString();
            _SAP = dsDI.Tables[0].Rows[0]["SAP"].ToString();
            _Node = dsDI.Tables[0].Rows[0]["Node"].ToString();
            //_Email = dsDI.Tables[0].Rows[0]["Email"].ToString();
            // Turned into a different method of doing the emails to allow multiple email addresses
            _BillingState = dsDI.Tables[0].Rows[0]["fk_BillingStateID"].ToString();
            _BillingZip = dsDI.Tables[0].Rows[0]["fk_BillingZipID"].ToString();
            _BillingCountry = dsDI.Tables[0].Rows[0]["fk_BillingCountryID"].ToString();
            _BillingCity = dsDI.Tables[0].Rows[0]["BillingCityName"].ToString();
            _PartsOnly = Convert.ToInt32(dsDI.Tables[0].Rows[0]["PartsOnly"].ToString());
            _manufacturerRep = Convert.ToInt32(dsDI.Tables[0].Rows[0]["ManufacturerRep"].ToString());
            _mainDistributor = Convert.ToInt32(dsDI.Tables[0].Rows[0]["MainDistributor"].ToString());

            _emails = new ArrayList();
            _emails.Clear();

            _contacts = new ArrayList();
            _contacts.Clear();

            GetEmails(p_distID);  // Get email addresses
            GetContacts(p_distID);  // Get contacts

        }

        public static void GetEmails(int p_distID)
        {
            DataSet ds;


            _emails.Clear();
            ds = DDA.DataAccess.Distributor_da.GetEmailAddresses(p_distID);

            int i;

            string sName;


            for (i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                sName = ds.Tables[0].Rows[i][1].ToString();

                if (ds.Tables[0].Rows[i][0].ToString() != "")
                    sName = sName + " - " + ds.Tables[0].Rows[i][0].ToString();

                AddEmail(sName);
            }

        }

        public static void GetContacts(int p_distID)
        {
            DataSet ds;


            _contacts.Clear();
            ds = DDA.DataAccess.Distributor_da.GetContacts(p_distID);

            int i;

            string sName;


            for (i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                sName = ds.Tables[0].Rows[i][1].ToString();

                if (ds.Tables[0].Rows[i][0].ToString() != "")
                    sName = sName + ", " + ds.Tables[0].Rows[i][0].ToString();

                 AddContact(sName);
            }

        }

        public static void GetContactNames_Titles(int p_distID, ref string p_names, ref string p_titles)
        {
            DataSet ds;


            
            ds = DDA.DataAccess.Distributor_da.GetContacts(p_distID);

            int i;

            string sName;
            string sTitle;

            p_titles = "";
            p_names = "";

            sTitle = "";

            for (i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                sName = ds.Tables[0].Rows[i][1].ToString();

                if (ds.Tables[0].Rows[i][0].ToString() != "")
                    sTitle = ds.Tables[0].Rows[i][0].ToString();

                if (p_names != "")
                    p_names = p_names + "\n";

                p_names = p_names + sName;

                if (p_titles != "")
                    p_titles = p_titles + "\n";

                if (sTitle != "")
                    p_titles = p_titles + sTitle;

            }

        }

        public static int GetRepresentativeCount(string p_type, int p_distID)
        {
            int returnInt;

            returnInt = Representative_da.GetRepresentativeCount(p_type, p_distID);

            return returnInt;
        }

        /// <summary>
        /// Constructor - not really needed.
        /// </summary>
        public Distributor()
        {
            
        }

        #endregion


        public static DataSet LoadManufacturerReps(int p_distributorID)
        {
            DataSet dsRepList = new DataSet();

            dsRepList = DDA.DataAccess.Distributor_da.GetManufacturerRepList(DDA.DataObjects.AppData.DistributorID);



            return dsRepList;
        }

        public static DataSet LoadDistList(int p_distributorID, ref System.Windows.Forms.DataGridView dgRepList, bool contractMode,string searchKey)
        {
            BindingSource bindingSource1 = new BindingSource();
            DataSet dsRepList = new DataSet();

            dsRepList = DDA.DataAccess.Distributor_da.GetDistributorBranchList(DDA.DataObjects.AppData.DistributorID, searchKey, DDA.DataObjects.AppData.IsManufacturerRep);

            DataGridViewCheckBoxColumn myDataCol3 = new DataGridViewCheckBoxColumn();
            myDataCol3.HeaderText = "ASSOCIATE";
            myDataCol3.ThreeState = true;


            int i;

            DataRow passer;

            for (i = 0; i < dsRepList.Tables[0].Rows.Count; i++)
            {
                //passer = new DataRow();
                passer = dsRepList.Tables[0].Rows[i];
                bindingSource1.Add(new DG_Dist(ref passer));
            }

            // Initialize the DataGridView.
            dgRepList.AutoGenerateColumns = false;
            dgRepList.AutoSize = true;
            dgRepList.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
            dgRepList.DataSource = bindingSource1;

            DataGridViewTextBoxColumn column = new DataGridViewTextBoxColumn();


            // Initialize and add a text box column.
            column = new DataGridViewTextBoxColumn();
            column.DataPropertyName = "distID";
            column.Name = "DISTRIBUTOR ID";
            column.Visible = false;
            dgRepList.Columns.Add(column);

            // Initialize and add a text box column.
            column = new DataGridViewTextBoxColumn();
            column.DataPropertyName = "DistName";
            column.Name = "BRANCH NAME";
            column.Visible = false;
            dgRepList.Columns.Add(column);

            // Initialize and add a text box column.
            column = new DataGridViewTextBoxColumn();
            column.DataPropertyName = "DistAddress";
            column.Name = "ADDRESS";
            dgRepList.Columns.Add(column);

            // Initialize and add a text box column.
            column = new DataGridViewTextBoxColumn();
            column.DataPropertyName = "DistCity";
            column.Name = "CITY";
            dgRepList.Columns.Add(column);

            // Initialize and add a text box column.
            column = new DataGridViewTextBoxColumn();
            column.DataPropertyName = "DistState";
            column.Name = "STATE";
            dgRepList.Columns.Add(column);

            // Initialize and add a text box column.
            column = new DataGridViewTextBoxColumn();
            column.DataPropertyName = "DistPhone";
            column.Name = "PHONE";
            dgRepList.Columns.Add(column);


            

            if (contractMode == true)
            {

                // Initialize and add a check box column.
                DataGridViewCheckBoxColumn column2 = new DataGridViewCheckBoxColumn();
                column2.ThreeState = true;
                column2.DataPropertyName = "DistAssociate";
                column2.Name = "Associate";
                dgRepList.Columns.Add(column2);

            }
            else
            {
                DataGridViewTextBoxColumn myDataCol4 = new DataGridViewTextBoxColumn();
                myDataCol4.HeaderText = "Srvc. Rep Count";
                myDataCol4.DataPropertyName = "ServRepCount";
                myDataCol4.Width = 60;
                myDataCol4.DisplayIndex = 5;

                DataGridViewTextBoxColumn myDataCol5 = new DataGridViewTextBoxColumn();
                myDataCol5.HeaderText = "Terr. Rep Count";
                myDataCol5.DataPropertyName = "TerrRepCount";
                myDataCol5.Width = 60;
                myDataCol5.DisplayIndex = 6;
                myDataCol5.Name = "Terr";

                dgRepList.Columns.Add(myDataCol4);
                dgRepList.Columns.Add(myDataCol5);

                DataGridViewButtonColumn dgbc1 = new DataGridViewButtonColumn();
                dgbc1.HeaderText = "ACTION";
                dgbc1.Text = "Edit";
                dgbc1.UseColumnTextForButtonValue = true;
                dgRepList.Columns.Add(dgbc1);

                DataGridViewButtonColumn dgbc2 = new DataGridViewButtonColumn();
                dgbc2.Text = "Delete";
                dgbc2.UseColumnTextForButtonValue = true;
                dgRepList.Columns.Add(dgbc2);
            }

            return dsRepList;
            // Initialize the form.
        }


        private class DG_Dist
        {

            private string id;
            private string name;
            private string city;
            private string state;
            private string phone;
            private int associate;
            private int distID;
            private int terrrepcount;
            private int servrepcount;
            private string address;

            public DG_Dist(ref DataRow dr)
            {
                this.address = Convert.ToString(dr["ADDRESS"]);
                this.name = Convert.ToString(dr["BRANCH NAME"]);
                this.city = Convert.ToString(dr["CITY"]);
                this.state = Convert.ToString(dr["STATE"]);
                this.phone = Convert.ToString(dr["PHONE"]);
                this.distID = Convert.ToInt32(dr["pk_DistributorID"]);


                DataSet ds;
                ds = DDA.DataAccess.Contract_da.GetDistributorContract(Convert.ToInt32(dr["pk_DistributorID"].ToString()));

                try
                {

                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        int i;

                        for (i = 0; i < ds.Tables[0].Rows.Count; i++)
                        {
                            if (Convert.ToInt32(ds.Tables[0].Rows[i]["fk_ContractID"].ToString()) == DDA.DataObjects.AppData.CurrentContract.ContractID)
                            {
                                // they are already associated to current contract.  Check the box
                                this.associate = 1;
                            }
                            else
                            {
                                this.associate = 2;
                            }
                        }

                    }
                    else
                    {
                        this.associate = 0;
                    }
                }
                catch
                {
                    this.associate = 0;
                }

                dr["srvcrep"] = Convert.ToString(this.associate);

         
                    int id;
                    id = Convert.ToInt32(dr["pk_DistributorID"]);

                    int srvRepCount = 0, terrRepCount = 0;

                    srvRepCount = DDA.DataObjects.Distributor.GetRepresentativeCount("Service", id);
                    terrRepCount = DDA.DataObjects.Distributor.GetRepresentativeCount("Territory", id);

                    this.servrepcount = srvRepCount;
                    this.terrrepcount = terrRepCount;
            }

            public string DistName
            {
                get { return name; }
                set { name = value; }
            }

            public string DistAddress
            {
                get  { return address; }
                set { address = value; }
            }

            public string DistCity
            {
                get { return city; }
                set { city = value; }
            }

            public string DistState
            {
                get { return state; }
                set { state = value; }
            }

            public string DistPhone
            {
                get { return phone; }
                set { phone = value; }
            }

            public int DistID
            {
                get { return distID; }
                set { distID = value; }
            }

            public int DistAssociate
            {
                get { return associate; }
                set { associate = value; }
            }

            public int TerrRepCount
            {

                get { return terrrepcount; }
                set { terrrepcount = value; }
            }

            public int ServRepCount
            {
                get { return servrepcount; }
                set { servrepcount = value; }
            }

        }


    }
}
