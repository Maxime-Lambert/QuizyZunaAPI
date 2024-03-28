using QuizyZunaAPI.Domain.Questions.ValueObjects;

namespace QuizyZunaAPI.Domain.Questions;

public interface IQuestionRepository
{
    public Task AddAsync(Question question);

    public void Delete(Question question);

    public void Update(Question question);

    public Task<Question?> GetByIdAsync(QuestionId questionId, CancellationToken cancellationToken);

    public Task<List<Question>> GetAllAsync(CancellationToken cancellationToken);
}
