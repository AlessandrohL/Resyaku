namespace Resyaku.Application.DTOs.Tables;

public record AvailableTableDto(
    int TableId, 
    string Name, 
    int MinCapacity, 
    int MaxCapacity, 
    string ServiceAreaName);
