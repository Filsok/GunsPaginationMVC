namespace GunsPaginationMVC.Models;

public class GunsModel
{
    public List<Gun> Guns { get; set; }
    public int CurrentPageIndex { get; set; }
    public int PageCount { get; set; }
}