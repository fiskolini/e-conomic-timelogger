using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using TimeLogger.Application.Common.Exceptions.Common;
using TimeLogger.Domain.Repositories;
using TimeLogger.Domain.Repositories.Common;

namespace TimeLogger.Application.Features.Times.Commands.Delete
{
    public class DeleteTimeHandler : IRequestHandler<DeleteTimeCommand, DeleteTimeResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ITimeRepository _repository;
        private readonly IMapper _mapper;

        public DeleteTimeHandler(IUnitOfWork unitOfWork, ITimeRepository repository, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<DeleteTimeResponse> Handle(DeleteTimeCommand command,
            CancellationToken cancellationToken)
        {
            // TODO filter by project id
            var entityToDelete = await _repository.GetSingle(command.Id, cancellationToken);

            if (!(entityToDelete is { DateDeleted: null }))
            {
                throw new ItemNotFoundException(command.Id);
            }
            
            _repository.SoftDelete(entityToDelete);
            await _unitOfWork.Commit(cancellationToken);

            return _mapper.Map<DeleteTimeResponse>(entityToDelete);
        }
    }
}