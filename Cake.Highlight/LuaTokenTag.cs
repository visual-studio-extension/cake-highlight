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
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.Composition;
    using Microsoft.VisualStudio.Text;
    using Microsoft.VisualStudio.Text.Classification;
    using Microsoft.VisualStudio.Text.Editor;
    using Microsoft.VisualStudio.Text.Tagging;
    using Microsoft.VisualStudio.Utilities;

    [Export(typeof(ITaggerProvider))]
    [ContentType("lua")]
    [TagType(typeof(LuaTokenTag))]
    internal sealed class LuaTokenTagProvider : ITaggerProvider
    {

        public ITagger<T> CreateTagger<T>(ITextBuffer buffer) where T : ITag
        {
            return new LuaTokenTagger(buffer) as ITagger<T>;
        }
    }

    public class LuaTokenTag : ITag 
    {
        public LuaTokenTypes type { get; private set; }

        public LuaTokenTag(LuaTokenTypes type)
        {
            this.type = type;
        }
    }

    internal sealed class LuaTokenTagger : ITagger<LuaTokenTag>
    {

        ITextBuffer _buffer;
        IDictionary<string, LuaTokenTypes> _luaTypes;

        internal LuaTokenTagger(ITextBuffer buffer)
        {
            _buffer = buffer;
            _luaTypes = new Dictionary<string, LuaTokenTypes>();

            _luaTypes["and"] = LuaTokenTypes.ReservedWord;
            _luaTypes["break"] = LuaTokenTypes.ReservedWord;
            _luaTypes["do"] = LuaTokenTypes.ReservedWord;
            _luaTypes["else"] = LuaTokenTypes.ReservedWord;
            _luaTypes["elseif"] = LuaTokenTypes.ReservedWord;
            _luaTypes["end"] = LuaTokenTypes.ReservedWord;
            _luaTypes["false"] = LuaTokenTypes.ReservedWord;
            _luaTypes["for"] = LuaTokenTypes.ReservedWord;
            _luaTypes["function"] = LuaTokenTypes.ReservedWord;
            _luaTypes["if"] = LuaTokenTypes.ReservedWord;
            _luaTypes["local"] = LuaTokenTypes.ReservedWord;
            _luaTypes["nil"] = LuaTokenTypes.ReservedWord;
            _luaTypes["not"] = LuaTokenTypes.ReservedWord;
            _luaTypes["or"] = LuaTokenTypes.ReservedWord;
            _luaTypes["repeat"] = LuaTokenTypes.ReservedWord;
            _luaTypes["return"] = LuaTokenTypes.ReservedWord;
            _luaTypes["then"] = LuaTokenTypes.ReservedWord;
            _luaTypes["true"] = LuaTokenTypes.ReservedWord;
            _luaTypes["until"] = LuaTokenTypes.ReservedWord;
            _luaTypes["while"] = LuaTokenTypes.ReservedWord;

            _luaTypes[">"] = LuaTokenTypes.Operators;
            _luaTypes["="] = LuaTokenTypes.Operators;
            _luaTypes["<"] = LuaTokenTypes.Operators;
            _luaTypes["~"] = LuaTokenTypes.Operators;
            _luaTypes["+"] = LuaTokenTypes.Operators;
            _luaTypes["-"] = LuaTokenTypes.Operators;
            _luaTypes["*"] = LuaTokenTypes.Operators;
            _luaTypes["/"] = LuaTokenTypes.Operators;
            _luaTypes["%"] = LuaTokenTypes.Operators;
            _luaTypes[".."] = LuaTokenTypes.Operators;
            _luaTypes["&&"] = LuaTokenTypes.Operators;
            _luaTypes["||"] = LuaTokenTypes.Operators;
        }

        public event EventHandler<SnapshotSpanEventArgs> TagsChanged
        {
            add { }
            remove { }            
        }

        public IEnumerable<ITagSpan<LuaTokenTag>> GetTags(NormalizedSnapshotSpanCollection spans)
        {

            foreach (SnapshotSpan curSpan in spans)
            {
                ITextSnapshotLine containingLine = curSpan.Start.GetContainingLine();
                int curLoc = containingLine.Start.Position;
                string[] tokens = containingLine.GetText().ToLower().Split(' ');

                foreach (string luaToken in tokens)
                {
                    if (_luaTypes.ContainsKey(luaToken))
                    {
                        var tokenSpan = new SnapshotSpan(curSpan.Snapshot, new Span(curLoc, luaToken.Length));
                        if( tokenSpan.IntersectsWith(curSpan) ) 
                            yield return new TagSpan<LuaTokenTag>(tokenSpan, 
                                                                  new LuaTokenTag(_luaTypes[luaToken]));
                    }

                    //add an extra char location because of the space
                    curLoc += luaToken.Length + 1;
                }
            }
            
        }
    }
}
