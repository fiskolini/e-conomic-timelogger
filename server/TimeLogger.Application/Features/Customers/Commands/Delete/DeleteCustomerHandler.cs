using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using TimeLogger.Application.Common.Exceptions.Common;
using TimeLogger.Application.Features.Customers.Commands.Create;
using TimeLogger.Domain.Entities;
using TimeLogger.Domain.Repositories;
using TimeLogger.Domain.Repositories.Common;

namespace TimeLogger.Application.Features.Customers.Commands.Delete
{
    public class DeleteCustomerHandler : IRequestHandler<DeleteCustomerCommand, DeleteCustomerResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ICustomerRepository _repository;
        private readonly IMapper _mapper;

        public DeleteCustomerHandler(IUnitOfWork unitOfWork, ICustomerRepository customerRepository, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _repository = customerRepository;
            _mapper = mapper;
        }

        public async Task<DeleteCustomerResponse> Handle(DeleteCustomerCommand command,
            CancellationToken cancellationToken)
        {
            var entityToDelete = await _repository.GetSingle(command.Id, cancellationToken);

            if (!(entityToDelete is { DateDeleted: null }))
            {
                throw new ItemNotFoundException(command.Id);
            }
            
            _repository.SoftDelete(entityToDelete);
            await _unitOfWork.Commit(cancellationToken);

            return _mapper.Map<DeleteCustomerResponse>(entityToDelete);
        }
    }
}