using Microsoft.AspNetCore.Mvc;
using MediatR;
using CleanArchitectrure.Application.UseCases.Customers.Queries.GetAllCustomerQuery;
using CleanArchitectrure.Application.UseCases.Customers.Queries.GetByIdCustomerQuery;
using CleanArchitectrure.Application.UseCases.Customers.Queries.GetAllWithPaginationCustomerQuery;
using CleanArchitectrure.Application.UseCases.Customers.Commands.CreateCustomerCommand;
using CleanArchitectrure.Application.UseCases.Customers.Commands.UpdateCustomerCommand;
using CleanArchitectrure.Application.UseCases.Customers.Commands.DeleteCustomerCommand;
using CleanArchitectrure.Domain.Events;

namespace CleanArchitectrure.WebApi.Controllers.V1
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController(IMediator mediator) : ControllerBase
    {
        private readonly IMediator _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));

        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAllAsync()
        {
            var response = await _mediator.Send(new GetAllCustomerQuery());
            if (response.Succcess)
            {
                return Ok(response);
            }

            return BadRequest(response);
        }

        [HttpGet("Get")]
        public async Task<IActionResult> GetAsync([FromQuery ]string customerId)
        {
            var response = await _mediator.Send(new GetByIdCustomerQuery() { CustomerId = customerId });
            if (response.Succcess)
            {
                return Ok(response);
            }

            return BadRequest(response);
        }

        [HttpGet("GetAllWithPagination")]
        public async Task<IActionResult> GetAllWithPaginationAsync([FromQuery] GetAllWithPaginationCustomerQuery query)
        {
            var response = await _mediator.Send(query);
            if (response.Succcess)
            {
                return Ok(response);
            }

            return BadRequest(response);
        }

        [HttpPost("Insert")]
        public async Task<ActionResult> InsertAsync([FromBody] CreateCustomerCommand command)
        {
            if (command is null) return BadRequest();

            var response = await _mediator.Send(command);
            
            if (response.Succcess)
            {
                await _mediator.Publish(new CustomerCreatedEvent
                {
                    ContactName = command.ContactName,
                });
                return Ok(response);
            }

            return BadRequest(response);
        }

        [HttpPut("Update")]
        public async Task<ActionResult> UpdateAsync([FromBody] UpdateCustomerCommand command)
        {
            if (command is null) return BadRequest();

            var response = await _mediator.Send(command);

            if (response.Succcess)
            {
                return Ok(response);
            }

            return BadRequest(response);
        }

        [HttpPut("Delete")]
        public async Task<ActionResult> DeleteAsync([FromQuery] DeleteCustomerCommand command)
        {
            if (command is null) return BadRequest();

            var response = await _mediator.Send(command);

            if (response.Succcess)
            {
                return Ok(response);
            }

            return BadRequest(response);
        }
    }
}
