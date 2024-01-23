using System;
using System.IO;
using static ConsoleHelper_50.Helper_50;

namespace sfModul8Final.Part2
{
    internal class Program2
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            string TagretFolder = @"C:\test\data";
            if (args.Length > 0)
            {
                Write("We have some agrument: ");
                WriteLn(args[0], ConsoleColor.White);
                TagretFolder = args[0];
            }
            if (Directory.Exists(TagretFolder))
            {
                long folderSize = 0;
                folderSize += CalcSize(TagretFolder);
                WriteLn($"Summary size of target folder is {folderSize} bytes, or {folderSize / 1024 / 1024} Mbytes", ConsoleColor.White);
            }
            else
            {
                WriteLn("Directory not exists, or path incorrect", ConsoleColor.Red);
            }
            //WriteLn("Press ENTER to exit...");
            //ReadLn();
        }
        static long CalcSize(string path)
        {
            long size = 0;
            DirectoryInfo directory = new DirectoryInfo(path);
            try
            {
                foreach (var dir in directory.GetDirectories())
                {
                    size += CalcSize(dir.FullName);
                }
                foreach (var file in directory.GetFiles())
                {
                    size += file.Length;
                }
            }
            catch (Exception e)
            {
                WriteLn($"Error while reading content of folder _{path}_: {e.Message}", ConsoleColor.Red);
            }
            return size;
        }
    }
}
