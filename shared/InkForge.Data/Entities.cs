namespace InkForge.Data
{
	public abstract class ValueEntity<TEntity>
	{
		public TEntity Value { get; set; } = default!;
	}

	public abstract class Entity<TEntity, TKey>
		: ValueEntity<TEntity>
	{
		public TKey Id { get; set; } = default!;
	}

	public abstract class Entity<TSelf, TEntity, TKey>
		: Entity<TEntity, TKey>
		where TSelf : Entity<TSelf, TEntity, TKey>
	{
		public TSelf? Parent { get; set; }
	}
}
