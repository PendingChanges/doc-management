using System.Linq;
using TechTalk.SpecFlow;
using Xunit;

namespace Doc.Management.UnitTests.Domain
{
    [Binding]
    public class CommonStepDefinitions
    {
        private readonly AggregateContext _aggregateContext;
        public CommonStepDefinitions(AggregateContext aggregateContext)
        {
            _aggregateContext = aggregateContext;
        }

        [Then(@"An error with code ""([^""]*)"" is raised")]
        public void ThenAnErrorWithCodeIsRaised(string errorCode)
        {
            Assert.NotNull(_aggregateContext.Aggregate);
            var error = _aggregateContext.GetErrors().FirstOrDefault(e => e.Code == errorCode);

            Assert.NotNull(error);
        }

        [Then(@"No errors")]
        public void ThenNoErrors()
        {
            Assert.NotNull(_aggregateContext.Aggregate);
            var errors = _aggregateContext.GetErrors();

            Assert.Empty(errors);
        }

    }
}
