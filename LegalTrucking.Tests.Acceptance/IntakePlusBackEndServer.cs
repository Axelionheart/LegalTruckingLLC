using System;
using System.Diagnostics;
using System.IO;
using System.Reflection;

namespace LegalTrucking.Tests.Acceptance
{
    internal class IntakePlusBackEndServer
    {
        private Process _process;
        private readonly string ui_project_path = "D:\\dev\\LegalTrucking\\LegalTrucking.Web.Ui";
        internal void StartServingApplication()
        {
            var projectName = "LegalTrucking.IntakePlus.Web.UI";
            var applicationPath = ui_project_path;
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