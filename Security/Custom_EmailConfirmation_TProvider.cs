using Microsoft.AspNetCore.DataProtection;//To use IDataProtectionProvider
using Microsoft.AspNetCore.Identity;//To use DataProtectorTokenProvider
using Microsoft.Extensions.Options;//To use IOptions
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AC_EmpManagement.Security
{
    public class Custom_EmailConfirmation_TProvider<TUser>
        : DataProtectorTokenProvider<TUser> where TUser : class
    {
        public Custom_EmailConfirmation_TProvider(IDataProtectionProvider DataPProviderI,
                              IOptions<Custom_EmailConfirmation_TPOptions> OptionsI_CEmailCTPO)
            : base(DataPProviderI, OptionsI_CEmailCTPO)
        {

        }
    }
}
