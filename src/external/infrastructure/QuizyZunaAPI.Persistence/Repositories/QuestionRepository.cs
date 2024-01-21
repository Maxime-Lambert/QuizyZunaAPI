using Microsoft.EntityFrameworkCore;

using QuizyZunaAPI.Domain.Questions;
using QuizyZunaAPI.Domain.Questions.ValueObjects;

namespace QuizyZunaAPI.Persistence.Repositories;

internal sealed class QuestionRepository(ApplicationDbContext context) : IQuestionRepository
{
    private readonly ApplicationDbContext _context = context;

    public void Add(Question question)
    {
        throw new NotImplementedException();
    }

    public void Delete(Question question)
    {
        throw new NotImplementedException();
    }

    public IEnumerable<Question> GetAll()
    {
        return _context.Questions.AsEnumerable();
    }

    public Task<Question?> GetByIdAsync(QuestionId questionId)
    {
        return _context.Questions.SingleOrDefaultAsync(question => question.Id == questionId);
    }

    public void Update(Question question)
    {
        throw new NotImplementedException();
    }
}
