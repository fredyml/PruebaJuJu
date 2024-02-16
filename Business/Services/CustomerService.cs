using Business.Dtos;
using Business.Entities;
using Business.Interfaces;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Business.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly IRepository<Customer> _repository;
        private readonly IRepository<Post> _postRepository;

        public CustomerService(IRepository<Customer> repository, IRepository<Post> postRepository)
        {
            _repository = repository;
            _postRepository = postRepository;
        }

        public Customer Create(CustomerDTO customerDTO)
        {
            Customer entity = new Customer()
            {
                CustomerId = customerDTO.CustomerId,
                Name = customerDTO.Name
            };

            if (_repository.GetAll().Any(e => e.Name == entity.Name))
            {
                throw new ArgumentException("Ya existe un cliente con el mismo nombre.");
            }

            return _repository.Create(entity);
        }

        public Customer Update(CustomerDTO customerDTO)
        {
            Customer entity = new Customer()
            {
                CustomerId = customerDTO.CustomerId,
                Name = customerDTO.Name
            };

            if (!_repository.GetAll().Any(e => e.CustomerId == entity.CustomerId))
            {
                throw new ArgumentException($"Registro con id {entity.CustomerId} no existe");
            }

            return _repository.Update(entity);
        }

        public async Task DeleteByCustomer(int customerId)
        {
            var posts = _postRepository.GetAll().Where(p => p.CustomerId == customerId);

            foreach (var post in posts)
            {
                await _postRepository.DeleteAsync(post);
            }
        }
    }
}
