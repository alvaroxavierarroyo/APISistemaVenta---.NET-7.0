﻿using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
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
    public class VentaService: IVentaService
    {
        private readonly IVentaRepository _ventaRepositorio;
        private readonly IGenericRepository<DetalleVenta> _detalleVentaRepositorio;
        private readonly IMapper _mapper;

        public VentaService(IVentaRepository ventaRepositorio, IGenericRepository<DetalleVenta> detalleVentaRepositorio, IMapper mapper)
        {
            _ventaRepositorio = ventaRepositorio;
            _detalleVentaRepositorio = detalleVentaRepositorio;
            _mapper = mapper;
        }

        public async Task<VentaDTO> Registrar(VentaDTO modelo)
        {
            try
            {
                var ventaGenerada = await _ventaRepositorio.Registrar(_mapper.Map<Venta>(modelo));
                if (ventaGenerada.IdVenta == 0)
                    throw new TaskCanceledException("Lo sentimos. La venta no ha sido creada");

                        return _mapper.Map<VentaDTO>(ventaGenerada);
            }
            catch
            {
                throw;
            }
        }
        public async Task<List<VentaDTO>> Historial(string buscarPor, string numeroVenta, string fechaInicio, string fechaFin)
        {
            IQueryable<Venta> query = await _ventaRepositorio.Consulta();
            var ListaResultado = new List<Venta>();
          try
            {
                if(buscarPor == "fecha")
                {
                    DateTime fech_inicio = DateTime.ParseExact(fechaInicio, "dd/MM/yyyy", new CultureInfo("es-CO"));
                    DateTime fech_fin = DateTime.ParseExact(fechaFin, "dd/MM/yyyy", new CultureInfo("es-CO"));


                    ListaResultado = await query.Where(v =>
                    v.FechaRegistro.Value.Date >= fech_inicio.Date &&
                    v.FechaRegistro.Value.Date <= fech_fin.Date
                    ).Include(dv => dv.DetalleVenta)
                    .ThenInclude(p => p.IdProductoNavigation)
                    .ToListAsync();
                }
                else
                {
                    ListaResultado = await query.Where(v =>
                   v.NumeroDocumento == numeroVenta).Include(dv => dv.DetalleVenta)
                    .ThenInclude(p => p.IdProductoNavigation)
                    .ToListAsync();
                }
            }
            catch
            {
                throw;
            }
            return _mapper.Map<List<VentaDTO>>(ListaResultado);
        }

        public async Task<List<ReporteDTO>> Reporte(string fechaInicio, string fechaFin)
        {
            IQueryable<DetalleVenta> query = await _detalleVentaRepositorio.Consulta();

            var ListaResultado = new List<DetalleVenta>();
            try
            {
                DateTime fech_inicio = DateTime.ParseExact(fechaInicio, "dd/MM/yyyy", new CultureInfo("es-CO"));
                DateTime fech_fin = DateTime.ParseExact(fechaFin, "dd/MM/yyyy", new CultureInfo("es-CO"));

                ListaResultado = await query
                      .Include(p => p.IdProductoNavigation)
                      .Include(v => v.IdVentaNavigation)
                      .Where(dv =>
                      dv.IdVentaNavigation.FechaRegistro.Value.Date >= fech_inicio.Date &&
                      dv.IdVentaNavigation.FechaRegistro.Value.Date <= fech_fin.Date
                      ).ToListAsync();
            } catch
            {
                throw;
            }
            return _mapper.Map<List<ReporteDTO>>(ListaResultado);
        }
    }
}
