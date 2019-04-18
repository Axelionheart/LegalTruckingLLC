using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using LegalTrucking.IntakePlus.Core.Domain.Common;

namespace LegalTrucking.IntakePlus.Core.Domain.Services
{
    public class ScheduledDate : IEquatable<ScheduledDate>, IAmAValueType<DateTime>
    {
        private readonly DateTime on;

        public ScheduledDate(DateTime on)
        {
            this.on = on;
        }

        public DateTime Value
        {
            get { return on; }
        }

        public bool Equals(ScheduledDate rhs)
        {
            if (ReferenceEquals(null, rhs)) return false;
            if (ReferenceEquals(this, rhs)) return true;
            return rhs.@on.Equals(@on);
        }

        public static implicit operator DateTime(ScheduledDate rhs)
        {
            return rhs.on;
        }

        public override bool Equals(object rhs)
        {
            if (ReferenceEquals(null, rhs)) return false;
            if (ReferenceEquals(this, rhs)) return true;
            if (rhs.GetType() != typeof(ScheduledDate)) return false;
            return Equals((ScheduledDate)rhs);
        }

        public override int GetHashCode()
        {
            return @on.GetHashCode();
        }

        public static bool operator ==(ScheduledDate left, ScheduledDate right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(ScheduledDate left, ScheduledDate right)
        {
            return !Equals(left, right);
        }

        public override string ToString()
        {
            return string.Format("{0}", @on);
        }
    }
}
