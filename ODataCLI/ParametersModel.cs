using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace ODataCLI
{
    class ParametersModel
    {
        [JsonPropertyName("$schema")]
        public string Schema { get; set; }

        [JsonPropertyName("contentVersion")]
        public string ContentVersion { get; set; }

        [JsonPropertyName("parameters")]
        public InnerParameters Parameters { get; set; }

    }

    class InnerParameters
    {
        [JsonPropertyName("sqlAdministratorLogin")]
        public string SqlAdminstratorLogin { get; set; }

        [JsonPropertyName("sqlAdministratorLoginPassword")]
        public string SqlAdminstratorLoginPassword { get; set; }

        [JsonPropertyName("siteName")]
        public string SiteName { get; set; }

        [JsonPropertyName("sqlServerName")]
        public string SqlServerName { get; set; }

        [JsonPropertyName("databaseName")]
        public string DatabaseName { get; set; }
    }
}
