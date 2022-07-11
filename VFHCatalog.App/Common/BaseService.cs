using System;
using System.Collections.Generic;
using System.Text;
using VFHCatalog.App;
using VFHCatalog.App.Abstract;
using VFHCatalog.Domain.Common;
using VFHCatalog.Domain.Entity;
using System.Linq;

namespace VFHCatalog.App.Common
{
    public class BaseService<T> : IService<T> where T : BaseEntity
    {
        public List<T> Items { get ; set ; }

        public BaseService()
        {
            Items = new List<T>();
        }
        public void AddItem(T item)
        {
            item.Id = GetLastId() + 1;
            Items.Add(item);
        }

        public void AddMenuItem(T item)
        {
            Items.Add(item);
        }

        public int GetLastId()
        {
            int lastId;
            if (Items.Any())
            {
                lastId = Items.OrderBy(e => e.Id).LastOrDefault().Id;
            }
            else
                lastId = 0;

            return lastId;

        }
        public List<T> GetItems()
        {
            if (Items.Any())
            {
                var entity = Items.OrderBy(e => e.Id).Where(e => e.DateTime.ToShortDateString() == DateTime.Now.ToShortDateString()).ToList();
                return entity;
            }
            else
            return null;
           
        }
        public void RemoveItem(T item)
        {
            Items.Remove(item);
        }

        public T GetItemById(int id)
        {      
                if (Items.Any())
                {
                    var entity = Items.FirstOrDefault(e => e.Id == id);
                    return entity;
                }
                else
                    return null;          
        }

        public List<T> GetAllItemsFromID(int lastId)
        {
            var listOfItems = Items.OrderBy(e => e.Id).Where(e => e.Id > lastId).ToList();

            return listOfItems;
        }

        public void AddItems(List<T> item)
        {
            foreach (var item2 in item)
            {
                Items.Add(item2);
            }

        }
    }
}
