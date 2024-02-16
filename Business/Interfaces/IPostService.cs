using Business.Dtos;
using Business.Entities;

namespace Business.Interfaces
{
    public interface IPostService
    {
        Post Create(PostDTO postDTO);
        Post Update(PostDTO postDTO);
    }
}
