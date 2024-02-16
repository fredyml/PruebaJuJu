using Business.Dtos;
using Business.Entities;
using Business.Interfaces;
using Business.Services;
using Moq;

namespace Test.Services
{
    public class CustomerServiceTests
    {
        private readonly Mock<IRepository<Customer>> _mockCustomerRepository;
        private readonly Mock<IRepository<Post>> _mockPostRepository;
        private readonly CustomerService _customerService;

        public CustomerServiceTests()
        {
            _mockCustomerRepository = new Mock<IRepository<Customer>>();
            _mockPostRepository = new Mock<IRepository<Post>>();
            _customerService = new CustomerService(_mockCustomerRepository.Object, _mockPostRepository.Object);
        }

        [Fact]
        public void Create_Customer_Success()
        {
            // Arrange
            var customerDTO = new CustomerDTO { CustomerId = 1, Name = "Test" };
            var customer = new Customer { CustomerId = 1, Name = "Test" };
            _mockCustomerRepository.Setup(r => r.GetAll()).Returns(new List<Customer>().AsQueryable());

            _mockCustomerRepository.Setup(r => r.GetAll()).Returns(new List<Customer>().AsQueryable());
            _mockCustomerRepository.Setup(r => r.Create(It.IsAny<Customer>())).Returns(customer); 
           

            // Act
            var result = _customerService.Create(customerDTO);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(customerDTO.Name, result.Name);
        }

        [Fact]
        public void Update_Customer_Success()
        {
            // Arrange
            var customerDTO = new CustomerDTO { CustomerId = 1, Name = "Test" };
            var customer = new Customer { CustomerId = 1, Name = "Test" };
            _mockCustomerRepository.Setup(r => r.GetAll()).Returns(new List<Customer> { new Customer { CustomerId = 1, Name = "Test" } }.AsQueryable());
            _mockCustomerRepository.Setup(r => r.Update(It.IsAny<Customer>())).Returns(customer);

            // Act
            var result = _customerService.Update(customerDTO);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(customerDTO.Name, result.Name);
        }
    }
}