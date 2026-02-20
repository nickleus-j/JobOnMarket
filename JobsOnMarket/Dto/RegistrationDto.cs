using System.ComponentModel.DataAnnotations;

namespace JobsOnMarket.Dto;

public class RegistrationDto:LoginDto
{
    [MinLength(1)]
    public string FirstName { get; set; }
    [MinLength(1)]
    public string Surname { get; set; }
    [MinLength(1)]
    public required string RoleName { get; set; }
}