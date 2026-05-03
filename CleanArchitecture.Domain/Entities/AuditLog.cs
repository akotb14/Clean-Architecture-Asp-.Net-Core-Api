using CleanArchitecture.Domain.Entities.Identity;

namespace CleanArchitecture.Domain.Entities
{
    public class AuditLog
    {
        public int LogID { get; set; }
        public string? UserID { get; set; }
        public string Action { get; set; }
        public string Entity { get; set; }
        public int? EntityID { get; set; }
        public DateTime Timestamp { get; set; }

        public User User { get; set; }
    }
}
