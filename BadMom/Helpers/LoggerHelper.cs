using BadMom.BLL.Interfaces;
using BadMom.BLL.DataTransferObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BadMom.Helpers
{
    public class LoggerHelper
    {
        IBadMomDataService data;

        public LoggerHelper(IBadMomDataService service)
        {
            data = service;
        }

        public bool ErrorMessage(string code, string descriptions)
        {
            return SendLoggerMessage(logTypes.Error, code, descriptions);
        }

        public bool SecurityMessage(string code, string descriptions)
        {
            return SendLoggerMessage(logTypes.Security, code, descriptions);
        }

        public bool InfoMessage(string code, string descriptions)
        {
            return SendLoggerMessage(logTypes.Info, code, descriptions);
        }

        private bool SendLoggerMessage(logTypes type, string code, string descriptions)
        {
            try
            {
                var logEvent = new logEvents()
                {
                    Created = DateTime.Now,
                    Type = type,
                    Code = code,
                    Description = descriptions
                };
                data.SendLoggerMessage(logEvent);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}