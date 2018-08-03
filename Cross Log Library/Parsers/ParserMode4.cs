using Cross_Log_Library.Classes;
using System;
using System.IO;
using System.Windows.Forms;

namespace Cross_Log_Library.Parsers
{
    public class ParserMode4 : Parser
    {
        #region Privates
        /// <summary>
        /// Return the date of the parsed string.
        /// </summary>
        /// <param name="line">The string to parse.</param>
        /// <returns></returns>
        public override DateLog ParseDateLog(string line)
        {
            var Hour = Int32.Parse(line[0].ToString()) * 10 + Int32.Parse(line[1].ToString());
            var Minute = Int32.Parse(line[3].ToString()) * 10 + Int32.Parse(line[4].ToString());
            var Second = Int32.Parse(line[6].ToString()) * 10 + Int32.Parse(line[7].ToString());
            DateLog tmp = new DateLog(new DateTime(1, 1, 1, Hour, Minute, Second, 0), 0);
            return tmp;
        }
        /// <summary>
        /// Return the description of parsed strings.
        /// </summary>
        /// <param name="line">The string to parse.</param>
        /// <returns></returns>
        internal override string ParseDescription(string line)
        {
            return line.Remove(0, 9);
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
        /// Return the name of the software.
        /// </summary>
        /// <param name="line">The string to parse.</param>
        /// <returns></returns>
        internal override string ParseSoftwareName(string line, string path)
        {
            return "DJ Pro";
        }
        /// <summary>
        /// Return the function call of parsed strings.
        /// </summary>
        /// <param name="line">The string to parse.</param>
        /// <returns></returns>
        internal override string ParseTitle(string line)
        {
            return "";
        }
        #endregion
        /// <summary> 
        /// Parse the date from the file name.
        /// </summary>
        /// <param name="path">The file path.</param>
        public override DateTime GetDateFromFileName(string path)
        {
            return new DateTime(1, 1, 1, 0, 0, 0, 0);

        }
    }
}
