using Cross_Log_Library.Classes;
using System;
using System.IO;
using System.Windows.Forms;

namespace Cross_Log_Library.Parsers
{
    public class ParserMode3 : Parser
    {
        #region Privates
        /// <summary>
        /// Return the name of the software.
        /// </summary>
        /// <param name="line">The string to parse.</param>
        /// <returns></returns>
        internal override string ParseSoftwareName(string line, string path)
        {
            return "DJPro";
        }
        /// <summary>
        /// Return the date of the parsed string.
        /// </summary>
        /// <param name="line">The string to parse.</param>
        /// <returns></returns>
        public override DateLog ParseDateLog(string line)
        {
            var Anno = Int32.Parse(line[3].ToString()) * 1000 + Int32.Parse(line[4].ToString()) * 100 + Int32.Parse(line[5].ToString()) * 10 + Int32.Parse(line[6].ToString());
            var Mese = Int32.Parse(line[7].ToString()) * 10 + Int32.Parse(line[8].ToString());
            var Giorno = Int32.Parse(line[9].ToString()) * 10 + Int32.Parse(line[10].ToString());

            var Hour = (ParseMilliseconds(line) / 3600000);
            var Minute = ((ParseMilliseconds(line) / 60000) - (Hour * 60));
            var Second = ((ParseMilliseconds(line) / 1000) - (Hour * 3600) - (Minute * 60));
            var Cents = ((ParseMilliseconds(line) / 10) - (Hour * 360000) - (Minute * 6000) - (Second * 100));
            DateLog tmp = new DateLog(new DateTime(1, 1, 1, Hour, Minute, Second, Cents), 0);
            return tmp;
        }
        /// <summary>
        /// Return the log typeof the parsed string.
        /// </summary>
        /// <param name="line">The string to parse.</param>
        /// <returns></returns>
        internal override LogType ParseLogType(string line)
        {
            return new LogType("LOG");
        }
        /// <summary>
        /// Return the description of parsed strings.
        /// </summary>
        /// <param name="line">The string to parse.</param>
        /// <returns></returns>
        internal override string ParseDescription(string line)
        {
            line = line.Remove(0, 13);

            return line;

        }
        /// <summary>
        /// Return the date of parsed strings.
        /// </summary>
        /// <param name="line">The string to parse.</param>
        /// <returns></returns>
        private int ParseMilliseconds(string line)
        {

            line = line.Remove(line.LastIndexOf(','));
            line = line.Remove(line.LastIndexOf(','));
            line = line.Remove(line.LastIndexOf(','));
            line = line.Remove(line.LastIndexOf(','));
            line = line.Remove(0, line.LastIndexOf(',') + 1);

            return Int32.Parse(line);
        }
        /// <summary> 
        /// Parse the date from the file name.
        /// </summary>
        /// <param name="path">The file path.</param>
        public override DateTime GetDateFromFileName(string path)
        {
            Path.GetFileName(path);

            try
            {
                Int32.Parse(path[2].ToString());
            }
            catch
            {
                MessageBox.Show("Cannot read the file date from the file name.", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return new DateTime(1, 1, 1, 0, 0, 0, 0);
            }
            var Year = Int32.Parse(path[6].ToString()) * 10 + Int32.Parse(path[7].ToString()) + 2000;
            var Month = Int32.Parse(path[4].ToString()) * 10 + Int32.Parse(path[5].ToString());
            var Day = Int32.Parse(path[2].ToString()) * 10 + Int32.Parse(path[3].ToString());
            return new DateTime(Year, Month, Day, 0, 0, 0, 0);

        }
        /// <summary> 
        /// Parse the date from the file name.
        /// </summary>
        /// <param name="line">The line to parse.</param>
        internal override string ParseTitle(string line)
        {
            return "";
        }
        #endregion
    }
}
