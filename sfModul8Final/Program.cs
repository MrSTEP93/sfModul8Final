﻿using System;
using System.IO;
using System.Text;
using static ConsoleHelper_50.Helper_50;
using static System.Net.Mime.MediaTypeNames;

namespace sfModul8Final.Part1
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
                Write("We have some agrument: ");
                WriteLn(args[0], ConsoleColor.White);
                TagretFolder = args[0];
            }
            if (Directory.Exists(TagretFolder))
            {
                DeleteOldFiles(TagretFolder, checkMinutes);
            }
            else
            {
                WriteLn("Directory not exists, or path incorrect", ConsoleColor.Red);
            }
            WriteLn("Press ENTER to exit...");
            ReadLn();
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
                WriteLn("Error while reading folders: " + e.Message, ConsoleColor.Red);
            }
            WriteLn("========== THE END ==========", ConsoleColor.White);
        }
    }
}
