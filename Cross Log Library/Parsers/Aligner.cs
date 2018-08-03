using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using Cross_Log_Library.Classes;
using Cross_Log_Library.Interfaces;

namespace Cross_Log_Library.Parsers
{
    public class Aligner : IAligner
    {

        #region Constructor
        public Aligner()
        {
        }
        #endregion

        #region Publics
        /// <summary> 
        /// Allign all the parsed lines.
        /// </summary>
        /// <param name="liste">The list containing all the log lists.</param>
        public List<LogList> AllignParsed(List<LogList> liste)
        {

            //var tmp = liste.ToArray();


            DateTime lastLog = new DateTime(1, 1, 1, 0, 0, 0, 0);
            DateTime firstLog = new DateTime(1, 1, 1, 0, 0, 0, 0);

            for (int n = 0; n < liste.Count; n++)
            {
                if (liste[n].ListLog.Count == 0)
                {
                    break;
                }
                if (n == 0)
                {
                    firstLog = liste[n].ListLog[0].Date.Date;
                }
                if (liste[n].ListLog[liste[n].ListLog.Count - 1].Date.Date > lastLog)
                {
                    lastLog = liste[n].ListLog[liste[n].ListLog.Count - 1].Date.Date;
                }
                if (liste[n].ListLog[0].Date.Date < firstLog)
                {
                    firstLog = liste[n].ListLog[0].Date.Date;
                }
            }

            for (int i = 0; true; i++)
            {
                var ToInsert = false;
                foreach (LogList lista in liste)
                {
                    if (lista.ListLog.Count > i)
                    {
                        if (lista.ListLog[i].Date.Date.Equals(firstLog))
                        {
                            ToInsert = true;
                        }
                    }
                    else
                    {
                        lista.ListLog.Insert(i, new Log("", "", "", new DateLog(firstLog, 0), new LogType("")));
                    }
                }
                if (!ToInsert)
                {
                    firstLog = firstLog.AddMilliseconds(10);
                    i--;
                }
                foreach (LogList lista in liste)
                {
                    if ((lista.ListLog[i].Date.Date.Equals(firstLog) != true) && ToInsert)
                    {
                        lista.ListLog.Insert(i, new Log("", "", "", new DateLog(firstLog, 0), new LogType("")));

                    }
                }
                if (firstLog >= lastLog)
                {
                    return liste;
                }
            }

        }
        /// <summary> 
        /// Allign all the parsed lines.
        /// </summary>
        /// <param name="liste">The list containing all the log lists.</param>
        public List<LogList> AllignParsedWithFilters(List<LogList> liste, int Hour, int Minute, int Duration)
        {
            DateTime firstLog = new DateTime(1, 1, 1, Hour, Minute, 0, 0);
            DateTime lastLog = firstLog.AddMinutes(Duration);
            int[] firstPos = new int[liste.Count];
            int[] lastPos = new int[liste.Count];
            for (int i = 0; i < liste.Count; i++)
            {
                if (liste[i].ListLog.Count == 0)
                {
                    break;
                }
                for (int x = 0; x < liste[i].ListLog.Count - 1; x++)
                {
                    if (liste[i].ListLog[x].Date.Date < firstLog && firstLog <= liste[i].ListLog[x + 1].Date.Date)
                    {
                        firstLog = liste[i].ListLog[x + 1].Date.Date;
                        firstPos[i] = x + 1;
                    }
                    if (liste[i].ListLog[x].Date.Date <= lastLog && lastLog < liste[i].ListLog[x + 1].Date.Date)
                    {
                        lastLog = liste[i].ListLog[x + 1].Date.Date;
                        lastPos[i] = x + 1;
                    }
                }
                
            }
            for (int i=1; true; i++)
            {
                var ToInsert = false;
                foreach (LogList lista in liste)
                {
                    if (lista.ListLog.Count > i)
                    {
                        if (lista.ListLog[i].Date.Date.Equals(firstLog))
                        {
                            ToInsert = true;
                        }
                    }
                    else
                    {
                        lista.ListLog.Insert(i, new Log("", "", "", new DateLog(firstLog, 0), new LogType(""), Color.White, Color.DarkSlateGray));
                    }
                }
                if (!ToInsert)
                {
                    firstLog = firstLog.AddMilliseconds(10);
                    i--;
                }
                foreach (LogList lista in liste)
                {
                    if ((lista.ListLog[i].Date.Date.Equals(firstLog) != true) && ToInsert)
                    {
                        lista.ListLog.Insert(i, new Log("", "", "", new DateLog(firstLog, 0), new LogType(""), Color.White, Color.DarkSlateGray));

                    }
                }
                if (firstLog >= lastLog)
                {
                    break;
                }
            }
            for (int i = 0; i < liste.Count; i++)
            {
                liste[i].ListLog = liste[i].ListLog.GetRange(firstPos[i], lastPos[i] - firstPos[i]);
            }
            return liste;
        }
        #endregion
        
    }
}
