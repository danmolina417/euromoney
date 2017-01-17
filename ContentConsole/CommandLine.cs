using System;
using System.Collections.Generic;

namespace ContentConsole
{
    public class CommandLine
    {
        public class Codes
        {
            public const string SETUSER = "setuser";
            public const string SHOWUSER = "showuser";
            public const string SETCONTENT = "setcontent";
            public const string SHOWCONTENT = "showcontent";
            public const string SHOWLIST = "showlist";
            public const string ADDWORD = "addword";
            public const string REMOVEWORD = "removeword";
            public const string SETFILTERON = "setfilteron";
            public const string SETFILTEROFF = "setfilteroff";
            public const string HELP = "help";
        }

        public void PrintHelp()
        {
            Console.WriteLine();
            Console.WriteLine("Usage: ");
            Console.WriteLine("Set Content: [{0}]", Codes.SETCONTENT);
            Console.WriteLine("Display Content: [{0}]", Codes.SHOWCONTENT);
            Console.WriteLine("Add Bad Word: [{0}]", Codes.ADDWORD);
            Console.WriteLine("Remove Bad Word: [{0}]", Codes.REMOVEWORD);
            Console.WriteLine("Display Bad Words: [{0}]", Codes.SHOWLIST);
            Console.WriteLine("Display User: [{0}]", Codes.SHOWUSER);
            Console.WriteLine("Set User: [{0}]", Codes.SETUSER);
            Console.WriteLine("Toggle Filter On: [0]", Codes.SETFILTERON);
            Console.WriteLine("Toggle Filter Off: [0]", Codes.SETFILTEROFF);
            Console.WriteLine("Help: [{0}]", Codes.HELP);
            Console.WriteLine();
        }

        public void SetUser(ref Models.User user, int choice)
        {
            if (choice == 1)
            {
                user.FirstName = "-";
                user.LastName = "-";
                user.UserName = "-";
                user.SecType = Models.SecurityType.UnAuthedUser;
            }
            if (choice == 2)
            {
                user.FirstName = "Dan";
                user.LastName = "Molina";
                user.UserName = "dmolina";
                user.SecType = Models.SecurityType.AdminUser;
            }
            if (choice == 3)
            {
                user.FirstName = "Bob";
                user.LastName = "Smith";
                user.UserName = "bsmith";
                user.SecType = Models.SecurityType.Reader; 
            }
            if (choice == 4)
            {
                user.FirstName = "Jane";
                user.LastName = "Doe";
                user.UserName = "jdoe";
                user.SecType = Models.SecurityType.Curator;
            }
        }

        public void DisplayBannedWords(List<string> words)
        {
            Console.WriteLine();
            Console.WriteLine("Banned Words: ");
            foreach(var word in words)
            {
                Console.WriteLine(" - {0}", word);
            }

        }
    }
}