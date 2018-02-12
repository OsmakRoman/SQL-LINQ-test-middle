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
        public Group Group { get; set; } // Navigation property
    }
    class Program
    {
        static void Main(string[] args)
        {
            Group phones = new Group { Id=1, Name = "PHONES" };
            Group tablets = new Group { Id = 2, Name = "TABLETS" };
            Group tvs = new Group { Id = 3, Name = "TVS" };
            Group computers = new Group { Id = 4, Name = "COMPUTERS" };
            
            IEnumerable<Product> products = new List<Product>
            {
                new Product { Id = 1, Name = "Iphone 8", GroupId = 1, Group = phones },
                new Product { Id = 2, Name = "Iphone 10", GroupId = 1, Group = phones },
                new Product { Id = 3, Name = "Samsung S8", GroupId = 1, Group = phones },
                new Product { Id = 4, Name = "Nokia 8", GroupId = 1, Group = phones },
                new Product { Id = 5, Name = "IPad 4", GroupId = 2, Group = tablets },
                new Product { Id = 6, Name = "Samsung Galaxy Tab", GroupId = 2, Group = tablets },
                new Product { Id = 7, Name = "Asus Nexus 7", GroupId = 2, Group = tablets },
                new Product { Id = 8, Name = "Samsung UHD", GroupId = 3, Group = tvs },
                new Product { Id = 9, Name = "LG FullHD", GroupId = 3, Group = tvs },
                new Product { Id = 10, Name = "Philips HD", GroupId = 3, Group = tvs },
                new Product { Id = 11, Name = "Intel i7", GroupId = 4, Group = computers },
                new Product { Id = 12, Name = "AMD Ryzen", GroupId = 4, Group = computers },
                new Product { Id = 13, Name = "Atari", GroupId = 4, Group = computers },
            };

            List<int> productIds = new List<int> {2,5,8,11,13};

            var selected1 = products
                .Join(
                productIds,
                e => e.Id,
                o => o,
                (e, o) => new { Name = e.Name, Group = e.Group.Name });

            var selected2 = from pr in products
                           join prid in productIds on pr.Id equals prid
                           select new { Name = pr.Name, Group = pr.Group.Name };

            foreach (var item in selected1)
                Console.WriteLine($"ProductName: {item.Name}, GroupName: {item.Group}");

            Console.ReadLine();

            foreach (var item in selected2)
                Console.WriteLine($"ProductName: {item.Name}, GroupName: {item.Group}");

            Console.ReadLine();





        }
  


    }

  
   

}


  
    
