using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer
{
    public class SegamDBContextInitializer : DropCreateDatabaseAlways<SegamDBContext>
    {
        protected override void Seed(SegamDBContext context)
        {
            base.Seed(context);
        }
    }
}