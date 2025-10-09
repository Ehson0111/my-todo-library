//using System;
//using System.Text.Json;

//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Text.Json.Serialization;
//using System.Threading.Tasks;

//namespace Todo.Core.Tests
//{
//    public class TodoList
//    {
//        private readonly List<TodoItem> _items = new();
//        public IReadOnlyList<TodoItem> Items => _items.AsReadOnly();


//        public TodoItem Add(string title)
//        {
//            var item = new TodoItem(title);
//            _items.Add(item);
//            return item;
//        }
//        public bool Remove(Guid id) => _items.RemoveAll(i => i.Id == id) > 0;

//        public IEnumerable<TodoItem> Find(string substring) =>
//            _items.Where(i => i.Title.ToLower().Contains(substring ?? string.Empty, StringComparison.OrdinalIgnoreCase));

//        public int Count => _items.Count;


//        public void Clear() => _items.Clear();

//        public void Save(string Path)
//        {


//        }


//        public class FileModel()
//        {

//            public int id { get; set; }
//            public string Name { get; set; }
//            public bool Mark { get; set; }


//        }
//        public FileModel Load(string Path)
//        {

//            try
//            {
//                if (!File.Exists(Path))
//                {
//                    throw new FileNotFoundException("файл не найден ");

//                }

//                string jsonString = File.ReadAllText(Path);


//                //
//                //var result=JsonConverter.DeserializeObject<FileModel>

//                var result = JsonSerializer.Deserialize<FileModel>(jsonString);


//                return result ?? throw new InvalidDataException("Данные невалидны");
//                //return forecast;

//            }
//            catch (Exception ex)
//            {


//                FileModel  file=new FileModel();
//                return file;
//                //return throw new InvalidDataException("данные невалидны "+ex);
//            }
//        }

//    }
//}

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;

namespace Todo.Core.Tests
{
    public class TodoList
    {
        private readonly List<TodoItem> _items = new();
        public IReadOnlyList<TodoItem> Items => _items.AsReadOnly();

        public TodoItem Add(string title)
        {
            var item = new TodoItem(title);
            _items.Add(item);
            return item;
        }

        public bool Remove(Guid id) => _items.RemoveAll(i => i.Id == id) > 0;

        public IEnumerable<TodoItem> Find(string substring) =>
            _items.Where(i => i.Title.ToLower().Contains(substring?.ToLower() ?? string.Empty));

        public int Count => _items.Count;

        public void Clear() => _items.Clear();

        public void Save(string path)
        {
            try
            {
                var options = new JsonSerializerOptions { WriteIndented = true };
                string jsonString = JsonSerializer.Serialize(_items, options);
                File.WriteAllText(path, jsonString);
                Console.WriteLine($"Список сохранен в {path}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка сохранения: {ex.Message}");
            }
        }

        public void Load(string path)
        {
            try
            {
                if (!File.Exists(path))
                {
                    Console.WriteLine("Файл не найден");
                    return;
                }

                string jsonString = File.ReadAllText(path);
                var loadedItems = JsonSerializer.Deserialize<List<TodoItem>>(jsonString);

                if (loadedItems != null)
                {
                    _items.Clear();
                    _items.AddRange(loadedItems);
                    Console.WriteLine($"Список загружен из {path}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка загрузки: {ex.Message}");
            }
        }
    }
}
