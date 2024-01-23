using System;
using System.IO;
using System.Text;
using static ConsoleHelper_50.Helper_50;
using static System.Net.Mime.MediaTypeNames;

namespace sfModul8Final
{
    internal class Program
    {

        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            const byte checkMinutes = 120;
            string TagretFolder = @"C:\test\data";
            if (args.Length > 0)
            {
                Write("We have some agrument: ", ConsoleColor.Green);
                WriteLn(args[0], ConsoleColor.White);
                TagretFolder = args[0];
            }

            DeleteOldFiles(TagretFolder, checkMinutes);
            WriteLn("Press ENTER to exit...", ConsoleColor.White);
            ReadLn();
        }

        static void DeleteOldFiles(string path, byte minuteInterval)
        {
            if (Directory.Exists(path))
            {
                DirectoryInfo directory = new DirectoryInfo(path);
                //WriteLn("Folders older than " + minuteInterval + " minutes (Parameter LastAccessTime):", ConsoleColor.White);
                WriteLn("========== FOLDERS ==========", ConsoleColor.White);
                try
                {
                    foreach (var folder in directory.GetDirectories())
                    {
                        Write(folder.Name);
                        WriteLn("   access: " + folder.LastAccessTime + "   write: " + folder.LastWriteTime, ConsoleColor.Green);
                        WriteLn((DateTime.Now - folder.LastAccessTime).ToString(), ConsoleColor.Blue);
                        if ((DateTime.Now - folder.LastAccessTime) > TimeSpan.FromMinutes(minuteInterval))
                        {
                            //WriteLn(folder.Name + "is OLDER!!", ConsoleColor.Red);
                            WriteLn("OLDER!!", ConsoleColor.Red);
                        }
                    }
                }
                catch (Exception e)
                {
                    WriteLn("Error while reading folders: " + e, ConsoleColor.Red);
                }

                WriteLn("========== FILES ==========", ConsoleColor.White);
                try
                {
                    foreach (var file in directory.GetFiles())
                    {
                        Write(file.Name);
                        WriteLn("   access: " + file.LastAccessTime + "   write: " + file.LastWriteTime, ConsoleColor.Green);
                        WriteLn((DateTime.Now - file.LastAccessTime).ToString(),ConsoleColor.Blue);
                        if ((DateTime.Now - file.LastAccessTime) > TimeSpan.FromMinutes(minuteInterval))
                        {
                            //WriteLn(file.Name);
                            WriteLn("OLDER!!", ConsoleColor.Red);
                        }
                    }
                }
                catch (Exception e)
                {
                    WriteLn("Error while reading folders: " + e, ConsoleColor.Red);
                }
                WriteLn("========== THE END ==========", ConsoleColor.White);
            }
            else
            {
                WriteLn("Directory not exists, or path incorrect", ConsoleColor.Red);
            }
        }
    }
}
