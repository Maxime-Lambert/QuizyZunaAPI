using QuizyZunaAPI.Domain.Questions.ValueObjects;

namespace QuizyZunaAPI.Domain.Questions;

public interface IQuestionRepository
{
    Task<Question?> GetByIdAsync(QuestionId questionId);

    IEnumerable<Question> GetAll();

    void Add(Question question);

    void Delete(Question question);

    void Update(Question question);
}
