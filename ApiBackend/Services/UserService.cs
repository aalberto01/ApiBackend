using ApiBackend.Models;
using ApiBackend.Context;
using Microsoft.EntityFrameworkCore;

namespace ApiBackend.Services
{
    public class UserService
    {
        private readonly ReminderdbContext _context;
        public UserService(ReminderdbContext context)
        {
            _context = context;
        }

        //Obtiene todos los usuarios
        public async Task<List<Users>> getUsers()
        {
            return await _context.Users.ToListAsync();
        }

        //Obtiene usuario por id
        public async Task<Users?> GetById(int id)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.Id == id);
        }

        //crear un usuario 
        public async Task<Users> CreateUser(Users user)
        {

            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return user;
        }

        //actualizar un usuario
        public async Task<bool> UpdateUser(int id, Users updatedUser)
        {
            var usuario = await _context.Users.FindAsync(id);
            if (usuario == null) return false;

            usuario.Name = updatedUser.Name;
            usuario.Email= updatedUser.Email;
            usuario.Password= updatedUser.Password;

            await _context.SaveChangesAsync();
            return true;
        }

        //eliminar un usuario
        public async Task<bool> DeleteUser(int id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null) return false;

            _context.Users.Remove(user);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
