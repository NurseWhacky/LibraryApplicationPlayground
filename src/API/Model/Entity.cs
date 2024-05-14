using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API.Model
{
    public abstract class Entity
    {
        protected Library? library;

        public Entity() { }

        public Entity(Library library)
        {
            this.library = library;
        }
    }
}
