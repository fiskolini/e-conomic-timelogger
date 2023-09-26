using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using TimeLogger.Application.Common.Exceptions.Common;
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
            // Retrieve the customer entity to be updated based on the provided customer ID
            var entityToUpdate = await _repository.GetSingle(command.Id, cancellationToken);

            // If no customer entity is found, throw an ItemNotFoundException
            if (entityToUpdate == null)
                throw new ItemNotFoundException(command.Id);

            // Map the data from the UpdateCustomerCommand to the existing customer entity
            _mapper.Map(command, entityToUpdate);

            // Update the customer entity using the repository
            _repository.Update(entityToUpdate);

            await _unitOfWork.Commit(cancellationToken);

            // Map the updated customer entity to an UpdateCustomerResponse object
            var response = _mapper.Map<UpdateCustomerResponse>(entityToUpdate);

            return response;
        }
    }
}