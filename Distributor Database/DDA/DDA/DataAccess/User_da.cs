using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace DDA.DataAccess
{
    class User_da
    {
        public static bool DoLogin(string login, string password, ref bool isAdmin)
        {
            bool result;
            result = false;

            DataSet dsUser;
            string sql;

            sql = "SELECT COUNT(userID) FROM [Users] WHERE [Login] = '" + login + "' AND [Password] = '" + password + "'";
            dsUser = DataLogic.DBA.DataLogic.Read(sql);

            
            try
            {
                if (Convert.ToInt32(dsUser.Tables[0].Rows[0][0]) > 0)
                {
                    // read our user
                    sql = "SELECT * FROM Users WHERE [Login] = '" + login + "' AND [Password] = '" + password + "'";
                    dsUser = DataLogic.DBA.DataLogic.Read(sql);

                    isAdmin = Convert.ToBoolean(dsUser.Tables[0].Rows[0]["Administrator"]);
                    DDA.DataObjects.AppData.UserIsAdmin = isAdmin;
                    DDA.DataObjects.AppData.UserName = login;
                    result = true;
                }
            } catch {
                result = false;
            }


            return result;
        }

        public static void AddUser(string p_name, string p_password, int p_Admin)
        {
            string sql;

            int nextID = DataLogic.DBA.DataLogic.GetNextID("Users", "userID");

            sql = "INSERT INTO Users VALUES (" + nextID + ",'" + p_name + "','" + p_password + "'," + p_Admin + ")";

            DataLogic.DBA.DataLogic.Update(sql);

        }

        public static void UpdateUser(int p_id, string p_name, string p_password, int p_Admin)
        {
            string sql;

            int nextID = DataLogic.DBA.DataLogic.GetNextID("Users", "userID");

            sql = "UPDATE Users SET [Login] = '" + p_name + "', [Password] = '" + p_password + "', Administrator = " + p_Admin + " WHERE userID = " + p_id;

            DataLogic.DBA.DataLogic.Update(sql);

        }
        
        public static void RemoveUser(int p_id)
        {
            string sql;

            sql = "DELETE FROM Users WHERE userID = " + p_id;

            DataLogic.DBA.DataLogic.Update(sql);

        }


        public static DataSet GetUsers()
        {
            DataSet ds;
            string sql;


            sql = "SELECT * FROM Users";

            ds = DataLogic.DBA.DataLogic.Read(sql);

            return ds;
        }
    }
}
