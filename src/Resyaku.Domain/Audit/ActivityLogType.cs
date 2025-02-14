namespace Resyaku.Domain.Audit
{
    public sealed class ActivityLogType
    {
        public int ActivityLogTypeId { get; set; }
        public string Action { get; set; } = null!;
        public ICollection<ActivityLog> ActivityLogs { get; } = [];
    }
}
