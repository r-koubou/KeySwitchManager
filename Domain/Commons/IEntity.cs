namespace ArticulationManager.Domain.Commons
{
    public interface IEntity
    {
        public EntityId Id { get; }
    }

    public interface IEntityWithoutId : IEntity
    {
        EntityId IEntity.Id => EntityId.Zero;
    }
}