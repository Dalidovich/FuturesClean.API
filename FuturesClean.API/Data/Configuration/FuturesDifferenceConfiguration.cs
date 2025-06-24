using FuturesClean.API.Code.Constants;
using FuturesClean.API.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace FuturesClean.API.Data.Configuration
{
    public class FuturesDifferenceConfiguration : IEntityTypeConfiguration<FuturesDifference>
    {
        public const string Table_name = "futures_difference";

        public void Configure(EntityTypeBuilder<FuturesDifference> builder)
        {
            builder.ToTable(Table_name);

            builder.HasKey(e => new { e.Id });

            builder.Property(e => e.Id)
                   .HasColumnType(EntityDataTypes.Guid)
                   .HasColumnName($"pk_{Table_name}_id");

            builder.Property(e => e.TimeMeasuredUtc)
                   .HasColumnName("time_measured_utc");

            builder.Property(e => e.Interval)
                   .HasColumnType(EntityDataTypes.CharacterVarying)
                   .HasColumnName("interval");

            builder.Property(e => e.SymbolCurrent)
                   .HasColumnType(EntityDataTypes.CharacterVarying)
                   .HasColumnName("symbol_current");

            builder.Property(e => e.SymbolNext)
                   .HasColumnType(EntityDataTypes.CharacterVarying)
                   .HasColumnName("symbol_next");

            builder.Property(e => e.Spread)
                   .HasColumnType(EntityDataTypes.CharacterVarying)
                   .HasColumnName("spread");
        }
    }
}
