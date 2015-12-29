/****************************** Module Header ******************************\
Module Name:  MainWindow.xaml.cs
Project:      FileGen
Copyright (c) Stephen Reynolds.

Interaction logic for MainWindow.xaml

This source is subject to the MIT License (MIT).
See https://github.com/usedflax/FileGen/blob/master/LICENSE.
All other rights reserved.

THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY OF ANY KIND, 
EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE IMPLIED 
WARRANTIES OF MERCHANTABILITY AND/OR FITNESS FOR A PARTICULAR PURPOSE.
\***************************************************************************/

using System;
using System.Diagnostics;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using WinForms = System.Windows.Forms;

namespace FileGenGUI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        /// <summary>
        ///     Create window.
        /// </summary>
        public MainWindow()
        {
            InitializeComponent();
        }

        /// <summary>
        ///     Open a dialog box to allow the user to select a directory, 
        ///     and put its path in TxtDirectory.
        /// </summary>
        private void BtnBrowse_Click(object sender, RoutedEventArgs e)
        {
            var dialog = new WinForms.FolderBrowserDialog();
            var result = dialog.ShowDialog();

            if (result == WinForms.DialogResult.OK)
            {
                // Show the path in text box.
                TxtDirectory.Text = dialog.SelectedPath;
            }
        }

        /// <summary>
        ///     Run FileGen.exe with input as arguments.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnRun_Click(object sender, RoutedEventArgs e)
        {
            DirectoryInfo dir = new DirectoryInfo(TxtDirectory.Text);
            string action = string.Empty;
            int count = Convert.ToInt32(NumCount.Value);

            // Get action from checkboxes
            if (IsChecked(ChkFiles))
            {
                if (IsChecked(ChkFolders))
                {
                    action = "-fd";
                }
                else
                {
                    action = "-f";
                }
            }
            else if (IsChecked(ChkFolders))
            {
                action = "-d";
            }
            else
            {
                MessageBox.Show("Neither files nor folders are checked!", 
                    "Invalid Input", MessageBoxButton.OK, MessageBoxImage.Error);
            }

            // Create files via console application
            string args = string.Format("{0} {1} {2}", dir, action, count);
            ProcessStartInfo start = new ProcessStartInfo();
            start.Arguments = args;
            start.FileName = Path.Combine(Environment.CurrentDirectory, "FileGen.exe");
            start.WindowStyle = ProcessWindowStyle.Hidden;
            start.CreateNoWindow = true;
            using (Process proc = Process.Start(start))
            {
                proc.WaitForExit();
            }

            // Report finished
            string message = string.Format("{0} files and folders created in \"{1}\".",
                        count, dir);
            MessageBox.Show(message, "Files/Folders Created", 
                MessageBoxButton.OK, MessageBoxImage.Information);
        }

        /// <summary>
        ///     Clears all input.
        /// </summary>
        private void BtnClear_Click(object sender, RoutedEventArgs e)
        {
            TxtDirectory.Clear();
            ChkFiles.IsChecked = false;
            ChkFolders.IsChecked = false;
            NumCount.Value = 0;
        }

        /// <summary>
        ///     Close the application.
        /// </summary>
        private void BtnExit_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        /// <summary>
        ///     Returns true if checkbox is checked.
        /// </summary>
        private static bool IsChecked(CheckBox check)
        {
            return (check.IsChecked.HasValue && check.IsChecked.Value);
        }
    }
}
