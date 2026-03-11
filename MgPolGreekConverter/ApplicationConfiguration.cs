using System;
using System.Collections.Generic;
using System.Text;

namespace MgPolGreekConverter
{
    internal static partial class ApplicationConfiguration
    {
        public static void Initialize()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.SetHighDpiMode(HighDpiMode.SystemAware);
        }
    }
}