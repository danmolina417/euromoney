using System;
using System.Collections.Generic;
using ContentConsole.Managers;
using ContentConsole.Models;

namespace ContentConsole
{
    //public static class Program
    public class Program
    {
        //public static void Main(string[] args)
        static void Main(string[] args)
        {
            CommandLine cmdLine = new CommandLine();
            ContentManager cm = new ContentManager();
            RoleManager rm = new RoleManager();

            string cmd;
            string content = "The weather in Manchester in winter is bad. It rains all the time - it must be horrible for people visiting.";
            List<string> bannedWords = new List<string>() { "swine", "bad", "nasty", "horrible" };
            User user = new User("-", "-", "-");
            var userType = rm.GetUserRole(user);
            
            Console.WriteLine("Content Console Test for Euromoney");
            Console.WriteLine();

            Console.WriteLine("This console allows you to do the following:");
            Console.WriteLine();
            Console.WriteLine("  - Display Content");
            Console.WriteLine("  - Change Content");
            Console.WriteLine("  - Change User (to test different roles)");
            Console.WriteLine("  - Add to Banned Words List");
            Console.WriteLine("  - Toggle the FilterView flag on/off");
            Console.WriteLine();
            Console.WriteLine("type [{0}] for a comprehensive list of commands.", CommandLine.Codes.HELP);
            Console.WriteLine();

            while (true)
            {
                Console.Write("Enter a command or q to quit (type help for list of commands):  ");
                cmd = Console.ReadLine().ToLower();
                if (cmd == "q" || cmd == "quit" || cmd == "exit")
                {
                    break;
                }
                else
                {
                    try
                    {
                        switch (cmd)
                        {
                            case CommandLine.Codes.SETCONTENT:
                                Console.WriteLine();
                                Console.WriteLine("Please type in new content: ");
                                content = Console.ReadLine();
                                break;
                            case CommandLine.Codes.SHOWCONTENT:
                                Console.WriteLine();
                                userType = rm.GetUserRole(user);
                                Console.WriteLine("Scanned the text:");
                                Console.WriteLine(cm.GetProcessedContent(content, bannedWords, userType));
                                if (userType == SecurityType.AdminUser || userType == SecurityType.Curator)
                                {
                                    Console.WriteLine("FilterViewOn flag: {0}", cm.GetFilterViewOn());
                                }
                                Console.WriteLine("Total number of negative words: {0}", cm.GetWordCount(content, bannedWords));
                                break;
                            case CommandLine.Codes.SHOWLIST:
                                Console.WriteLine();
                                cmdLine.DisplayBannedWords(bannedWords);
                                break;
                            case CommandLine.Codes.ADDWORD:
                                Console.WriteLine();
                                Console.Write("Please type new word to add: ");
                                var badword = Console.ReadLine();
                                userType = rm.GetUserRole(user);
                                if (badword != "")
                                {
                                    if (userType != SecurityType.AdminUser)
                                    {
                                        Console.WriteLine("Only admins can add bad words to the banned word list");
                                        break;
                                    }
                                    else
                                    {
                                        cm.AddWordToList(ref bannedWords, badword, userType);
                                    }
                                }
                                else
                                {
                                    Console.WriteLine("You cannot add an empty word");
                                    break;
                                }
                                Console.WriteLine("Banned Words: ");
                                cmdLine.DisplayBannedWords(bannedWords);
                                break;
                            case CommandLine.Codes.REMOVEWORD:
                                Console.WriteLine();
                                Console.WriteLine("TODO: Remove Bad word not yet implemented");
                                break;
                            case CommandLine.Codes.SETUSER:
                                Console.WriteLine("Select One of the following users: ");
                                Console.WriteLine("1. Unknown - UnAuthed User");
                                Console.WriteLine("2. Dan Molina - Admin");
                                Console.WriteLine("3. Bob Smith - Reader");
                                Console.WriteLine("4. Jane Doe - Curator");

                                var choice = Console.ReadLine();

                                int j;
                                if (Int32.TryParse(choice, out j))
                                {
                                    cmdLine.SetUser(ref user, j);
                                }

                                Console.Write("New User: {0} {1} - {2} ({3})", 
                                    user.FirstName, 
                                    user.LastName,
                                    user.UserName, 
                                    user.SecType);
                                break;
                            case CommandLine.Codes.SHOWUSER:
                                Console.WriteLine();
                                Console.Write("Current User: {0} {1} - {2} ({3})",
                                    user.FirstName,
                                    user.LastName,
                                    user.UserName,
                                    user.SecType);
                                break;
                            case "setfilteron":
                                userType = rm.GetUserRole(user);
                                if (userType != SecurityType.AdminUser
                                    && userType != SecurityType.Curator)
                                {
                                    Console.WriteLine("Only admins and curators can set the filter flag on/off");
                                    break;
                                }
                                Console.WriteLine();
                                userType = rm.GetUserRole(user);
                                cm.SetFilter(true, userType);
                                Console.WriteLine("FilterOnOff: {0}", cm.GetFilterViewOn());
                                Console.WriteLine();
                                break;
                            case "setfilteroff":
                                userType = rm.GetUserRole(user);
                                if (userType != SecurityType.AdminUser
                                    && userType != SecurityType.Curator)
                                {
                                    Console.WriteLine("Only admins and curators can set the filter flag on/off");
                                    break;
                                }
                                Console.WriteLine();
                                userType = rm.GetUserRole(user);
                                cm.SetFilter(false, userType);
                                Console.WriteLine("FilterOnOff: {0}", cm.GetFilterViewOn());
                                Console.WriteLine();
                                break;
                            case "help":
                                cmdLine.PrintHelp();
                                break;
                            default:
                                Console.WriteLine("Invalid command name or parameters");
                                break;
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.Out.Write(false);
                        string exmsg = ex.ToString();
                    }

                    Console.WriteLine("");
                }

            }
                Console.WriteLine();
                Console.WriteLine();
        }// end of while 
    }
}

