namespace GunsPaginationMVC.Entities;

public class Gun
{
    public int Id { get; set; }

    public string Name { get; set; }

    public double Cartridge { get; set; }
}

public class GunDtoDetailed
{
    public string Name { get; }

    public double Cartridge { get; }
}

public class GunDto
{
    public string Name { get; }
}