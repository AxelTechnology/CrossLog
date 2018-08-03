using System;
using System.Collections.Generic;
using Cross_Log_Library.Classes;

namespace Cross_Log_Library.Interfaces
{
    public interface IAligner
    {
        /// <summary> 
        /// Allign all the parsed lines.
        /// </summary>
        /// <param name="liste">The list containing all the log lists.</param>
        List<LogList> AllignParsed(List<LogList> liste);
    }
}
