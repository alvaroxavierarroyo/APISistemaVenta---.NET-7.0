using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SistemaVenta.DAL.DBContext;
using SistemaVenta.DAL.Repositorios.Contrato;
using SistemaVenta.Model;

namespace SistemaVenta.DAL.Repositorios
{
    public class VentaRepository: GenericRepository<Venta>, IVentaRepository
    {
        private readonly DbventaContext _dbcontext;

        public VentaRepository(DbventaContext dbcontext) : base(dbcontext)
        {
            _dbcontext = dbcontext;
        }

        public async Task<Venta> Registrar(Venta modelo)
        {
            Venta VentaGenerada = new Venta();
            using(var Transaction = _dbcontext.Database.BeginTransaction())
            {
                try
                {
                    foreach (DetalleVenta dv in modelo.DetalleVenta)
                    {
                        Producto producto_encontrado = _dbcontext.Productos.Where(p => p.IdProducto == dv.IdProducto).First();
                        _dbcontext.Productos.Update(producto_encontrado);
                    }
                    await _dbcontext.SaveChangesAsync();
                    NumeroDocumento correlativo = _dbcontext.NumeroDocumentos.First();
                    correlativo.UltimoNumero = correlativo.UltimoNumero + 1;
                    correlativo.FechaRegistro = DateTime.Now;

                    _dbcontext.NumeroDocumentos.Update(correlativo);
                    await _dbcontext.SaveChangesAsync();

                    //0000X1
                    int CantidadDigitos = 4;
                    string ceros = string.Concat(Enumerable.Repeat("0", CantidadDigitos));
                    string NumeroVenta = ceros + correlativo.UltimoNumero.ToString();
                    //01
                    NumeroVenta.Substring(NumeroVenta.Length - CantidadDigitos, CantidadDigitos);

                    modelo.NumeroDocumento = NumeroVenta;

                    await _dbcontext.Venta.AddAsync(modelo);
                    await _dbcontext.SaveChangesAsync();

                    VentaGenerada = modelo;
                    Transaction.Commit();

                }
                catch
                {
                    Transaction.Rollback();
                    throw;
                }
                return VentaGenerada;
            }
        }
    }
}
