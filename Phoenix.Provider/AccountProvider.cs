using Phoenix.DataAccess.Models;
using Phoenix.DTO.Account;
using System.Security.Claims;

namespace Phoenix.Provider {
    public class AccountProvider : BaseProvider {
        #region singleton
        private static AccountProvider _instance = new AccountProvider();

        public static AccountProvider GetSingleton() { return _instance; }

        private AccountProvider() { }
        #endregion

        public IndexAdminDTO GetAdminTable(int page) {
            IndexAdminDTO dto;
            using(var dbContext = new PhoenixContext()) { 
                var query = from acc in dbContext.Administrators
                            select new AdminRowDTO { 
                                Username = acc.Username,
                                JobTitle = acc.JobTitle,
                            };
                var totalPages = GetTotalPages(query.Count());
                query = query.Skip(GetSkip(page)).Take((int)_rowPerPage);

                dto = new IndexAdminDTO { 
                    Table = query.ToList(),
                    TotalPages = totalPages,
                };
            }
            return dto;
        }

        public UpdateAdminDTO GetSingleAdmin(string username) {
            UpdateAdminDTO dto;

            using(var dbContext = new PhoenixContext()) {
                var selectedEntity = dbContext.Administrators.SingleOrDefault(acc => acc.Username == username);
                dto = new UpdateAdminDTO { 
                    Username = selectedEntity.Username,
                    JobTitle= selectedEntity.JobTitle,
                };
            }
            return dto;
        }

        public Administrator RegisterAdmin(InsertAdminDTO dto) {
            var password = "Admin";
            var hashPassword = BCrypt.Net.BCrypt.HashPassword(password);
            var entity = new Administrator {
                Username = dto.Username,
                Password = hashPassword,
                JobTitle = dto.JobTitle
            };

            using (var dbContext = new PhoenixContext()) { 
                var newData = dbContext.Administrators.Add(entity).Entity;
                dbContext.SaveChanges();
                return newData;
            }
        }

        public void UpdateAdmin(UpdateAdminDTO dto) {
            using(var dbContext = new PhoenixContext()) {
                var selectedEntity = dbContext.Administrators.SingleOrDefault(acc => acc.Username == dto.Username);
                selectedEntity.Username = dto.Username;
                selectedEntity.JobTitle = dto.JobTitle;
                dbContext.SaveChanges();
            }
        }

        public void ChangePassword(ChangeAccountPasswordDTO dto) {
            var hashPassword = BCrypt.Net.BCrypt.HashPassword(dto.NewPassword);
            if (dto.Role == "Administrator") {
                using(var dbContext = new PhoenixContext()) {
                    var selectedEntity = dbContext.Administrators.SingleOrDefault(acc => acc.Username == dto.Username);
                    selectedEntity.Password = hashPassword;
                    dbContext.SaveChanges();
                }
            } else if (dto.Role == "Guest") { 
                using(var dbContext = new PhoenixContext()) {
                    var selectedEntity = dbContext.Guests.SingleOrDefault(acc => acc.Username == dto.Username);
                    selectedEntity.Password = hashPassword;
                    dbContext.SaveChanges();
                }
            }
        }

        public void RegisterGuest(UpsertGuestDTO dto) {
            string hashPassword = BCrypt.Net.BCrypt.HashPassword(dto.Password);
            using(var dbContext = new PhoenixContext()) { 
                var entity = new Guest { 
                    Username = dto.Username,
                    Password = hashPassword,
                    FirstName = dto.FirstName,
                    MiddleName = dto.MiddleName,
                    LastName = dto.LastName,
                    BirtDate = HTMLDateToDatabase(dto.BirthDate),
                    Gender = dto.Gender,
                    Citizenship = dto.Citizenship,
                    IdNumber = dto.IDNumber
                };
                dbContext.Add(entity);
                dbContext.SaveChanges();
            }
        }

        public bool IsAuthenticated(LoginDTO dto) { 
            using (var dbContext = new PhoenixContext()) { 
                if(dto.Role == "Guest") { 
                    var existingGuest = dbContext.Guests.SingleOrDefault(acc => acc.Username == dto.Username);
                    if (existingGuest != null) { 
                        return BCrypt.Net.BCrypt.Verify(dto.Password, existingGuest.Password);
                    }
                    return false;
                } else if(dto.Role == "Administrator") { 
                    var existingAdmin = dbContext.Administrators.SingleOrDefault(acc => acc.Username == dto.Username);
                    if (existingAdmin != null) { 
                        return BCrypt.Net.BCrypt.Verify(dto.Password, existingAdmin.Password);
                    }
                    return false;
                }
            }
            return false;
        }
    }
}
