using System.ComponentModel.DataAnnotations.Schema;
using WpfViewModelBasics.Core.Interfaces;

namespace WpfViewModelBasics.Core.Entities
{
    public class FriendEmail: IEntity
    {
        public int Id { get; set; }

        public string Email { get; set; }

        public virtual Friend Friend { get; set; }
        [ForeignKey("Friend")]
        public int? FriendId { get; set; }
    }
}
