/****************************** Module Header ******************************\
Module Name:  Program.cs
Project:      FileGen
Copyright (c) Stephen Reynolds.

Entry point and other operations. Sole source file.

This source is subject to the MIT License (MIT).
See https://github.com/usedflax/FileGen/blob/master/LICENSE.
All other rights reserved.

THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY OF ANY KIND, 
EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE IMPLIED 
WARRANTIES OF MERCHANTABILITY AND/OR FITNESS FOR A PARTICULAR PURPOSE.
\***************************************************************************/


using System;
using System.IO;

namespace FileGen
{
    class Program
    {
        enum Action { None, Files, Folders, Both };

        /// <summary>
        ///     Entry point.
        ///     Gets input and generates files.
        /// </summary>
        /// <param name="args">
        ///     Must follow format: path -action count.
        ///     Example: filegen "C:\Henta-I mean work stuff\" -fd 420
        ///     See readme.md for additional details
        /// </param>
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
                dir = GetPath();
                action = GetAction();
                count = GetCount();
            }

            // Create files
            switch (action)
            {
                case Action.Files:
                    CreateFiles(dir, count);
                    Console.WriteLine("{0} files created in \"{1}\".",
                        count, dir);
                    break;
                case Action.Folders:
                    CreateFolders(dir, count);
                    Console.WriteLine("{0} folders created in \"{1}\".",
                        count, dir);
                    break;
                case Action.Both:
                    CreateFiles(dir, count);
                    CreateFolders(dir, count);
                    Console.WriteLine("{0} files and folders created in \"{1}\".", 
                        count, dir);
                    break;
                default:
                    return;
            }
        }

        /// <summary>
        ///     Gets arguments and assigns a path, action, and count from them.
        /// </summary>
        /// <param name="args">Arguments listed in command line.</param>
        /// <param name="dir">Where to create files/folders?</param>
        /// <param name="action">Create files, folders, or both?</param>
        /// <param name="count">How many files and folders?</param>
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
        ///     Get path to create files/folders in from input.
        /// </summary>
        /// <returns>DirectoryInfo to create files/folders in.</returns>
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
        ///     Prompt for what the user wants to create.
        ///     Files, folders, or both?
        /// </summary>
        /// <returns>Enum Action with value according to operation</returns>
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
        ///     Get the amount of files/folders to create from input.
        /// </summary>
        /// <returns>Int32 containing the amount of files/folders to create.</returns>
        static int GetCount()
        {
            Console.Write("How many?: ");
            int count = int.Parse(Console.ReadLine());

            return count;
        }

        /// <summary>
        ///     Create files in directory according to amount given.
        /// </summary>
        /// <param name="dir">Directory to create files in.</param>
        /// <param name="count">Amount of files to create.</param>
        static void CreateFiles(DirectoryInfo dir, int count)
        {
            for (int i = 1; i <= count; i++)
            {
                File.Create(dir.ToString() + "\\generated" + i + ".gen");
            }
        }

        /// <summary>
        ///     Create folders in directory according to amount given.
        /// </summary>
        /// <param name="dir">Directory to create folders in.</param>
        /// <param name="count">Amount of folders to create.</param>
        static void CreateFolders(DirectoryInfo dir, int count)
        {
            for (int i = 1; i <= count; i++)
            {
                Directory.CreateDirectory(dir.ToString() + "\\generated" + i);
            }
        }
    }
}
