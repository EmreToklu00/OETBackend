using Core.Entity.Concrete;
using Core.Utilities.Results;
using Entity.Dtos.User;

namespace Business.Abstract
{
    public interface IUserService
    {
        List<OperationClaim> GetClaims(User user);

        void Add(User user);
        //void Update(User user);//temp

        User GetByEmail(string email);

        IDataResult<UserForGetDto> GetById(Guid userId);
    }
}
