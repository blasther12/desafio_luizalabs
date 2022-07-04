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
    public class CustomerController : ControllerBase
    {

        private readonly IApplicationServiceCustomer _applicationServiceCustomer;

        public CustomerController(IApplicationServiceCustomer applicationServiceCustomer)
        {
            this._applicationServiceCustomer = applicationServiceCustomer;
        }

        // GET: api/<CustomerController>
        [HttpGet]
        public async Task<IActionResult> GetAsync(int page = 1, int quantity = 10)
        {
            try
            {
                //Valida a quantidade de registros pro busca -> Atualmente o maximo é 50
                if (quantity > 50)
                    throw new TesteLuizaLabsExceptions(TesteLuizaLabsExceptions.Type.ExceededNumberOfData);

                return StatusCode((int)HttpStatusCode.OK, await this._applicationServiceCustomer.GetAllAsync(page, quantity));
            }catch (Exception ex)
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

        // GET api/<CustomerController>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetAsync(long id)
        {
            var customer = await this._applicationServiceCustomer.GetByIdAsync(id);
            return StatusCode(customer is not null ? (int)HttpStatusCode.OK : (int)HttpStatusCode.NotFound, customer);
        }

        // POST api/<CustomerController>
        [HttpPost]
        public async Task<IActionResult> PostAsync([FromBody] CustomerDTO value)
        {
            try
            {
                await this._applicationServiceCustomer.AddAsync(value);
                return Ok();
            }catch (Exception ex)
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

        // PUT api/<CustomerController>/5
        [HttpPut]
        public async Task<IActionResult> PutAsync([FromBody] CustomerDTO value)
        {
            try
            {
                await this._applicationServiceCustomer.UpdateAsync(value);
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

        // DELETE api/<CustomerController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(long id)
        {
            try
            {
                await this._applicationServiceCustomer.RemoveAsync(id);
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
