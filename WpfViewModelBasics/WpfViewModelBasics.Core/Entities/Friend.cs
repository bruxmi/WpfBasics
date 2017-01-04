using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using MediatR;
using WpfViewModelBasics.Core.Interfaces;

namespace WpfViewModelBasics.Core.Entities
{
    public class Friend: IEntity, IAsyncRequest<Friend>
    {
        public Friend()
        {
            this.Emails = new HashSet<FriendEmail>();
        }

        public int Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public DateTime? Birthday { get; set; }

        public bool IsDeveloper { get; set; }

        public virtual Address Address { get; set; }

        [ForeignKey("Address")]
        public int? AddressId { get; set; }

        public virtual ICollection<FriendEmail> Emails { get; set; }
    }
}
