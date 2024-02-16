using Business.Entities;
using Business.Interfaces;
using System.Linq;
using System;
using Business.Dtos;

namespace Business.Services
{
    public class PostService : IPostService
    {
        private readonly IRepository<Post> _repository;
        private readonly IRepository<Customer> _customerRepository;

        public PostService(IRepository<Post> repository, IRepository<Customer> customerRepository)
        {
            _repository = repository;
            _customerRepository = customerRepository;
        }

        public Post Create(PostDTO postDTO)
        {
            Post entity = new Post()
            {
                PostId = postDTO.PostId,
                Title = postDTO.Title,
                Body = postDTO.Body,
                Type = postDTO.Type,
                Category = postDTO.Category,
                CustomerId = postDTO.CustomerId
            };

            entity.Body = ValidateNameLength(entity.Body);
            entity.Category = AssignCategory(entity.Type, entity.Category);

            if (!_customerRepository.GetAll().Any(e => e.CustomerId == entity.CustomerId))
            {
                throw new ArgumentException($"El cliente con id {entity.CustomerId} no existe");
            }

            return _repository.Create(entity);
        }

        public Post Update(PostDTO postDTO)
        {
            Post entity = new Post()
            {
                PostId = postDTO.PostId,
                Title = postDTO.Title,
                Body = postDTO.Body,
                Type = postDTO.Type,
                Category = postDTO.Category,
                CustomerId = postDTO.CustomerId
            };

            if (!_repository.GetAll().Any(e => e.PostId == entity.PostId))
            {
                throw new ArgumentException($"Registro con id {entity.PostId} no existe");
            }

            return _repository.Update(entity);
        }


        private string ValidateNameLength(string name)
        {
            if (name != null && name.Length > 20)
            {
                return name.Length <= 97 ? name : name.Substring(0, 97) + "...";
            }

            return name;
        }

        private string AssignCategory(int type, string category)
        {
            switch (type)
            {
                case 1:
                    return Constants.Category1;
                case 2:
                    return Constants.Category2;
                case 3:
                    return Constants.Category3;
                default:
                    return category;
            }
        }
    }
}
