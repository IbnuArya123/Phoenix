using Phoenix.DataAccess.Models;
using Phoenix.DTO.Reservation;
using Phoenix.DTO.Room;
using System.Globalization;

namespace Phoenix.Provider {
    public abstract class BaseProvider {

        protected const double _rowPerPage = 10;

        protected static int GetTotalPages(int totalData) {
            double result = (double)totalData / _rowPerPage;
            return (int)Math.Ceiling(result);
        }

        protected static int GetSkip(int page) {
            return (page - 1) * (int)_rowPerPage;
        }

        protected static string ToRupiah(double money) { 
            var indonesia = CultureInfo.CreateSpecificCulture("id-ID");
            return money.ToString("C2", indonesia);
        }

        protected static string ToRupiah(decimal money) { 
            var indonesia = CultureInfo.CreateSpecificCulture("id-ID");
            return money.ToString("C2", indonesia);
        }

        //Convert Date dari ke database menjadi date Indonesia
        public static string ToIndoDate(DateTime? date) {
            if (date != null)
            {
                var indonesia = CultureInfo.GetCultureInfo("id-ID");
                return ((DateTime)date).ToString("dd-MM-yyyy", indonesia);
            }
            return null;
        }

        protected static string DateToHTML(DateTime date) {
            return date.ToString("yyyy-MM-dd");
        }


        //Date inputconverter
        protected static DateTime HTMLDateToDatabase(string date) {
            return DateTime.Parse(date);
        }

        protected static string GetBookingCode(string roomNumber, int count) {
            return $"{roomNumber}-{ToIndoDate(DateTime.Now)}-{count+1}";
        }

        protected static string GetRoomType(RoomType roomType) { 
            switch(roomType) {
                case RoomType.RS:
                    return "Regular Single Bed";
                case RoomType.RD:
                    return "Regular Double Bed";
                case RoomType.RT:
                    return "Regular Twin Bed";
                case RoomType.VS:
                    return "VIP Single Bed";
                case RoomType.VD:
                    return "VIP Double Bed";
                case RoomType.VT:
                    return "VIP Twin Bed";
                default:
                    return "Tidak ada type yang tersedia";
            }
        }

        protected static string GetPaymentMethod(PaymentMethod paymentMethod) { 
            switch(paymentMethod) {
                case PaymentMethod.CC:
                    return "Credit Card";
                case PaymentMethod.DD:
                    return "Debit Card";
                case PaymentMethod.VO:
                    return "Voucher";
                case PaymentMethod.EM:
                    return "Electric Money";
                case PaymentMethod.CA:
                    return "Cash";
                default:
                    return "Tidak ada payment yang tersedia";
            }
        }
    }
}