using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AgeRanger.Infrastructure.Private
{
    public class Config
    {
        private static Config _current;
        public static Config Current
        {
            get
            {
                if (_current == null)
                {
                    _current = new Config();
                }
                return _current;
            }
        }

        public string ConnectionString
        {
            get
            {
                return System.Configuration.ConfigurationManager.ConnectionStrings["ageranger"].ConnectionString;
            }
        }
    }
}