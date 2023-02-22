using System.ComponentModel.DataAnnotations;
using API.Entities.Models;

namespace API.Entities.Domain;

public class User
{
    [Key]
    public int Id { get; set; }
    
    [Required,MinLength(6),MaxLength(30)]
    public string Username { get; set; }
    
    [Required]
    public byte[] PasswordHash { get; set; }
    
    [Required]
    public byte[] PasswordSalt { get; set; }
    
    [Required,EmailAddress]
    public string Email { get; set; }
    
}