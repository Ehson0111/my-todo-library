using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Todo.Core.Tests
{
    public class TodoList
    {
        private readonly List<TodoItem> _items = new ();
        public IReadOnlyList<TodoItem> Items=> _items.AsReadOnly ();


        public TodoItem Add(string title) {
            var item = new TodoItem(title);
            _items.Add (item);
            return item;
        }
        public bool Remove(Guid id) => _items.RemoveAll(i => i.Id == id) > 0;

        public IEnumerable<TodoItem> Find(string substring) =>
            _items.Where(i => i.Title.ToLower().Contains(substring ?? string.Empty, StringComparison.OrdinalIgnoreCase));

        public int Count=> _items.Count;


        public void Clear() => _items.Clear ();

        public void Save(string Path) { 

        
        }


        //public void Load(string Path) { 
        
        //    //Path.
        //    var result=JsonConverter.DeserializeObject<FileModel>
        //}

    }
}
