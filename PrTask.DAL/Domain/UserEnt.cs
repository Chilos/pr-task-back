using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PrTask.DAL.Domain
{
    [Table("Users")]
    public class UserEnt
    {
        public Guid Id { get; set; }
        public string Username { get; set; }
        
        public string RefreshToken { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }
}