using System;
using FieldNationApp.FieldNationSoapService;

namespace FieldNationApp.App_Code
{
    public class FieldNationLoginFactory
    {
        public static Login GetLogin()
        {
            var appSettings = System.Configuration.ConfigurationManager.AppSettings;

            return new Login
            {
                effectiveUser = appSettings.Get("FN_EFFECTIVE_USER"),
                apiKey = appSettings.Get("FN_API_KEY"),
                customerID = Int32.Parse(appSettings.Get("FN_CUSTOMER_ID"))
            };
        }
    }
}