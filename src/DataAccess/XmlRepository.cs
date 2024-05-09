﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;

namespace DataAccess
{
    public class XmlRepository<T> : IRepository<T> where T : class, new()
    {
        private XElement xLibrary; // ==>> Maybe use XDocument to represent ENTIRE library??
        //private readonly string filePath = "Library.xml"; // debug folder in entry point project
        private List<T> entities; // List of ONLY T entities. TODO: MUST be updated after each operation that changes library state

        public XmlRepository()
        {
            entities = new List<T>();
            xLibrary = LoadFile();
            //var xList = from el in xLibrary.Descendants($"{typeof(T).Name}") select el;
            //foreach (var el in xList)
            //{
            //    T entity = el.ToEntity<T>();
            //    entities.Add(entity);
            //}
        }
        private XElement LoadFile()
        {
            return Utilities.ReadFromFile();
        }

        public void Add(T? entity)
        {
            if (entity is null)
            {
                Console.WriteLine($"Could not add {typeof(T).Name}");
            }
            entities.Add(entity);
        }

        //private void UpdateLastUsedId()
        //{
        //    XElement? root = xLibrary.Element($"{typeof(T).Name}s");
        //    string idPropertyName = $"{typeof(T).Name}Id";
        //    PropertyInfo propertyInfo = typeof(T).GetProperty(idPropertyName);
        //    if (propertyInfo == null)
        //    {
        //        throw new InvalidOperationException($"Entity of type {typeof(T).Name} doesn't have a property named {idPropertyName}");
        //    }

        //    int lastUsedId = entities.Cast<dynamic>().Max(e => (int) propertyInfo.GetValue(e));

        //    XAttribute lastUsedIdAttribute = root.Attribute("LastUsedId");
        //    if(lastUsedId != null)
        //    {
        //        lastUsedIdAttribute.SetValue(lastUsedId.ToString());
        //    }
        //    else
        //    {
        //        root.Add(new XAttribute("LastUsedId", lastUsedId.ToString()));

        //    }

        //}

        public void Delete(T entity)
        {
            // FindById(entityToDeleteId);
            T toDelete = FindById((int)typeof(T).GetProperty($"{typeof(T).Name}Id").GetValue(entity));

            // if null return
            if (toDelete is null)
            { return; }

            // Delete entity from entities object
            entities.Remove(toDelete);

            //SaveChanges();
        }

        //TODO : check this -> https://learn.microsoft.com/en-us/dotnet/standard/linq/find-descendants-specific-element-name

        public IEnumerable<T> FindAll()
        {
            List<T> entities = new List<T>();

            var xElements = from el in xLibrary.Descendants(typeof(T).Name) select el;
            foreach (var el in xElements)
            {
                T? entity = el.ToEntity<T>();
                entities.Add(entity);
            }
            return entities;
        }

        public T? FindById(int? id)
        {
            PropertyInfo? idProperty = typeof(T).GetProperty($"{typeof(T).Name}Id");

            bool isIdInteger = idProperty.PropertyType == typeof(int);

            if (idProperty == null || idProperty.PropertyType != typeof(int))
            {
                return null;
            }
            return FindAll().FirstOrDefault(r => isIdInteger && (int)idProperty.GetValue(r) == id);
        }

        public IEnumerable<T>? FindByEntityId(int? entityId, Type entityType)
        {
            PropertyInfo? entityIdProperty = entityType.GetProperty($"{entityType.Name}Id");

            if (entityIdProperty == null || entityIdProperty.PropertyType != typeof(int))// || !typeof(T).GetProperties().Contains(typeof(T).GetProperty(entityIdProperty.Name)))
            {
                // error message specific for invalid id
                return null;
            }
            return FindAll().Where(result => entityId == (int?) entityIdProperty.GetValue(result));
        }


        public void SaveChanges()
        {
            // Get the root element for this type
            XElement? root = xLibrary.Element($"{typeof(T).Name}s");

            // Delete existing xlibrary
            xLibrary.Elements($"{typeof(T).Name}").Remove();

            // Convert each entity in the entities list to an XElement and add it to the root
            foreach (var entity in entities)
            {
                XElement entityElement = Utilities.FromEntity(entity);
                root.Add(entityElement);
                xLibrary.Elements(typeof(T).Name).Append(entityElement);
            }

            

            // Save the updated library back to the XML file
            xLibrary.Save(Utilities.DataBase);

        }

        public void Update(T entity)
        {
            int entityId = (int)entity.GetType().GetProperty($"{entity.GetType().Name}Id").GetValue(entity);
            T? entityToUpdate = FindById(entityId);

            PropertyInfo[] properties = typeof(T).GetProperties();
            foreach (var property in properties)
            {
                property.SetValue(entityToUpdate, property.GetValue(entity));
            }

            //SaveChanges();
        }
    }
}
