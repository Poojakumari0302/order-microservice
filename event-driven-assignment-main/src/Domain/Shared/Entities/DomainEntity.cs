using System;
using System.Collections.Generic;
using MediatR;

namespace Domain.Shared.Entities
{
    public class DomainEntity
    {
        private int? _requestedHashCode;
        private Guid _id;
        public virtual Guid Id 
        {
            get { return _id; }
            protected set { _id = value; }
        }
        private List<INotification> _domainEvents;
        public IReadOnlyCollection<INotification> DomainEvents => _domainEvents?.AsReadOnly();

        protected void AddDomainEvent(INotification eventItem)
        {
            _domainEvents ??= [];
            _domainEvents.Add(eventItem);
        }

        public void RemoveDomainEvent(INotification eventItem)
        {
            _domainEvents?.Remove(eventItem);
        }

        public void ClearDomainEvents()
        {
            _domainEvents?.Clear();
        }

        public override bool Equals(object obj)
        {
            if (obj is not DomainEntity)
                return false;

            if (!ReferenceEquals(this, obj))
            {
                if (GetType() != obj.GetType())
                    return false;

                var item = (DomainEntity) obj;

                if (item.IsTransient() || IsTransient())
                    return false;

                return item.Id.Equals(Id);
            }

            return true;
        }
        
        public override int GetHashCode()
        {
            if (IsTransient())
                return
                    // ReSharper disable once BaseObjectGetHashCodeCallInGetHashCode
                    base.GetHashCode();

            // ReSharper disable once NonReadonlyMemberInGetHashCode
            if (!_requestedHashCode.HasValue)
                // ReSharper disable once NonReadonlyMemberInGetHashCode
                _requestedHashCode =
                    // ReSharper disable once NonReadonlyMemberInGetHashCode
                    Id.GetHashCode() ^ 31; // XOR for random distribution (http://blogs.msdn.com/b/ericlippert/archive/2011/02/28/guidelines-and-rules-for-gethashcode.aspx)
            // ReSharper disable once NonReadonlyMemberInGetHashCode
            return _requestedHashCode.Value;
        }
        public static bool operator ==(DomainEntity left, DomainEntity right)
        {
            if (Object.Equals(left, null))
                return Object.Equals(right, null);
            else
                return left.Equals(right);
        }
        public static bool operator !=(DomainEntity left, DomainEntity right)
        {
            return !(left == right);
        }

        private bool IsTransient()
        {
            return Id == default;
        }
    }
}