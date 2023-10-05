using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Jegyek.Controllers
{
    [Route("jegy")]
    [ApiController]
    public class JegyekController : ControllerBase
    {
        private static readonly List<Jegyek> grades = new()
        {
            new Jegyek(Guid.NewGuid(),"Töri",2,DateTimeOffset.UtcNow),
            new Jegyek(Guid.NewGuid(),"Matek",5,DateTimeOffset.UtcNow),
            new Jegyek(Guid.NewGuid(),"Fizika",3,DateTimeOffset.UtcNow),
        };

        [HttpGet]
        public IEnumerable<Jegyek> GetAll()
        { return grades; }

        [HttpGet("{id}")]
        public ActionResult<Jegyek> GetById(Guid id)
        {
            var jegy = grades.Where(x => x.Id == id).FirstOrDefault();
            if (jegy == null)
            {
                return NotFound();
            }
            return Ok(jegy);
        }

        [HttpPost]
        public ActionResult<Jegyek> PostJegy(CreateJegyek createJegyek)
        {
            var jegy = new Jegyek
                (
                    Guid.NewGuid(),
                    createJegyek.jegyLeiras,
                    createJegyek.jegySzam,
                    DateTimeOffset.UtcNow
                );

            grades.Add(jegy);
            return CreatedAtAction(nameof(GetById), new { id = jegy.Id }, jegy);
        }

        [HttpPut]
        public Jegyek PullJegyek(Guid id, UpdateJegyek updateJegyek)
        {
            var existingJegyek = grades.Where(x => x.Id == id).FirstOrDefault();
            var jegy = existingJegyek with
            {
                jegyLeiras = updateJegyek.jegyLeiras,
                jegySzam = updateJegyek.jegySzam,
                CreatedTime = DateTimeOffset.UtcNow,
            };

            var index = grades.FindIndex(x => x.Id == id);
            grades[index] = jegy;
            return grades[index];
        }

        [HttpDelete("{id})")]
        public ActionResult<string> DeleteJegyek(Guid id)
        {
            var index = grades.FindIndex(x => x.Id == id);
            grades.RemoveAt(index);
            if (index == 0)
            {
                return NotFound();
            }
            return StatusCode(205, "Törölt");
        }
    }
}