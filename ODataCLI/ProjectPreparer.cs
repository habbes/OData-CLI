using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace ODataCLI
{
    class ProjectPreparer
    {

        string projectRoot;
        string sourceCsdlFile;
        string siteName;
        string dbUser;
        string dbPassword;

        public string ZipPath { get; private set; }
        public string ParamsPath { get; private set; }

        public string SqlServerName { get { return $"sqlServer_{siteName}"; } }

        private string ConnectionString
        {
            get
            {
                return $"Server=tcp:{SqlServerName}.database.windows.net,1433;Database={siteName};User ID={dbUser}@{SqlServerName};Password={dbPassword};Trusted_Connection=False;Encrypt=True;";
            }
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="projectRoot">Root of the project to deploy</param>
        /// <param name="sourceCsdlFile">The path of the CSDL file</param>
        /// <param name="siteName">The app's name. This will be the subdomain of the deployed service. It will also be used in the db name</param>
        public ProjectPreparer(string projectRoot, string sourceCsdlFile, string siteName)
        {
            this.projectRoot = projectRoot;
            this.sourceCsdlFile = sourceCsdlFile;
            this.siteName = siteName;
            this.dbUser = "HackathonUser";
            this.dbPassword = "HackathonPassword";

        }

        public void UpdateProjectConfig()
        {
            string mainProject = "Hackathon2020.Poc01";

            var csdlTarget = Path.Combine(projectRoot, mainProject, "project.csdl");
            var connStringTarget = Path.Combine(projectRoot, mainProject, "connectionstring.txt");

            File.Copy(sourceCsdlFile, csdlTarget, true);
            File.WriteAllText(connStringTarget, ConnectionString);
        }

        public void CreateZip()
        {
            ZipPath = Path.GetTempFileName();
            ZipFile.CreateFromDirectory(projectRoot, ZipPath);
        }

        public void GenerateParamsJson()
        {
            var parameters = new ParametersModel()
            {
                Schema = "https://schema.management.azure.com/schemas/2019-04-01/deploymentParameters.json#",
                ContentVersion = "1.0.0.0",
                Parameters =
                {
                    SiteName = siteName,
                    SqlServerName = SqlServerName,
                    DatabaseName = siteName,
                    SqlAdminstratorLogin = dbUser,
                    SqlAdminstratorLoginPassword = dbPassword
                }
            };

            var json = JsonSerializer.Serialize(parameters);
            ParamsPath = Path.GetTempFileName();
            File.WriteAllText(ParamsPath, json);
        }

        public void Prepare()
        {
            UpdateProjectConfig();
            CreateZip();
            GenerateParamsJson();
        }
    }
}

