namespace MarktguruApi.Controllers.Products
{
    using Asp.Versioning;
    using global::MediatR;
    using MediatR.Commands;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Models.Product.Dtos;

    [ApiController]
    [Route("api/[controller]")]
    [ApiVersion("1.0")]
    [Authorize]
    public class ProductsController(IMediator mediator) : ControllerBase
    {
        
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<ActionResult<ProductResponseDto>> CreateProduct(
            [FromBody] CreateProductDto createProductDto,
            CancellationToken cancellationToken = default)
        {
            var command = new CreateProductCommand(createProductDto, Guid.NewGuid());
            ProductResponseDto response = await mediator.Send(command, cancellationToken);
            return response == null ? BadRequest() : CreatedAtAction(nameof(CreateProduct), new { id = response.Id }, response);
        }
    }
}