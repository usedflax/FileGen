using System;
using System.IO;

namespace FileGen
{
    class Program
    {
        enum Action { None, Files, Folders, Both };

        /// <summary>
        /// 
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            DirectoryInfo dir = null;
            Action action = Action.None;
            int count = 0;

            // Get inputs via arguments if present
            if (args.Length > 0)
            {
                GetArguments(ref args, ref dir, ref action, ref count);
            }
            else // Get inputs via prompts
            {
                PromptInputs(ref dir, ref action, ref count);
            }

            // Create files
            switch (action)
            {
                case Action.Files:
                    CreateFiles(dir, count);
                    Console.WriteLine("{0} files created in \"{1}\".",
                        count, dir.ToString());
                    break;
                case Action.Folders:
                    CreateFolders(dir, count);
                    Console.WriteLine("{0} folders created in \"{1}\".",
                        count, dir.ToString());
                    break;
                case Action.Both:
                    CreateFiles(dir, count);
                    CreateFolders(dir, count);
                    Console.WriteLine("{0} files and folders created in \"{1}\".", 
                        count, dir.ToString());
                    break;
                default:
                    return;
            }

            Console.ReadLine();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="args"></param>
        /// <param name="dir"></param>
        /// <param name="action"></param>
        /// <param name="count"></param>
        static void GetArguments(ref string[] args, ref DirectoryInfo dir,
            ref Action action, ref int count)
        {
            const int argCount = 3;

            // Check for right amount of arguments
            if (args.Length < argCount)
            {
                Console.WriteLine("Not enough arguments!");
                return;
            }
            else if (args.Length > argCount)
            {
                Console.WriteLine("Too many arguments!");
                return;
            }

            // Get directory
            dir = new DirectoryInfo(args[0]);
            if (!dir.Exists)
            {
                Console.WriteLine("Directory does not exist!");
                return;
            }

            // Get action
            switch (args[1])
            {
                case "-f":
                    action = Action.Files;
                    break;
                case "-d":
                    action = Action.Folders;
                    break;
                case "-fd":
                    action = Action.Both;
                    break;
                case "-df":
                    action = Action.Both;
                    break;
                default:
                    break;
            }

            // Get count
            count = int.Parse(args[2]);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dir"></param>
        /// <param name="action"></param>
        /// <param name="count"></param>
        static void PromptInputs(ref DirectoryInfo dir, 
            ref Action action, ref int count)
        {
            dir = GetPath();
            action = GetAction();
            count = GetCount();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        static DirectoryInfo GetPath()
        {
            DirectoryInfo dir;

            do
            {
                Console.Write("Enter folder path: ");
                dir = new DirectoryInfo(Console.ReadLine());
            } while (!dir.Exists);

            return dir;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        static Action GetAction()
        {
            Action action = Action.None;

            bool? validSelection = null;
            while (validSelection == false || validSelection == null)
            {
                if (validSelection == false)
                {
                    Console.WriteLine("Invalid input!");
                }
                Console.WriteLine("[1] Files only");
                Console.WriteLine("[2] Folders only");
                Console.WriteLine("[3] Files and folders");
                Console.Write("Select an action (1-3): ");
                int selection = Convert.ToInt32(Console.ReadLine());

                switch (selection)
                {
                    case 1:
                        action = Action.Files;
                        validSelection = true;
                        break;
                    case 2:
                        action = Action.Folders;
                        validSelection = true;
                        break;
                    case 3:
                        action = Action.Both;
                        validSelection = true;
                        break;
                    default:
                        validSelection = false;
                        break;
                }
            }

            return action;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        static int GetCount()
        {
            Console.Write("How many?: ");
            int count = Convert.ToInt32(Console.ReadLine());

            return count;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dir"></param>
        /// <param name="count"></param>
        static void CreateFiles(DirectoryInfo dir, int count)
        {
            for (int i = 1; i <= count; i++)
            {
                File.Create(dir.ToString() + "\\generated" + i + ".gen");
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dir"></param>
        /// <param name="count"></param>
        static void CreateFolders(DirectoryInfo dir, int count)
        {
            for (int i = 1; i <= count; i++)
            {
                Directory.CreateDirectory(dir.ToString() + "\\generated" + i);
            }
        }
    }
}
