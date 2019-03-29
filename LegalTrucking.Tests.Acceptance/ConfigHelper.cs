using System;
using System.Collections.Generic;
using System.Text;

namespace LegalTrucking.Tests.Acceptance
{
    using Microsoft.Extensions.Configuration;
    namespace Employee.UnitTests
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
}
