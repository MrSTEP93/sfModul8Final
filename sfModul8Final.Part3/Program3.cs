using System;
using System.IO;
using static ConsoleHelper_50.Helper_50;

namespace sfModul8Final.Part3
{
    internal class Program3
    {

        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            const byte checkMinutes = 120;
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
                folderSize = CalcSize(TagretFolder);
                WriteLn($"Original size of directory {TagretFolder} is {folderSize} bytes, or {folderSize / 1024 / 1024} Mbytes", ConsoleColor.White);
                WriteLn($"Now we start deleting objects... ");
                DeleteOldFiles(TagretFolder, checkMinutes);

                folderSize = CalcSize(TagretFolder);
                WriteLn($"Final size of directory {TagretFolder} is {folderSize} bytes, or {folderSize / 1024 / 1024} Mbytes", ConsoleColor.White);
            }
            else
            {
                WriteLn("Directory not exists, or path incorrect", ConsoleColor.Red);
            }
            //WriteLn("Press ENTER to exit...");
            //ReadLn();
        }

        static void DeleteOldFiles(string path, byte minuteInterval)
        {
            DirectoryInfo directory = new DirectoryInfo(path);
            WriteLn("========== FOLDERS ==========", ConsoleColor.White);
            try
            {
                foreach (var folder in directory.GetDirectories())
                {
                    if ((DateTime.Now - folder.LastAccessTime) > TimeSpan.FromMinutes(minuteInterval))
                    {
                        Directory.Delete(folder.FullName, true);
                        WriteLn(folder.FullName + " deleted", ConsoleColor.Blue);
                    }
                }
            }
            catch (Exception e)
            {
                WriteLn("Error while reading folders: " + e.Message, ConsoleColor.Red);
            }

            WriteLn("==========  FILES  ==========", ConsoleColor.White);
            try
            {
                foreach (var file in directory.GetFiles())
                {
                    if ((DateTime.Now - file.LastAccessTime) > TimeSpan.FromMinutes(minuteInterval))
                    {
                        File.Delete(file.FullName);
                        WriteLn(file.FullName + " deleted", ConsoleColor.Blue);
                    }
                }
            }
            catch (Exception e)
            {
                WriteLn("Error while reading files: " + e.Message, ConsoleColor.Red);
            }
            WriteLn("========== THE END ==========", ConsoleColor.White);
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
