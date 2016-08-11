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

namespace Cake
{
    internal static class OrdinaryClassificationDefinition
    {
        #region Type definition
        /// <summary>
        /// Defines the "reservedWord" classification type.
        /// </summary>
        [Export(typeof(ClassificationTypeDefinition))]
        [Name("reservedWord")]
        internal static ClassificationTypeDefinition reservedWord = null;

        /// <summary>
        /// Defines the "operator" classification type.
        /// </summary>
        [Export(typeof(ClassificationTypeDefinition))]
        [Name("operators")]
        internal static ClassificationTypeDefinition operators = null;
        #endregion
    }
}
