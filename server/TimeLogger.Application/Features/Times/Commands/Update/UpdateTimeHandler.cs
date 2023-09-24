using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using TimeLogger.Application.Common.Exceptions.Common;
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
            // TODO filter project id
            var entityToUpdate = await _repository.GetSingle(command.Id, cancellationToken);

            if (!(entityToUpdate is { DateDeleted: null }))
            {
                throw new ItemNotFoundException(command.Id);
            }

            _mapper.Map(command, entityToUpdate);

            _repository.Update(entityToUpdate);

            await _unitOfWork.Commit(cancellationToken);

            return _mapper.Map<UpdateTimeResponse>(entityToUpdate);
        }
    }
}