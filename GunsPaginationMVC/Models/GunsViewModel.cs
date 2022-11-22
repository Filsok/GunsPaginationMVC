using GunsPaginationMVC.Entities;

namespace GunsPaginationMVC.Models;

public class GunsViewModel
{
	public List<Gun> GunList { get; set; }
	public int CurrentPageIndex { get; set; }
	public int PageCount { get; set; }
	public string Filter { get; set; }
}