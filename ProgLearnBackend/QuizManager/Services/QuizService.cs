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
            return await GetQuizAsDTO(quiz);
        }

        public override async Task<QuizzesDTO> GetQuizzesByCategoryAndDifficultyAndNumber(GetQuizByCategoryAndDifficultyAndNumberDTO request, ServerCallContext context)
        {
            List<Database.Models.Quiz> quizzesFromDb = await _unitOfWork.QuizRepository.GetQuizzesByCategoryAndDifficulty(request.Category, request.Difficulty);
            List<QuizDTO> quizList = new List<QuizDTO>();
            QuizzesDTO quizzes = new QuizzesDTO();

            for (int i=0; i < request.Number; i++)
            {
                quizList.Add(await GetQuizAsDTO(quizzesFromDb[i]));
            }

            quizzes.Quizzes.AddRange(quizList);
            return quizzes;
        }

        private async Task<QuizDTO> GetQuizAsDTO(Database.Models.Quiz quiz)
        {
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
    }
}
