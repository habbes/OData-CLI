using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace ODataCLI
{
    class CsdlUpdater
    {
        public static void UpdateProjectConfig(string projectRoot, string csdlFile, string connectionString)
        {
            string mainProject = "Hackathon2020.Poc01";

            var csdlTarget = Path.Combine(projectRoot, mainProject, "project.csdl");
            var connStringTarget = Path.Combine(projectRoot, mainProject, "connectionstring.txt");

            File.Copy(csdlFile, csdlTarget, true);
            File.WriteAllText(connStringTarget, connectionString);
        }
    }
}
