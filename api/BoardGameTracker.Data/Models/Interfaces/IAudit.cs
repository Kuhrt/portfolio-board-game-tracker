namespace BoardGameTracker.Data.Models.Interfaces;

public interface ICreated
{
    Guid CreatedBy { get; set; }
    DateTime CreatedOn { get; set; }
}

public interface IUpdated
{
    Guid? UpdatedBy { get; set; }
    DateTime? UpdatedOn { get; set; }
}

public interface IDeleted
{
    bool IsDeleted { get; set; }
    Guid? DeletedBy { get; set; }
    DateTime? DeletedOn { get; set; }
}

public interface IAudit : ICreated, IUpdated
{
}

public static class AuditExtensions
{
    public static void SetCreated(this ICreated model, Guid userId)
    {
        model.CreatedBy = userId;
        model.CreatedOn = DateTime.UtcNow;
    }

    public static void SetUpdated(this IUpdated model, Guid userId)
    {
        model.UpdatedBy = userId;
        model.UpdatedOn = DateTime.UtcNow;
    }

    public static void SetDeleted(this IDeleted model, Guid userId)
    {
        model.IsDeleted = true;
        model.DeletedBy = userId;
        model.DeletedOn = DateTime.UtcNow;
    }
}