using System.ComponentModel.DataAnnotations.Schema;
using WpfViewModelBasics.Core.Interfaces;

namespace WpfViewModelBasics.Core.Entities
{
    public class FriendEmail: IEntity
    {
        public int Id { get; set; }

        public string Email { get; set; }

        [ForeignKey("FriendId")]
        public virtual Friend Friend { get; set; }

        public int? FriendId { get; set; }
    }
}
