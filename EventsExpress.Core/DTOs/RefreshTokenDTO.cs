﻿using System;
using EventsExpress.Db.Entities;
using EventsExpress.Db.Enums;

namespace EventsExpress.Core.DTOs
{
    public class RefreshTokenDto
    {
        public TokenType Type { get; set; }

        public string Token { get; set; }

        public DateTime Expires { get; set; }

        public bool IsExpired => DateTime.UtcNow >= Expires;

        public DateTime Created { get; set; }

        public DateTime? Revoked { get; set; }

        public string ReplacedByToken { get; set; }

        public string CreatedByIp { get; set; }

        public string RevokedByIp { get; set; }

        public bool IsActive => Revoked == null && !IsExpired;
    }
}
