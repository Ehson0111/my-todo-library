//using System;
//using System.Collections.Generic;
//using System.Data.SqlTypes;
//using System.Diagnostics.Eventing.Reader;
//using System.Linq;
//using System.Net.Security;
//using System.Text;
//using System.Threading.Tasks;
//using Todo.Core.Tests;

//namespace TodoSolution
//{
//    internal class Program
//    {
//        static void Main(string[] args)
//        {

//            Console.WriteLine("Введите команду ");

//            Console.WriteLine("Simple Todo CLI. Commands: add <task>, remove <index>, list, done <index>, search <query>, clear, exit, Load path, Save path");

//            TodoList todolist = new TodoList();

//            while (true)
//            {
//                Console.WriteLine(">");
//                String line = Console.ReadLine();

//                string[] cmd = line.Split();

//                if (cmd.Length < 1)
//                {
//                    //Console.WriteLine(strings[1]);
//                    Console.WriteLine(cmd.Length);
//                    break;
//                }



//                switch (cmd[0].ToLower())
//                {

//                    case "add":

//                        if (cmd.Length > 1 && !string.IsNullOrWhiteSpace(cmd[1]))
//                        {
//                            todolist.Add(cmd[1]);
//                            Console.WriteLine("Задача добавлено ");

//                        }

//                        break;
//                    case "remove":

//                        if (cmd.Length>1  )
//                        {
//                            var d = Guid.TryParse(cmd[1], out Guid removedIndex);
//                            var item = todolist.Items.FirstOrDefault(x => x.Id == removedIndex);
//                            Console.WriteLine($"{removedIndex} {item}");
//                            if (item!=null)
//                            {
//                                todolist.Remove(item.Id);

//                                Console.WriteLine("Item removed.");
//                            }
//                            else
//                            {
//                                Console.WriteLine("Item not found.");

//                            }

//                        }
//                        else
//                        {
//                            Console.WriteLine("Не валидныные данные");
//                        }
//                        break;
//                    case "list":
//                        foreach (var item in todolist.Items) { Console.WriteLine($" {item.Title}"); }
//                        break;


//                }



//            }
//            Console.ReadKey();
//        }
//    }
//}
using System;
using System.Linq;
using Todo.Core.Tests;

namespace TodoSolution
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Simple Todo CLI. Commands: add <task>, remove <index>, list, done <index>, undone <index>, search <query>, clear, save <path>, load <path>, exit");

            TodoList todolist = new TodoList();

            while (true)
            {
                Console.Write("> ");
                string line = Console.ReadLine()?.Trim();

                if (string.IsNullOrEmpty(line)) continue;

                string[] cmd = line.Split(' ', 2);
                string command = cmd[0].ToLower();

                switch (command)
                {
                    case "add":
                        if (cmd.Length > 1 && !string.IsNullOrWhiteSpace(cmd[1]))
                        {
                            todolist.Add(cmd[1]);
                            Console.WriteLine("Задача добавлена");
                        }
                        else
                        {
                            Console.WriteLine("Укажите название задачи");
                        }
                        break;

                    case "remove":
                        if (cmd.Length > 1 && Guid.TryParse(cmd[1], out Guid removeId))
                        {
                            if (todolist.Remove(removeId))
                            {
                                Console.WriteLine("Задача удалена");
                            }
                            else
                            {
                                Console.WriteLine("Задача не найдена");
                            }
                        }
                        else
                        {
                            Console.WriteLine("Укажите корректный ID задачи");
                        }
                        break;

                    case "done":
                        if (cmd.Length > 1 && Guid.TryParse(cmd[1], out Guid doneId))
                        {
                            var doneItem = todolist.Items.FirstOrDefault(x => x.Id == doneId);
                            if (doneItem != null)
                            {
                                doneItem.MarkDone();
                                Console.WriteLine("Задача выполнена");
                            }
                            else
                            {
                                Console.WriteLine("Задача не найдена");
                            }
                        }
                        break;

                    case "undone":
                        if (cmd.Length > 1 && Guid.TryParse(cmd[1], out Guid undoneId))
                        {
                            var undoneItem = todolist.Items.FirstOrDefault(x => x.Id == undoneId);
                            if (undoneItem != null)
                            {
                                undoneItem.MarkUndone();
                                Console.WriteLine("Задача не выполнена");
                            }
                            else
                            {
                                Console.WriteLine("Задача не найдена");
                            }
                        }
                        break;

                    case "list":
                        Console.WriteLine($"Всего задач: {todolist.Count}");
                        foreach (var item in todolist.Items)
                        {
                            string status = item.IsDone ? "[✓]" : "[ ]";
                            Console.WriteLine($"{status} {item.Id} - {item.Title}");
                        }
                        break;

                    case "search":
                        if (cmd.Length > 1)
                        {
                            var results = todolist.Find(cmd[1]);
                            Console.WriteLine($"Найдено задач: {results.Count()}");
                            foreach (var item in results)
                            {
                                string status = item.IsDone ? "[✓]" : "[ ]";
                                Console.WriteLine($"{status} {item.Id} - {item.Title}");
                            }
                        }
                        break;

                    case "clear":
                        todolist.Clear();
                        Console.WriteLine("Все задачи удалены");
                        break;

                    case "save":
                        if (cmd.Length > 1)
                        {
                            todolist.Save(cmd[1]);
                        }
                        else
                        {
                            Console.WriteLine("Укажите путь для сохранения");
                        }
                        break;

                    case "load":
                        if (cmd.Length > 1)
                        {
                            todolist.Load(cmd[1]);
                        }
                        else
                        {
                            Console.WriteLine("Укажите путь для загрузки");
                        }
                        break;

                    case "exit":
                        Console.WriteLine("Выход...");
                        return;

                    default:
                        Console.WriteLine("Неизвестная команда");
                        break;
                }
            }
        }
    }
}
