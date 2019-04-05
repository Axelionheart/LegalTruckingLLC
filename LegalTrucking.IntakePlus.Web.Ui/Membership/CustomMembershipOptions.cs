using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LegalTrucking.IntakePlus.Web.Ui.Membership
{
    public class CustomMembershipOptions
    {
        public string DefaultPathAfterLogin { get; set; }
        public string DefaultPathAfterLogout { get; set; }

        public string InteractiveAuthenticationType { get; set; }
        public string OneTimeAuthenticationType { get; set; }
        public string AuthenticationType { get; internal set; }
    }
}
