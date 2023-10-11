using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace WebMedicina.BackEnd.Model;

public partial class IdentityContext : IdentityDbContext<IdentityUser> {
    public IdentityContext()
    {
    }

    public IdentityContext(DbContextOptions<IdentityContext> options)
        : base(options)
    {
    }
}
