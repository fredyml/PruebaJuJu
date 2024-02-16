using Business.Dtos;
using Business.Entities;
using Business.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace API.Controllers.PostController
{
    [Route("[controller]")]
    public class PostController : ControllerBase
    {
        private readonly IRepository<Post> _repositoryPostService;
        private readonly IPostService _postService;

        public PostController(IRepository<Post> repositoryPostService, IPostService postService)
        {
            _repositoryPostService = repositoryPostService;
            _postService = postService;
        }

        [HttpGet()]
        public IQueryable<PostDTO> GetAll()
        {
            return _repositoryPostService.GetAll().Select(post => new PostDTO
            {
                PostId = post.PostId,
                Title = post.Title,
                Body = post.Body,
                Type = post.Type,
                Category = post.Category,
                CustomerId = post.CustomerId
            });
        }


        [HttpPost()]
        public IActionResult Create([FromBody] PostDTO postDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var createdEntity = _postService.Create(postDto);
            return Ok(createdEntity);
        }

        [HttpPost("CreateMultiple")]
        public IActionResult CreateMultiple([FromBody] List<PostDTO> postDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

             var createdEntities = new List<Post>();
            foreach (var entity in postDto)
            {
                var createdEntity = _postService.Create(entity);
                createdEntities.Add(createdEntity);
            }

            return Ok(createdEntities);
        }


        [HttpPut()]
        public IActionResult Update([FromBody] PostDTO postDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var updatedEntity = _postService.Update(postDto);

            return Ok(updatedEntity);
        }

        [HttpDelete()]
        public IActionResult Delete([FromBody] PostDTO postDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Post entity = new Post()
            {
                PostId = postDTO.PostId,
                Title = postDTO.Title,
                Body = postDTO.Body,
                Type = postDTO.Type,
                Category = postDTO.Category,
                CustomerId = postDTO.CustomerId
            };

            var deletedEntity = _repositoryPostService.DeleteAsync(entity);
            return Ok(deletedEntity);
        }
    }
}
