using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API.Model
{
    public abstract class Entity
    {
        public int Id { get; set; }

        public Entity()
        {
           // Id = GetNextId();
           Id = 0; // just to shut up compiler!
        }
    }
}
