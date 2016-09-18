using System;

namespace NicheFinder.DataBase.Entities
{
    public abstract class Entity : IDbEntity, IEquatable<Entity>
    {
        public const String EF_ONLY = "EF only";

        public Int32 ID { get; set; }

        public Boolean IsNew { get { return this.ID == default(Int32); } }

        public override Int32 GetHashCode()
        {
            return this.ID.GetHashCode();
        }

        public override Boolean Equals(Object obj)
        {
            return this.Equals(obj as Entity);
        }

        public Boolean Equals(Entity other)
        {
            return other != null && other.ID == this.ID;
        }
    }
}
