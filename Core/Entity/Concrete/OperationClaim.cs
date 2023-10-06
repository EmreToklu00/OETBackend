using Core.Entity.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entity.Concrete
{
    public class OperationClaim : IEntity
    {
        public Guid Id { get; set; }

        public string Name { get; set; }
    }
}
