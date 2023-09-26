using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using TimeLogger.Domain.Entities;
using TimeLogger.Domain.Repositories;
using TimeLogger.Domain.Repositories.Common;

namespace TimeLogger.Application.Features.Customers.Commands.Create
{
    public class CreateCustomerHandler : IRequestHandler<CreateCustomerCommand, CreateCustomerResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ICustomerRepository _repository;
        private readonly IMapper _mapper;

        public CreateCustomerHandler(IUnitOfWork unitOfWork, ICustomerRepository customerRepository, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _repository = customerRepository;
            _mapper = mapper;
        }

        public async Task<CreateCustomerResponse> Handle(CreateCustomerCommand command,
            CancellationToken cancellationToken)
        {
            // Map the data from the CreateCustomerCommand to a Customer entity
            var customer = _mapper.Map<Customer>(command);

            // Create the customer entity in the repository
            _repository.Create(customer);

            await _unitOfWork.Commit(cancellationToken);

            // Map the created customer entity to a CreateCustomerResponse object
            var response = _mapper.Map<CreateCustomerResponse>(customer);

            return response;
        }
    }
}