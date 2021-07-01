using System;
using System.Collections.Generic;
using System.Linq;
using Catalog.Entities;

namespace Catalog.Repositories
{
    public class InMemoItemsRepository
    {
        private readonly List<Item> items = new()
        {
            new Item{ Id = Guid.NewGuid(), Name = "Potion", Price = 9, CreatedDate = DateTimeOffset.UtcNow },
            new Item{ Id = Guid.NewGuid(), Name = "Iron Sword", Price = 20, CreatedDate = DateTimeOffset.UtcNow },
            new Item{ Id = Guid.NewGuid(), Name = "Horse", Price = 6, CreatedDate = DateTimeOffset.UtcNow },
            new Item{ Id = Guid.NewGuid(), Name = "Drone", Price = 10, CreatedDate = DateTimeOffset.UtcNow },
            new Item{ Id = Guid.NewGuid(), Name = "Castle", Price = 32, CreatedDate = DateTimeOffset.UtcNow },
            new Item{ Id = Guid.NewGuid(), Name = "Games", Price = 3, CreatedDate = DateTimeOffset.UtcNow }
        };

        public IEnumerable<Item> GetItems()
        {
            return items;
        }

        public Item GetItem(Guid id)
        {
            return items.Where(item => item.Id == id).SingleOrDefault();
        }
    }
}