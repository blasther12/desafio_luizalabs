using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using TesteLuizaLabs.Application.DTO.DTO;
using TesteLuizaLabs.Application.Exceptions;
using TesteLuizaLabs.Application.Interfaces;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TesteLuizaLabs.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Policy = "RequireAdminRole")]
    public class CustomerProductController : ControllerBase
    {
        private readonly IApplicationServiceCustomerProduct _applicationServiceCustomerProduct;

        public CustomerProductController(IApplicationServiceCustomerProduct applicationServiceCustomerProduct)
        {
            this._applicationServiceCustomerProduct = applicationServiceCustomerProduct;
        }

        // GET: api/<CustomerProductController>
        [HttpGet]
        public async Task<IActionResult> GetAsync(long customerId = 0, int page = 1, int quantity = 10)
        {
            try
            {
                //Valida a quantidade de registros pro busca -> Atualmente o maximo é 50
                if (quantity > 50)
                    throw new TesteLuizaLabsExceptions(TesteLuizaLabsExceptions.Type.ExceededNumberOfData);

                return StatusCode((int)HttpStatusCode.OK, await this._applicationServiceCustomerProduct.GetAllAsync(customerId, page, quantity));
            }
            catch (Exception ex)
            {
                if (ex is TesteLuizaLabsExceptions e)
                {
                    return StatusCode(e.statusCode, new { type = e.type, message = e.Message });
                }
                else
                {
                    return StatusCode((int)HttpStatusCode.InternalServerError,
                        new { type = TesteLuizaLabsExceptions.Type.Error, message = new TesteLuizaLabsExceptions(TesteLuizaLabsExceptions.Type.Error).Message });
                }
            }
        }

        // GET api/<CustomerProductController>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetAsync(long id)
        {
            var customerProduct = await this._applicationServiceCustomerProduct.GetByIdAsync(id);
            return StatusCode(customerProduct is not null ? (int)HttpStatusCode.OK : (int)HttpStatusCode.NotFound, customerProduct);
        }

        // POST api/<CustomerProductController>
        [HttpPost]
        public async Task<IActionResult> PostAsync([FromBody] ProductPostDTO value)
        {
            try
            {
                await this._applicationServiceCustomerProduct.AddAsync(value);
                return StatusCode((int)HttpStatusCode.Created);
            }
            catch (Exception ex)
            {
                if (ex is TesteLuizaLabsExceptions e)
                {
                    return StatusCode(e.statusCode, new { type = e.type, message = e.Message });
                }
                else
                {
                    return StatusCode((int)HttpStatusCode.InternalServerError,
                        new { type = TesteLuizaLabsExceptions.Type.Error, message = new TesteLuizaLabsExceptions(TesteLuizaLabsExceptions.Type.Error).Message });
                }
            }
        }

        // DELETE api/<CustomerProductController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(long id)
        {
            try
            {
                await this._applicationServiceCustomerProduct.RemoveAsync(id);
                return Ok();
            }
            catch (Exception ex)
            {
                if (ex is TesteLuizaLabsExceptions e)
                {
                    return StatusCode(e.statusCode, new { type = e.type, message = e.Message });
                }
                else
                {
                    return StatusCode((int)HttpStatusCode.InternalServerError,
                        new { type = TesteLuizaLabsExceptions.Type.Error, message = new TesteLuizaLabsExceptions(TesteLuizaLabsExceptions.Type.Error).Message });
                }
            }
        }
    }
}
