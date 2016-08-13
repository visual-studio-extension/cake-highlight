using System.Text.RegularExpressions;
using FluentAssertions;
using Xunit;
using CSharpVerbalExpressions;

namespace Cake.Hightlight.Tests.Spec
{
    public class RegexSpec
    {
        [Fact]
        public void Hello()
        {
            var pattern = "\\\"(.*?)\\\"";
            var data = @"Print(""Hello world"");";

            var regex = new Regex(pattern);
            var rs = regex.Matches(data);
            rs.Count.Should().Be(1);

            rs[0].Value.Should().Be(@"""Hello world""");

        }

        [Fact]
        public void VerbalExpression()
        {
            var verbEx = new VerbalExpressions()
                        .StartOfLine()
                        .Then("http")
                        .Maybe("s")
                        .Then("://")
                        .Maybe("www.")
                        .AnythingBut(" ")
                        .EndOfLine();


        }
    }
}
