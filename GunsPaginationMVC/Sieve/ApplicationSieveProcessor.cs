using Microsoft.Extensions.Options;
using GT.Sieve.Models;
using GT.Sieve.Services;
using GunsPaginationMVC.Entities;

namespace GunsPaginationMVC.Sieve;

public class ApplicationSieveProcessor : SieveProcessor
{
    public ApplicationSieveProcessor(IOptions<SieveOptions> options, IPaginationExecutor executor)
        : base(options, executor)
    {
    }

    protected override SievePropertyMapper MapProperties(SievePropertyMapper mapper)
    {
        mapper.Property<Gun>(g => g.Name)
            .CanSort();
        mapper.Property<Gun>(g => g.Name)
            .CanFilter();

        return mapper;
    }
}