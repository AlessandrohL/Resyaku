namespace Resyaku.Domain.Audit
{
    public sealed class ActivityLog
    {
        public int ActivityLogId { get; set; }
        public string Message { get; set; } = null!;
        public string ReferenceRowUlid { get; init; } = null!;
        public int ActivityLogTypeId { get; set; }
        public ActivityLogType ActivityLogType { get; private set; } = null!;
        public string UserId { get; set; } = null!;
        public DateTime CreatedOnUtc { get; }
    }
}
