using System.ComponentModel.DataAnnotations;

namespace JobsOnMarket.Dto;

public class RegistrationDto:LoginDto
{
    [MinLength(5)]
    public string FirstName { get; set; }
    [MinLength(6)]
    public string Surname { get; set; }
    [MinLength(1)]
    public string RoleName { get; set; }
}