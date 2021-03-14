using System;
using System.ComponentModel.DataAnnotations;

namespace PrTask.DAL.Domain
{
    public class UserEnt
    {
        public Guid Id { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }
}