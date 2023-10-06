using Business.Abstract;
using Business.Validation.FluentValidation;
using Core.Aspects;
using Core.Entity.Concrete;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entity.Dtos.User;

namespace Business.Concrete
{
    public class UserManager : IUserService
    {
        private IUserDal _userDal;

        public UserManager(IUserDal userDal)
        {
            _userDal = userDal;
        }

        public void Add(User user)
        {
            _userDal.Add(user);
        }

        public User GetByEmail(string email)
        {
            return _userDal.Get(user => user.Email == email);
        }

        public IDataResult<UserForGetDto> GetById(Guid userId)
        {
            User entity = _userDal.Get(u => u.Id == userId);
            UserForGetDto responseModel = new()
            {
                Id = userId,
                Email = entity.Email,
                FirstName = entity.FirstName,
                LastName = entity.LastName,
                Status = entity.Status
            };
            return new SuccessDataResult<UserForGetDto>(responseModel);
        }

        public List<OperationClaim> GetClaims(User user)
        {
            return _userDal.GetClaims(user);
        }

        /*public void Update(User user)
        {
            _userDal.Update(user);//temp
        }*/


    }
}
