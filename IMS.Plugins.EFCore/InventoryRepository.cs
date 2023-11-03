using IMS.CoreBusiness;
using IMS.UseCases.PluginInterfaces;
using Microsoft.EntityFrameworkCore;

namespace IMS.Plugins.EFCore
{
    public class InventoryRepository : IInventoryRepository
    {
        private readonly IMSContext _context;
        public InventoryRepository(IMSContext context)
        {
            _context = context;
        }

        public async Task AddInventoryAsync(Inventory inventory)
        {
            _context.Inventories.Add(inventory);
            await _context.SaveChangesAsync();

        }

        public async Task<bool> ExistsAsync(Inventory inventory)
        {
            return await Task.FromResult(_context.Inventories.Any(x => x.InventoryName.Equals(inventory.InventoryName, StringComparison.OrdinalIgnoreCase)));
        }

        public async Task<IEnumerable<Inventory>> GetInventoriesByNameAsync(string name)
        {

            return await _context.Inventories.Where(x => x.InventoryName.Contains(name) ||
            string.IsNullOrWhiteSpace(name)).ToListAsync();
        }

        public async Task<Inventory> GetInventoryByIdAsync(int inventoryId)
        {
            var inv = await _context.Inventories.FirstAsync(x => x.InventoryId == inventoryId);
            var newInv = new Inventory
            {
                InventoryId = inv.InventoryId,
                InventoryName = inv.InventoryName,
                Quantity = inv.Quantity,
                Price = inv.Price
            };

            return await Task.FromResult(newInv);
        }

        public async Task UpdateInventoryAsync(Inventory inventory)
        {
            var inv = _context.Inventories.FirstOrDefault(x => x.InventoryId == inventory.InventoryId);

            if (inv != null)
            {
                inv.InventoryName = inventory.InventoryName;
                inv.Quantity = inventory.Quantity;
                inv.Price = inventory.Price;
                _context.Inventories.Update(inv);
            }
            await _context.SaveChangesAsync();
        }
    }
}
