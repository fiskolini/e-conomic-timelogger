using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using TimeLogger.Domain.Entities;
using TimeLogger.Domain.Repositories;
using TimeLogger.Domain.Repositories.Common;

namespace TimeLogger.Application.Features.Times.Commands.Create
{
    public class CreateTimeHandler : IRequestHandler<CreateTimeCommand, CreateTimeResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ITimeRepository _repository;
        private readonly IMapper _mapper;

        public CreateTimeHandler(IUnitOfWork unitOfWork, ITimeRepository repository, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<CreateTimeResponse> Handle(CreateTimeCommand command,
            CancellationToken cancellationToken)
        {
            var time = _mapper.Map<Time>(command);
            _repository.Create(time);
            await _unitOfWork.Commit(cancellationToken);

            return _mapper.Map<CreateTimeResponse>(time);
        }
    }
}