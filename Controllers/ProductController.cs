using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using storehouse_backend.Models;
using storehouse_backend.Services;

namespace storehouse_backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly ProductService productService;
        public ProductController(ProductService _productService)
        {
            this.productService = _productService;
        }
        [HttpGet]
        public async Task<List<Product>> Get() => await productService.GetAsync();
        [HttpGet("{id:length(24)}")]
        public async Task<ActionResult<Product>> Get(string Id)
        {
            var product = await productService.GetAsync(Id);
            if (product is null)
            {
                return NotFound();
            }
            return product;
        }
        [HttpPost]
        public async Task<IActionResult> Post(Product product)
        {
            await productService.CreateAsync(product);
            return NoContent();
        }
        [HttpPut("{Id:length(24)}")]
        public async Task<IActionResult> Update(string Id, Product uProduct)
        {
            var product = await productService.GetAsync(Id);
            if (product is null)
            {
                return NotFound();
            }
            uProduct.Id = product.Id;
            await productService.UpdateAsync(Id, uProduct);
            return NoContent();
        }
        [HttpDelete("{Id:length(24)}")]
        public async Task<IActionResult> Delete(string Id)
        {
            var product = await productService.GetAsync(Id);
            if (product is null)
            {
                return NotFound();
            }
            await productService.RemoveAsync(Id);
            return NoContent();
        }
    }
}
