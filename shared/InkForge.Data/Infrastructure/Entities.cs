using System.Numerics;

namespace InkForge.Data
{
	public abstract class ValueEntity<TEntity>
	{
		public TEntity Value { get; set; } = default!;
	}

	public abstract class Entity<TEntity, TKey>
		: ValueEntity<TEntity>
		where TKey : struct, INumber<TKey>
	{
		public TKey? Id { get; set; }
	}

	public abstract class VersionedEntity<TEntity, TKey>
		: ValueEntity<TEntity>
		where TKey : struct, INumber<TKey>
	{
		public TKey Id { get; set; }

		public int? Version { get; set; }
	}
}
