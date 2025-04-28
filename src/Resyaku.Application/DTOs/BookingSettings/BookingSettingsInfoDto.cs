namespace Resyaku.Application.DTOs.BookingSettings;

public record BookingSettingsInfoDto(
    int BookingTimeIncrement,
    TimeOnly DailyOpeningTime,
    TimeOnly DailyClosingTime,
    int MinAdvanceNoticeDays,
    int MaxAdvanceNoticeDays,
    int MaxPartySize,
    string ContactEmail);
