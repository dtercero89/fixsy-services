using System.ComponentModel.DataAnnotations.Schema;

namespace FixsyWebApi.Data.Agg
{
    public class PermissionByRole :EntityBase
    {

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int PermissionId { get; set; }
        public int RoleId { get; set; }

    }
}
