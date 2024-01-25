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
                WriteLn($"\"{ args[0]}\",   \"{args[1]} \"", ConsoleColor.White);
                binFilePath = args[0];
                TagretFolder = args[1];
            }
            List<Student> students = new List<Student>();
            if (File.Exists(binFilePath))
            {
                //WriteLn("Writing NEW students to BIN file...");
                //WriteNewStudentsToBinFile();

                WriteLn("Reading students from binary file...");
                ReadBinFile(binFilePath, ref students);
                WriteLn("Reading students from NEW binary file...");
                ReadBinFile("students2.dat", ref students);
                WriteLn($"Successfully read {students.Count} records", ConsoleColor.White);
            }
            if (students.Count > 0)
            {
                WriteLn("Writing students to text file...");
                WriteTextFile(TagretFolder, ref students);
                WriteLn("Operation complete", ConsoleColor.White);
            }
        }

        static void ReadBinFile(string filePath, ref List<Student> students)
        {
            using (FileStream fs = new FileStream(filePath, FileMode.Open))
            {
                //students = new List<Student>();   // what the hell is going here???
                try
                {
                    BinaryReader br = new BinaryReader(fs);
                    while (fs.Position < fs.Length)
                    {
                        Student student = new Student();
                        student.Name = br.ReadString();
                        student.Group = br.ReadString();
                        long tempDate = br.ReadInt64();
                        student.BirthDate = DateTime.FromBinary(tempDate);
                        student.AverageScore = br.ReadDecimal();
                        students.Add(student);
                    }

                    fs.Close();
                } catch (Exception e)
                {
                    Write($"Data Error! ");
                    WriteLn(e.Message, ConsoleColor.Red);
                }
            }
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
                    Write($"Error while creating folder \"{dir}\": ");
                    WriteLn(e.Message, ConsoleColor.Red);
                }
            } 
            foreach (var student in students)
            {
                string fileName = $"{dir}\\{student.Group}.txt";
                try
                {
                    using (FileStream fs = new FileStream(fileName, FileMode.Append))
                    {
                        StreamWriter writer = new StreamWriter(fs);
                        writer.Write(student.Name + ", ");
                        writer.Write(student.BirthDate.ToString() + ", ");
                        writer.Write(student.AverageScore);
                        writer.WriteLine();
                        writer.Close();
                        fs.Close();
                    }
                }
                catch (Exception e)
                {
                    Write($"Error while creating text file  \"{fileName}\": ");
                    WriteLn(e.Message, ConsoleColor.Red);
                }
            }
        }

        static void WriteNewStudentsToBinFile()
        {
            List<Student> students = new List<Student>
            {
                new Student { Name = "Пёс", Group = "G1", BirthDate = new DateTime(2000, 1, 12), AverageScore = 5.6M },
                new Student { Name = "Тарас", Group = "P7", BirthDate = new DateTime(1999, 9, 9), AverageScore = 2.5M},
                new Student { Name = "Светка", Group = "P7", BirthDate = new DateTime(1998, 11, 7), AverageScore = 5M},
                new Student { Name = "Олех", Group = "F2", BirthDate = new DateTime(1989, 4, 3), AverageScore = 3.1M},
                new Student { Name = "Саша", Group = "G1", BirthDate = new DateTime(2001, 8, 30), AverageScore = 4.4M}
            };
            string fileName = "students2.dat";
            using FileStream fs = new FileStream(fileName, FileMode.Create);
            using BinaryWriter bw = new BinaryWriter(fs);

            foreach (Student student in students)
            {
                bw.Write(student.Name);
                bw.Write(student.Group);
                bw.Write(student.BirthDate.ToBinary());
                bw.Write(student.AverageScore);
            }
            bw.Close();
            fs.Close();
        }

    }
}
