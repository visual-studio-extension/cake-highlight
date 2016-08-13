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

namespace Cake.Classification
{
    [Export(typeof(EditorFormatDefinition))]
    [ClassificationType(ClassificationTypeNames = "reservedWord")]
    [Name("reservedWord")]
    [UserVisible(false)]
    [Order(Before = Priority.Default)]
    internal sealed class ReservedWord : ClassificationFormatDefinition
    {
        public ReservedWord()
        {
            DisplayName = "reservedWord"; //human readable version of the name
            ForegroundColor = Colors.MediumPurple;
        }
    }

    [Export(typeof(EditorFormatDefinition))]
    [ClassificationType(ClassificationTypeNames = "operators")]
    [Name("operators")]
    [UserVisible(false)]
    [Order(Before = Priority.Default)]
    internal sealed class Operators : ClassificationFormatDefinition
    {
        public Operators()
        {
            DisplayName = "operators"; //human readable version of the name
            ForegroundColor = Colors.PaleVioletRed;
        }
    }

    [Export(typeof(EditorFormatDefinition))]
    [ClassificationType(ClassificationTypeNames = "cakeFunctions")]
    [Name("cakeFunctions")]
    [UserVisible(false)]
    [Order(Before = Priority.Default)]
    internal sealed class CakeFunctions : ClassificationFormatDefinition
    {
        public CakeFunctions()
        {
            DisplayName = "cakeFunctions";
            ForegroundColor = Colors.Orange;
        }
    }

    [Export(typeof(EditorFormatDefinition))]
    [ClassificationType(ClassificationTypeNames = "quote")]
    [Name("quote")]
    [UserVisible(false)]
    [Order(Before = Priority.Default)]
    internal sealed class Quote : ClassificationFormatDefinition
    {
        public Quote()
        {
            DisplayName = "quote";
            ForegroundColor = Colors.LimeGreen;
        }

    }
}
