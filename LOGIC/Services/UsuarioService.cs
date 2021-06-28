using AutoMapper;
using DAL.Repositories;
using ENTITIES.DTOs;
using ENTITIES.Entities;
using LOGIC.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace LOGIC.Services
{
    public class UsuarioService
    {
        private readonly UsuarioRepo _usuarioRepo = new UsuarioRepo();
        private readonly IMapper _mapper;

        public UsuarioService(IMapper mapper)
        {
            this._mapper = mapper;
        }

        public async Task<Generic_ResultSet<bool>> CreateUsuario(Usuario usuario)
        {
            Generic_ResultSet<bool> result = new Generic_ResultSet<bool>();
            try
            {
                usuario.CreationDate = DateTime.UtcNow;
                usuario.ModifiedDate = DateTime.UtcNow;

                bool state = await _usuarioRepo.Create(usuario);

                result.UserMessage = state ?
                    $"El usuario { usuario.Nombre } fue creado corretamente" : $"El usuario no pudo crearse";
                result.InternalMessage = $"LOGIC.Services.UsuarioService: CreateUsuario() method executed successfully";
                result.Success = true;
                result.ResultSet = state;
            }
            catch (Exception exception)
            {
                result.UserMessage = $"Ocurrió un error, por lo que el usuario { usuario.Nombre } no pudo ser creado";
                result.InternalMessage = $"ERROR: LOGIC.Services.UsuarioService: CreateUsuario(): { exception.Message }";
                result.Exception = exception;
            }

            return result;
        }

        public async Task<Generic_ResultSet<DTOUsuarioRead>> ReadUsuario(int id)
        {
            Generic_ResultSet<DTOUsuarioRead> result = new Generic_ResultSet<DTOUsuarioRead>();
            try
            {
                Usuario usuario = await _usuarioRepo.Read(id);

                DTOUsuarioRead dtoUsuario = null;
                if (usuario != null)
                {
                    dtoUsuario = await CreateDTOUsuario(usuario);
                }

                result.UserMessage = usuario != null ?
                    $"El usuario { usuario.Nombre } fue obtenido corretamente" : $"El usuario solicitado no existe";
                result.InternalMessage = $"LOGIC.Services.UsuarioService: ReadUsuario() method executed successfully";
                result.Success = true;
                result.ResultSet = dtoUsuario;
            }
            catch (Exception exception)
            {
                result.UserMessage = $"Ocurrió un error, por lo que el usuario no pudo obtenerse";
                result.InternalMessage = $"ERROR: LOGIC.Services.UsuarioService: ReadUsuario(): { exception.Message }";
                result.Exception = exception;
            }

            return result;
        }

        public async Task<Generic_ResultSet<List<DTOUsuarioRead>>> ReadAllUsuario()
        {
            Generic_ResultSet<List<DTOUsuarioRead>> result = new Generic_ResultSet<List<DTOUsuarioRead>>();
            try
            {
                List<Usuario> usuarios = await _usuarioRepo.ReadAll();

                List<DTOUsuarioRead> dtoListUsuario = new List<DTOUsuarioRead>();
                foreach (var usuario in usuarios)
                {
                    dtoListUsuario.Add(await CreateDTOUsuario(usuario));
                }

                result.UserMessage = $"La lista de usuarios fue obtenida corretamente";
                result.InternalMessage = $"LOGIC.Services.UsuarioService: ReadAllUsuario() method executed successfully";
                result.Success = true;
                result.ResultSet = dtoListUsuario;
            }
            catch (Exception exception)
            {
                result.UserMessage = $"Ocurrió un error, por lo que no pudo obtenerse la lista de usuarios";
                result.InternalMessage = $"ERROR: LOGIC.Services.UsuarioService: ReadAllUsuario(): { exception.Message }";
                result.Exception = exception;
            }

            return result;
        }

        public async Task<Generic_ResultSet<bool>> UpdateUsuario(int id, Usuario usuario)
        {
            Generic_ResultSet<bool> result = new Generic_ResultSet<bool>();
            try
            {
                usuario.ModifiedDate = DateTime.UtcNow;

                bool state = await _usuarioRepo.Update(id, usuario);

                result.UserMessage = state ?
                    $"El usuario { usuario.Nombre } fue actualizado corretamente" : $"El usuario solicitado no existe";
                result.InternalMessage = $"LOGIC.Services.UsuarioService: UpdateUsuario() method executed successfully";
                result.Success = true;
                result.ResultSet = state;
            }
            catch (Exception exception)
            {
                result.UserMessage = $"Ocurrió un error, por lo que el usuario { usuario.Nombre } no pudo ser actualizado";
                result.InternalMessage = $"ERROR: LOGIC.Services.UsuarioService: UpdateUsuario(): { exception.Message }";
                result.Exception = exception;
            }

            return result;
        }

        public async Task<Generic_ResultSet<bool>> DeleteUsuario(int id)
        {
            Generic_ResultSet<bool> result = new Generic_ResultSet<bool>();
            try
            {
                bool state = await _usuarioRepo.Delete(id);

                result.UserMessage = state ?
                    $"El usuario fue eliminado corretamente" : $"El usuario solicitado no existe";
                result.InternalMessage = $"LOGIC.Services.UsuarioService: DeleteUsuario() method executed successfully";
                result.Success = true;
                result.ResultSet = state;
            }
            catch (Exception exception)
            {
                result.UserMessage = $"Ocurrió un error, por lo que el usuario no pudo eliminarse";
                result.InternalMessage = $"ERROR: LOGIC.Services.UsuarioService: DeleteUsuario(): { exception.Message }";
                result.Exception = exception;
            }

            return result;
        }

        public async static Task<Usuario> ReadSimple(int id)
        {
            try
            {
                Usuario usuario = await new UsuarioRepo().Read(id);

                return usuario;
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public async static Task<List<Usuario>> ReadAllSimple()
        {
            try
            {
                List<Usuario> usuarios = await new UsuarioRepo().ReadAll();

                return usuarios;
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        private async Task<DTOUsuarioRead> CreateDTOUsuario(Usuario usuario)
        {
            DTOUsuarioRead dtoUsuario = _mapper.Map<DTOUsuarioRead>(usuario);

            dtoUsuario.EstadoUsuario = await EstadoUsuarioService.ReadSimple(usuario.IdEstadoUsuario);
            dtoUsuario.Rol = await RolService.ReadSimple(usuario.IdEstadoUsuario);

            return dtoUsuario;
        }
    }
}
