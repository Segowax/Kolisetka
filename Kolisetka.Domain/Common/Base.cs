using System;

namespace Kolisetka.Domain.Common
{
    public abstract class Base
    {
        public int Id { get; set; }
        public DateTime DateCreated { get; set; }
    }
}
