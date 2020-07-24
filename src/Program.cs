namespace dispose_pattern
{
    class Program
    {
        static void Main(string[] args)
        {
            using (DisposePattern pattern = new DisposePattern())
            {
                CreateFile createFile = new CreateFile();

                createFile.Exec();
            }
        }
    }
}