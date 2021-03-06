﻿using System;
using System.Windows.Forms;
using Microsoft.Office.Interop.PowerPoint;
using Microsoft.Office.Tools;
using PowerPointLabs.Models;

namespace PowerPointLabs.ActionFramework.Common.Extension
{
    /// <summary>
    /// Provide powerpoint context for Action Framework
    /// </summary>
    [Obsolete("DO NOT use this class in your feature! Used only by Action Framework.")]
    class ActionFrameworkExtensions
    {
        public static DocumentWindow GetCurrentWindow()
        {
            return Globals.ThisAddIn.Application.ActiveWindow;
        }

        public static Selection GetCurrentSelection()
        {
            return PowerPointCurrentPresentationInfo.CurrentSelection;
        }

        public static PowerPointSlide GetCurrentSlide()
        {
            return PowerPointCurrentPresentationInfo.CurrentSlide;
        }

        public static PowerPointPresentation GetCurrentPresentation()
        {
            return PowerPointPresentation.Current;
        }

        public static Ribbon1 GetRibbonUi()
        {
            return Globals.ThisAddIn.Ribbon;
        }

        /// <summary>
        /// Go to a slide
        /// </summary>
        /// <param name="slideIndex">1-based</param>
        public static void GotoSlide(int slideIndex)
        {
            Globals.ThisAddIn.Application.ActiveWindow.View.GotoSlide(slideIndex);
        }

        public static void ExecuteOfficeCommand(string commandMso)
        {
            var commandBars = Globals.ThisAddIn.Application.CommandBars;
            commandBars.ExecuteMso(commandMso);
        }

        public static void StartNewUndoEntry()
        {
            Globals.ThisAddIn.Application.StartNewUndoEntry();
        }

        public static CustomTaskPane GetTaskPane(Type taskPaneType)
        {
            return Globals.ThisAddIn.GetActivePane(taskPaneType);
        }

        public static CustomTaskPane RegisterTaskPane(Type taskPaneType, string taskPaneTitle, 
            EventHandler visibleChangeEventHandler = null, EventHandler dockPositionChangeEventHandler = null)
        {
            try
            {
                var taskPane = Globals.ThisAddIn.GetActivePane(taskPaneType);
                if (taskPane != null)
                {
                    return taskPane;
                }

                var taskPaneControl = (UserControl) Activator.CreateInstance(taskPaneType);
                if (taskPaneControl == null)
                {
                    throw new InvalidCastException("Failed to convert " + taskPaneType + " to UserControl.");
                }

                var activeWindow = Globals.ThisAddIn.Application.ActiveWindow;

                return Globals.ThisAddIn.RegisterTaskPane(taskPaneControl, taskPaneTitle, activeWindow,
                    visibleChangeEventHandler, dockPositionChangeEventHandler);
            }
            catch (Exception e)
            {
                PowerPointLabsGlobals.LogException(e, "RegisterTaskPane_Extension");
                Views.ErrorDialogWrapper.ShowDialog("PowerPointLabs", e.Message, e);
                return null;
            }
        }
    }
}
