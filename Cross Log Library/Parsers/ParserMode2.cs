using Cross_Log_Library.Classes;
using System;
using System.Collections.Generic;

namespace Cross_Log_Library.Parsers
{
    public class ParserMode2 : Parser
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

            return line;

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
    }
}
