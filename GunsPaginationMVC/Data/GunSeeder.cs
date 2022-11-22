using GunsPaginationMVC.Entities;

namespace GunsPaginationMVC.Data
{
	public class GunSeeder
	{
		private readonly GunsContext _dbContext;

		public GunSeeder(GunsContext context)
		{
			_dbContext = context;
		}

		public void Seed()
		{
			if (_dbContext.Database.CanConnect())
			{
				if (_dbContext.Gun.Count() == 0)
				{
					_dbContext.Gun.Any();
					var guns = GetGuns();
					_dbContext.Gun.AddRange(guns);
					_dbContext.SaveChanges();
				}
			}
		}

		private IEnumerable<Gun> GetGuns()
		{
			var guns = new List<Gun>();
			var cartridges = new List<double>() { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };
			var cartridge = cartridges.First();
			int counter = 0;
			for (int i = 1; i <= 100; i++)
			{
				if (counter == 10)
				{
					cartridges.Remove(cartridge);
					cartridge = cartridges.First();
					counter = 0;
				}

				guns.Add(new Gun()
				{
					Name = $"Gun{i.ToString("000")}",
					Cartridge = cartridge,
				});
				counter++;
			}

			return guns;
		}
	}
}