using System;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using WebApplication2.Models;
using WebApplication2.DTO;
using System.Linq.Expressions;


namespace WebApplication2.Controllers
{
    public class HomesController : ApiController
    {
        private WaterCounterDbContext db = new WaterCounterDbContext();

        private static readonly Expression<Func<Home, HomeDto>> AsHomeDto = x => new HomeDto
        {
            HomeId = x.HomeId,
            Address = x.Address,
        };
            

        // GET: api/Homes
        public IQueryable<HomeDto> GetHomes()
        {
            return db.Homes.Select(AsHomeDto);
        }

        // GET: api/Homes/5
        [ResponseType(typeof(Home))]
        public async Task<IHttpActionResult> GetHome(int id)
        {
            Home home = await db.Homes.FindAsync(id);
            if (home == null)
            {
                return NotFound();
            }

            return Ok(home);
        }

        // PUT: api/Homes/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutHome(int id, Home home)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != home.HomeId)
            {
                return BadRequest();
            }

            db.Entry(home).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!HomeExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/Homes
        [ResponseType(typeof(Home))]
        public async Task<IHttpActionResult> PostHome(Home home)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Homes.Add(home);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = home.HomeId }, home);
        }

        // DELETE: api/Homes/5
        [ResponseType(typeof(Home))]
        public async Task<IHttpActionResult> DeleteHome(int id)
        {
            Home home = await db.Homes.FindAsync(id);
            if (home == null)
            {
                return NotFound();
            }

            db.Homes.Remove(home);
            await db.SaveChangesAsync();

            return Ok(home);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool HomeExists(int id)
        {
            return db.Homes.Count(e => e.HomeId == id) > 0;
        }
    }
}