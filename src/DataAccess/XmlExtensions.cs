using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace DataAccess
{
    //to the garbage compactor!
    public static class XmlExtensions
    {
        public static T? ToEntity<T>(this XElement element) where T : class, new() 
        { 
            XmlSerializer serializer = new XmlSerializer(typeof(T));
            using(var xmlReader = element.CreateReader())
            {
                return (T)serializer.Deserialize(xmlReader);
            }
        }
    }
}
