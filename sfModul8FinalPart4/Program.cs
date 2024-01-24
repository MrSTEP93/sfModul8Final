using System;
using System.Collections.Generic;
using System.IO;
using static ConsoleHelper_50.Helper_50;

namespace sfModul8FinalPart4
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string binFilePath = "students.dat";
            string TagretFolder = Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + "\\Students";
            if (args.Length > 0)
            {
                Write("We have some agrument: ");
                WriteLn($"\"{ args[0]}\",   \"{args[1] } \"", ConsoleColor.White);
                binFilePath = args[0];
                TagretFolder = args[1];
            }
            List<Student> students = new List<Student>();
            if (File.Exists(binFilePath))
            {
                ReadBinFile(binFilePath, out students);
            }
            if (students.Count > 0)
            {
                WriteTextFile(TagretFolder, ref students);
            }
        }

        static void ReadBinFile(string filePath, out List<Student> students)
        {
            using (FileStream fs = new FileStream(filePath, FileMode.Open))
            {
                students = new List<Student>();
                //using StreamReader sr = new StreamReader(fs);
                //Console.WriteLine(sr.ReadToEnd());

                fs.Position = 0;

                BinaryReader br = new BinaryReader(fs);

                while (fs.Position < fs.Length)
                {
                    Student student = new Student();
                    student.Name = br.ReadString();
                    student.Group = br.ReadString();
                    long dt = br.ReadInt64();
                    student.DateOfBirth = DateTime.FromBinary(dt);
                    student.AverageScore = br.ReadDecimal();

                    students.Add(student);
                }

                fs.Close();
            }
            //return students;
        }

        static void WriteTextFile(string dir, ref List<Student> students)
        {
            if (!Directory.Exists(dir))
            {
                try
                {
                    Directory.CreateDirectory(dir);
                }
                catch (Exception e)
                {
                    WriteLn($"Error while creating folder \"{dir}\": {e.Message} ", ConsoleColor.Red);
                }
            } 
            foreach (var student in students)
            {

            }
        }
    }
}
