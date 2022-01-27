using MediatR;
using TadbirTest.MainApp.Domain.Configs;
using TadbirTest.MainApp.Domain.UnitOfWork;

namespace TadbirTest.MainApp.Application.Base
{
    public class ApplicationBase
    {
        public IUnitOfWork UnitOfWork { get; set; }
        public IConnectionConfig ConnectionConfig { get; set; }
        public IMediator Mediator;

        public ApplicationBase(
            IConnectionConfig connectionConfig,
            IUnitOfWork unitOfWork,
            IMediator mediator)
        {
            ConnectionConfig = connectionConfig;
            UnitOfWork = unitOfWork;
            Mediator = mediator;
        }
    }
}
