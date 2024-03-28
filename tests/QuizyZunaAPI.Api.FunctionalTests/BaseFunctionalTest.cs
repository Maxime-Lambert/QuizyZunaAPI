namespace QuizyZunaAPI.Api.FunctionalTests;

public abstract class BaseFunctionalTest : IClassFixture<FunctionalTestWebAppFactory>
{
    protected HttpClient HttpClient { get; init; }
    protected Uri BaseApiUrl { get; init; }

    protected BaseFunctionalTest(FunctionalTestWebAppFactory factory)
    {
        ArgumentNullException.ThrowIfNull(factory);
        HttpClient = factory.CreateClient();
        BaseApiUrl = new("https://localhost:7012/api/v1/questions/");
    }
}
