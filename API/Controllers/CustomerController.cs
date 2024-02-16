using Business.Dtos;
using Business.Entities;
using Business.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;

namespace API.Controllers.CustomerController
{
    [Route("[controller]")]
    public class CustomerController : ControllerBase
    {
        private readonly ICustomerService _customerService;
        private IRepository<Customer> _repositoryCustomerService;

        public CustomerController(IRepository<Customer> repositoryCustomerService, ICustomerService customerService)
        {
            _customerService = customerService;
            _repositoryCustomerService = repositoryCustomerService;
        }


        [HttpGet()]
        public IQueryable<Customer> GetAll()
        {
            return _repositoryCustomerService.GetAll();
        }

        [HttpPost()]
        public IActionResult Create([FromBody] CustomerDTO customerDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var createdEntity = _customerService.Create(customerDTO);
            return Ok(createdEntity);
        }

        [HttpPut()]
        public IActionResult Update([FromBody] CustomerDTO customerDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var updatedEntity = _customerService.Update(customerDTO);

            return Ok(updatedEntity);
        }


        [HttpDelete()]
        public async Task<IActionResult> DeleteAsync([FromBody] CustomerDTO customerDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Customer entity = new Customer()
            {
                CustomerId = customerDTO.CustomerId,
                Name = customerDTO.Name
            };

            await _customerService.DeleteByCustomer(entity.CustomerId);

            await _repositoryCustomerService.DeleteAsync(entity);
            return Ok("Registro eliminado de manera exitosa");
        }
    }
}
