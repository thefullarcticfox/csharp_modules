using System;

namespace d04_ex03.Models
{
    public class IdentityUser
    {
        public IdentityUser() { }

        public IdentityUser(string userName) : this() => UserName = userName;

        public virtual string UserName { get; set; }

        public virtual string NormalizedUserName { get; set; }

        public virtual string Email { get; set; }

        public virtual string NormalizedEmail { get; set; }

        public virtual bool EmailConfirmed { get; set; }

        public virtual string PasswordHash { get; set; }

        public virtual string SecurityStamp { get; set; }

        public virtual string ConcurrencyStamp() => Guid.NewGuid().ToString();

        public virtual string PhoneNumber { get; set; }

        public virtual bool PhoneNumberConfirmed { get; set; }

        public virtual bool TwoFactorEnabled { get; set; }

        public virtual DateTimeOffset? LockoutEnd { get; set; }

        public virtual bool LockoutEnabled { get; set; }

        public override string ToString() => $"User: {UserName}, {Email}, {PhoneNumber}";
    }
}
