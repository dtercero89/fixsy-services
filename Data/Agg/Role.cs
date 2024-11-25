using System.ComponentModel.DataAnnotations.Schema;

namespace FixsyWebApi.Data.Agg
{
    public class Role : EntityBase
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int RoleId { get; set; }
        public string Name { get; set; }
    }
}
