namespace dispose_pattern
{
    class Program
    {
        static void Main(string[] args)
        {
            using (BaseClass baseClass = new BaseClass())
            {
                CreateFile createFile = new CreateFile();

                createFile.Exec();
            }
        }
    }
}