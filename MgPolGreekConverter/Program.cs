namespace MgPolGreekConverter
{
    internal static class Program
    {
        public static List<string> ConvFiles { get; private set; } = new List<string>();
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            try
            {
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Application.SetHighDpiMode(HighDpiMode.SystemAware);

                OpenFileDialog dialog = new OpenFileDialog();
                dialog.Filter = "Word documents (*.docx)|*.docx";
                dialog.Multiselect = true;

                if (dialog.ShowDialog() != DialogResult.OK)
                    return;

                if (dialog.FileNames.Length > 0)
                    ConvFiles.AddRange(dialog.FileNames);
                else
                    ConvFiles.Add(dialog.FileName);

                // To customize application configuration such as set high DPI settings or default font,
                // see https://aka.ms/applicationconfiguration.
                //ApplicationConfiguration.Initialize();
                Application.Run(new frmProgress());
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}{Environment.NewLine}Trace:{Environment.NewLine}{ex.StackTrace}");
            }
        }
    }
}