using Resyaku.Application.DTOs.ServiceAreas;

namespace Resyaku.Application.DTOs.Tables;

public record TableSummaryDto(
    int Id,
    string Name,
    int MinCapacity,
    int MaxCapacity,
    ServiceAreaSummaryDto ServiceArea,
    bool IsActive);
