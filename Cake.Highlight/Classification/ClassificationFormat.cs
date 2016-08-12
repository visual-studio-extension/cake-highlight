//***************************************************************************
//
//    Copyright (c) Microsoft Corporation. All rights reserved.
//    This code is licensed under the Visual Studio SDK license terms.
//    THIS CODE IS PROVIDED *AS IS* WITHOUT WARRANTY OF
//    ANY KIND, EITHER EXPRESS OR IMPLIED, INCLUDING ANY
//    IMPLIED WARRANTIES OF FITNESS FOR A PARTICULAR
//    PURPOSE, MERCHANTABILITY, OR NON-INFRINGEMENT.
//
//***************************************************************************

using System.ComponentModel.Composition;
using System.Windows.Media;
using Microsoft.VisualStudio.Text.Classification;
using Microsoft.VisualStudio.Utilities;

namespace Cake
{
    /// <summary>
    /// Defines the editor format for the reservedWord classification type. Text is colored Green
    /// </summary>
    [Export(typeof(EditorFormatDefinition))]
    [ClassificationType(ClassificationTypeNames = "reservedWord")]
    [Name("reservedWord")]
    //this should be visible to the end user
    [UserVisible(false)]
    //set the priority to be after the default classifiers
    [Order(Before = Priority.Default)]
    internal sealed class ReservedWord : ClassificationFormatDefinition
    {
        /// <summary>
        /// Defines the visual format for the "question" classification type
        /// </summary>
        public ReservedWord()
        {
            DisplayName = "reservedWord"; //human readable version of the name
            ForegroundColor = Colors.Firebrick;
        }
    }

    /// <summary>
    /// Defines the editor format for the operator classification type. Text is colored Green
    /// </summary>
    [Export(typeof(EditorFormatDefinition))]
    [ClassificationType(ClassificationTypeNames = "operators")]
    [Name("operators")]
    //this should be visible to the end user
    [UserVisible(false)]
    //set the priority to be after the default classifiers
    [Order(Before = Priority.Default)]
    internal sealed class Operators : ClassificationFormatDefinition
    {
        /// <summary>
        /// Defines the visual format for the "question" classification type
        /// </summary>
        public Operators()
        {
            DisplayName = "operators"; //human readable version of the name
            ForegroundColor = Colors.ForestGreen;
        }
    }

    [Export(typeof(EditorFormatDefinition))]
    [ClassificationType(ClassificationTypeNames = "functions")]
    [Name("functions")]
    [UserVisible(false)]
    [Order(Before = Priority.Default)]
    internal sealed class Functions : ClassificationFormatDefinition
    {
        public Functions()
        {
            DisplayName = "functions"; 
            ForegroundColor = Colors.Green;
        }
    }


}
