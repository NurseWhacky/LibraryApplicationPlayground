using API.Model;

namespace API.Services
{
    public abstract class Service
    {
        protected Library library; 

        public Service(Library library)
        {
            this.library = library;
        }
    }
}
