namespace MarktguruApi.Controllers.Products
{
    using Asp.Versioning;
    using global::MediatR;
    using MediatR.Commands;
    using MediatR.Queries;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Models.Product.Dtos;
    using Utils;

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
        
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [AllowAnonymous]
        [ResponseCache(Duration = 10, Location = ResponseCacheLocation.Any, NoStore = false)]
        public async Task<ActionResult<List<ProductResponseDto>>> GetAllProducts(CancellationToken cancellationToken = default)
        {
            var query = new GetAllProductsQuery();
            List<ProductReducedDto> response = await mediator.Send(query, cancellationToken);
            return Ok(response);
        }
        
        [HttpGet("paginated")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [AllowAnonymous]
        public async Task<ActionResult<PaginatedList<ProductReducedDto>>> GetPaginatedProducts(
            [FromQuery] int page = 1,
            [FromQuery] int pageSize = 10,
            CancellationToken cancellationToken = default)
        {
            var query = new GetPaginatedProductsQuery(page, pageSize);
            PaginatedList<ProductReducedDto> response = await mediator.Send(query, cancellationToken);
            return Ok(response);
        }
    }
}