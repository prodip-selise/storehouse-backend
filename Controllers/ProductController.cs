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
        [HttpGet("list")]
        public async Task<ActionResult<Product[]>> Get() {
            try
            {
                var products = await productService.GetAsync();
                return StatusCode(200, products);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return StatusCode(500, new ResponseObject(false, "Internal Server Error", e));
            }
        }
        [HttpGet("{id:length(24)}")]
        public async Task<ActionResult<Product>> GetById(string Id)
        {
            try
            {
                var product = await productService.GetAsync(Id);
                if (product is null)
                {
                    return StatusCode(404, new ResponseObject(false, "Id not found"));
                }
                return StatusCode(200, product);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return StatusCode(500, new ResponseObject(false, "Internal Server Error", e));
            }
        }
        [HttpPost("create")]
        public async Task<IActionResult> Post(Product product)
        {
            try
            {
                await productService.CreateAsync(product);
                return StatusCode(201, new ResponseObject(true, "Inserted")) ;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return StatusCode(500, new ResponseObject(false, "Internal Server Error", e));
            }
        }
        [HttpPut("update/{Id:length(24)}")]
        public async Task<IActionResult> Update(string Id, Product uProduct)
        {
            try
            {
                var product = await productService.GetAsync(Id);
                if (product is null)
                {
                    return StatusCode(404, new ResponseObject(false, "Id not found"));
                }
                uProduct.Id = product.Id;
                await productService.UpdateAsync(Id, uProduct);
                return StatusCode(205, new ResponseObject(true, "Updated"));
            }
            catch (Exception e)
            {
                return StatusCode(500, new ResponseObject(false, "Internal Server Error", e));
            }
        }
        [HttpDelete("delete/{Id:length(24)}")]
        public async Task<IActionResult> Delete(string Id)
        {
            var product = await productService.GetAsync(Id);
            if (product is null)
            {
                return StatusCode(204, new ResponseObject(true, "Already deleted or Id not found"));
            }
            await productService.RemoveAsync(Id);
            return StatusCode(200, new ResponseObject(true, "Deleted"));
        }
    }
}
