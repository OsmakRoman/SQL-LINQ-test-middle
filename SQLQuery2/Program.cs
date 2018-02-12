using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Forecsys
{
    // Группа товаров – таблица Groups
    class Group
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }

    // Товар – таблица Products
    class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int GroupId { get; set; } // Идентификатор группы товаров (внешний ключ)
       
    }
    class Program
    {
        static void Main(string[] args)
        {
            IEnumerable<Group> groups = new List<Group>
            { new Group { Id=1, Name = "PHONES" },
              new Group { Id = 2, Name = "TABLETS" },
              new Group { Id = 3, Name = "TVS" },
              new Group { Id = 4, Name = "COMPUTERS" }

            };
          
            
            IEnumerable<Product> products = new List<Product>
            {
                new Product { Id = 1, Name = "Iphone 8", GroupId = 1 },
                new Product { Id = 2, Name = "Iphone 10", GroupId = 1 },
                new Product { Id = 3, Name = "Samsung S8", GroupId = 1 },
                new Product { Id = 4, Name = "Nokia 8", GroupId = 1},
                new Product { Id = 5, Name = "IPad 4", GroupId = 2 },
                new Product { Id = 6, Name = "Samsung Galaxy Tab", GroupId = 2 },
                new Product { Id = 7, Name = "Asus Nexus 7", GroupId = 2 },
                new Product { Id = 8, Name = "Samsung UHD", GroupId = 3 },
                new Product { Id = 9, Name = "LG FullHD", GroupId = 3 },
                new Product { Id = 10, Name = "Philips HD", GroupId = 3 },
                new Product { Id = 11, Name = "Intel i7", GroupId = 4 },
                new Product { Id = 12, Name = "AMD Ryzen", GroupId = 4 },
                new Product { Id = 13, Name = "Atari", GroupId = 4},
                new Product { Id = 14, Name = "SonyPlaystation", GroupId = 5}
            };


            var selected1 = products
                .GroupJoin(
                groups,
                e => e.GroupId,
                o => o.Id,
                (e, os) => os
                    .DefaultIfEmpty()
                    .Select(o => new
                    { Name = e.Name, GroupName = o != null ? o.Name : "No Group" }))
                        .SelectMany(r => r);

            var selected2 = from pr in products
                           join gr in groups on pr.GroupId equals gr.Id into pg
                           from sub in pg.DefaultIfEmpty()
                           select new { Name = pr.Name, GroupName = sub?.Name ?? "No Group" };

            foreach (var item in selected1)
                Console.WriteLine($"ProductName: {item.Name}, GroupName: {item.GroupName}");

            Console.ReadLine();

            foreach (var item in selected2)
                Console.WriteLine($"ProductName: {item.Name}, GroupName: {item.GroupName}");

            Console.ReadLine();





        }
  


    }

  
   

}


  
    
