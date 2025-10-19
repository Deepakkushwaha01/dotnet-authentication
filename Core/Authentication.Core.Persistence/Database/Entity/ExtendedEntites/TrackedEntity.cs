namespace Authentication.Core.Persistence.Database.Entity
{
    public class TrackedEntity : Entity
    {
        public string CreatedBy { get; set; } = string.Empty;

        public string UpdatedBy { get; set; } = string.Empty;

    }
}