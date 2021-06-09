using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using d04_ex02.Attributes;

namespace d04_ex02.Models
{
    public class IdentityUser
    {
        public IdentityUser() { }

        public IdentityUser(string userName) : this() => UserName = userName;

        [Required, Description("User name"), DefaultValue("Me")]
        public virtual string UserName { get; set; }

        [NoDisplay]
        public virtual string NormalizedUserName { get; set; }

        [Required, Description("Email address"), DefaultValue("test@test")]
        public virtual string Email { get; set; }

        [NoDisplay]
        public virtual string NormalizedEmail { get; set; }

        [NoDisplay]
        public virtual bool EmailConfirmed { get; set; }

        [NoDisplay]
        public virtual string PasswordHash { get; set; }

        [NoDisplay]
        public virtual string SecurityStamp { get; set; }

        public virtual string ConcurrencyStamp() => Guid.NewGuid().ToString();

        [Required, Description("Phone number"), DefaultValue("1234567890")]
        public virtual string PhoneNumber { get; set; }

        [NoDisplay]
        public virtual bool PhoneNumberConfirmed { get; set; }

        [NoDisplay]
        public virtual bool TwoFactorEnabled { get; set; }

        [NoDisplay]
        public virtual DateTimeOffset? LockoutEnd { get; set; }

        [NoDisplay]
        public virtual bool LockoutEnabled { get; set; }

        public override string ToString() => $"User: {UserName}, {Email}, {PhoneNumber}";
    }
}
