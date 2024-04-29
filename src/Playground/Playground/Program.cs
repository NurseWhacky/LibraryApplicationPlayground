
using DataAccess;
using System.Xml.Linq;

XElement library = Utilities.PopulateLibrary();
Utilities.WriteLibraryToFile(library);
