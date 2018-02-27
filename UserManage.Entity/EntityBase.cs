using System;
using System.Collections.Generic;
using System.Text;

namespace UserManage.Entity
{
    public class EntityBase
    {
        public EntityBase()
        {
            Isvalid = true;
            CreatedTime = DateTime.Now;
            ModifiedTime = DateTime.Now;
        }

       
        public virtual int Id { get; set; }


        public Nullable<int> CreatedBy { get; set; }

        public Nullable<int> ModifiedBy { get; set; }

        public DateTime ModifiedTime { get; set; }

        public bool Isvalid { get; set; }

        public DateTime CreatedTime { get; set; }


        public override bool Equals(object obj)
        {
            return Equals(obj as EntityBase);
        }

        private static bool IsTransient(EntityBase obj)
        {
            return obj != null && Equals(obj.Id, default(int));
        }


        private Type GetUnproxiedType()
        {
            return GetType();
        }

        public virtual bool Equals(EntityBase other)
        {
            if (other == null)
                return false;

            if (ReferenceEquals(this, other))
                return true;

            if (!IsTransient(this) &&
                !IsTransient(other) &&
                Equals(Id, other.Id))
            {
                var otherType = other.GetUnproxiedType();
                var thisType = GetUnproxiedType();
                return thisType.IsAssignableFrom(otherType) ||
                        otherType.IsAssignableFrom(thisType);
            }

            return false;
        }

        public override int GetHashCode()
        {
            if (Equals(Id, default(int)))
                return base.GetHashCode();
            return Id.GetHashCode();
        }

        public static bool operator ==(EntityBase x, EntityBase y)
        {
            return Equals(x, y);
        }

        public static bool operator !=(EntityBase x, EntityBase y)
        {
            return !(x == y);
        }
        protected virtual void SetParent(dynamic child)
        {

        }
        protected virtual void SetParentToNull(dynamic child)
        {

        }
    }
}
