using QuizyZunaAPI.Application.Questions.GetById;
using System.Net;

namespace QuizyZunaAPI.Api.FunctionalTests.Questions;

public class DeleteByIdQuestionTests(FunctionalTestWebAppFactory functionalTestWebAppFactory) : BaseFunctionalTest(functionalTestWebAppFactory)
{
    [Fact]
    public async Task ShouldReturn_204NoContent_WhenCommandIsValid()
    {
        //Arrange
        GetQuestionByIdQuery request = new(Guid.NewGuid());
        var requestPath = new Uri(BaseApiUrl, request.questionid.ToString());

        //Act
        var response = await HttpClient.DeleteAsync(requestPath);

        //Assert
        response.StatusCode.Should().Be(HttpStatusCode.NoContent);
    }
}
