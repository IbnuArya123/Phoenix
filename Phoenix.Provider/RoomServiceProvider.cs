using Phoenix.DataAccess.Models;
using Phoenix.DTO.RoomService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Phoenix.Provider {
    public class RoomServiceProvider : BaseProvider {
        #region singleton
        private static RoomServiceProvider _instance = new RoomServiceProvider();

        public static RoomServiceProvider GetSingleton() { return _instance; }

        private RoomServiceProvider() { }
        #endregion

        #region Room Service

        public IndexRoomServiceDTO GetRoomServiceTable(string empNumber, string fullname, int page) {
            IndexRoomServiceDTO dto;
            using (var dbContext = new PhoenixContext()) { 
                var query = from service in dbContext.RoomServices
                            where service.EmployeeNumber.ToLower().Contains(empNumber.ToLower().Trim()) &&
                            (service.FirstName.ToLower() + service.MiddleName.ToLower() + service.LastName.ToLower()).Contains(fullname.ToLower().Trim())
                            select new RoomServiceRowDTO {
                                EmployeeNumber = service.EmployeeNumber,
                                FullName = service.FirstName + " " + service.MiddleName + " " + service.LastName,
                                Company = service.OutsourcingCompany
                            };
                var totalPages = GetTotalPages(query.Count());
                query = query.Skip(GetSkip(page)).Take((int)_rowPerPage);

                dto = new IndexRoomServiceDTO { 
                    Table = query.ToList(),
                    TotalPages = totalPages
                };
            }
            return dto;
        }

        public RoomService InsertRoomService(InsertRoomServiceDTO dto) {
            var entity = new RoomService { 
                EmployeeNumber = dto.EmployeeNumber,
                FirstName = dto.FirstName,
                MiddleName = dto.MiddleName,
                LastName = dto.LastName,
                OutsourcingCompany = dto.Company
            };
            using(var dbContext = new PhoenixContext()) { 
                var result = dbContext.RoomServices.Add(entity).Entity;
                dbContext.SaveChanges();
                return result;
            }
        }

        public UpdateRoomServiceDTO GetSingleRoomService(string empNumber) {
            UpdateRoomServiceDTO dto;

            using(var dbContext = new PhoenixContext()) {
                var selectedEntity = dbContext.RoomServices.SingleOrDefault(serv => serv.EmployeeNumber == empNumber);
                dto = new UpdateRoomServiceDTO {
                    EmployeeNumber = selectedEntity.EmployeeNumber,
                    FirstName = selectedEntity.FirstName,
                    MiddleName = selectedEntity.MiddleName,
                    LastName = selectedEntity.LastName,
                    Company = selectedEntity.OutsourcingCompany
                };
            }
            return dto;
        }

        public void UpdateRoomService(UpdateRoomServiceDTO dto) { 
            using(var dbContext = new PhoenixContext()) {
                var entity = dbContext.RoomServices.SingleOrDefault(serv => serv.EmployeeNumber == dto.EmployeeNumber);
                entity.FirstName = dto.FirstName;
                entity.MiddleName = dto.MiddleName;
                entity.LastName = dto.LastName;
                entity.OutsourcingCompany = dto.Company;
                dbContext.SaveChanges();
            }
        }

        #endregion

        #region Room Roster

        public IndexRosterDTO GetRosterIndex(string empNumber) {
            IndexRosterDTO dto;
            using(var dbContext = new PhoenixContext()) { 
                var selectedEntity = dbContext.RoomServices.SingleOrDefault(ros => ros.EmployeeNumber == empNumber);
                dto = new IndexRosterDTO { 
                    EmployeeNumber = selectedEntity.EmployeeNumber,
                    FullName = selectedEntity.FirstName + " " + selectedEntity.MiddleName + " " + selectedEntity.LastName,
                    Company = selectedEntity.OutsourcingCompany,
                    MondayRosterStart = selectedEntity.MondayRosterStart,
                    MondayRosterFinish = selectedEntity.MondayRosterFinish,
                    TuesdayRosterStart = selectedEntity.TuesdayRosterStart,
                    TuesdayRosterFinish = selectedEntity.TuesdayRosterFinish,
                    WednesdayRosterStart = selectedEntity.WednesdayRosterStart,
                    WednesdayRosterFinish = selectedEntity.WednesdayRosterFinish,
                    ThursdayRosterStart = selectedEntity.ThursdayRosterStart,
                    ThursdayRosterFinish = selectedEntity.ThursdayRosterFinish,
                    FridayRosterStart = selectedEntity.FridayRosterStart,
                    FridayRosterEnd = selectedEntity.FridayRosterEnd,
                    SaturdayRosterStart = selectedEntity.SaturdayRosterStart,
                    SaturdayRosterFinish = selectedEntity.SaturdayRosterFinish,
                    SundayRosterStart = selectedEntity.SundayRosterStart,
                    SundayRosterFinish = selectedEntity.SundayRosterFinish
                };
            }
            return dto;
        }

        public void EditRoster(UpsertRosterDTO dto) {
            using(var dbContext = new PhoenixContext()) { 
                var selectedEntity = dbContext.RoomServices.SingleOrDefault(rs => rs.EmployeeNumber == dto.EmployeeNumber);
                selectedEntity.MondayRosterStart = dto.MondayRosterStart;
                selectedEntity.MondayRosterFinish = dto.MondayRosterFinish;
                selectedEntity.TuesdayRosterStart = dto.TuesdayRosterStart;
                selectedEntity.TuesdayRosterFinish = dto.TuesdayRosterFinish;
                selectedEntity.WednesdayRosterStart = dto.WednesdayRosterStart;
                selectedEntity.WednesdayRosterFinish = dto.WednesdayRosterFinish;
                selectedEntity.ThursdayRosterStart = dto.ThursdayRosterStart;
                selectedEntity.ThursdayRosterFinish = dto.ThursdayRosterFinish;
                selectedEntity.FridayRosterStart = dto.FridayRosterStart;
                selectedEntity.FridayRosterEnd = dto.FridayRosterEnd;
                selectedEntity.SaturdayRosterStart = dto.SaturdayRosterStart;
                selectedEntity.SaturdayRosterFinish = dto.SaturdayRosterFinish;
                selectedEntity.SundayRosterStart = dto.SundayRosterStart;
                selectedEntity.SundayRosterFinish = dto.SundayRosterFinish;
                dbContext.SaveChanges();
            }
        }

        #endregion
    }
}
