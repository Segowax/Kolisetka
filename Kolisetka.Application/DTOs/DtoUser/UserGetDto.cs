﻿namespace Kolisetka.Application.DTOs.DtoUser
{
    public class UserGetDto
    {
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserName { get; set; }
        public bool EmailConfirmed { get; set; }
    }
}
