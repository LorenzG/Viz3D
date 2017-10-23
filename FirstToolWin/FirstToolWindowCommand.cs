//------------------------------------------------------------------------------
// <copyright file="FirstToolWindowCommand.cs" company="Company">
//     Copyright (c) Company.  All rights reserved.
// </copyright>
//------------------------------------------------------------------------------

using System;
using System.ComponentModel.Design;
using System.Globalization;
using Microsoft.VisualStudio.Shell;
using Microsoft.VisualStudio.Shell.Interop;

namespace FirstToolWin
{
    /// <summary>
    /// Command handler
    /// </summary>
    internal sealed class FirstToolWindowCommand
    {
        /// <summary>
        /// Command ID.
        /// </summary>
        public const int CommandId = 0x0100;

        /// <summary>
        /// Command menu group (command set GUID).
        /// </summary>
        public static readonly Guid CommandSet = new Guid("1936f159-b2d9-445f-882a-6feded16d923");

        /// <summary>
        /// VS Package that provides this command, not null.
        /// </summary>
        private readonly Package package;

        /// <summary>
        /// Initializes a new instance of the <see cref="FirstToolWindowCommand"/> class.
        /// Adds our command handlers for menu (commands must exist in the command table file)
        /// </summary>
        /// <param name="package">Owner package, not null.</param>
        private FirstToolWindowCommand(Package package)
        {
            if (package == null)
            {
                throw new ArgumentNullException("package");
            }

            this.package = package;

            OleMenuCommandService commandService = this.ServiceProvider.GetService(typeof(IMenuCommandService)) as OleMenuCommandService;
            if (commandService != null)
            {
                var menuCommandID = new CommandID(CommandSet, CommandId);
                var menuItem = new MenuCommand(this.ShowToolWindow, menuCommandID);
                commandService.AddCommand(menuItem);
            }
        }

        /// <summary>
        /// Gets the instance of the command.
        /// </summary>
        public static FirstToolWindowCommand Instance
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the service provider from the owner package.
        /// </summary>
        private IServiceProvider ServiceProvider
        {
            get
            {
                return this.package;
            }
        }

        /// <summary>
        /// Initializes the singleton instance of the command.
        /// </summary>
        /// <param name="package">Owner package, not null.</param>
        public static void Initialize(Package package)
        {
            Instance = new FirstToolWindowCommand(package);
        }

        /// <summary>
        /// Shows the tool window when the menu item is clicked.
        /// </summary>
        /// <param name="sender">The event sender.</param>
        /// <param name="e">The event args.</param>
        private void ShowToolWindow(object sender, EventArgs e)
        {
            for (int i = 0; i < 10; i++)
            {
                /*
                 * https://msdn.microsoft.com/en-us/library/cc512945.aspx
                 * In the MIToolWindowCommand.cs file, find the ShowToolWindos() method.In this method, call the FindToolWindow method and set its create flag to false so that it will iterate through existing tool window instances until an available id is found.
                 * To create a tool window instance, call the FindToolWindow method and set its id to an available value and its create flag to true.
                 * By default, the value of the id parameter of the FindToolWindow method is 0.This makes a single - instance tool window.For more than one instance to be hosted, every instance must have its own unique id.
                 * Call the Show method on the IVsWindowFrame object that is returned by the Frame property of the tool window instance.
                 * By default, the ShowToolWindow method that is created by the tool window item template creates a single - instance tool window.The following example shows how to modify the ShowToolWindow method to create multiple instances.
                 */
                FirstToolWindow window = (FirstToolWindow)this.package.FindToolWindow(typeof(FirstToolWindow), i, false);
                if ((window == null) || (window.Frame == null))
                {
                    // Create the window with the first free ID.
                    FirstToolWindow.id = i;
                    window = (FirstToolWindow)this.package.FindToolWindow(typeof(FirstToolWindow), i, true);

                    if ((null == window) || (null == window.Frame))
                        throw new NotSupportedException("Cannot create tool window");

                    IVsWindowFrame windowFrame = (IVsWindowFrame)window.Frame;
                    Microsoft.VisualStudio.ErrorHandler.ThrowOnFailure(windowFrame.Show());


                    break;
                }
            }

        }
    }
}
