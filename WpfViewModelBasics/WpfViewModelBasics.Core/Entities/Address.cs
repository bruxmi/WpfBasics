using System.Collections.Generic;
using WpfViewModelBasics.Core.Interfaces;

namespace WpfViewModelBasics.Core.Entities
{
    public class Address: IEntity
    {
        public Address()
        {
            this.Friends = new HashSet<Friend>();
        }
        public int Id { get; set; }

        public string City { get; set; }

        public string Street { get; set; }

        public string StreetNumber { get; set; }

        public virtual ICollection<Friend> Friends { get; set; }
    }
}
