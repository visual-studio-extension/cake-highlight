using Cake.Additoins;
using Microsoft.VisualStudio.Text;
using Microsoft.VisualStudio.Text.Tagging;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace Cake.Tags
{
    public class Definition
    {
        public CakeTokenTypes TokenType { set; get; }
        public Regex Regex { set; get; }
    }
    internal sealed class CakeTokenTagger : ITagger<CakeTokenTag>
    {

        ITextBuffer _buffer;
        IDictionary<string, CakeTokenTypes> _cakeTypes;


        List<Definition> _definitions = new List<Definition>()
        {
            new Definition {
                TokenType = CakeTokenTypes.Quote,
                //Regex = new Regex("\\\"(.*?)\\\"", RegexOptions.IgnoreCase)
                Regex = new Regex(@"""[^""\\]*(?:\\.[^""\\]*)*""", RegexOptions.IgnoreCase)
            }
        };


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

            foreach (var span in spans)
            {
                ITextSnapshotLine line = span.Start.GetContainingLine();
                int location = line.Start.Position;
                string text = line.GetText();

                foreach (var definition in _definitions)
                {
                    if (definition.Regex.IsMatch(text))
                    {
                        MatchCollection matches = definition.Regex.Matches(text);

                        foreach (Match m in matches)
                        {
                            var token = new CakeTokenTag(definition.TokenType);
                            var snap = new SnapshotSpan(span.Snapshot, location + m.Index, m.Length);
                            yield return
                                new TagSpan<CakeTokenTag>(snap, token);
                        }

                    }
                }
            }
        }
    }
}
