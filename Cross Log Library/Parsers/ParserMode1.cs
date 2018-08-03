using Cross_Log_Library.Classes;
using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;

namespace Cross_Log_Library.Parsers
{
    public class ParserMode1 : Parser
    {
        #region Privates
        /// <summary>
        /// Return the log typeof the parsed string.
        /// </summary>
        /// <param name="line">The string to parse.</param>
        /// <returns></returns>
        internal override LogType ParseLogType(string line)
        {
            if (line.Contains("DEBUG"))
            {
                return new LogType("DEBUG");
            }
            else if (line.Contains("INFO"))
            {
                return new LogType("INFO");
            }
            else if (line.Contains("ERROR"))
            {
                return new LogType("ERROR");
            }
            else
            {
                return new LogType("DEBUG_VERBOSE");
            }
        }
        /// <summary>
        /// Return the title of parsed strings.
        /// </summary>
        /// <param name="line">The string to parse.</param>
        /// <returns></returns>
        internal override string ParseTitle(string line)
        {
            var lineCheck = line.Clone();
            line = line.Replace("\t", "£");
            var lineCheck2 = line.Clone();
            var tabPosition = line.IndexOf("£");
            line = line.Remove(0, tabPosition + 1);
            var lineCheck3 = line.Clone();

            tabPosition = line.IndexOf("£");
            line = line.Remove(0, tabPosition + 1);
            var lineCheck4 = line.Clone();

            if (line.IndexOf(" ") > 0)
            {
                var spacePosition = line.IndexOf(" ");
                line = line.Remove(spacePosition);
            }

            return line;
        }
        /// <summary>
        /// Return the description of parsed strings.
        /// </summary>
        /// <param name="line">The string to parse.</param>
        /// <returns></returns>
        internal override string ParseDescription(string line)
        {
            line = line.Replace("\t", "£");
            var tabPosition = line.IndexOf("£");
            line = line.Remove(0, tabPosition + 1);

            tabPosition = line.IndexOf("£");
            line = line.Remove(0, tabPosition + 1);

            var spacePosition = line.IndexOf(" ");
            if (spacePosition < 0)
            {
                return "";
            }
            line = line.Remove(0, spacePosition);

            var tmp = 0;
            foreach (char value in line)
            {
                if (value == ' ')
                {
                    tmp++;
                }
                else
                {
                    break;
                }
            }
            line = line.Remove(0, tmp);

            return line;

        }

        #endregion
    }
}
