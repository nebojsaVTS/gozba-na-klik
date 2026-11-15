using GozbaNaKlik.API.Models;
using Microsoft.EntityFrameworkCore;

namespace GozbaNaKlik.API.Data.Seed.Sid;

public static class SidSeed
{
    public static void Seed(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Municipality>().HasData(
            new Municipality
            {
                Id = 1,
                Name = "Šid"
            }
        );

        modelBuilder.Entity<Settlement>().HasData(
            new Settlement
            {
                Id = 1001,
                Name = "Šid",
                MunicipalityId = 1
            },
            new Settlement
            {
                Id = 1002,
                Name = "Erdevik",
                MunicipalityId = 1
            },
            new Settlement
            {
                Id = 1003,
                Name = "Kukujevci",
                MunicipalityId = 1
            },
            new Settlement
            {
                Id = 1004,
                Name = "Adaševci",
                MunicipalityId = 1
            },
            new Settlement
            {
                Id = 1005,
                Name = "Morović",
                MunicipalityId = 1
            },
            new Settlement
            {
                Id = 1006,
                Name = "Višnjićevo",
                MunicipalityId = 1
            },
            new Settlement
            {
                Id = 1007,
                Name = "Vašica",
                MunicipalityId = 1
            },
            new Settlement
            {
                Id = 1008,
                Name = "Bačinci",
                MunicipalityId = 1
            },
            new Settlement
            {
                Id = 1009,
                Name = "Berkasovo",
                MunicipalityId = 1
            },
            new Settlement
            {
                Id = 1010,
                Name = "Gibarac",
                MunicipalityId = 1
            },
            new Settlement
            {
                Id = 1011,
                Name = "Jamena",
                MunicipalityId = 1
            },
            new Settlement
            {
                Id = 1012,
                Name = "Bingula",
                MunicipalityId = 1
            },
            new Settlement
            {
                Id = 1013,
                Name = "Ilinci",
                MunicipalityId = 1
            },
            new Settlement
            {
                Id = 1014,
                Name = "Sot",
                MunicipalityId = 1
            },
            new Settlement
            {
                Id = 1015,
                Name = "Ljuba",
                MunicipalityId = 1
            },
            new Settlement
            {
                Id = 1016,
                Name = "Bikić Do",
                MunicipalityId = 1
            },
            new Settlement
            {
                Id = 1017,
                Name = "Batrovci",
                MunicipalityId = 1
            },
            new Settlement
            {
                Id = 1018,
                Name = "Molovin",
                MunicipalityId = 1
            },
            new Settlement
            {
                Id = 1019,
                Name = "Privina Glava",
                MunicipalityId = 1
            }
        );
    }
}