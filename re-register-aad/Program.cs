using Windows.Management.Deployment;
using Windows.ApplicationModel;
using System.Diagnostics;

// See https://aka.ms/new-console-template for more information
var windir = Environment.GetEnvironmentVariable("windir");
Debug.Assert(windir != null);

PackageManager pm = new();
var names = new[] { "Microsoft.AAD.BrokerPlugin_cw5n1h2txyewy", "Microsoft.Windows.CloudExperienceHost_cw5n1h2txyewy" };
foreach (var n in names)
{
    var package = pm.FindPackagesForUser(string.Empty, n).FirstOrDefault();
    if ((package == null) || !package.Status.VerifyIsOK())
    {
        var manifest = new Uri(Path.Combine(windir, "SystemApps", n, "AppxManifest.xml"));
        var op = pm.RegisterPackageAsync(manifest, null, DeploymentOptions.ForceApplicationShutdown).GetResults();
        if (op.IsRegistered)
        {
            Console.WriteLine($"Repaired {n}");
        }
        else
        {
            Console.WriteLine($"Could not repair {n}, {op.ErrorText} : {op.ExtendedErrorCode.ToString()}");
        }
    }
    else
    {
        Console.WriteLine($"Package {n} is healthy");
    }
}
