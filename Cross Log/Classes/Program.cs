using System;
using System.IO;
using System.Windows.Forms;
using System.Xml.Linq;

namespace Cross_Log
{
    static class Program
    {
        /// <summary>
        /// Punto di ingresso principale dell'applicazione.
        /// </summary>
        [STAThread]
        static void Main()
        {
            CreateSettingsXML();
            
            NewMethod();

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new FrmMain());
        }

        private static void NewMethod()
        {
            try
            {
                var AppDataPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
                // Check if all required files were created
                if (!File.Exists(AppDataPath + "\\Axel Technology\\Cross Log\\preferences.xml"))
                {
                    // Create the folder and the XML settings file
                    Directory.CreateDirectory(AppDataPath + "\\Axel Technology\\Cross Log");

                    new XDocument(
                        new XElement("preferences",
                            new XElement("font",
                                new XElement("name", "Microsoft Sans Serif"),
                                new XElement("size", "8")
                            )
                        )
                    )
                    .Save(AppDataPath + "\\Axel Technology\\Cross Log\\preferences.xml");

                }
            }
            catch (Exception e)
            {
                Console.WriteLine("The process failed: {0}", e.ToString());
            }
        }

        private static void CreateSettingsXML()
        {
            try
            {
                var AppDataPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
                // Check if all required files were created
                if (!File.Exists(AppDataPath + "\\Axel Technology\\Cross Log\\settings.xml"))
                {
                    // Create the folder and the XML settings file
                    Directory.CreateDirectory(AppDataPath + "\\Axel Technology\\Cross Log");

                    new XDocument(
                        new XElement("settings",
                            new XElement("software",
                                new XElement("name", "([12]\\d{3}(0[1-9]|1[0-2])(0[1-9]|[12]\\d|3[01]))_MMEngineUI(\\d).txt"),
                                new XElement("value", 1)
                            ),
                            new XElement("software",
                                new XElement("name", "([12]\\d{3}(0[1-9]|1[0-2])(0[1-9]|[12]\\d|3[01]))_VJPro Server.txt"),
                                new XElement("value", 2)
                            ),
                            new XElement("software",
                                new XElement("name", "RE((0[1-9]|[12]\\d|3[01])(0[1-9]|1[0-2])(0[1-9]|[0-9]\\d)).txt"),
                                new XElement("value", 3)
                            ),
                            new XElement("software",
                                new XElement("name", "((0[1-9]|[12]\\d|3[01])-(0[1-9]|1[0-2])-(0[1-9]|[0-9]\\d)).txt"),
                                new XElement("value", 4)
                            ),
                            new XElement("software",
                                new XElement("name", "((0[1-9]|[12]\\d|3[01])-(0[1-9]|1[0-2])-(0[1-9]|[0-9]\\d)).djl"),
                                new XElement("value", 4)
                            ),
                            new XElement("software",
                                new XElement("name", "([12]\\d{3}(0[1-9]|1[0-2])(0[1-9]|[12]\\d|3[01])).txt"),
                                new XElement("value", 4)
                            ),
                            new XElement("software",
                                new XElement("name", "((0[1-9]|[12]\\d|3[01])-(0[1-9]|1[0-2])-(0[1-9]|[0-9]\\d)).reg"),
                                new XElement("value", 4)
                            ),
                            new XElement("software",
                                new XElement("name", "((0[1-9]|[12]\\d|3[01])-(0[1-9]|1[0-2])-(0[1-9]|[0-9]\\d)).iol"),
                                new XElement("value", 4)
                            )
                        )
                    )
                    .Save(AppDataPath + "\\Axel Technology\\Cross Log\\settings.xml");

                }
            }
            catch (Exception e)
            {
                Console.WriteLine("The process failed: {0}", e.ToString());
            }
        }
    }
}
