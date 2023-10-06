using Core.DataAccess.EntityFramework;
using Core.Entity.Concrete;
using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework.Contexts;
using System.Linq;

namespace DataAccess.Concrete.EntityFramework
{
    public class EfUserDal : EfCoreEntityRepository<User, OETContext>, IUserDal
    {

        public List<OperationClaim> GetClaims(User user)
        {
            using (var context = new OETContext())
            {
                var result = from operationsClaim in context.OperationClaims 
                             join UserOperationClaim in context.UserOperationClaims
                             on operationsClaim.Id equals UserOperationClaim.OperationClaimId
                             where UserOperationClaim.UserId == user.Id select new OperationClaim { Id = operationsClaim.Id, Name = operationsClaim.Name };
                return result.ToList();
            }
        }
    }
}
