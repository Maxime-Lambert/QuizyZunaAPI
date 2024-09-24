using Microsoft.EntityFrameworkCore;

using QuizyZunaAPI.Domain.Questions;
using QuizyZunaAPI.Domain.Questions.ValueObjects;

namespace QuizyZunaAPI.Persistence.Repositories;

public sealed class QuestionRepository(ApplicationDbContext context) : IQuestionRepository
{
    private readonly ApplicationDbContext _context = context;

    public async Task AddAsync(Question question)
    {
        await _context.Questions.AddAsync(question).ConfigureAwait(true);
    }

    public void Delete(Question question)
    {
        _context.Questions.Remove(question);
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
        _context.Questions.Update(question);
    }

    public Task<Question?> GetByTitleAsync(QuestionTitle questionTitle, CancellationToken cancellationToken)
    {
        return _context.Questions.SingleOrDefaultAsync(question => question.Title == questionTitle, cancellationToken);
    }


}
