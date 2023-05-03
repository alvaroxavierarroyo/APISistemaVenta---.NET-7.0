﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using SistemaVenta.BLL.Servicios.Contrato;
using SistemaVenta.DAL.Repositorios;
using SistemaVenta.DAL.Repositorios.Contrato;
using SistemaVenta.DTO;
using SistemaVenta.Model;
namespace SistemaVenta.BLL.Servicios
{
    public class MenuService: IMenuService
    {
        private readonly IGenericRepository<Usuario> _UsuarioRepositorio;
        private readonly IGenericRepository<MenuRol> _MenuRolRepositorio;
        private readonly IGenericRepository<Menu> _MenuRepositorio;
        private readonly IMapper _mapper;

        public MenuService(IGenericRepository<Usuario> usuarioRepositorio, IGenericRepository<MenuRol> menuRolRepositorio, IGenericRepository<Menu> menuRepositorio, IMapper mapper)
        {
            _UsuarioRepositorio = usuarioRepositorio;
            _MenuRolRepositorio = menuRolRepositorio;
            _MenuRepositorio = menuRepositorio;
            _mapper = mapper;
        }

        public async Task<List<MenuDTO>> Lista(int idUsuario)
        {
            IQueryable<Usuario> tbUsuario = await _UsuarioRepositorio.Consulta(u => u.IdUsuario == idUsuario);
            IQueryable<MenuRol> tbMenuRol = await _MenuRolRepositorio.Consulta();
            IQueryable<Menu> tbMenu = await _MenuRepositorio.Consulta();

            try
            {
                IQueryable<Menu> tbResultado = (from u in tbUsuario
                                                join mr in tbMenuRol on u.IdRol equals mr.IdRol
                                                join m in tbMenu on mr.IdMenu equals m.IdMenu
                                                select m).AsQueryable();
                var listaMenus = tbResultado.ToList();
                return _mapper.Map<List<MenuDTO>>(listaMenus);
            } catch
            {
                throw;
            }
        }
    }
}
