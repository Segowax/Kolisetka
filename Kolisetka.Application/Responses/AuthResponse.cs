﻿namespace Kolisetka.Application.Responses
{
    public class AuthResponse : BaseCommandResponse
    {
        public string Id { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Token { get; set; }
    }
}
