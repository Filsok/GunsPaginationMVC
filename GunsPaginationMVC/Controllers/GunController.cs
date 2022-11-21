using Microsoft.AspNetCore.Mvc;
using GunsPaginationMVC.Data;
using GunsPaginationMVC.Models;
using Microsoft.EntityFrameworkCore;

namespace GunsPaginationMVC.Controllers;

public class GunsController : Controller
{
    private readonly GunsContext _context;

    public GunsController(GunsContext context)
    {
        _context = context;
    }

    #region GET

    // GET: Guns
    public async Task<IActionResult> Index()
    {
        return View(await _context.Gun.ToListAsync());
    }

    // GET: Guns/Details/5
    public async Task<IActionResult> Details(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var Gun = await _context.Gun
            .FirstOrDefaultAsync(m => m.Id == id);
        if (Gun == null)
        {
            return NotFound();
        }

        return View(Gun);
    }

    //// GET: Guns/Create
    //public IActionResult Create()
    //{
    //    return View();
    //}

    #endregion GET

    //// POST: Guns/Create
    //// To protect from overposting attacks, enable the specific properties you want to bind to.
    //// For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    //[HttpPost]
    //[ValidateAntiForgeryToken]
    //public async Task<IActionResult> Create([Bind("Id,Description,IsDone")] Gun Gun)
    //{
    //    if (ModelState.IsValid)
    //    {
    //        _context.Add(Gun);
    //        await _context.SaveChangesAsync();
    //        return RedirectToAction(nameof(Index));
    //    }
    //    return View(Gun);
    //}

    //// GET: Guns/Edit/5
    //public async Task<IActionResult> Edit(int? id)
    //{
    //    if (id == null)
    //    {
    //        return NotFound();
    //    }

    //    var Gun = await _context.Gun.FindAsync(id);
    //    if (Gun == null)
    //    {
    //        return NotFound();
    //    }
    //    return View(Gun);
    //}

    //// POST: Guns/Edit/5
    //// To protect from overposting attacks, enable the specific properties you want to bind to.
    //// For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    //[HttpPost]
    //[ValidateAntiForgeryToken]
    //public async Task<IActionResult> Edit(int id, [Bind("Id,Description,IsDone")] Gun Gun)
    //{
    //    if (id != Gun.Id)
    //    {
    //        return NotFound();
    //    }

    //    if (ModelState.IsValid)
    //    {
    //        try
    //        {
    //            _context.Update(Gun);
    //            await _context.SaveChangesAsync();
    //        }
    //        catch (DbUpdateConcurrencyException)
    //        {
    //            if (!GunExists(Gun.Id))
    //            {
    //                return NotFound();
    //            }
    //            else
    //            {
    //                throw;
    //            }
    //        }
    //        return RedirectToAction(nameof(Index));
    //    }
    //    return View(Gun);
    //}

    //// GET: Guns/Delete/5
    //public async Task<IActionResult> Delete(int? id)
    //{
    //    if (id == null)
    //    {
    //        return NotFound();
    //    }

    //    var Gun = await _context.Gun
    //        .FirstOrDefaultAsync(m => m.Id == id);
    //    if (Gun == null)
    //    {
    //        return NotFound();
    //    }

    //    return View(Gun);
    //}

    //// POST: Guns/Delete/5
    //[HttpPost, ActionName("Delete")]
    //[ValidateAntiForgeryToken]
    //public async Task<IActionResult> DeleteConfirmed(int id)
    //{
    //    var Gun = await _context.Gun.FindAsync(id);
    //    _context.Gun.Remove(Gun);
    //    await _context.SaveChangesAsync();
    //    return RedirectToAction(nameof(Index));
    //}

    private bool GunExists(int id)
    {
        return _context.Gun.Any(e => e.Id == id);
    }
}