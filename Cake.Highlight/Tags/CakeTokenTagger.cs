using Cake.Additoins;
using Microsoft.VisualStudio.Text;
using Microsoft.VisualStudio.Text.Tagging;
using System;
using System.Collections.Generic;

namespace Cake.Tags
{
    internal sealed class CakeTokenTagger : ITagger<CakeTokenTag>
    {
        ITextBuffer _buffer;
        IDictionary<string, CakeTokenTypes> _cakeTypes;

        internal CakeTokenTagger(ITextBuffer buffer)
        {
            _buffer = buffer;
            _cakeTypes = new Dictionary<string, CakeTokenTypes>();

            foreach (var word in CakeKeyword.Operators)
            {
                _cakeTypes[word] = CakeTokenTypes.Operators;
            }

            foreach (var word in CakeKeyword.Keywords)
            {
                _cakeTypes[word] = CakeTokenTypes.ReservedWord;
            }

            foreach (var word in CakeKeyword.ContextualKeywords)
            {
                _cakeTypes[word] = CakeTokenTypes.ReservedWord;
            }

            foreach (var word in CakeKeyword.Functions)
            {
                _cakeTypes[word] = CakeTokenTypes.CakeFunctions;
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
                string[] tokens = containingLine.GetText().Split(' ');

                foreach (string cakeToken in tokens)
                {
                    if (_cakeTypes.ContainsKey(cakeToken))
                    {
                        var tokenSpan = new SnapshotSpan(curSpan.Snapshot, new Span(curLoc, cakeToken.Length));
                        if (tokenSpan.IntersectsWith(curSpan))
                        {
                            var tag = new CakeTokenTag(_cakeTypes[cakeToken]);
                            var span = new TagSpan<CakeTokenTag>(tokenSpan, tag);
                            yield return span;
                        }
                    }

                    //add an extra char location because of the space
                    curLoc += cakeToken.Length + 1;
                }
            }
        }
    }
}
