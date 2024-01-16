﻿using QuizyZunaAPI.Domain.Questions.Exceptions;

namespace QuizyZunaAPI.Domain.Questions.ValueObjects;

public sealed record Answers(CorrectAnswer CorrectAnswer, WrongAnswers WrongAnswers)
{

    public static Answers Create(CorrectAnswer correctAnswer, WrongAnswers wrongAnswers)
    {
        if (correctAnswer is null)
        {
            throw new CorrectAnswerIsNullDomainException($"{nameof(correctAnswer)} can't be null");
        }

        if (wrongAnswers is null)
        {
            throw new WrongAnswersIsNullDomainException($"{nameof(wrongAnswers)} can't be null");
        }

        if (wrongAnswers.Value.Contains(correctAnswer.Value))
        {
            throw new WrongAnswersContainsCorrectAnswerDomainException($"{nameof(wrongAnswers)} can't contain {nameof(correctAnswer)}");
        }

        return new Answers(correctAnswer, wrongAnswers);
    }
};