using System;
using Logic;

namespace console
{
    class Program
    {
        static void Main(string[] args)
        {
            string hostName;
            string userID;
            string password;
            string database;
            if (args.Length < 4)
            {
                Console.WriteLine("Input parameters \"ServerName UserID Password DatabaseName [portNo]\"");
                return;
            }
            hostName = args[0];
            userID = args[1];
            password = args[2];
            database = args[3];
            if (args.Length > 4)
            {
                var port = int.Parse(args[4]);
                Entry.CreateSources(hostName, userID, password, database, port);
            }
            else{
                Entry.CreateSources(hostName, userID, password, database);
            }


        }
    }
}
