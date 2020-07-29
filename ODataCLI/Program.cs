using System;
using System.CommandLine;
using System.CommandLine.Invocation;
using System.IO;
using System.Management.Automation;
using System.Threading.Tasks;

namespace ODataCLI
{
    class Program
    {
        static async Task Main(string[] args)
        {
            if (args.Length == 0)
            {
                Console.WriteLine("-------------");
                Console.WriteLine("Odata CLI Tool to bootstrap an Odata service");
                Console.WriteLine("-------------");
                Console.WriteLine();
                Console.WriteLine("OPTIONS");
                Console.WriteLine("--csdl <Path to metadata file>");
                Console.WriteLine("--subscriptionId <The Azure Subscription Id>");
                Console.WriteLine("--appServiceName <The App Service Name.>");
                return;
            }
            var rootCommand = new RootCommand("Odata CLI Tool to bootstrap an Odata service")
            {
                new Option(new string[] {"--csdl", "--metadata"}, "The path to the metadata file.")
                {
                    Argument = new Argument<FileInfo>()
                },
                new Option(new string[] { "--subscriptionId", "--id" }, "The Azure Subscription Id.")
                {
                    Argument = new Argument<string>()
                },
                new Option(new string[] { "--appServiceName", "--app" }, "The App Service Name.")
                {
                    Argument = new Argument<string>()
                },
            };

            rootCommand.Handler = CommandHandler.Create<FileInfo, string>(Bootstrap);
            await rootCommand.InvokeAsync(args);
        }

        static void Bootstrap(FileInfo csdl, string subscriptionId)
        {
            if (subscriptionId != null)
            {
                Console.WriteLine($"Subscription Id: {subscriptionId}");
            }

            if (csdl != null)
            {
                Console.WriteLine($"Csdl Path: {csdl.FullName}");
            }

            string projectFilePath = "";

            // Clone the repo https://github.com/habbes/ODataApiServiceHackathon.git
            string _args = "https://github.com/habbes/ODataApiServiceHackathon.git";
            Process.StartProcess("git clone ", _args);
            // Pass the csdl file to a folder in the cloned repo

            //  Save the connection string <MyProject>

            // Zip the repo

            //var script = Assembly.GetExecutingAssembly().GetManifestResourceStream("deploy.ps1");
            using (PowerShell PowerShellInst = PowerShell.Create())
            {
                //string path = System.IO.Path.GetDirectoryName(@"C:\Temp\") + "\\Get-EventLog.ps1";
                string path = @"deploy.ps1";
                if (!string.IsNullOrEmpty(path))
                    PowerShellInst
                        .AddScript(File.ReadAllText(path))
                        .AddParameter("subscriptionId", subscriptionId)
                        .AddParameter("projectFilePath", projectFilePath)
                        .Invoke();
            }
        }
    }
}
