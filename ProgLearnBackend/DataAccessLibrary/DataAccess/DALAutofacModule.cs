using Autofac;
using Microsoft.EntityFrameworkCore;

namespace DataAccessLibrary.DataAccess
{
    public class DALAutofacModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);
            builder.RegisterType<QuizContext>().AsSelf().As<DbContext>().InstancePerLifetimeScope();
        }
    }
}