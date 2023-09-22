using TimeLogger.Api.Tests.TestFixtures;
using Xunit;

namespace TimeLogger.Api.Tests.Collections
{
    [CollectionDefinition("HTTPClientCollection")]
    public class HttpClientFixtureCollection : ICollectionFixture<HttpClientFixture>
    {
    }
}