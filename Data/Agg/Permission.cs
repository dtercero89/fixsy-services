using System.ComponentModel.DataAnnotations.Schema;

namespace FixsyWebApi.Data.Agg
{
    public class Permission:EntityBase
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int PermissionId { get; set; }
        public string Name { get; set; }

    }
}
