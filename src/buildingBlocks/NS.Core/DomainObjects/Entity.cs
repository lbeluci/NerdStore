using NS.Core.Messages;
using System;
using System.Collections.Generic;

namespace NS.Core.DomainObjects
{
    public abstract class Entity
    {
        private readonly List<Event> _events;

        public IReadOnlyCollection<Event> Events => _events.AsReadOnly();

        public Guid Id { get; set; }

        protected Entity()
        {
            _events = new List<Event>();

            Id = Guid.NewGuid();
        }

        public void AddEvent(Event value)
        {
            _events.Add(value);
        }

        public void RemoveEvent(Event value)
        {
            _events.Remove(value);
        }

        public void ClearEvents()
        {
            _events.Clear();
        }

        #region EntityBase

        public override bool Equals(object obj)
        {
            var compareTo = obj as Entity;

            if (ReferenceEquals(this, compareTo))
            {
                return true;
            }

            if (ReferenceEquals(null, compareTo))
            {
                return false;
            }

            return Id.Equals(compareTo.Id);
        }

        public static bool operator ==(Entity left, Entity right)
        {
            if (ReferenceEquals(left, null) && ReferenceEquals(right, null))
            {
                return true;
            }

            if (ReferenceEquals(left, null) || ReferenceEquals(right, null))
            {
                return false;
            }

            return left.Equals(right);
        }

        public static bool operator !=(Entity left, Entity right)
        {
            return !(left == right);
        }

        public override int GetHashCode()
        {
            return (GetType().GetHashCode() * 218) + Id.GetHashCode();
        }

        public override string ToString()
        {
            return $"{GetType().Name} [Id={Id}]";
        }

        #endregion
    }
}