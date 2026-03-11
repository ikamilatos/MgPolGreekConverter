using System;
using System.Collections.Generic;
using System.IO.Compression;
using System.Text;
using System.Xml;
using System.Xml.Linq;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Window;

namespace MgPolGreekConverter
{
    public sealed class MgConverter
    {
        private static MgConverter _instance;
        private List<string> _srcFiles;
        private List<string> _destFiles;
        private Dictionary<string, string> _errsFiles;
        private int _filesTotal;
        private int _filesProcessed;
        private int _fileProgress;
        private int _fileTotal;

        public static MgConverter Instance { 
            get {
                if(_instance==null)
                    _instance=new MgConverter(); 
                return _instance; 
            } 
        }

        public int FilesTotal { get { return _filesTotal; } }
        public int FilesProcessed {  get { return _filesProcessed; } }
        public int FileProgress {  get { return _fileProgress; } }
        public int FileTotal {  get { return _fileTotal; } }

        public List<string> ConvertedFiles { get { return _destFiles; } }

        public Dictionary<string, string> Errors { get { return _errsFiles; } }

        public void SetSourceFiles(List<string> files)
        {
            _srcFiles = files;
        }


        public void ProcessDocs()
        {
            if (_srcFiles == null || _srcFiles.Count == 0)
            {
                MessageBox.Show("No files to convert.");
                return;
            }

            _filesTotal = _srcFiles.Count;
            _filesProcessed = 0;
            _destFiles = new List<string>();
            foreach (string file in _srcFiles)
            {
                try
                {
                    string mOutput = Path.ChangeExtension(file, "_fixed.docx");
                    ProcessXDoc(file, mOutput);
                    _destFiles.Add(mOutput);
                    _filesProcessed++;
                }
                catch (Exception ex)
                {
                    _errsFiles ??= new Dictionary<string, string>();
                    _errsFiles[file] = ex.Message;
                    _filesProcessed++;
                }
            }

            if (_errsFiles != null)
            {
                StringBuilder sb = new StringBuilder();
                sb.AppendLine("Errors occurred during conversion:");
                foreach (var kvp in _errsFiles)
                {
                    sb.AppendLine($"{kvp.Key}: {kvp.Value}");
                }
                MessageBox.Show(sb.ToString(), "Conversion Errors", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                MessageBox.Show("All files converted successfully.");
            }
        }

        private void ProcessXDoc(string inputPath, string outputPath)
        {
            // copy original → new file
            File.Copy(inputPath, outputPath, true);

            using var archive = ZipFile.Open(outputPath, ZipArchiveMode.Update);

            // process all word xml parts that may contain text
            string[] xmlParts =
            {
        "word/document.xml",
        "word/footnotes.xml",
        "word/endnotes.xml"
    };

            List<ZipArchiveEntry> entryNames = archive.Entries
                     .Where(e => e.FullName.StartsWith("word/header") ||
                                 e.FullName.StartsWith("word/footer")).ToList();

            _fileTotal = xmlParts.Length + entryNames.Count;
            _fileProgress = 0;

            foreach (var entry in entryNames)
            {
                _fileProgress++;
                FixXmlEntry(entry);
            }

            foreach (string part in xmlParts)
            {
                _fileProgress++;
                var entry = archive.GetEntry(part);
                if (entry != null)
                    FixXmlEntry(entry);
            }
        }

        private void FixXmlEntry(ZipArchiveEntry entry)
        {
            XDocument xml;

            // READ XML
            using (var stream = entry.Open())
            {
                xml = XDocument.Load(stream);
            }

            XNamespace w = "http://schemas.openxmlformats.org/wordprocessingml/2006/main";

            // find ALL text nodes
            foreach (var t in xml.Descendants(w + "t"))
            {
                if (string.IsNullOrEmpty(t.Value))
                    continue;

                string fixedText = FixGreek(t.Value);

                if (fixedText != t.Value)
                    t.Value = fixedText;
            }

            // OPTIONAL: change font MgPolNewTimes → Arial
            foreach (var rFonts in xml.Descendants(w + "rFonts"))
            {
                var attr = rFonts.Attribute(w + "ascii");
                if (attr != null && attr.Value == "MgPolNewTimes")
                {
                    attr.Value = "Times New Roman";
                    rFonts.SetAttributeValue(w + "hAnsi", "Times New Roman");
                }
            }

            // WRITE BACK XML
            using var writeStream = entry.Open();
            writeStream.SetLength(0);

            using var writer = XmlWriter.Create(writeStream,
                new XmlWriterSettings { Encoding = Encoding.UTF8 });

            xml.Save(writer);
        }

        private string FixGreek(string s)
        {
            byte[] chars = Encoding.UTF8.GetBytes(s);

            for (int i = 0; i < chars.Length - 1; i++)
            {
                if (chars[i] == 0xC3)
                {
                    if (chars[i + 1] >= 0xB0 && chars[i + 1] <= 0xBF)
                    {
                        chars[i] = 0xCF;
                        chars[i + 1] = (byte)((chars[i + 1] - 0x30) & 0xFF);
                    }
                    else
                    {
                        chars[i] = 0xCE;
                        chars[i + 1] = (byte)((chars[i + 1] + 0x10) & 0xFF);
                    }
                    i += 1;
                }
            }

            return Encoding.UTF8.GetString(chars);
        }
    }
}