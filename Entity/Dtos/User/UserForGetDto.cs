using Core.Entity.Abstract;

namespace Entity.Dtos.User
{
    public class UserForGetDto : IDto
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public bool Status { get; set; }
    }
}
