using System;
using System.IO;
using static ConsoleHelper_50.Helper_50;

namespace sfModul8Final.Part2
{
    internal class Program
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
                DirectoryInfo directory = new DirectoryInfo(TagretFolder);
                folderSize += CalcSize(directory);
                WriteLn($"Summary size of target folder is {folderSize} bytes, or {folderSize / 1024 / 1024} Mbytes", ConsoleColor.White);
            }
            else
            {
                WriteLn("Directory not exists, or path incorrect", ConsoleColor.Red);
            }
            //WriteLn("Press ENTER to exit...");
            //ReadLn();
        }
        static long CalcSize(DirectoryInfo folder)
        {
            long size = 0;
            try
            {
                foreach (var dir in folder.GetDirectories())
                {
                    size += CalcSize(dir);
                }
                foreach (var file in folder.GetFiles())
                {
                    size += file.Length;
                }
            }
            catch (Exception e)
            {
                WriteLn($"Error while reading content of folder _{folder}_: {e.Message}", ConsoleColor.Red);
            }
            return size;
        }
    }
}
