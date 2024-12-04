namespace QuizyZunaAPI.Api.FunctionalTests;

public abstract class BaseFunctionalTest : IClassFixture<FunctionalTestWebAppFactory>
{
    private const string LocalhostUrl = "https://localhost:7012/api/v1/questions/";

    protected HttpClient HttpClient { get; init; }
    protected Uri BaseApiUrl { get; init; }

    protected BaseFunctionalTest(FunctionalTestWebAppFactory factory)
    {
        ArgumentNullException.ThrowIfNull(factory);
        HttpClient = factory.CreateClient();
        BaseApiUrl = new(LocalhostUrl);
    }
}
