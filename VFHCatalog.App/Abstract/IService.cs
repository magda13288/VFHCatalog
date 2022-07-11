using System;
using System.Collections.Generic;
using System.Text;

namespace VFHCatalog.App.Abstract
{
    public interface IService <T>
    {
        List<T> Items { get; set; }
        void AddItem(T item); 
        void AddItems(List<T> item);
        void AddMenuItem(T item);
        List<T> GetItems();
        void RemoveItem(T item);
        T GetItemById(int id);
        public int GetLastId();
        List<T> GetAllItemsFromID(int lastId);
    }
}
