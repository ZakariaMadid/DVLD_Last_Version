using System;
using System.Configuration;
namespace DVLD_DataAccess
{
    //Old hard Method
  
    //        public static string ConnectionString = "Server=.;Database=DVLD;User Id=sa;Password=123456;";

    //New Method using app.config
    static class clsDataAccessSettings
    {
        public static string ConnectionString {
            get
            {
               return ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
            }
        }
    }
}
