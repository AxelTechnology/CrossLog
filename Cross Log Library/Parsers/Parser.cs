using Cross_Log_Library.Classes;
using Cross_Log_Library.Interfaces;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace Cross_Log_Library.Parsers
{
    /// <summary> 
    /// Implement the parser class.
    /// </summary>
    public abstract class Parser : IParser
    {
        /// <summary> 
        /// Execute the parsing process.
        /// </summary>
        /// <param name="path">The file path to parse.</param>
        public List<Log> Parse(string path)
        {

            List<Log> list = new List<Log>();
            string[] lines = System.IO.File.ReadAllLines(path);
            int n = 0;
            foreach (string line in lines)
            {
                if (Char.IsDigit(line[0]))
                {
                    
                    if (n % 2 != 0)
                    {
                        list.Add(new Log(
                        ParseSoftwareName(line, path),
                        ParseTitle(line),
                        ParseDescription(line),
                        ParseDateLog(line),
                        ParseLogType(line),
                        Color.White,
                        Color.Black));
                    }
                    if (n%2==0)
                    {
                        list.Add(new Log(
                        ParseSoftwareName(line, path),
                        ParseTitle(line),
                        ParseDescription(line),
                        ParseDateLog(line),
                        ParseLogType(line),
                        Color.FromArgb(245, 245, 245),
                        Color.Black));
                    }
                    if (list[n].LogDescription == "")
                    {
                        list[n].ForeColor = Color.DimGray;
                    }
                    if (list[n].Type.Type == "ERROR")
                    {
                        list[n].ForeColor = Color.Red;
                        list[n].BackColor = Color.White;
                    }
                     n++;
                }

               
            }
            return list;
        }
        /// <summary>
        /// Execute the parsing of the log type.
        /// </summary>
        /// <param name="string">The string to parse.</param>
        internal abstract LogType ParseLogType(string line);
        /// <summary> 
        /// Execute the parsing of the date.
        /// </summary>
        /// <param name="line">The string to parse.</param>
        public virtual DateLog ParseDateLog(string line)
        {
            try
            {
                Int32.Parse(line[0].ToString());
            }
            catch
            {
                return new DateLog(new DateTime(1, 1, 1, 0, 0, 0, 0), 0);
            }
            var Year = Int32.Parse(line[6].ToString()) * 1000 + Int32.Parse(line[7].ToString()) * 100 + Int32.Parse(line[8].ToString()) * 10 + Int32.Parse(line[9].ToString());
            var Month = Int32.Parse(line[3].ToString()) * 10 + Int32.Parse(line[4].ToString());
            var Day = Int32.Parse(line[0].ToString()) * 10 + Int32.Parse(line[1].ToString());
            var Hour = Int32.Parse(line[11].ToString()) * 10 + Int32.Parse(line[12].ToString());
            var Minute = Int32.Parse(line[14].ToString()) * 10 + Int32.Parse(line[15].ToString());
            var Second = Int32.Parse(line[17].ToString()) * 10 + Int32.Parse(line[18].ToString());
            var Cents = 0;

            var firstIndex = line.IndexOf(' ');
            line = line.Remove(0, firstIndex + 1);
            firstIndex = line.IndexOf(' ');
            line = line.Remove(firstIndex);
            var lastIndex = line.LastIndexOf('.');
            line = line.Remove(0, lastIndex + 1);
            if (line.Length == 2)
            {
                Cents = Int32.Parse(line[0].ToString()) * 100 + Int32.Parse(line[1].ToString()) * 10;
            }
            else
            {
                Cents = Int32.Parse(line[0].ToString()) * 100 + Int32.Parse(line[1].ToString()) * 10 + Int32.Parse(line[2].ToString());
            }

            DateLog tmp = new DateLog(new DateTime(1, 1, 1, Hour, Minute, Second, Cents), 0);
            return tmp;

        }
        /// <summary> 
        /// Execute the parsing of the description.
        /// </summary>
        /// <param name="line">The string to parse.</param>
        internal abstract string ParseDescription(string line);
        /// <summary> 
        /// Execute the parsing of the title.
        /// </summary>
        /// <param name="line">The string to parse.</param>
        internal abstract string ParseTitle(string line);
        /// <summary> 
        /// Execute the parsing of the software name.
        /// </summary>
        /// <param name="path">The file path to parse.</param>
        /// <param name="line">The string to parse.</param>
        internal virtual string ParseSoftwareName(string line, string path)
        {
            bool control = false;
            List<char> name = new List<char>();
            foreach (char c in path)
            {
                if (c == '.')
                {
                    control = false;
                }
                if (control == true)
                {
                    name.Add(c);
                }
                if (c == '_')
                {
                    control = true;
                }
            }

            return new String(name.ToArray());
        }
        /// <summary> 
        /// Parse the date from the file name.
        /// </summary>
        /// <param name="path">The file path.</param>
        public virtual DateTime GetDateFromFileName(string path)
        {
            path = Path.GetFileName(path);

            try
            {
                Int32.Parse(path[0].ToString());
            }
            catch
            {
                MessageBox.Show("Cannot read the file date from the file name.", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return new DateTime(1, 1, 1, 0, 0, 0, 0);
            }
            var Year = Int32.Parse(path[0].ToString()) * 1000 + Int32.Parse(path[1].ToString()) * 100 + Int32.Parse(path[2].ToString()) * 10 + Int32.Parse(path[3].ToString());
            var Month = Int32.Parse(path[4].ToString()) * 10 + Int32.Parse(path[5].ToString());
            var Day = Int32.Parse(path[6].ToString()) * 10 + Int32.Parse(path[7].ToString());
            return new DateTime(Year, Month, Day, 0, 0, 0, 0);

        }
    }
}