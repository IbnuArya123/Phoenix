using Microsoft.EntityFrameworkCore.Query;
using Phoenix.DataAccess.Models;
using Phoenix.DTO;
using Phoenix.DTO.Room;
using Phoenix.DTO.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Phoenix.Provider {
    public class RoomProvider : BaseProvider {
        #region singleton
        private static RoomProvider _instance = new RoomProvider();

        public static RoomProvider GetSingleton() { return _instance; }

        private RoomProvider() { }
        #endregion

        public IndexRoomDTO GetRoomTable(int page, string roomNumber, string type, string status) {
            IndexRoomDTO dto;
            using(var dbContext = new PhoenixContext()) {
                var query = (from room in dbContext.Rooms
                            join resv in dbContext.Reservations on room.Number equals resv.RoomNumber into roomStat
                            from rr in roomStat.DefaultIfEmpty()
                            where room.Number.ToLower().Trim().Contains(roomNumber.ToLower().Trim()) && 
                            room.RoomType.ToLower().Trim().Contains(type.ToLower().Trim())
                            select new RoomRowDTO {
                                RoomNumber = room.Number,
                                Floor = room.Floor,
                                Cost = ToRupiah(room.Cost),
                                GuestLimit = room.GuestLimit,
                                Type = room.RoomType,
                                Status = (DateTime.Now < rr.CheckOut && rr.BookDate < DateTime.Now) ? "Booked" : "Vacant"
                            });
                query = query.Where(q =>
                            q.Status.ToLower().Contains(status.Trim().ToLower()) &&
                            q.RoomNumber.ToLower().Contains(roomNumber.Trim().ToLower()) &&
                            q.Type.ToLower().Contains(type.Trim().ToLower()));
                var totalPage = GetTotalPages(query.Count());
                query = query.Skip(GetSkip(page)).Take((int)_rowPerPage);
                dto = new IndexRoomDTO {
                    Table = query.Distinct().ToList(),
                    TotalPages = totalPage,
                };
            }
            return dto;
        }

        public Room InsertRoom(InsertRoomDTO dto) {  
            var entity = new Room { 
                Number = dto.RoomNumber,
                Floor = dto.Floor,
                Cost = dto.Cost,
                GuestLimit = dto.GuestLimit,
                RoomType = dto.RoomType,
                Description = dto.Description
            };
            using(var dbContext = new PhoenixContext()) {
                var result = dbContext.Rooms.Add(entity).Entity;
                dbContext.SaveChanges();
                return result;
            }
        }

        public UpdateRoomDTO GetSingleRoom(string roomNumber) {
            var dto = new UpdateRoomDTO();

            using(var dbContext = new PhoenixContext()) { 
                var selectedEntity = dbContext.Rooms.SingleOrDefault(r => r.Number == roomNumber);
                dto.RoomNumber = selectedEntity.Number;
                dto.Floor = selectedEntity.Floor; 
                dto.Cost = selectedEntity.Cost;
                dto.Description = selectedEntity.Description;
                dto.GuestLimit = selectedEntity.GuestLimit;
                dto.RoomType = selectedEntity.RoomType;
            }
            return dto;
        }

        public void UpdateRoom(UpdateRoomDTO dto) { 
            using(var dbContext = new PhoenixContext()) {
                var entity = dbContext.Rooms.SingleOrDefault(r => r.Number == dto.RoomNumber);
                entity.Cost = dto.Cost;
                entity.Description = dto.Description;
                entity.Floor = dto.Floor;
                entity.RoomType = dto.RoomType;
                entity.GuestLimit = dto.GuestLimit;
                dbContext.SaveChanges();
            }
        }

        public IndexRoomInventoryDTO GetRoomInventoryTable(string roomNumber, int page) {
            IndexRoomInventoryDTO dto;
            using(var dbContext = new PhoenixContext()) { 
                var selectedRoom = dbContext.Rooms.SingleOrDefault(room => room.Number == roomNumber);
                var query = from roomInv in dbContext.RoomInventories
                            join inv in dbContext.Inventories on roomInv.InventoryName equals inv.Name
                            where roomInv.RoomNumber == roomNumber && inv.Stock > 0
                            select new RoomInventoryRowDTO { 
                                Id = roomInv.Id,
                                Name = roomInv.InventoryName,
                                Stock = inv.Stock,
                                Quantity = roomInv.Quantity
                            };
                var totalPage = GetTotalPages(query.Count());
                query = query.Skip(GetSkip(page)).Take((int) _rowPerPage);
                dto = new IndexRoomInventoryDTO { 
                    Table = query.ToList(),
                    TotalPages = totalPage,
                    RoomFloor = selectedRoom.Floor,
                    RoomType = selectedRoom.RoomType,
                    RoomNumber = selectedRoom.Number,
                    GuestLimit = selectedRoom.GuestLimit
                };
            }
            return dto;
        }

        public RoomInventory InsertRoomInventory(UpsertRoomInventoryDTO dto) { 
            var entity = new RoomInventory { 
                InventoryName = dto.InventoryName,
                Quantity = dto.Quantity,
                RoomNumber = dto.RoomNumber
            };

            using(var dbContext = new PhoenixContext()) {
                var result = dbContext.RoomInventories.Add(entity).Entity;
                dbContext.SaveChanges();
                return result;
            }
        }

        public void DeleteRoomInventory(long id) {
            using(var dbContext = new PhoenixContext()) {
                var selectedEntity = dbContext.RoomInventories.SingleOrDefault(room => room.Id == id);
                dbContext.Remove(selectedEntity);
                dbContext.SaveChanges();
            }
        }

        public void StockChange(UpsertRoomInventoryDTO dto) { 
            using(var dbContext = new PhoenixContext()) { 
                var selectedInventory = dbContext.Inventories.SingleOrDefault(inv => inv.Name == dto.InventoryName);
                selectedInventory.Stock -= dto.Quantity;
                dbContext.SaveChanges();
            }
        }

        public List<DropdownDTO> GetRoomTypeDropdown() { 
            var dropdown = new List<DropdownDTO>();
            var roomTypes = Enum.GetValues(typeof(RoomType)).Cast<RoomType>().ToList();
            foreach (var roomType in roomTypes) { 
                dropdown.Add(new DropdownDTO { 
                    Value = roomType,
                    DisplayName = GetRoomType(roomType)
                });
            }
            return dropdown;
        }

        public RoomDetailDTO GetRoomDetail(string roomNumber, string status) {
            RoomDetailDTO dto;

            using(var dbContext = new PhoenixContext()) { 
                var selectedRoom = dbContext.Rooms.SingleOrDefault(room => room.Number == roomNumber);
                Console.WriteLine(selectedRoom.Reservations.Count());
                dto = new RoomDetailDTO { 
                    RoomNumber = selectedRoom.Number,
                    Floor = selectedRoom.Floor,
                    RoomType = selectedRoom.RoomType,
                    GuestLimit = selectedRoom.GuestLimit,
                    Cost = ToRupiah(selectedRoom.Cost),
                    Description = selectedRoom.Description,
                    Status = status
                };
                return dto;
            }
        }

        public List<DropdownDTO> GetInventoryDropdown() {
            var dropdown = new List<DropdownDTO>();
            using(var dbContext = new PhoenixContext()) { 
                var query = from inv in dbContext.Inventories
                            select new DropdownDTO { 
                                Value = inv.Name,
                                DisplayName = inv.Name
                            };
                dropdown = query.ToList();
            }
            return dropdown;
        }
    }
}
