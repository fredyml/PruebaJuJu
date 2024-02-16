using Business.Dtos;
using Business.Entities;
using System.Threading.Tasks;

namespace Business.Interfaces
{
    public interface ICustomerService
    {
         Customer Create(CustomerDTO customerDTO);
         Customer Update(CustomerDTO customerDTO);
         Task DeleteByCustomer(int customerId);
    }
}
