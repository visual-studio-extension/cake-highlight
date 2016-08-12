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
    using Microsoft.VisualStudio.Text.Tagging;
    using Microsoft.VisualStudio.Utilities;
    using System.Linq;
    using Additoins;

    [Export(typeof(ITaggerProvider))]
    [ContentType("cake")]
    [TagType(typeof(CakeTokenTag))]
    internal sealed class CakeTokenTagProvider : ITaggerProvider
    {

        public ITagger<T> CreateTagger<T>(ITextBuffer buffer) where T : ITag
        {
            return new CakeTokenTagger(buffer) as ITagger<T>;
        }
    }

    public class CakeTokenTag : ITag
    {
        public CakeTokenTypes Type { get; private set; }

        public CakeTokenTag(CakeTokenTypes type)
        {
            this.Type = type;
        }
    }

    internal sealed class CakeTokenTagger : ITagger<CakeTokenTag>
    {

        ITextBuffer _buffer;
        IDictionary<string, CakeTokenTypes> _cakeTypes;

        internal CakeTokenTagger(ITextBuffer buffer)
        {
            _buffer = buffer;
            _cakeTypes = new Dictionary<string, CakeTokenTypes>();

            _cakeTypes[">"] = CakeTokenTypes.Operators;
            _cakeTypes["="] = CakeTokenTypes.Operators;
            _cakeTypes["<"] = CakeTokenTypes.Operators;
            _cakeTypes["~"] = CakeTokenTypes.Operators;
            _cakeTypes["+"] = CakeTokenTypes.Operators;
            _cakeTypes["-"] = CakeTokenTypes.Operators;
            _cakeTypes["*"] = CakeTokenTypes.Operators;
            _cakeTypes["/"] = CakeTokenTypes.Operators;
            _cakeTypes["%"] = CakeTokenTypes.Operators;
            _cakeTypes[".."] = CakeTokenTypes.Operators;
            _cakeTypes["&&"] = CakeTokenTypes.Operators;
            _cakeTypes["||"] = CakeTokenTypes.Operators;


            foreach (var word in CakeFunctions.Keywords)
            {
                _cakeTypes[word] = CakeTokenTypes.ReservedWord;
            }

            foreach (var word in CakeFunctions.ContextualKeywords)
            {
                _cakeTypes[word] = CakeTokenTypes.ReservedWord;
            }

            foreach (var word in CakeFunctions.Functions)
            {
                _cakeTypes[word] = CakeTokenTypes.Functions;
            }
;
        }

        public event EventHandler<SnapshotSpanEventArgs> TagsChanged
        {
            add { }
            remove { }
        }

        public IEnumerable<ITagSpan<CakeTokenTag>> GetTags(NormalizedSnapshotSpanCollection spans)
        {

            foreach (SnapshotSpan curSpan in spans)
            {
                ITextSnapshotLine containingLine = curSpan.Start.GetContainingLine();
                int curLoc = containingLine.Start.Position;
                string[] tokens = containingLine.GetText().ToLower().Split(' ');

                foreach (string cakeToken in tokens)
                {
                    if (_cakeTypes.ContainsKey(cakeToken))
                    {
                        var tokenSpan = new SnapshotSpan(curSpan.Snapshot, new Span(curLoc, cakeToken.Length));
                        if (tokenSpan.IntersectsWith(curSpan))
                            yield return new TagSpan<CakeTokenTag>(tokenSpan,
                                                                  new CakeTokenTag(_cakeTypes[cakeToken]));
                    }

                    //add an extra char location because of the space
                    curLoc += cakeToken.Length + 1;
                }
            }

        }
    }
}
