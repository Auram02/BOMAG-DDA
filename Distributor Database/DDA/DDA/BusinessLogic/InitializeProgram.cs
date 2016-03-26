using System;
using System.Collections.Generic;
using System.Text;

namespace DDA.BusinessLogic
{
    class InitializeProgram
    {
        private static XmlConfig.Config xcfg = new XmlConfig.Config();

        public static void InitializeGlobals()
        {

            LoadXMLData();

            DataLogic.DBA.DataLogic.SetupConnection();
            DDA.DataObjects.AppData.CurrentContract.InitializeArrays();
        }

        private static void LoadXMLData()
        {
            xcfg.cfgFile = "Settings.xml";
            DataLogic.DataAccessVariables.database_location = xcfg.GetValue("//Settings//DatabasePath");

        }

    }
}
