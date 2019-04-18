using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LegalTrucking.IntakePlus.Core.Domain.Common
{
    public class Command 
    {
        public Command(Guid id) {
            this.Id = id;
        }

        public Guid Id { get; set; }
    }
}
