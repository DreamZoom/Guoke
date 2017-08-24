using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Text;

namespace Guoke
{
    public class EntityConfiguration<TContext> : DbMigrationsConfiguration<TContext>
        where TContext : DbContext
    {
        public EntityConfiguration()
        {
            AutomaticMigrationsEnabled = true;
            AutomaticMigrationDataLossAllowed = true;
        }
    }
}
