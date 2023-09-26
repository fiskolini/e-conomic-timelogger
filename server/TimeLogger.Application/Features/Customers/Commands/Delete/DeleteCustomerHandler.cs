using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using TimeLogger.Application.Common.Exceptions.Common;
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
            // Retrieve the customer entity to be deleted based on the provided customer ID
            var entityToDelete = await _repository.GetSingle(command.Id, cancellationToken);

            // If no customer entity is found, throw an ItemNotFoundException
            if (entityToDelete == null)
                throw new ItemNotFoundException(command.Id);

            // Soft delete the customer entity using the repository
            _repository.SoftDelete(entityToDelete);

            // Commit the unit of work to save the changes made during the soft delete
            await _unitOfWork.Commit(cancellationToken);

            // Map the deleted customer entity to a DeleteCustomerResponse object
            var response = _mapper.Map<DeleteCustomerResponse>(entityToDelete);

            return response;
        }
    }
}