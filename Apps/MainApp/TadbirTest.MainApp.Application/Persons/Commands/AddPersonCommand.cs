using MediatR;
using System.Threading;
using System.Threading.Tasks;
using TadbirTest.MainApp.Application.Base;
using TadbirTest.MainApp.Domain.Configs;
using TadbirTest.MainApp.Domain.Entities.Persons;
using TadbirTest.MainApp.Domain.UnitOfWork;

namespace TadbirTest.MainApp.Application.Persons.Commands
{
    public class AddPersonCommand : IRequest<bool>
    {
        public AddPersonCommand(Person model)
        {
            Model = model;
        }

        public Person Model { get; }

        public class AddPersonCommandHandler : ApplicationBase, IRequestHandler<AddPersonCommand, bool>
        {
            public AddPersonCommandHandler(
                IConnectionConfig connectionConfig,
                IUnitOfWork unitOfWork,
                IMediator mediator) : 
                base(connectionConfig, unitOfWork, mediator)
            {
            }

            public async Task<bool> Handle(AddPersonCommand request, CancellationToken cancellationToken)
            {
                await UnitOfWork.PersonRepository.Insert(request.Model);
                return true;
            }
        }
    }
}
