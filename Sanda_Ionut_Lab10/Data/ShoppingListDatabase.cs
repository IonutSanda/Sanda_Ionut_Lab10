using SQLite;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Sanda_Ionut_Lab10.Models;

namespace Sanda_Ionut_Lab10.Data
{
    public class ShoppingListDatabase
    {
        readonly SQLiteAsyncConnection _database;

        public ShoppingListDatabase(string dbPath)
        {
            _database = new SQLiteAsyncConnection(dbPath);
            _database.CreateTableAsync<ShopList>().Wait();
        }

        public Task<List<ShopList>> GetShopListsAsync()
        {
            return _database.Table<ShopList>().ToListAsync();
        }

        public Task<ShopList> GetShopListAsync(int id)
        {
            return _database.Table<ShopList>()
                .Where(i => i.ID == id)
                .FirstOrDefaultAsync();
        }

        public Task<int> SaveShopListAsync(ShopList sList)
        {
            if(sList.ID != 0)
            {
                return _database.UpdateAsync(sList);
            } else
            {
                return _database.InsertAsync(sList);
            }
        }

        public Task<int> DeleteShopListAsync(ShopList sList)
        {
            return _database.DeleteAsync(sList);
        }
    }
}
