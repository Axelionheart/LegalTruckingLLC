using LegalTrucking.Tests.Acceptance.Employee.UnitTests;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using System;
using System.Diagnostics;
using System.IO;
using System.Reflection;

namespace LegalTrucking.Tests.Acceptance
{
    internal class IntakePlusBackEndServer
    {
        private Process _process;

        internal void StartServingApplication()
        {
            var projectName = "LegalTrucking.IntakePlus.Web.UI";

            var settings = ConfigHelper.GetConfig();
            var applicationPath = settings["ClientApplicationPath"];

            _process = new Process
            {
                StartInfo =
                {
                    FileName = @"dotnet.exe",
                    Arguments = $@"run {applicationPath}\{projectName}.csproj"
                }
            };
            _process.Start();
        }

        internal void StopServingApplication()
        {
            if (_process.HasExited == false)
            {
                _process.Kill();
            }
        }
    }
}