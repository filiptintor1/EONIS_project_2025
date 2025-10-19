using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Mvc;
using WebShopProject.Application.Products.Commands.CreateProduct;
using WebShopProject.Application.Products.Commands.DeleteProduct;
using WebShopProject.Application.Products.Commands.UpdateProduct;
using WebShopProject.Application.Products.Dto;
using WebShopProject.Application.Products.Queries.GetAllProducts;
using WebShopProject.Application.Products.Queries.GetProductById;
using WebShopProject.Application.Products.Queries.GetProductsByCategory;

namespace WebShopProject.API.Controllers
{
    [ApiController]
    [Route("/products")]
    public class ProductsController(IMediator mediator) : ControllerBase
    {
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<ProductDto>>> GetAll([FromQuery] GetAllProductsQuery query)
        {
            var p = await mediator.Send(query);
            return Ok(p);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ProductDto>> GetById(Guid id)
        {
            var p = await mediator.Send(new GetProductByIdQuery(id));

            return Ok(p);
        }

        [HttpPost]
        public async Task<IActionResult> CreateProduct(CreateProductCommand command)
        {

            Guid productId = await mediator.Send(command);
            return CreatedAtAction(nameof(GetById), new { id = productId }, null);
        }

        [HttpGet("kind/{kind}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<IEnumerable<ProductDto>>> GetByKind(string kind)
        {
            var products = await mediator.Send(new GetProductsByKindQuery(kind));
            if (products == null || !products.Any())
            {
                return NotFound();
            }
            return Ok(products);
        }

        [HttpGet("category/{category}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<IEnumerable<ProductDto>>> GetByCategory(string category)
        {
            var products = await mediator.Send(new GetProductsByCategoryQuery(category));
            if (products == null || !products.Any())
            {
                return NotFound();
            }
            return Ok(products);
        }

        [HttpDelete("{productId}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteProduct(Guid productId)
        {
            await mediator.Send(new DeleteProductCommand(productId));
            return NoContent();
        }

        [HttpPatch("{productId}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> UpdateProduct(Guid productId, UpdateProductCommand command)
        {
            command.ProductId = productId;
            await mediator.Send(command);
            return NoContent();

        }  
    }
}
