// See https://aka.ms/new-console-template for more information
using System.Runtime.InteropServices;

string targetDir = args.FirstOrDefault(Directory.GetCurrentDirectory());
EnumerationOptions opts = new();
opts.RecurseSubdirectories = true;
opts.ReturnSpecialDirectories = false;
List<SafeHandle> handles = new();
foreach (var f in Directory.EnumerateFileSystemEntries(targetDir, "*", opts))
{
    try
    {
        if ((File.GetAttributes(f) & FileAttributes.Directory) == FileAttributes.Directory)
        {
            Console.WriteLine($"skipped {f} as it's a directory");
        }
        else
        {
            handles.Add(File.OpenHandle(f, FileMode.Open, FileAccess.Read, FileShare.None, FileOptions.None));
            Console.WriteLine($"opened {f}");
        }
    }
    catch(Exception e)
    {
        Console.WriteLine($"Could not open {f}, error {e}");
    }
}
Console.WriteLine($"Opened {handles.Count} files, hit a key to close them...");
Console.ReadKey();
