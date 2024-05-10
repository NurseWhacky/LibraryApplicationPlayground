using API.Interfaces;
using API.Model;
using DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API
{
    public class LibraryManager
    {
        private IBookService bookService;
        private IReservationService reservationService;
        private IUserService userService;
        private LoggedUser currentUser;
        private IRepository<Library> repository;

    }
}
