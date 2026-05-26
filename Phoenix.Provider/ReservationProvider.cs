using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Phoenix.DataAccess.Models;
using Phoenix.DTO;
using Phoenix.DTO.Reservation;
using Phoenix.DTO.Room;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Phoenix.Provider {
    public class ReservationProvider : BaseProvider {
        #region singleton
        private static ReservationProvider _instance = new ReservationProvider();

        public static ReservationProvider GetSingleton() { return _instance; }

        private ReservationProvider() { }
        #endregion

        public IndexReservationDTO GetReservationLog(string roomNumber, int page) {
            IndexReservationDTO dto;
            using(var dbContext = new PhoenixContext()) {
                var query = from resv in dbContext.Reservations
                            where resv.RoomNumber.ToLower().Contains(roomNumber.ToLower())
                            select new ReservationRowDTO {
                                Code = resv.Code,
                                RoomNumber = resv.RoomNumber,
                                Username = resv.GuestUsername,
                                BookDate = ToIndoDate(resv.BookDate),
                                CheckIn = ToIndoDate(resv.CheckIn),
                                CheckOut = ToIndoDate(resv.CheckOut),
                                PaymentDate = ToIndoDate(resv.PaymentDate)
                            };
                var totalPages = GetTotalPages(query.Count());
                query = query.Skip(GetSkip(page)).Take((int)_rowPerPage);

                dto = new IndexReservationDTO { 
                    Table = query.ToList(),
                    TotalPages = totalPages
                };
            }
            return dto;
        }

        public ReservationDetailDTO GetSingleReservationDetail(string code) {
            ReservationDetailDTO dto;
            using (var dbContext = new PhoenixContext())
            {
                var selectedReservation = dbContext.Reservations.SingleOrDefault(resv => resv.Code == code);
                var selectedRoom = dbContext.Rooms.SingleOrDefault(room => room.Number == selectedReservation.RoomNumber);
                var selectedGuest = dbContext.Guests.SingleOrDefault(guest => guest.Username == selectedReservation.GuestUsername);
                dto = new ReservationDetailDTO
                {
                    ReservationCode = selectedReservation.Code,
                    ReservationMethod = selectedReservation.ReservationMethod,
                    RoomNumber = selectedReservation.RoomNumber,
                    RoomFloor = selectedRoom.Floor,
                    RoomType = selectedRoom.RoomType,
                    GuestUsername = selectedReservation.GuestUsername,
                    GuestFullName = selectedGuest.FirstName + " " + selectedGuest.MiddleName + " " + selectedGuest.LastName,
                    BookDate = ToIndoDate(selectedReservation.BookDate),
                    CheckIn = ToIndoDate(selectedReservation.CheckIn),
                    CheckOut = ToIndoDate(selectedReservation.CheckOut),
                    Cost = ToRupiah(selectedReservation.Cost),
                    PaymentDate = ToIndoDate(selectedReservation.BookDate),
                    PaymentMethod = selectedReservation.PaymentMethod,
                    Remark = selectedReservation.Remark
                };
            }
            return dto;
        }

        public InsertReservationDTO GetSingleRoomReservation(string roomNumber, string username) {
            InsertReservationDTO dto;
            using(var dbContext = new PhoenixContext()) { 
                var selectedRoom = dbContext.Rooms.SingleOrDefault(r => r.Number ==  roomNumber);
                var selectedGuest = dbContext.Guests.SingleOrDefault(g => g.Username == username);
                dto = new InsertReservationDTO {
                    RoomNumber = selectedRoom.Number,
                    RoomType = selectedRoom.RoomType,
                    Floor = selectedRoom.Floor,
                    Cost = selectedRoom.Cost,
                    GuestLimit = selectedRoom.GuestLimit,
                    Username = selectedGuest.Username
                };
            }
            return dto;
        }

        public Reservation InsertNewReservation(InsertReservationDTO dto) {
            var entity = new Reservation { 
                ReservationMethod = "OW",
                RoomNumber = dto.RoomNumber,
                GuestUsername = dto.Username,
                BookDate = DateTime.Now,
                CheckIn = HTMLDateToDatabase(dto.CheckIn),
                CheckOut = HTMLDateToDatabase(dto.CheckOut),
                Cost = dto.TotalCost,
                PaymentDate = DateTime.Now,
                PaymentMethod = dto.PaymentMethod,
                Remark = dto.Remark
            };
            using (var dbContext = new PhoenixContext()) {
                int count = dbContext.Reservations.Count();
                entity.Code = GetBookingCode(dto.RoomNumber, count);
                var result = dbContext.Reservations.Add(entity).Entity;
                dbContext.SaveChanges();
                return result;
            }
        }

        public IndexGuestRoomDetailDTO GetGuestBookedRoom(string username) {
            IndexGuestRoomDetailDTO dto;
            using(var dbContext = new PhoenixContext()) { 
                var query = from resv in dbContext.Reservations
                            join room in dbContext.Rooms on resv.RoomNumber equals room.Number into roomStat
                            from rr in roomStat.DefaultIfEmpty()
                            where resv.GuestUsername == username
                            select new GuestRoomDetailRowDTO {
                                RoomNumber = rr.Number,
                                Floor = rr.Floor,
                                RoomType = rr.RoomType,
                                GuestLimit = rr.GuestLimit,
                                Cost = ToRupiah(rr.Cost),
                                CheckIn = ToIndoDate(resv.CheckIn),
                                CheckOut = ToIndoDate(resv.CheckOut),
                                TotalCost = ToRupiah(resv.Cost),
                                PaymentDate = ToIndoDate(resv.PaymentDate),
                                PaymentMethod = resv.PaymentMethod,
                                Remark = resv.Remark,
                                BookingStatus = (DateTime.Now > resv.CheckOut) ? "Expired" : "On-going"
                            };
                dto = new IndexGuestRoomDetailDTO {
                    Table = query.ToList()
                };
            }
            return dto;
        }

        public string GetTotalIncome(MonthAndYearIncomeDTO dto) {
            decimal totalCost = 0;
            using(var dbContext = new PhoenixContext()) {
                var query = from resv in dbContext.Reservations
                            where resv.BookDate.Month == dto.Month + 1 && resv.BookDate.Year == dto.Year
                            select resv.Cost;
                foreach(var cost in query.ToList()) {
                    totalCost += cost;
                }
            }
            return ToRupiah(totalCost);
        }

        public List<DropdownDTO> GetPaymentMethodDropdown() { 
            var dropdown = new List<DropdownDTO>();
            var paymentMethod = Enum.GetValues(typeof(PaymentMethod)).Cast<PaymentMethod>().ToList();
            foreach (var payment in paymentMethod) { 
                dropdown.Add(new DropdownDTO { 
                    Value = payment,
                    DisplayName = GetPaymentMethod(payment)
                });
            }
            return dropdown;
        }

        public List<DropdownDTO> GetYearDropdown() {
            var dropdown = new List<DropdownDTO>();
            for(int index = DateTime.Now.Year; index <= DateTime.Now.Year + 20; index++) {
                dropdown.Add(new DropdownDTO { 
                    Value = index,
                    DisplayName = index.ToString()
                });
            }
            return dropdown;
        }

        public List<DropdownDTO> GetMonthDropdown() {
            var dropdown = new List<DropdownDTO>();
            for(int index = 0; index <= 11; index++) {
                dropdown.Add(new DropdownDTO { 
                    Value = index,
                    DisplayName = DateTimeFormatInfo.InvariantInfo.MonthNames[index]
                });
            }
            return dropdown;
        }

    }
}
