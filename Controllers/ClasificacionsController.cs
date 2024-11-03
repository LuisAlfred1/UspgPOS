using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using UspgPOS.Data;
using UspgPOS.Models;

namespace UspgPOS.Controllers
{
    public class ClasificacionsController : Controller
    {
        private readonly AppDbContext _context;
        private readonly Cloudinary _cloudinary;

        public ClasificacionsController(AppDbContext context,Cloudinary cloudinary)
        {
            _context = context;
            _cloudinary = cloudinary;
        }

        // GET: Clasificacions
        public async Task<IActionResult> Index()
        {
            return View(await _context.Clasificaciones.ToListAsync());
        }

        // GET: Clasificacions/Details/5
        public async Task<IActionResult> Details(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var clasificacion = await _context.Clasificaciones
                .FirstOrDefaultAsync(m => m.id == id);
            if (clasificacion == null)
            {
                return NotFound();
            }

            return View(clasificacion);
        }

        // GET: Clasificacions/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Clasificacions/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("id,Nombre")] Clasificacion clasificacion, IFormFile imageFile)
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
                    clasificacion.ImgUrl = uploadResult.SecureUrl.ToString();

                    var thumbnailParams = new Transformation().Width(150).Height(150).Crop("thumb");

                    clasificacion.ThumbnailUrl = _cloudinary.Api.UrlImgUp.Transform(thumbnailParams).BuildUrl(uploadResult.PublicId);
                }
                _context.Add(clasificacion);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(clasificacion);
        }

        // GET: Clasificacions/Edit/5
        public async Task<IActionResult> Edit(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var clasificacion = await _context.Clasificaciones.FindAsync(id);
            if (clasificacion == null)
            {
                return NotFound();
            }
            return View(clasificacion);
        }

        // POST: Clasificacions/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, [Bind("id,Nombre")] Clasificacion clasificacion)
        {
            if (id != clasificacion.id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(clasificacion);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ClasificacionExists(clasificacion.id))
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
            return View(clasificacion);
        }

        // GET: Clasificacions/Delete/5
        public async Task<IActionResult> Delete(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var clasificacion = await _context.Clasificaciones
                .FirstOrDefaultAsync(m => m.id == id);
            if (clasificacion == null)
            {
                return NotFound();
            }

            return View(clasificacion);
        }

        // POST: Clasificacions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            var clasificacion = await _context.Clasificaciones.FindAsync(id);
            if (clasificacion != null)
            {
                _context.Clasificaciones.Remove(clasificacion);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ClasificacionExists(long id)
        {
            return _context.Clasificaciones.Any(e => e.id == id);
        }
    }
}
