using Microsoft.VisualStudio.Text;
using Microsoft.VisualStudio.Text.Classification;
using Microsoft.VisualStudio.Text.Tagging;
using Microsoft.VisualStudio.Utilities;
using System.ComponentModel.Composition;

namespace Cake.Classification
{

    [Export(typeof(ITaggerProvider))]
    [ContentType("cake")]
    [TagType(typeof(ClassificationTag))]
    internal sealed class CakeClassifierProvider : ITaggerProvider
    {

        [Export]
        [Name("cake")]
        [BaseDefinition("code")]
        internal static ContentTypeDefinition CakeContentType = null;

        [Export]
        [FileExtension(".cake")]
        [ContentType("cake")]
        internal static FileExtensionToContentTypeDefinition CakeFileType = null;

        [Import]
        internal IClassificationTypeRegistryService ClassificationTypeRegistry = null;

        [Import]
        internal IBufferTagAggregatorFactoryService aggregatorFactory = null;

        public ITagger<T> CreateTagger<T>(ITextBuffer buffer) where T : ITag
        {

            ITagAggregator<CakeTokenTag> cakeTagAggregator =
                                            aggregatorFactory.CreateTagAggregator<CakeTokenTag>(buffer);

            return new CakeClassifier(buffer, cakeTagAggregator, ClassificationTypeRegistry) as ITagger<T>;
        }
    }

}
