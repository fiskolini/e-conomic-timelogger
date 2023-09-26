using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using TimeLogger.Application.Common.Exceptions.Common;
using TimeLogger.Domain.Entities;
using TimeLogger.Domain.Repositories;
using TimeLogger.Domain.Repositories.Common;

namespace TimeLogger.Application.Features.Times.Commands.Update
{
    public class UpdateTimeHandler : IRequestHandler<UpdateTimeCommand, UpdateTimeResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ITimeRepository _repository;
        private readonly IMapper _mapper;

        public UpdateTimeHandler(IUnitOfWork unitOfWork, ITimeRepository repository, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<UpdateTimeResponse> Handle(UpdateTimeCommand command,
            CancellationToken cancellationToken)
        {
            // Retrieve the time entity by ID
            var entityToUpdate = await _repository.GetSingle(command.Id, cancellationToken);

            // Throw an exception if the time entity is not found
            if (entityToUpdate == null)
                throw new ItemNotFoundException(command.Id);

            UpdateEntity(ref entityToUpdate, command);

            // Update the entity in the repository
            _repository.Update(entityToUpdate);

            await _unitOfWork.Commit(cancellationToken);

            return _mapper.Map<UpdateTimeResponse>(entityToUpdate);
        }

        /// <summary>
        /// Partially update entity with the given Project Request
        /// </summary>
        private static void UpdateEntity(ref Time entityToUpdate, UpdateTimeCommand command)
        {
            entityToUpdate.Minutes = command.Minutes;
        }
    }
}