﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;


namespace RepositoryPatternApi.Domain.Entities
{
    public class User:IdentityUser<string>
    {

    }
}
