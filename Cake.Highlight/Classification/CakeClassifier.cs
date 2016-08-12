﻿//***************************************************************************
//
//    Copyright (c) Microsoft Corporation. All rights reserved.
//    This code is licensed under the Visual Studio SDK license terms.
//    THIS CODE IS PROVIDED *AS IS* WITHOUT WARRANTY OF
//    ANY KIND, EITHER EXPRESS OR IMPLIED, INCLUDING ANY
//    IMPLIED WARRANTIES OF FITNESS FOR A PARTICULAR
//    PURPOSE, MERCHANTABILITY, OR NON-INFRINGEMENT.
//
//***************************************************************************

namespace Cake.Classification
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.Composition;
    using Microsoft.VisualStudio.Text;
    using Microsoft.VisualStudio.Text.Classification;
    using Microsoft.VisualStudio.Text.Tagging;
    using Microsoft.VisualStudio.Utilities;

    internal sealed class CakeClassifier : ITagger<ClassificationTag>
    {
        ITextBuffer _buffer;
        ITagAggregator<CakeTokenTag> _aggregator;
        IDictionary<CakeTokenTypes, IClassificationType> _cakeTypes;

        internal CakeClassifier(ITextBuffer buffer, 
                               ITagAggregator<CakeTokenTag> cakeTagAggregator, 
                               IClassificationTypeRegistryService typeService)
        {
            _buffer = buffer;
            _aggregator = cakeTagAggregator;
            _cakeTypes = new Dictionary<CakeTokenTypes, IClassificationType>();
            _cakeTypes[CakeTokenTypes.ReservedWord] = typeService.GetClassificationType("ReservedWord");
            _cakeTypes[CakeTokenTypes.Operators] = typeService.GetClassificationType("Operators");
            _cakeTypes[CakeTokenTypes.CakeFunctions] = typeService.GetClassificationType("FakeFunctions");
        }

        public event EventHandler<SnapshotSpanEventArgs> TagsChanged
        {
            add { }
            remove { }
        }

        public IEnumerable<ITagSpan<ClassificationTag>> GetTags(NormalizedSnapshotSpanCollection spans)
        {
            foreach (var tagSpan in _aggregator.GetTags(spans))
            {
                var tagSpans = tagSpan.Span.GetSpans(spans[0].Snapshot);
                yield return 
                    new TagSpan<ClassificationTag>(tagSpans[0], new ClassificationTag(_cakeTypes[tagSpan.Tag.Type]));
            }
        }
    }
}
