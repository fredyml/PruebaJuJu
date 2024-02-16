using Business.Dtos;
using Business.Entities;
using Business.Interfaces;
using Business.Services;
using Moq;

namespace Test.Services
{
    public class PostServiceTests
    {
        private readonly Mock<IRepository<Post>> _mockPostRepository;
        private readonly Mock<IRepository<Customer>> _mockCustomerRepository;
        private readonly PostService _postService;

        public PostServiceTests()
        {
            _mockPostRepository = new Mock<IRepository<Post>>();
            _mockCustomerRepository = new Mock<IRepository<Customer>>();
            _postService = new PostService(_mockPostRepository.Object, _mockCustomerRepository.Object);
        }

        [Fact]
        public void Create_Post_Success()
        {
            // Arrange
            var postDTO = new PostDTO { PostId = 1, Title = "Test", Body = "Test Body", Type = 1, Category = "Test Category", CustomerId = 1 };
            _mockCustomerRepository.Setup(r => r.GetAll()).Returns(new List<Customer> { new Customer { CustomerId = 1, Name = "Test" } }.AsQueryable());
            _mockPostRepository.Setup(r => r.Create(It.IsAny<Post>())).Returns(new Post { PostId = 1, Title = "Test", Body = "Test Body", Type = 1, Category = "Test Category", CustomerId = 1 });

            // Act
            var result = _postService.Create(postDTO);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(postDTO.Title, result.Title);
        }

        [Fact]
        public void Update_Post_Success()
        {
            // Arrange
            var postDTO = new PostDTO { PostId = 1, Title = "Test", Body = "Test Body", Type = 1, Category = "Test Category", CustomerId = 1 };
            _mockPostRepository.Setup(r => r.GetAll()).Returns(new List<Post> { new Post { PostId = 1, Title = "Test", Body = "Test Body", Type = 1, Category = "Test Category", CustomerId = 1 } }.AsQueryable());
            _mockPostRepository.Setup(r => r.Update(It.IsAny<Post>())).Returns(new Post { PostId = 1, Title = "Test", Body = "Test Body", Type = 1, Category = "Test Category", CustomerId = 1 });

            // Act
            var result = _postService.Update(postDTO);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(postDTO.Title, result.Title);
        }
    }
}
