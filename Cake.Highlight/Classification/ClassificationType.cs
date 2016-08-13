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
using Microsoft.VisualStudio.Text.Classification;
using Microsoft.VisualStudio.Utilities;

namespace Cake.Classification
{
    internal static class OrdinaryClassificationDefinition
    {
        [Export(typeof(ClassificationTypeDefinition))]
        [Name("reservedWord")]
        internal static ClassificationTypeDefinition reservedWord = null;

        [Export(typeof(ClassificationTypeDefinition))]
        [Name("operators")]
        internal static ClassificationTypeDefinition operators = null;

        [Export(typeof(ClassificationTypeDefinition))]
        [Name("cakeFunctions")]
        internal static ClassificationTypeDefinition cakeFunctions = null;

        [Export(typeof(ClassificationTypeDefinition))]
        [Name("quote")]
        internal static ClassificationTypeDefinition quote = null;
    }
}
