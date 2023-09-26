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
        private readonly ITimeRepository _repository;
        private readonly IMapper _mapper;

        public DeleteTimeHandler(ITimeRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<DeleteTimeResponse> Handle(DeleteTimeCommand command,
            CancellationToken cancellationToken)
        {
            var entityToDelete = await _repository.GetSingle(command.Id, cancellationToken);

            if (entityToDelete == null)
                throw new ItemNotFoundException(command.Id);

            _repository.SoftDelete(entityToDelete);
            
            return _mapper.Map<DeleteTimeResponse>(entityToDelete);
        }
    }
}