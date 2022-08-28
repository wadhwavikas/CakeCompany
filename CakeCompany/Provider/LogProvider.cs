using System;
namespace CakeCompany.Provider
{
    public class LogProvider : ILogProvider
    {
        public string filePath = @"C:\CakeCompanyLog.txt";

        public void Log(string message)
        {
            using (StreamWriter streamWriter = new StreamWriter(filePath))
            {
                streamWriter.WriteLine(message);
                streamWriter.Close();
            }

        }



    }
}

