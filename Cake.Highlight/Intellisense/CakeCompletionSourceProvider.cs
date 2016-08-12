using Microsoft.VisualStudio.Language.Intellisense;
using Microsoft.VisualStudio.Text;
using Microsoft.VisualStudio.Utilities;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cake.Intellisense
{

    [Export(typeof(ICompletionSourceProvider))]
    [ContentType("cake")]
    [Name("cakeCompletion")]
    class CakeCompletionSourceProvider : ICompletionSourceProvider
    {
        public ICompletionSource TryCreateCompletionSource(ITextBuffer textBuffer)
        {
            return new CakeCompletionSource(textBuffer);
        }
    }

}
