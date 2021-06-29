using AutoMapper;
using ENTITIES.DTOs;
using ENTITIES.Entities;
using LOGIC.Services;
using Microsoft.AspNetCore.Authentication.Cookies;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace LOGIC.LogicEntities
{
    public class UsuarioLogic
    {
        public async Task<DTOUsuarioRead> Validar(IMapper mapper, string email, string clave)
        {
            DTOUsuarioRead dtoUsuario = null;
            List<Usuario> usuarios = await UsuarioService.ReadAllSimple();

            foreach (var usuario in usuarios)
            {
                if (usuario.Email.Equals(email))
                {
                    if (this.CompararClaves(usuario.Clave, clave))
                    {
                        dtoUsuario = mapper.Map<DTOUsuarioRead>(usuario);
                        dtoUsuario.EstadoUsuario = await EstadoUsuarioService.ReadSimple(dtoUsuario.IdEstadoUsuario);
                        dtoUsuario.Rol = await RolService.ReadSimple(dtoUsuario.IdRol);
                    }
                    else
                    {
                        break;
                    }
                }
            }

            if (dtoUsuario == null)
            {
                throw new Exception("Credenciales incorrectas");
            }

            return dtoUsuario;
        }

        public string EncriptarClave(string clave)
        {
            byte[] salt;
            new RNGCryptoServiceProvider().GetBytes(salt = new byte[16]);

            var pbkdf2 = new Rfc2898DeriveBytes(clave, salt, 100000);
            byte[] hash = pbkdf2.GetBytes(20);

            byte[] hashBytes = new byte[36];
            Array.Copy(salt, 0, hashBytes, 0, 16);
            Array.Copy(hash, 0, hashBytes, 16, 20);

            string passwordHashed = Convert.ToBase64String(hashBytes);

            return passwordHashed;
        }

        private bool CompararClaves(string claveCorrecta, string claveActual)
        {
            bool coincide = false;

            byte[] hashBytes = Convert.FromBase64String(claveCorrecta);

            byte[] salt = new byte[16];
            Array.Copy(hashBytes, 0, salt, 0, 16);

            var pbkdf2 = new Rfc2898DeriveBytes(claveActual, salt, 100000);
            byte[] hash = pbkdf2.GetBytes(20);

            for (int i = 0; i < 20; i++)
            {
                if (hashBytes[i + 16] != hash[i])
                {
                    coincide = false;
                    break;
                } else
                {
                    coincide = true;
                }
            }

            return coincide;
        }

        private List<Claim> CreateClaims(DTOUsuarioRead dtoUsuario)
        {
            List<Claim> claims = new List<Claim>();
            claims.Add(new Claim("userEmail", dtoUsuario.Email));
            claims.Add(new Claim(ClaimTypes.Email, dtoUsuario.Email));
            claims.Add(new Claim(ClaimTypes.Name, dtoUsuario.Nombre));
            claims.Add(new Claim(ClaimTypes.Surname, dtoUsuario.Apellido));
            claims.Add(new Claim(ClaimTypes.Role, dtoUsuario.Rol.Nombre));

            return claims;
        }

        private ClaimsIdentity CreateClaimsIdentity(List<Claim> claims)
        {
            ClaimsIdentity claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

            return claimsIdentity;
        }

        public ClaimsPrincipal CreateClaimsPrincipal(DTOUsuarioRead dtoUsuario)
        {
            List<Claim> claims = this.CreateClaims(dtoUsuario);
            ClaimsIdentity claimsIdentity = this.CreateClaimsIdentity(claims);
            ClaimsPrincipal claimsPrincipal = new ClaimsPrincipal(claimsIdentity);

            return claimsPrincipal;
        }
    }
}
