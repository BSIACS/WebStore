using Microsoft.AspNetCore.Identity;

namespace WebStore.Domain.Identity
{
    public class Role : IdentityRole 
    {
        public const string Administrator = "Administartors";

        public const string User = "Users";

        public string Description { get; set; }
    }
}
