using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using TimeLogger.Application.Common.Exceptions.Common;
using TimeLogger.Domain.Entities;
using TimeLogger.Domain.Repositories;
using TimeLogger.Domain.Repositories.Common;

namespace TimeLogger.Application.Features.Customers.Commands.Update
{
    public class UpdateCustomerHandler : IRequestHandler<UpdateCustomerCommand, UpdateCustomerResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ICustomerRepository _repository;
        private readonly IMapper _mapper;

        public UpdateCustomerHandler(IUnitOfWork unitOfWork, ICustomerRepository customerRepository, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _repository = customerRepository;
            _mapper = mapper;
        }

        public async Task<UpdateCustomerResponse> Handle(UpdateCustomerCommand command,
            CancellationToken cancellationToken)
        {
            var entityToUpdate = await _repository.GetSingle(command.Id, cancellationToken);

            if (!(entityToUpdate is { DateDeleted: null }))
            {
                throw new ItemNotFoundException(command.Id);
            }

            _mapper.Map(command, entityToUpdate);

            _repository.Update(entityToUpdate);

            await _unitOfWork.Commit(cancellationToken);

            return _mapper.Map<UpdateCustomerResponse>(entityToUpdate);
        }
    }
}