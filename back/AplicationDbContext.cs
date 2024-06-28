using back.Models;
using Microsoft.EntityFrameworkCore;
using MySqlConnector;

namespace back
{
    public class AplicationDbContext: DbContext
    {
       public DbSet<Info> Info { get; set; }

        public AplicationDbContext(DbContextOptions<AplicationDbContext> options): base(options) { }


        public async Task<List<Info>> GetAllInfoAsync()
        {
            return await Info.FromSqlRaw("CALL GetAllInfo()").ToListAsync();
        }

        public async Task<Info> GetInfoByIdAsync(int id)
        {
            var param = new MySqlParameter("@p_Id", id);
            return await Info.FromSqlRaw("CALL GetInfoById(@p_Id)", param).FirstOrDefaultAsync();
        }

        public async Task<int> InsertInfoAsync(string nombre, string apellido, int edad, DateTime fechaCreacion)
        {
            var param1 = new MySqlParameter("@p_Nombre", nombre);
            var param2 = new MySqlParameter("@p_Apellido", apellido);
            var param3 = new MySqlParameter("@p_Edad", edad);
            var param4 = new MySqlParameter("@p_FechaCreacion", fechaCreacion);
            return await Database.ExecuteSqlRawAsync("CALL InsertInfo(@p_Nombre, @p_Apellido, @p_Edad, @p_FechaCreacion)", param1, param2, param3, param4);
        }

        public async Task<int> UpdateInfoAsync(int id, string nombre, string apellido, int edad, DateTime fechaCreacion)
        {
            var param1 = new MySqlParameter("@p_Id", id);
            var param2 = new MySqlParameter("@p_Nombre", nombre);
            var param3 = new MySqlParameter("@p_Apellido", apellido);
            var param4 = new MySqlParameter("@p_Edad", edad);
            var param5 = new MySqlParameter("@p_FechaCreacion", fechaCreacion);
            return await Database.ExecuteSqlRawAsync("CALL UpdateInfo(@p_Id, @p_Nombre, @p_Apellido, @p_Edad, @p_FechaCreacion)", param1, param2, param3, param4, param5);
        }

        public async Task<int> DeleteInfoAsync(int id)
        {
            var param = new MySqlParameter("@p_Id", id);
            return await Database.ExecuteSqlRawAsync("CALL DeleteInfo(@p_Id)", param);
        }
    }
}

