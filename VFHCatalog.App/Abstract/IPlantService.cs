using System;
using System.Collections.Generic;
using System.Text;

namespace VFHCatalog.App.Abstract
{
    public interface IPlantService <T>
    {
        List<T> Items { get; set; }
        List<T> GetAllItems(int id, int? NameId);
        T GetItemById(int id);
        void AddItem(T item);
        void AddItems(List<T> item);
        List<T> GetAllItemsFromID(int lastId);
        void UpdateItem(T item);
        void RemoveItem(T item);
        public int GetLastId();



    }
}
