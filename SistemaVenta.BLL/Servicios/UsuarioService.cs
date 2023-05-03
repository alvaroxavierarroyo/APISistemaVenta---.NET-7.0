﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SistemaVenta.BLL.Servicios.Contrato;
using SistemaVenta.DAL.Repositorios;
using SistemaVenta.DAL.Repositorios.Contrato;
using SistemaVenta.DTO;
using SistemaVenta.Model;

namespace SistemaVenta.BLL.Servicios
{
    public class UsuarioService: IUsuarioService
    {
        private readonly IGenericRepository<Usuario> _UsuarioRepositorio;
        private readonly IMapper _mapper;

        public UsuarioService(IGenericRepository<Usuario> usuarioRepositorio, IMapper mapper)
        {
            _UsuarioRepositorio = usuarioRepositorio;
            _mapper = mapper;
        }

        public async Task<List<UsuarioDTO>> Lista()
        {
            try
            {
                var queryUsuario = await _UsuarioRepositorio.Consulta();
                var listaUsuarios = queryUsuario.Include(rol => rol.IdRolNavigation).ToList();
                return _mapper.Map<List<UsuarioDTO>>(listaUsuarios);
            }
            catch
            {
                throw;
            }
        }
        public async Task<SesionDTO> ValidarCredenciales(string correo, string clave)
        {
            try
            {
                var queryUsuario = await _UsuarioRepositorio.Consulta(u => u.Correo == correo && u.Clave == clave);
                if (queryUsuario.FirstOrDefault() == null)
                    throw new TaskCanceledException("Lo sentimos. El usuario no existe");

                Usuario devolverUsuario = queryUsuario.Include(Rol => Rol.IdRolNavigation).First();
                return _mapper.Map<SesionDTO>(devolverUsuario);
            }
            catch
            {
                throw;
            }
        }
        public async Task<UsuarioDTO> Crear(UsuarioDTO modelo)
        {
            try
            {
                var usuarioCreado = await _UsuarioRepositorio.Crear(_mapper.Map<Usuario>(modelo));

                if (usuarioCreado.IdUsuario == 0)
                    throw new TaskCanceledException("Lo sentimos. El usuario no pudo crearse");

                var query = await _UsuarioRepositorio.Consulta(u => u.IdUsuario == usuarioCreado.IdUsuario);

                usuarioCreado = query.Include(rol => rol.IdRolNavigation).First();

                return _mapper.Map<UsuarioDTO>(usuarioCreado);
            } catch
            {
                throw;
            }
        }

        public async Task<bool> Editar(UsuarioDTO modelo)
        {
            try
            {
                var usuarioModelo = _mapper.Map<Usuario>(modelo);
                var usuarioEncontrado = await _UsuarioRepositorio.Obtener(u => u.IdUsuario == usuarioModelo.IdUsuario);

                if (usuarioEncontrado == null)
                    throw new TaskCanceledException("Lo sentimos. El usuario no existe");

                usuarioEncontrado.NombreCompleto = usuarioModelo.NombreCompleto;
                usuarioEncontrado.IdRol = usuarioModelo.IdRol;
                usuarioEncontrado.Correo = usuarioModelo.Correo;
                usuarioEncontrado.Clave = usuarioModelo.Clave;
                usuarioEncontrado.EsActivo = usuarioModelo.EsActivo;

                bool respuesta = await _UsuarioRepositorio.Editar(usuarioEncontrado);
                if (!respuesta)
                    throw new TaskCanceledException("Lo sentimos. El usuario no ha podido crearse");

                return respuesta;

            } catch
            {
                throw;
            }
        }

        public async Task<bool> Eliminar(int id)
        {
            try
            {
                var usuarioEncontrado = await _UsuarioRepositorio.Obtener(u => u.IdUsuario == id);
                if (usuarioEncontrado == null)
                    throw new TaskCanceledException("Lo sentimos. El usuario no existe");

                bool respuesta = await _UsuarioRepositorio.Delete(usuarioEncontrado);
                if (!respuesta)
                    throw new TaskCanceledException("Lo sentimos. El usuario no ha podido crearse");

                return respuesta;
            }
            catch
            {
                throw;
            }
        }

        

       
    }
}
