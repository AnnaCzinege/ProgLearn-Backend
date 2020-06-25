using Grpc.Core;
using Grpc.Net.Client;
using Microsoft.Extensions.Logging;
using QuizManager.Database.Repositories.Interfaces;
using QuizManager.Database.RepositoryContainer;
using QuizManager.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QuizManager.Services
{
    public class QuizService : Quiz.QuizBase
    {
        private readonly IUnitOfWork _unitOfWork;

        public QuizService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public override async Task<QuizDTO> GetQuizById(GetQuizDTO request, ServerCallContext context)
        {
            Database.Models.Quiz quiz = await _unitOfWork.QuizRepository.GetQuizById(request.QuizId);
            List<string> incorrectAnswers = await _unitOfWork.IncorrectAnswerRepository.GetIncorrectAnswers(quiz.IncorrectAnswers.Select(ia => ia.IncorrectAnswerId).ToList());
            List<IncorrectAnswerDTO> options = new List<IncorrectAnswerDTO>();

            foreach (var item in incorrectAnswers)
            {
                options.Add(new IncorrectAnswerDTO { Option = item });
            }

            QuizDTO output = new QuizDTO
            {
                QuizId = quiz.Id,
                Category = quiz.Category,
                Difficulty = quiz.Difficulty,
                Question = quiz.Question,
                Answer = quiz.CorrectAnswer,
            };
            output.IncorrectAnswers.AddRange(options);
            return output;
        }

        public override Task<QuizzesDTO> GetQuizByCategoryAndDifficulty(GetQuizByCategoryAndDifficultyDTO request, ServerCallContext context)
        {
            return base.GetQuizByCategoryAndDifficulty(request, context);
        }
    }
}
