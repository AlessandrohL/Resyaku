namespace Resyaku.Domain.Audit
{
    public sealed class ActivityLog
    {
        public int ActivityLogId { get; set; }
        public string Message { get; set; } = null!;
        public string ReferenceRowGuid { get; set; } = null!;
        public int ActivityLogTypeId { get; set; }
        public ActivityLogType ActivityLogType { get; set; } = null!;
        public string UserId { get; set; } = null!;
        public DateTime CreatedAt { get; }
    }
}
