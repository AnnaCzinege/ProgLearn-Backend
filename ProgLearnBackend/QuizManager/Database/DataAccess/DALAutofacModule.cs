using Autofac;
using Microsoft.EntityFrameworkCore;
using QuizManager.Database.Repositories.Interfaces;
using QuizManager.Database.Repositories.SQL;
using QuizManager.Database.RepositoryContainer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QuizManager.Database.DataAccess
{
    public class DALAutofacModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);
            builder.RegisterType<UnitOfWork>().As<IUnitOfWork>();
            builder.RegisterType<QuizRepository>().As<IQuizRepository>();
            builder.RegisterType<IncorrectAnswerRepository>().As<IIncorrectAnswerRepository>();
            builder.RegisterType<QuizIncorrectAnswerRepository>().As<IQuizIncorrectAnswerRepository>();
            builder.RegisterType<UserRepository>().As<IUserRepository>();
            builder.RegisterType<UserQuizRepository>().As<IUserQuizRepository>();
            //builder.RegisterType<EmailConfirmationSender>().As<IEmailConfirmationSender>();
            builder.RegisterType<QuizContext>().AsSelf().As<DbContext>().InstancePerLifetimeScope();
        }
    }
}
