﻿namespace PP.CREDStroreService.Models.Dtos
{
    public class CreateCredentialResponseDto
    {
        public int Id { get; set; }
        public string Website { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
    }
}
