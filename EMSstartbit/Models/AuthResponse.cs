using BOL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BOL.Responses;

namespace EMSstartbit.Models
{
    public class AuthResponse
    {
        public statusResponse status { get; set; }
        public IEnumerable<permission> permissionlist { get; set; }

    }
}
