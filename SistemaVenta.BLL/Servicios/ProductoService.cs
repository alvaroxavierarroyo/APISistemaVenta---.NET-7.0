using System;
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
    public class ProductoService: IProductoService
    {
        private readonly IGenericRepository<Producto> _ProductoRepositorio;
        private readonly IMapper _mapper;

        public ProductoService(IGenericRepository<Producto> productoRepositorio, IMapper mapper)
        {
            _ProductoRepositorio = productoRepositorio;
            _mapper = mapper;
        }

        public async Task<List<ProductoDTO>> Lista()
        {
            try
            {
                var queryProducto = await _ProductoRepositorio.Consulta();
                var listaProductos = queryProducto.Include(cat => cat.IdCategoriaNavigation).ToList();

                return _mapper.Map<List<ProductoDTO>>(listaProductos.ToList());
            }
            catch
            {
                throw; 
            }
        }
        public async Task<ProductoDTO> Crear(ProductoDTO modelo)
        {
            try
            {
                var productoCreado = await _ProductoRepositorio.Crear(_mapper.Map<Producto>(modelo));
                if (productoCreado.IdProducto == 0)
                    throw new TaskCanceledException("Lo sentimos. El producto no se ha creado");
                return _mapper.Map<ProductoDTO>(productoCreado);
            }
            catch
            {
                throw;
            }
        }

        public async Task<bool> Editar(ProductoDTO modelo)
        {
            try
            {
                var productoModelo = _mapper.Map<Producto>(modelo);
                var productoEncontrado = await _ProductoRepositorio.Obtener(u => u.IdProducto == productoModelo.IdProducto);

                if (productoEncontrado == null)
                    throw new TaskCanceledException("Lo sentimos. El producto no existe");


                productoEncontrado.Nombre = productoModelo.Nombre;
                productoEncontrado.IdCategoria = productoModelo.IdCategoria;
                productoEncontrado.Stock = productoModelo.Stock;
                productoEncontrado.Precio = productoModelo.Precio;
                productoEncontrado.EsActivo = productoModelo.EsActivo;

                bool respuesta = await _ProductoRepositorio.Editar(productoEncontrado);

                if(!respuesta)
                    throw new TaskCanceledException("Lo sentimos. El producto no pudo editarse");

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
                var productoEncontrado = await _ProductoRepositorio.Obtener(p => p.IdProducto == id);
                if (productoEncontrado == null)
                    throw  new TaskCanceledException("Lo sentimos. El producto no existe");

                bool respuesta = await _ProductoRepositorio.Delete(productoEncontrado);
                if (!respuesta)
                    throw new TaskCanceledException("Lo sentimos. El producto no pudo eliminarse");

                return respuesta;

            } catch
            {
                throw;
            }
        }

       
    }
}
