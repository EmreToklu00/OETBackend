using Business.Abstract;
using Business.Constants.Results;
using Business.Validation.FluentValidation;
using Core.Aspects.Autofac.Caching;
using Core.Aspects.Autofac.Performance;
using Core.Aspects.Autofac.Transaction;
using Core.Aspects.Autofac.Validation;
using Core.Entity.Concrete;
using Core.Utilities.Results;
using Core.Utilities.Security.Hashing;
using Core.Utilities.Security.Jwt;
using Entity.Dtos.User;

namespace Business.Concrete
{
    public class AuthManager : IAuthService
    {
        private IUserService _userService;
        private ITokenHelper _tokenHelper;

        public AuthManager(IUserService userService, ITokenHelper tokenHelper)
        {
            _userService = userService;
            _tokenHelper = tokenHelper;
        }


        //[PerformanceAspect(interval: 5)]
        public IDataResult<User> Login(UserForLoginDto userForLoginDto)
        {
            //Thread.Sleep(millisecondsTimeout: 5000); //performance test
            var userToCheck = _userService.GetByEmail(userForLoginDto.Email);
            if (userToCheck == null)
            {
                return new ErrorDataResult<User>(message: ResultMessages.USER_NOT_FOUND);
            }
            if (!HashingHelper.VerifyPasswordHash(userForLoginDto.Password, userToCheck.PasswordHash, userToCheck.PasswordSalt))
            {
                return new ErrorDataResult<User>(message: ResultMessages.USER_NOT_FOUND);
            }
            return new SuccessDataResult<User>(userToCheck, message: ResultMessages.USER_LOGIN);


        }

        [ValidationAspect(typeof(UserValidator), Priority = 1)]
        public IDataResult<User> Register(UserForRegisterDto userForRegisterDto)
        {

            byte[] passwordHash, passwordSalt;
            HashingHelper.CreatePasswordHash(userForRegisterDto.Password, out passwordHash, out passwordSalt);
            User user = new()
            {
                Email = userForRegisterDto.Email,
                FirstName = userForRegisterDto.FirstName,
                LastName = userForRegisterDto.LastName,
                PasswordHash = passwordHash,
                PasswordSalt = passwordSalt,
                Status = true,//if you want to verify user, make sure status is false then you can make true later !
            };
            _userService.Add(user);
            return new SuccessDataResult<User>(user, ResultMessages.USER_REGISTER);

        }

        public IResult UserExist(string email)
        {
            if (_userService.GetByEmail(email) != null)
            {
                return new ErrorDataResult<User>(message: ResultMessages.USER_ALREADY_EXISTS);
            }
            return new SuccessResult();
        }

        public IDataResult<AccessToken> CreateAccessToken(User user)
        {
            var claims = _userService.GetClaims(user);
            var accessToken = _tokenHelper.CreateToken(user, claims);
            return new SuccessDataResult<AccessToken>(accessToken, ResultMessages.ACCESS_TOKEN_CREATED);
        }


        //[CacheAspect(/*duration*/1)]
        //public T methodName(A asd){}

        //[CacheRemoveAspect("IUserService.Get")]
        //public T getMethodName(){}


        /*[TransactionScopeAspect]//temp
        public IResult TransactionalOperation(UserForRegisterDto user)
        {
            User entity = _userService.GetByEmail(user.Email);
            _userService.Update(entity);
            _userService.Add(entity);
            return new SuccessResult(ResultMessages.USER_ALREADY_EXISTS);
        }*/
    }
}
