using Phoenix.DataAccess.Models;
using Phoenix.DTO.Inventory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Phoenix.Provider {
    public class InventoryProvider : BaseProvider {

        #region singleton
        private static InventoryProvider _instance = new InventoryProvider();

        public static InventoryProvider GetSingleton() { return _instance; }

        private InventoryProvider() { }
        #endregion

        public IndexInventoryDTO GetInventoryTable(int page) {
            IndexInventoryDTO dto;
            using (var dbContext = new PhoenixContext()) { 
                var query = from inv in dbContext.Inventories
                            select new InventoryRowDTO { 
                                Name = inv.Name,
                                Stock = inv.Stock,
                                Description = inv.Description
                            };
                var totalPages = GetTotalPages(query.Count());
                query = query.Skip(GetSkip(page)).Take((int)_rowPerPage);

                dto = new IndexInventoryDTO { 
                    Table = query.ToList(),
                    TotalPages = totalPages
                };
            }
            return dto;
        }

        public Inventory InsertInventory(UpsertInventoryDTO dto) {
            var entity = new Inventory {
                Name = dto.Name,
                Stock = dto.Stock,
                Description = dto.Description,
            };
            using (var dbContext = new PhoenixContext()) { 
                var newData = dbContext.Inventories.Add(entity).Entity;
                dbContext.SaveChanges();
                return newData;
            }
        }

        public UpsertInventoryDTO GetSingleInventory(string name) {
            UpsertInventoryDTO dto;
            using (var dbContext = new PhoenixContext()) { 
                var selectedData = dbContext.Inventories.SingleOrDefault(inv => inv.Name == name);
                dto = new UpsertInventoryDTO {
                    Name = selectedData.Name,
                    Stock = selectedData.Stock,
                    Description = selectedData.Description,
                };
            }
            return dto;
        }

        public void UpdateInventory(UpsertInventoryDTO dto) {
            using (var dbContext = new PhoenixContext()) {
                var selectedData = dbContext.Inventories.SingleOrDefault(inv => inv.Name == dto.Name);
                selectedData.Name = dto.Name;
                selectedData.Stock = dto.Stock;
                selectedData.Description = dto.Description;
                dbContext.SaveChanges();
            }
        }

        public int DeleteInventory(string name) {
            int totalFK = 0;
            using (var dbContext = new PhoenixContext()) {
                totalFK = dbContext.RoomInventories.Where(roomInv => roomInv.InventoryName == name).Count();

                if (totalFK == 0) {
                var selectedEntity = dbContext.Inventories.SingleOrDefault(inv => inv.Name == name);
                    dbContext.Inventories.Remove(selectedEntity);
                    dbContext.SaveChanges();
                }
            }
            return totalFK;
        }

        public List<string> GetName() {
            List<string> dtos = new List<string>();
            using(var dbContext = new PhoenixContext()) { 
                var query = from inv in dbContext.Inventories
                            select inv.Name;
                dtos = query.ToList();
            }
            return dtos;
        }
    }
}
