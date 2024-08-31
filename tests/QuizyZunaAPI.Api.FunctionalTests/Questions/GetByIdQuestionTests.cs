using QuizyZunaAPI.Application.Questions.CreateQuestion;
using QuizyZunaAPI.Application.Questions.GetById;
using QuizyZunaAPI.Application.Questions.Responses;

using System.Net;
using System.Net.Http.Json;

namespace QuizyZunaAPI.Api.FunctionalTests.Questions;

public class GetByIdQuestionTests(FunctionalTestWebAppFactory functionalTestWebAppFactory) : BaseFunctionalTest(functionalTestWebAppFactory)
{
    [Fact]
    public async Task ShouldReturn_200Ok_WhenIdExists()
    {
        //Arrange
        CreateQuestionRequest createRequest = new("Is this a Question ?", "Yes", ["No", "Maybe", "?"],
                "Novice", "", ["Literature", "Mangas"]);
        var createResponse = await HttpClient.PostAsJsonAsync(BaseApiUrl, createRequest);
        var createContent = await createResponse.Content.ReadFromJsonAsync<QuestionResponse>();
        var createdQuestionId = createContent!.id;
        GetQuestionByIdQuery request = new(createdQuestionId);
        var requestPath = new Uri(BaseApiUrl, request.questionid.ToString());

        //Act
        var response = await HttpClient.GetAsync(requestPath);

        //Assert
        response.StatusCode.Should().Be(HttpStatusCode.OK);
    }

    [Fact]
    public async Task ShouldReturn_400BadRequest_WhenIdDoesntExist()
    {
        //Arrange
        GetQuestionByIdQuery request = new(Guid.NewGuid());
        var requestPath =  new Uri(BaseApiUrl, request.questionid.ToString());

        //Act
        var response = await HttpClient.GetAsync(requestPath);

        //Assert
        response.StatusCode.Should().Be(HttpStatusCode.NotFound);
    }
}
