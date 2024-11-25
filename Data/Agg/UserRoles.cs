using System.ComponentModel.DataAnnotations.Schema;

namespace FixsyWebApi.Data.Agg
{
    public class UserRoles :EntityBase
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int UserId { get; set; }
        public int RoleId { get; set; }
    }
}
