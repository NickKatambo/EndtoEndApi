using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Catalog.Api.Dtos;
using Catalog.Api.Entities;
using Catalog.Api.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Catalog.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ItemsController : ControllerBase
    {
        private readonly IItemsRepository repository;
        private readonly ILogger<ItemsController> logger;

        public ItemsController(IItemsRepository repository, ILogger<ItemsController> logger)
        {
            this.repository = repository;
            this.logger = logger;
        }

        // GET /items
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ItemDto>>> GetItemsAsync()
        {
            var items = (await repository.GetItemsAsync())
                        .Select(item => item.AsDto());  // Some nice stuff here about Data Transfer Object and Extension methods
            logger.LogInformation($"{DateTime.UtcNow.ToString("hh:mm:ss")} : Retrieed {items.Count()} items.");
            return Ok(items);
        }

        // GET /items/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<ItemDto>> GetItemAsync(Guid id) 
        {
            var item = await repository.GetItemAsync(id);

            if(item is null)
            {
                return NotFound();
            }
            return Ok(item.AsDto());
        }

        // POST /items
        [HttpPost]
        public async Task<ActionResult<ItemDto>> CreateItem(CreateItemDto itemDto)
        {
            Item item = new()
            {
                Id = Guid.NewGuid(),
                Name = itemDto.Name,
                Price = itemDto.Price,
                CreatedDate = DateTimeOffset.UtcNow
            };

            await repository.CreateItemAsync(item);
            return CreatedAtAction(nameof(GetItemAsync), new { id = item.Id}, item.AsDto());
        }

        // PUT /items/{id}
        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateItemAsync(Guid id, UpdateItemDto itemDto)
        {
            var existingItem = await repository.GetItemAsync(id);

            if(existingItem is null)
            {
                return NotFound();
            }

            Item updateItem = existingItem with
            {
                Name = itemDto.Name,
                Price = itemDto.Price
            };

            await repository.UpdateItemAsync(updateItem);
            
            return NoContent();
        }

        // DELETE /items/{id}
        [HttpDelete]
        public async Task<ActionResult> DeleteItemAsync(Guid id)
        {
            var existingItem = await repository.GetItemAsync(id);

            if(existingItem is null)
            {
                return NotFound();
            }

            await repository.DeleteItemAsync(id);

            return NoContent();
        }
    }
}