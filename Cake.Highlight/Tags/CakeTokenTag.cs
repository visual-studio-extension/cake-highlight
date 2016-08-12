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

namespace Cake
{
    using Microsoft.VisualStudio.Text.Tagging;

    public class CakeTokenTag : ITag
    {
        public CakeTokenTypes Type { get; private set; }

        public CakeTokenTag(CakeTokenTypes type)
        {
            this.Type = type;
        }
    }

}
