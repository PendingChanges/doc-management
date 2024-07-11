using Xunit;

namespace Doc.Management.UnitTests.Integration;

[CollectionDefinition("integration")]
public class IntegrationCollection : ICollectionFixture<AppFixture> { }
