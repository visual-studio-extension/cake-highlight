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

    internal sealed class CakeClassifier : ITagger<ClassificationTag>
    {
        ITextBuffer _buffer;
        ITagAggregator<CakeTokenTag> _aggregator;
        IDictionary<CakeTokenTypes, IClassificationType> _cakeTypes;

        /// <summary>
        /// Construct the classifier and define search tokens
        /// </summary>
        internal CakeClassifier(ITextBuffer buffer, 
                               ITagAggregator<CakeTokenTag> cakeTagAggregator, 
                               IClassificationTypeRegistryService typeService)
        {
            _buffer = buffer;
            _aggregator = cakeTagAggregator;
            _cakeTypes = new Dictionary<CakeTokenTypes, IClassificationType>();
            _cakeTypes[CakeTokenTypes.ReservedWord] = typeService.GetClassificationType("ReservedWord");
            _cakeTypes[CakeTokenTypes.Operators] = typeService.GetClassificationType("Operators");
            _cakeTypes[CakeTokenTypes.Functions] = typeService.GetClassificationType("Functions");
        }

        public event EventHandler<SnapshotSpanEventArgs> TagsChanged
        {
            add { }
            remove { }
        }

        /// <summary>
        /// Search the given span for any instances of classified tags
        /// </summary>
        public IEnumerable<ITagSpan<ClassificationTag>> GetTags(NormalizedSnapshotSpanCollection spans)
        {
            foreach (var tagSpan in _aggregator.GetTags(spans))
            {
                var tagSpans = tagSpan.Span.GetSpans(spans[0].Snapshot);
                yield return 
                    new TagSpan<ClassificationTag>(tagSpans[0], 
                                                   new ClassificationTag(_cakeTypes[tagSpan.Tag.Type]));
            }
        }
    }
}
