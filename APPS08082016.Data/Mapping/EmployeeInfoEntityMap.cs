using System.Data.Entity.ModelConfiguration;
using APPS08082016.Core.EntityModel;

namespace APPS08082016.Data.Mapping
{
    public class EmployeeInfoEntityMap : EntityTypeConfiguration<EmployeeInfoEntity>
    {
        public EmployeeInfoEntityMap()
        {
            this.ToTable("EmployeeInfoEntity");
        }
    }
}
