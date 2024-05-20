using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API.Views
{
    public record BookInfo(string Title, string AuthorName, string AuthorSurname, string Publisher, int Qty)
    {
        public override string ToString()
        {
            return
                $@"
  =========================================================================

 {"||    Title:",-25}<< {Title} >>
 {"||    Author:",-25} {AuthorSurname}, {AuthorName}
 {"||    Publisher:",-25} {Publisher}

  =========================================================================
";
        }
    }
}
