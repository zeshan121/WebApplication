using System;
using System.Collections.Generic;
using System.Text;

namespace MyWebApp.Core.Services
{
    public class GamingMachineServiceErrors
    {
        public const string ADD_ERROR_CODE = "ADD_01";
        public const string UPDATE_ERROR_CODE = "UPDATE_01";
        public const string DELETE_ERROR_CODE = "DEL_01";
        public const string GAMENAME_ERROR_CODE = "GAMENAME_01";
        public const string GAMENAME_ERROR_MESSAGE = "Game name is required";
        public const string SERIALNUMBER_ERROR_CODE = "SERIALNUMBER_01";
        public const string SERIALNUMBER_ERROR_MESSAGE = "Gaming Serial Number is not unique.";
    }
}
