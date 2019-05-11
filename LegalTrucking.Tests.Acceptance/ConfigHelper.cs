using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.Configuration;

namespace LegalTrucking.Tests.Acceptance
{
        public static class ConfigHelper
        {
            public static IConfiguration GetConfig()
            {
                var builder = new ConfigurationBuilder().
                    SetBasePath(System.AppContext.BaseDirectory).
                    AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);

                return builder.Build();
            }
        }
   
}
