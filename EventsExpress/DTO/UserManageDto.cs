using System;
using EventsExpress.Db.Enums;

namespace EventsExpress.DTO
{
    public class UserManageDto
    {
        public Guid Id { get; set; }

        public string Email { get; set; }

        public string Username { get; set; }

        public string PhotoUrl { get; set; }

        public DateTime Birthday { get; set; }

        public Gender Gender { get; set; }

        public bool IsBlocked { get; set; }

        public byte Attitude { get; set; }

        public virtual RoleDto Role { get; set; }

        public double Rating { get; set; }
    }
}
