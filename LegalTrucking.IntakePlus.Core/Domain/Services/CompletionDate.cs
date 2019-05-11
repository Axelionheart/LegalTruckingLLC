using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LegalTrucking.IntakePlus.Core.Adapters.Repositories;
using LegalTrucking.IntakePlus.Core.Domain.Common;

namespace LegalTrucking.IntakePlus.Core.Domain.Services
{
    public class CompletionDate: IEquatable<CompletionDate>, IAmAValueType<DateTime>
    {
        private readonly DateTime on;

        public CompletionDate(DateTime on)
        {
            this.on = on;
        }

        public DateTime Value
        {
            get { return on; }
        }

        public bool Equals(CompletionDate rhs)
        {
            if (ReferenceEquals(null, rhs)) return false;
            if (ReferenceEquals(this, rhs)) return true;
            return rhs.@on.Equals(@on);
        }

        public static implicit operator DateTime(CompletionDate rhs)
        {
            return rhs.on;
        }

        public override bool Equals(object rhs)
        {
            if (ReferenceEquals(null, rhs)) return false;
            if (ReferenceEquals(this, rhs)) return true;
            if (rhs.GetType() != typeof(CompletionDate)) return false;
            return Equals((CompletionDate)rhs);
        }

        public override int GetHashCode()
        {
            return @on.GetHashCode();
        }

        public static bool operator ==(CompletionDate left, CompletionDate right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(CompletionDate left, CompletionDate right)
        {
            return !Equals(left, right);
        }

        public override string ToString()
        {
            return string.Format("{0}", @on);
        }
    }
}
