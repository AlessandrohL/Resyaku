namespace Resyaku.Application.DTOs.Customers;

public record CustomerInfoDto(
    string Name,
    string? Lastname,
    string Dni,
    string Email,
    string Phone);
