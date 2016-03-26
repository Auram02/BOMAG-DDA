using System;
using System.Collections.Generic;
using System.Text;

using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Globalization;
using System.Threading;

namespace DDA.DataObjects
{
    class AppData
    {

        private const int MF_BYPOSITION = 0x0400;
        private const int MF_DISABLED = 0x0002;

        [DllImport("user32.dll")]
        private static extern int GetSystemMenu(int hwnd, int revert);

        [DllImport("user32.dll")]
        private static extern int GetMenuItemCount(int menu);

        [DllImport("user32.dll")]
        private static extern int RemoveMenu(int menu, int position, int flags);

        public static void DisableCloseButton(IntPtr myHandle )
        {

            int menu = GetSystemMenu(myHandle.ToInt32(), 0);
            int count = GetMenuItemCount(menu);
            RemoveMenu(menu, count - 1, MF_DISABLED | MF_BYPOSITION);
        }
 

        //private static Distributor _distributor;
        private static string _representativeMode;
        private static string _distributorMode;
        private static string _distributorEditType;  // if editing main distributor or branch
        private static int _distributorID;
        private static int _RepID;
        private static int _branchID;
        private static string _previousForm;
        private static string _contractMode;
        private static bool _distributorBranchListContractMode;
        public static DDA.DataObjects.Contract CurrentContract = new Contract();
        private static bool _exitapplication;
        public static frmMain activeMain;
        private static bool _UserIsAdmin;
        private static bool _IsLoggedIn;
        private static string _userName;
        private static bool _ViewContractMode;
        private static bool _fromMainDistList;
        private static string _NewRepName;
        private static bool _DataChanged;
        private static string _ReportName;
        private static int _isManufacturerRep;

        /*
        public static Distributor Distributor
        {
            get
            { return _distributor; }

            set
            { _distributor = value; }
        }
        */

        public static string ToTitleCase(string p_in)
        {
            // REMOVED: 10/12/2007 at request of client

            //string sReturn;

            ////Get the culture property of the thread.
            //CultureInfo cultureInfo = Thread.CurrentThread.CurrentCulture;

            ////Create TextInfo object.
            //TextInfo textInfo = cultureInfo.TextInfo;

            //sReturn = (textInfo.ToTitleCase(p_in.ToLower()));

            //return sReturn;
            return p_in;
        }

        public static string ReportName
        {
            get
            {
                return ReportName;
            }
            set
            {
                _ReportName = value;
            }
        }

        public static bool DataChanged
        {
            get { return _DataChanged; }
            set { _DataChanged = value; }
        }

        public static string NewRepName
        {
            get { return _NewRepName; }
            set { _NewRepName = value; }
        }

        public static bool FromMainDistList
        {
            get { return _fromMainDistList; }
            set { _fromMainDistList = value; }
        }

        public static bool ViewContractMode
        {
            get { return _ViewContractMode; }
            set { _ViewContractMode = value; }
        }

        public static bool IsLoggedIn
        {
            get { return _IsLoggedIn; }
            set { _IsLoggedIn = value; }
        }

        public static string UserName
        {
            get { return _userName; }
            set { _userName = value; }
        }

        public static bool UserIsAdmin
        {
            get { return _UserIsAdmin; }
            set { _UserIsAdmin = value; }
        }

        public static bool ExitApplication
        {
            get { return _exitapplication; }
            set { _exitapplication = value; }
        }

        public static bool DistributorBranchListContractMode
        {
            get { return _distributorBranchListContractMode; }
            set { _distributorBranchListContractMode = value; }
        }

        public static string ContractMode
        {
            get { return _contractMode; }
            set { _contractMode = value; }
        }

        public static int BranchID
        {
            get { return _branchID; }
            set { _branchID = value; }
        }


        public static string RepMode
        {
            get { return _representativeMode; }
            set { _representativeMode = value; }
        }

        public static string DistributorMode
        {
            get { return _distributorMode; }
            set { _distributorMode = value; }
        }

        public static int DistributorID
        {
            get { return _distributorID; }
            set { _distributorID = value; }
        }

        public static int RepID
        {
            get { return _RepID; }
            set { _RepID = value; }
        }

        public static string PreviousForm
        {
            get { return _previousForm; }
            set { _previousForm = value; }
        }

        public static string DistributorEditType
        {
            get { return _distributorEditType; }
            set { _distributorEditType = value; }
        }

        public static int IsManufacturerRep
        {
            get { return _isManufacturerRep; }
            set { _isManufacturerRep = value; }
        }
   
    }
}
