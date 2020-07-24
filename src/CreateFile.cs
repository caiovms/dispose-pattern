using System;
using System.IO;
using System.Text;

namespace dispose_pattern
{
    public class CreateFile
    {
        /// <summary>
        /// Create a file in Desktop folder if not exists
        /// </summary>
        public void Exec()
        {
            FileStream fs = null;
            string path = @$"{Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory)}\ExampleDispose.txt";

            try
            {
                fs = new FileStream(path, FileMode.CreateNew, FileAccess.ReadWrite);
                byte[] info = new UTF8Encoding(true).GetBytes("This is some text in the file.\n");
                fs.Write(info, 0, info.Length);
                fs.Close();
            }
            catch (IOException)
            {
                if (File.Exists(path))
                {
                    using (StreamWriter file = new StreamWriter(path, true))
                    {
                        file.WriteLine("File Exists!\n");
                    }
                }
            }
            finally
            {
                if (fs != null)
                {
                    fs.Dispose();
                }
            }
        }
    }
}