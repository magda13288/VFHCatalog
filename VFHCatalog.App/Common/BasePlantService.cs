using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VFHCatalog.App;
using VFHCatalog.App.Abstract;
using VFHCatalog.Domain.Common;

namespace VFHCatalog.App.Common
{
    public class BasePlantService<T> : IPlantService<T> where T : BasePlantEntity
    {
        public List<T> Items { get; set ; }

        public BasePlantService()
        {
            Items = new List<T>();
        }
        public void AddItem(T item)
        {
            item.Id = GetLastId() + 1;
            Items.Add(item);
        }

        public void AddItems(List<T> item)
        {           
            foreach (var item2 in item)
                {
                    Items.Add(item2);
                }
            
        }
        public void UpdateItem(T item)
        {
            var entity = Items.Find(x => x.Id == item.Id);

            if (entity != null)
            {
                Items.Remove(entity);
                entity = item;               
                Items.Add(entity);
            }
            else
                entity = null;
        }

        public void RemoveItem(T item)
        {
            Items.Remove(item);
        }

       public List<T> GetAllItems(int id, int? NameId)
        {
            if (Items.Any())
            {
                if (NameId == null)
                {
                    var entity = Items.OrderBy(x => x.Id).Where(x=>x.GroupId == id).ToList();
                    return entity;
                }
                else
                {
                    if (NameId != null)
                    {
                        var entity = Items.OrderBy(x=>x.Id).Where(e => e.GroupId == id && e.NameId == NameId).ToList();
                        return entity;
                    }
                    else return null;
                }
            }
            else
                return null;
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
        public T GetItemByName(string name)
        {
            if (Items.Any())
            {
                var entity = Items.FirstOrDefault(e => e.FullName == name);
                return entity;
            }
            else
                return null;
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
        public List<T> GetAllItemsFromID(int lastId)
        {

            var listOfItems = Items.OrderBy(e => e.Id).Where(e => e.Id > lastId).ToList();

            return listOfItems;
        }
    }
}
