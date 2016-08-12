using Microsoft.VisualStudio.Text;
using Microsoft.VisualStudio.Text.Tagging;
using Microsoft.VisualStudio.Utilities;
using System.ComponentModel.Composition;

namespace Cake.Tags
{
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
}
