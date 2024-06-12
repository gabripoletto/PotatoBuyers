using PotatoBuyers.Domain.Entities.BaseEntities;

namespace PotatoBuyers.Domain.Entities.Users
{
    public class UserBase : EntityBase
    {
        public string Name { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Cpf { get; set; } = string.Empty;
        public string Telefone { get; set; } = string.Empty;
    }
}
