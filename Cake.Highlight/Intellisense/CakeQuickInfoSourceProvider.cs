using Cake.Intellicense;
using Microsoft.VisualStudio.Language.Intellisense;
using Microsoft.VisualStudio.Text;
using Microsoft.VisualStudio.Text.Tagging;
using Microsoft.VisualStudio.Utilities;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cake.Intellisense
{
    [Export(typeof(IQuickInfoSourceProvider))]
    [ContentType("cake")]
    [Name("cakeQuickInfo")]
    class CakeQuickInfoSourceProvider : IQuickInfoSourceProvider
    {

        [Import]
        IBufferTagAggregatorFactoryService aggService = null;

        public IQuickInfoSource TryCreateQuickInfoSource(ITextBuffer textBuffer)
        {
            return new CakeQuickInfoSource(textBuffer, aggService.CreateTagAggregator<CakeTokenTag>(textBuffer));
        }
    }

}
