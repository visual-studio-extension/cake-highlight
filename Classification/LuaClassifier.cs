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

namespace LuaLanguage
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
    [TagType(typeof(ClassificationTag))]
    internal sealed class LuaClassifierProvider : ITaggerProvider
    {

        [Export]
        [Name("lua")]
        [BaseDefinition("code")]
        internal static ContentTypeDefinition LuaContentType = null;

        [Export]
        [FileExtension(".lua")]
        [ContentType("lua")]
        internal static FileExtensionToContentTypeDefinition LuaFileType = null;

        [Import]
        internal IClassificationTypeRegistryService ClassificationTypeRegistry = null;

        [Import]
        internal IBufferTagAggregatorFactoryService aggregatorFactory = null;

        public ITagger<T> CreateTagger<T>(ITextBuffer buffer) where T : ITag
        {

            ITagAggregator<LuaTokenTag> luaTagAggregator = 
                                            aggregatorFactory.CreateTagAggregator<LuaTokenTag>(buffer);

            return new LuaClassifier(buffer, luaTagAggregator, ClassificationTypeRegistry) as ITagger<T>;
        }
    }

    internal sealed class LuaClassifier : ITagger<ClassificationTag>
    {
        ITextBuffer _buffer;
        ITagAggregator<LuaTokenTag> _aggregator;
        IDictionary<LuaTokenTypes, IClassificationType> _luaTypes;

        /// <summary>
        /// Construct the classifier and define search tokens
        /// </summary>
        internal LuaClassifier(ITextBuffer buffer, 
                               ITagAggregator<LuaTokenTag> luaTagAggregator, 
                               IClassificationTypeRegistryService typeService)
        {
            _buffer = buffer;
            _aggregator = luaTagAggregator;
            _luaTypes = new Dictionary<LuaTokenTypes, IClassificationType>();
            _luaTypes[LuaTokenTypes.ReservedWord] = typeService.GetClassificationType("ReservedWord");
            _luaTypes[LuaTokenTypes.Operators] = typeService.GetClassificationType("Operators");
            _luaTypes[LuaTokenTypes.Comment] = typeService.GetClassificationType("Comment");
            _luaTypes[LuaTokenTypes.StringMarker] = typeService.GetClassificationType("StringMarker");
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
                                                   new ClassificationTag(_luaTypes[tagSpan.Tag.type]));
            }
        }
    }
}
