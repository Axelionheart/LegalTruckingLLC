using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LegalTrucking.IntakePlus.Core.Domain.Common
{
    public interface IAmAValueType<out T>
    {
        T Value { get; }
    }
}
