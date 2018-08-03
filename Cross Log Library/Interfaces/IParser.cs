using Cross_Log_Library.Classes;
using System;
using System.Collections.Generic;

namespace Cross_Log_Library.Interfaces
{
    public interface IParser
    {
        /// <summary>
        /// Return the list of parsed strings.
        /// </summary>
        /// <param name="path">The path of the file to parse.</param>
        List<Log> Parse(string path);
    }
}
