using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using ExcelDataReader;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using UspgPOS.Data;
using UspgPOS.Models;

namespace UspgPOS.Controllers
{
    public class ProductoesController : Controller
    {
        private readonly AppDbContext _context;
        private readonly Cloudinary _cloudinary;

        public ProductoesController(AppDbContext context, Cloudinary cloudinary)
        {
            _context = context;
            _cloudinary = cloudinary;
        }

        // GET: Productoes
        public async Task<IActionResult> Index()
        {
            var appDbContext = _context.Productos.Include(p => p.Clasificacion).Include(p => p.Marca);
            return View(await appDbContext.ToListAsync());
        }

        // GET: Productoes/Details/5
        public async Task<IActionResult> Details(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var producto = await _context.Productos
                .Include(p => p.Clasificacion)
                .Include(p => p.Marca)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (producto == null)
            {
                return NotFound();
            }

            return View(producto);
        }


        // GET: Productoes/Create
        public IActionResult Create()
        {
            ViewData["ClasificacionId"] = new SelectList(_context.Clasificaciones, "id", "Nombre");
            ViewData["MarcaId"] = new SelectList(_context.Marcas, "Id", "Nombre");
            return View();
        }

        // POST: Productoes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Nombre,MarcaId,ClasificacionId,Precio,Cantidad")] Producto producto, IFormFile imageFile)
        {
            if (ModelState.IsValid)
            {
                if(imageFile != null)
                {
                    var uploadParams = new ImageUploadParams()
                    {
                        File = new FileDescription(imageFile.FileName, imageFile.OpenReadStream()),
                        Transformation = new Transformation().Width(500).Height(500).Crop("fill")
                    };

                    var uploadResult = await _cloudinary.UploadAsync(uploadParams);
                    producto.ImgUrl = uploadResult.SecureUrl.ToString();

                    var thumbnailParams = new Transformation().Width(150).Height(150).Crop("thumb");

                    producto.ThumbnailUrl = _cloudinary.Api.UrlImgUp.Transform(thumbnailParams).BuildUrl(uploadResult.PublicId);

                }
                _context.Add(producto);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ClasificacionId"] = new SelectList(_context.Clasificaciones, "id", "Nombre", producto.ClasificacionId);
            ViewData["MarcaId"] = new SelectList(_context.Marcas, "Id", "Nombre", producto.MarcaId);
            return View(producto);
        }

        // GET: Productoes/Edit/5
        public async Task<IActionResult> Edit(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var producto = await _context.Productos.FindAsync(id);
            if (producto == null)
            {
                return NotFound();
            }
            ViewData["ClasificacionId"] = new SelectList(_context.Clasificaciones, "id", "Nombre", producto.ClasificacionId);
            ViewData["MarcaId"] = new SelectList(_context.Marcas, "Id", "Nombre", producto.MarcaId);
            return View(producto);
        }

        // POST: Productoes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, [Bind("Id,Nombre,MarcaId,ClasificacionId,Precio,Cantidad")] Producto producto)
        {
            if (id != producto.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(producto);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductoExists(producto.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["ClasificacionId"] = new SelectList(_context.Clasificaciones, "id", "Nombre", producto.ClasificacionId);
            ViewData["MarcaId"] = new SelectList(_context.Marcas, "Id", "Nombre", producto.MarcaId);
            return View(producto);
        }

        // GET: Productoes/Delete/5
        public async Task<IActionResult> Delete(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var producto = await _context.Productos
                .Include(p => p.Clasificacion)
                .Include(p => p.Marca)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (producto == null)
            {
                return NotFound();
            }

            return View(producto);
        }

        // POST: Productoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            var producto = await _context.Productos.FindAsync(id);
            if (producto != null)
            {
                _context.Productos.Remove(producto);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProductoExists(long id)
        {
            return _context.Productos.Any(e => e.Id == id);
        }

        [HttpPost]
        public async Task<IActionResult> CargarArchivo(IFormFile file)
        {
            if (file == null || file.Length == 0)
            {
                return BadRequest("Por favor, seleccione un archivo de Excel");
            }

            if (!file.FileName.EndsWith(".xlsx"))
            {
                return BadRequest("Por favor, seleccione un archivo de Excel");
            }

            using (var stream = file.OpenReadStream())
            {
                using (var reader = ExcelReaderFactory.CreateReader(stream))
                {
                    var dataTable = reader.AsDataSet().Tables[0];

                    for (int fila = 1; fila < dataTable.Rows.Count; fila++)
                    {
                        string codigo = dataTable.Rows[fila][0].ToString();
                        string nombre = dataTable.Rows[fila][1].ToString();
                        string nombreMarca = dataTable.Rows[fila][2].ToString();
                        string nombreClasificacion = dataTable.Rows[fila][3].ToString();
                        decimal precio = decimal.Parse(dataTable.Rows[fila][4].ToString());
                        int cantidad = int.Parse(dataTable.Rows[fila][5].ToString());

                        //esta buscando la marca
                        Marca? marca = await _context.Marcas.FirstOrDefaultAsync(m => m.Nombre == nombreMarca);

                        //Si la marca es nula entonces la creamos
                        if (marca == null)
                        {
                            marca = new Marca {
                                Nombre = nombreMarca 
                            };

                            //lo agregamos a nuestro _context
                            _context.Marcas.Add(marca);
                            await _context.SaveChangesAsync();
                        }

                        //esta buscando la clasificación
                        Clasificacion? clasificacion = await _context.Clasificaciones.FirstOrDefaultAsync(c => c.Nombre == nombreClasificacion);

                        //Si la clasificacion es nula entonces la creamos
                        if (clasificacion == null)
                        {
                            clasificacion = new Clasificacion
                            {
                                Nombre = nombreClasificacion
                            };

                            _context.Clasificaciones.Add(clasificacion);
                            await _context.SaveChangesAsync();
                        }

                        //Buscando el producto
                        Producto? producto = await _context.Productos.FirstOrDefaultAsync(p => p.Codigo == codigo);

                        if (producto == null)
                        {
                            //Crear el producto
                            Producto nuevoProducto = new Producto
                            {
                                Codigo = codigo,
                                Nombre = nombre,
                                Precio = precio,
                                Cantidad = cantidad,
                                MarcaId = marca.Id,
                                ClasificacionId = clasificacion.id
                            };

                            _context.Productos.Add(nuevoProducto);
                        }
                        else
                        {
                            //Editar el producto
                            producto.Nombre = nombre;
                            producto.MarcaId = marca.Id;
                            producto.ClasificacionId = clasificacion.id;
                            producto.Precio = precio;
                            producto.Cantidad = cantidad;

                            _context.Productos.Update(producto);
                        }
                    }
                }
            }
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }
    }
}
