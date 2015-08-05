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

namespace LuaLanguage
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

        /// <summary>
        /// Defines the "comment" classification type.
        /// </summary>
        [Export(typeof(ClassificationTypeDefinition))]
        [Name("comment")]
        internal static ClassificationTypeDefinition comment = null;

        /// <summary>
        /// Defines the "string" classification type.
        /// </summary>
        [Export(typeof(ClassificationTypeDefinition))]
        [Name("stringMarker")]
        internal static ClassificationTypeDefinition stringMarker = null;
        #endregion
    }
}
