using Microsoft.EntityFrameworkCore;

using QuizyZunaAPI.Domain.Questions;
using QuizyZunaAPI.Domain.Questions.ValueObjects;

namespace QuizyZunaAPI.Persistence.Repositories;

public sealed class QuestionRepository(ApplicationDbContext context) : IQuestionRepository
{
    private readonly ApplicationDbContext _context = context;

    public async Task AddAsync(Question question)
    {
        _ = await _context.Questions.AddAsync(question).ConfigureAwait(false);
    }

    public void Delete(Question question)
    {
        _ = _context.Questions.Remove(question);
    }

    public Task<Question?> GetByIdAsync(QuestionId questionId, CancellationToken cancellationToken)
    {
        return _context.Questions.Include(question => question.Answers).Include(question => question.Tags).SingleOrDefaultAsync(question => question.Id == questionId, cancellationToken);
    }

    public Task<List<Question>> GetAllAsync(CancellationToken cancellationToken)
    {
        return _context.Questions.AsNoTracking().ToListAsync(cancellationToken);
    }

    public void Update(Question question)
    {
        _ = _context.Questions.Update(question);
    }

    public Task<Question?> GetByTitleAsync(QuestionTitle questionTitle, CancellationToken cancellationToken)
    {
        return _context.Questions.SingleOrDefaultAsync(question => question.Title == questionTitle, cancellationToken);
    }


}
