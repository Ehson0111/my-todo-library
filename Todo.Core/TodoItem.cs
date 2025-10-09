﻿//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace Todo.Core.Tests
//{

//    public class TodoItem
//    {

//        public Guid Id { get; } = Guid.NewGuid();

//        public string Title { get; private set; }

//        public bool IsDone { get; private set; }

//        public TodoItem(string title)
//        {
//            Title = title?.Trim() ?? throw new ArgumentNullException(nameof(title));


//        }

//        public void MarkDone() => IsDone = true;
//        public void MarkUndone() => IsDone = false;




//        public void Rename(string newTitle)
//        {
//            if (string.IsNullOrWhiteSpace(newTitle)) throw new ArgumentException("Title required", nameof(newTitle));
//            Title = newTitle.Trim();

//        } 



//    }
//}


using System;
using System.Text.Json.Serialization;

namespace Todo.Core.Tests
{
    public class TodoItem
    {
        public Guid Id { get; } = Guid.NewGuid();

        public string Title { get; private set; }

        public bool IsDone { get; private set; }

        public TodoItem(string title)
        {
            Title = title?.Trim() ?? throw new ArgumentNullException(nameof(title));
        }

        // Конструктор для десериализации
        [JsonConstructor]
        public TodoItem(Guid id, string title, bool isDone)
        {
            Id = id;
            Title = title;
            IsDone = isDone;
        }

        public void MarkDone() => IsDone = true;
        public void MarkUndone() => IsDone = false;

        public void Rename(string newTitle)
        {
            if (string.IsNullOrWhiteSpace(newTitle))
                throw new ArgumentException("Title required", nameof(newTitle));
            Title = newTitle.Trim();
        }
    }
}
