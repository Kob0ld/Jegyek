using System.ComponentModel.DataAnnotations;

namespace Jegyek
{
        public record Jegyek(Guid Id, string jegyLeiras, int jegySzam,
            DateTimeOffset CreatedTime);

        public record CreateJegyek([Required] string jegyLeiras, [Range(1, 5)] int jegySzam);

        public record UpdateJegyek([Required] string jegyLeiras, [Range(1, 5)] int jegySzam);
}
