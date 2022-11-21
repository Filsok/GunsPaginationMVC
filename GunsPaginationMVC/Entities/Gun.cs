using GT.Sieve.Attributes;

namespace GunsPaginationMVC.Entities;

public class Gun
{
    public int Id { get; set; }

    [Sieve(CanFilter = false, CanSort = true)]
    public string Name { get; set; }

    [Sieve(CanFilter = true, CanSort = false)]
    public double Cartridge { get; set; }
}

public class GunDtoDetailed
{
    public string Name { get; }
    public double Cartridge { get; }
}

public class GunDto
{
    [Sieve(CanFilter = false, CanSort = true)]
    public string Name { get; }
}