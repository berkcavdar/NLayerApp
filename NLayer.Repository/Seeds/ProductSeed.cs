using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NLayer.Core.Models;

namespace NLayer.Repository.Seeds
{
    internal class ProductSeed : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.HasData(
                new Product { Id = 1, CategoryId = 1, Name = "Yüzüklerin Efendisi Yüzük Kardeşliği", Price = 60, Stock = 100, CreatedDate = DateTime.Now },
                new Product { Id = 2, CategoryId = 1, Name = "Yüzüklerin Efendisi İki Kule", Price = 50, Stock = 150, CreatedDate = DateTime.Now },
                new Product { Id = 3, CategoryId = 1, Name = "Yüzüklerin Efendisi Kralın Dönüşü", Price = 80, Stock = 200, CreatedDate = DateTime.Now, },
                new Product { Id = 4, CategoryId = 2, Name = "Kırmızı Ruj", Price = 10, Stock = 300, CreatedDate = DateTime.Now, });
        }
    }
}
