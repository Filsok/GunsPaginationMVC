using GunsPaginationMVC.Data;
using GunsPaginationMVC.Models;
using GunsPaginationMVC.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GunsPaginationMVC.Controllers;

public class GunsController : Controller
{
	private readonly GunsContext _context;
	private readonly int _rowsPerPage = 25;

	public GunsController(GunsContext context)
	{
		_context = context;
	}

	public async Task<IActionResult> Index()
	{
		return View(await GetGunList(1));
	}

	[HttpPost]
	public async Task<IActionResult> Index(int currentPageIndex, double filter)
	{
		return View(await GetGunListFiltered(currentPageIndex, filter));
	}

	[HttpPost]
	public async Task<IActionResult> Filter(double filter)
	{
		return View(await GetGunListFiltered(1, filter));
	}

	public async Task<GunsViewModel> GetGunListFiltered(int currentPageIndex, double filter)
	{
		var gunsModel = new GunsViewModel();
		var filteredList = _context.Gun.ToList()
			.Where(g => ((decimal)filter) == 0 ? g.Id >= 0 : g.Cartridge == filter);
		gunsModel.GunList = filteredList
			.OrderBy(g => g.Id)
			.Skip((currentPageIndex - 1) * _rowsPerPage)
			.Take(_rowsPerPage)
			.ToList();

		double pageCount = (double)((decimal)filteredList.Count() / Convert.ToDecimal(_rowsPerPage));
		gunsModel.PageCount = (int)Math.Ceiling(pageCount);
		gunsModel.CurrentPageIndex = currentPageIndex;

		return gunsModel;
	}

	public async Task<GunsViewModel> GetGunList(int currentPageIndex)
	{
		var gunsModel = new GunsViewModel();
		gunsModel.GunList = _context.Gun.ToList()
			.OrderBy(g => g.Id)
			.Skip((currentPageIndex - 1) * _rowsPerPage)
			.Take(_rowsPerPage)
			.ToList();

		double pageCount = (double)((decimal)_context.Gun.Count() / Convert.ToDecimal(_rowsPerPage));
		gunsModel.PageCount = (int)Math.Ceiling(pageCount);
		gunsModel.CurrentPageIndex = currentPageIndex;

		return gunsModel;
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

	// GET: Guns/Create
	public IActionResult Create()
	{
		return View();
	}

	// POST: Guns/Create
	// To protect from overposting attacks, enable the specific properties you want to bind to.
	// For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
	[HttpPost]
	[ValidateAntiForgeryToken]
	public async Task<IActionResult> Create([Bind("Id,Name,Cartridge")] Gun Gun)
	{
		if (ModelState.IsValid)
		{
			_context.Add(Gun);
			await _context.SaveChangesAsync();
			return RedirectToAction(nameof(Index));
		}
		return View(Gun);
	}

	// GET: Guns/Edit/5
	public async Task<IActionResult> Edit(int? id)
	{
		if (id == null)
		{
			return NotFound();
		}

		var Gun = await _context.Gun.FindAsync(id);
		if (Gun == null)
		{
			return NotFound();
		}
		return View(Gun);
	}

	// POST: Guns/Edit/5
	// To protect from overposting attacks, enable the specific properties you want to bind to.
	// For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
	[HttpPost]
	[ValidateAntiForgeryToken]
	public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Cartridge")] Gun Gun)
	{
		if (id != Gun.Id)
		{
			return NotFound();
		}

		if (ModelState.IsValid)
		{
			try
			{
				_context.Update(Gun);
				await _context.SaveChangesAsync();
			}
			catch (DbUpdateConcurrencyException)
			{
				if (!GunExists(Gun.Id))
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
		return View(Gun);
	}

	// GET: Guns/Delete/5
	public async Task<IActionResult> Delete(int? id)
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

	// POST: Guns/Delete/5
	[HttpPost, ActionName("Delete")]
	[ValidateAntiForgeryToken]
	public async Task<IActionResult> DeleteConfirmed(int id)
	{
		var Gun = await _context.Gun.FindAsync(id);
		_context.Gun.Remove(Gun);
		await _context.SaveChangesAsync();
		return RedirectToAction(nameof(Index));
	}

	private bool GunExists(int id)
	{
		return _context.Gun.Any(e => e.Id == id);
	}
}