using Dapper;
using Dapper.Contrib.Extensions;
using DbUp;
using Shop.Domain;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;

namespace PaginationDapper
{
  class Program
  {
    private const string CONNECTION_STRING = "Server=A-305-07;Database=ShopDapperT;Trusted_Connection=True;";
    static void Main(string[] args)
    {
      EnsureDatabase.For.SqlDatabase(CONNECTION_STRING);

      var upgrader =
        DeployChanges.To
            .SqlDatabase(CONNECTION_STRING)
            .WithScriptsEmbeddedInAssembly(Assembly.GetExecutingAssembly())
            .LogToConsole()
            .Build();

      upgrader.PerformUpgrade();

      //InsertTable();

      Console.WriteLine("Введите имя товара: ");
      var nameItem = Console.ReadLine();
      List<Item> items = new List<Item>();
      items = Search(nameItem) as List<Item>;

      Console.WriteLine("Введите страницу: ");
      int page = 1, count = 3;
      if(int.TryParse(Console.ReadLine(), out page))
      {
        var paginationResult = ToPagination(items, page, count);
        foreach(var item in paginationResult)
        {
          Console.WriteLine($"{item.Name} | {item.Price} | {item.Description} | {item.CategoryId} |");
        }
        if(paginationResult.Count == 0)
        {
          Console.WriteLine("Пусто!");
        }
      }
      else
      {
        Console.WriteLine("Не корректно!");
      }
    }

    public static List<Item> ToPagination(List<Item> result,int page = 0, int count = 0 )
    {
      var paginationResult = result.Skip(page * count).Take(count);
      var items = paginationResult.ToList();

      return items;
    }

    private static ICollection<Item> Search(string nameItem)
    {
      string sql = "Select * from Items where Name = @Name";
      List<Item> items = new List<Item>();
      using (var connection = new SqlConnection(CONNECTION_STRING))
      {
        items = connection.Query<Item>(sql, new { Name = nameItem }).ToList();
      }
      return items;
    }

    private static void InsertTable()
    {
      List<Category> categories = new List<Category>
      {
        new Category
        {
          Name = "Phone",
          ImagePath = "C:/data"
        },
        new Category
        {
          Name = "Computer",
          ImagePath = "C:/data"
        },
        new Category
        {
          Name = "Furniture",
          ImagePath = "C:/data"
        },
        new Category
        {
          Name = "Things",
          ImagePath = "C:/data"
        },
        new Category
        {
          Name = "Paper",
          ImagePath = "C:/data"
        },
        new Category
        {
          Name = "Flats",
          ImagePath = "C:/data"
        }
      };
      List<Item> items = new List<Item>
      {
        new Item
        {
          Name = "Iphone 11",
          ImagePath = "C:/data",
          Price = 785_990,
          Description = "Cool, but too expensive",
          CategoryId = categories[0].Id
        },
        new Item
        {
          Name = "Froze",
          ImagePath = "C:/data",
          Price = 200000,
          Description = "Very cold",
          CategoryId = categories[2].Id
        },
        new Item
        {
          Name = "Froze 11",
          ImagePath = "C:/data",
          Price = 905_990,
          Description = "Cool, but too expensive",
          CategoryId = categories[0].Id
        },
        new Item
        {
          Name = "Mac Pro",
          ImagePath = "C:/data",
          Price = 1000_990,
          Description = "Good, but too expensive",
          CategoryId = categories[0].Id
        },
        new Item
        {
          Name = "Mac Pro",
          ImagePath = "C:/data",
          Price = 500000,
          Description = "i buy it",
          CategoryId = categories[0].Id
        },
        new Item
        {
          Name = "Mac Pro",
          ImagePath = "C:/data",
          Price = 780000,
          Description = "Cool, but too expensive",
          CategoryId = categories[0].Id
        },
        new Item
        {
          Name = "Mac Pro",
          ImagePath = "C:/data",
          Price = 800000,
          Description = "Cool, but too expensive",
          CategoryId = categories[0].Id
        },
        new Item
        {
          Name = "Mac Pro",
          ImagePath = "C:/data",
          Price = 999990,
          Description = "Cool, but too expensive",
          CategoryId = categories[0].Id
        },
        new Item
        {
          Name = "Mac Pro",
          ImagePath = "C:/data",
          Price = 999_999_999,
          Description = "What",
          CategoryId = categories[0].Id
        }
      };
      string sql = "Insert into Items (@Id, @CreationDate, @DeletedDate, @Name, @ImagePath, @Price, @Description, @CategoryId);";
      using (var connection = new SqlConnection(CONNECTION_STRING))
      {
        foreach (var item in items)
        {
          connection.Insert(item);
        }
      }
    }
  }
}
