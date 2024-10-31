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

    /// <summary>
    /// Controller for managing products.
    /// </summary>
    /// <param name="mediator">The mediator instance for handling commands and queries.</param>
    [ApiController]
    [Route("api/[controller]")]
    [ApiVersion("1.0")]
    [Authorize]
    public class ProductsController(IMediator mediator) : ControllerBase
    {
        /// <summary>
        /// Creates a new product.
        /// </summary>
        /// <param name="createProductDto">The data transfer object containing the product details.</param>
        /// <param name="cancellationToken">Token to cancel the operation.</param>
        /// <returns>The created product response.</returns>
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

        /// <summary>
        /// Retrieves all products.
        /// </summary>
        /// <param name="cancellationToken">Token to cancel the operation.</param>
        /// <returns>A list of all products.</returns>
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

        /// <summary>
        /// Retrieves a paginated list of products.
        /// </summary>
        /// <param name="page">The page number to retrieve. Defaults to 1.</param>
        /// <param name="pageSize">The number of items per page. Defaults to 10.</param>
        /// <param name="cancellationToken">Token to cancel the operation.</param>
        /// <returns>A paginated list of products.</returns>
        [HttpGet("paginated")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [AllowAnonymous]
        [ResponseCache(Duration = 10, Location = ResponseCacheLocation.Any, NoStore = false)]
        public async Task<ActionResult<PaginatedList<ProductReducedDto>>> GetPaginatedProducts(
            [FromQuery] int page = 1,
            [FromQuery] int pageSize = 10,
            CancellationToken cancellationToken = default)
        {
            var query = new GetPaginatedProductsQuery(page, pageSize);
            PaginatedList<ProductReducedDto> response = await mediator.Send(query, cancellationToken);
            return Ok(response);
        }

        /// <summary>
        /// Retrieves a product by its unique identifier.
        /// </summary>
        /// <param name="id">The unique identifier of the product.</param>
        /// <param name="cancellationToken">Token to cancel the operation.</param>
        /// <returns>The product response if found; otherwise, a 404 Not Found response.</returns>
        [HttpGet("{id:guid}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [AllowAnonymous]
        [ResponseCache(Duration = 10, Location = ResponseCacheLocation.Any, NoStore = false)]
        public async Task<ActionResult<ProductResponseDto>> GetProductById(
            Guid id,
            CancellationToken cancellationToken = default)
        {
            var query = new GetProductByIdQuery(id);
            ProductResponseDto product = await mediator.Send(query, cancellationToken);
            return product == null ? NotFound() : Ok(product);
        }

        /// <summary>
        /// Updates an existing product.
        /// </summary>
        /// <param name="id">The unique identifier of the product to update.</param>
        /// <param name="updateProductDto">The data transfer object containing the updated product details.</param>
        /// <param name="cancellationToken">Token to cancel the operation.</param>
        /// <returns>The updated product response.</returns>
        [HttpPut("{id:guid}")]
        public async Task<ActionResult<ProductResponseDto>> UpdateProduct(
            Guid id,
            [FromBody] UpdateProductDto updateProductDto,
            CancellationToken cancellationToken = default)
        {
            var command = new UpdateProductCommand(id, updateProductDto);
            ProductResponseDto response = await mediator.Send(command, cancellationToken);
            return response == null ? BadRequest() : Ok(response);
        }
    }
}