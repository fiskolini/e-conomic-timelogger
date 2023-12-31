﻿using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TimeLogger.Application.Features.Customers.Commands.Create;
using TimeLogger.Application.Features.Customers.Commands.Delete;
using TimeLogger.Application.Features.Customers.Commands.Update;
using TimeLogger.Application.Features.Customers.Queries.Get;
using TimeLogger.Application.Features.Customers.Queries.GetById;
using TimeLogger.Domain.Entities;

namespace TimeLogger.Api.Controllers
{
    [Route("api/[controller]")]
    public class CustomersController : Controller
    {
        private readonly IMediator _mediator;

        public CustomersController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<PagedResults<GetCustomersResponse>>> GetAll(GetCustomersCommand pagedCommand,
            CancellationToken cancellationToken)
        {
            var response = await _mediator.Send(pagedCommand, cancellationToken);
            return Ok(response);
        }

        [HttpGet("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<ActionResult<GetCustomersResponse>> GetSingle(
            int id,
            GetSingleCustomerCommand customerCommand,
            CancellationToken cancellationToken)
        {
            customerCommand.Id = id;

            var response = await _mediator.Send(customerCommand, cancellationToken);
            return Ok(response);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<CreateCustomerResponse>> Create(
            [FromBody] CreateCustomerCommand command,
            CancellationToken cancellationToken)
        {
            var response = await _mediator.Send(command, cancellationToken);
            return CreatedAtAction(nameof(GetSingle), new { id = response.Id }, response);
        }

        [HttpPatch("{id:int}")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<UpdateCustomerResponse>> Update(
            int id,
            [FromBody] UpdateCustomerCommand command,
            CancellationToken cancellationToken)
        {
            command.Id = id;

            await _mediator.Send(command, cancellationToken);
            return NoContent();
        }

        [HttpDelete("{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<NoContentResult> Delete(
            int id,
            DeleteCustomerCommand request,
            CancellationToken cancellationToken)
        {
            request.Id = id;
            
            await _mediator.Send(request, cancellationToken);
            return NoContent();
        }
    }
}