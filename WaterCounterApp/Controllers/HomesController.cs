﻿using System;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using WaterCounterApp.Models;
using WaterCounterApp.DTO;
using System.Linq.Expressions;


namespace WaterCounterApp.Controllers
{
    public class HomesController : ApiController
    {
        private WaterCounterDbContext db = new WaterCounterDbContext();

        private static readonly Expression<Func<Home, HomeDto>> AsHomeDto = x => new HomeDto
        {
            HomeId = x.HomeId,
            Address = x.Address,
        };

        //private static readonly Expression<Func<WaterCounter, WaterCounterDTO>> AsWaterCounterDto = x => new WaterCounterDTO
        //{
        //    WaterCounterId = x.WaterCounterId,
        //    SerialNum = x.SerialNum,
        //    Readings = x.Readings,
        //    HomeId = x.HomeId
        //};


        // GET: api/Homes
        [ResponseType(typeof(HomeDto))]
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

        // GET: api/Homes/most
        [ResponseType(typeof(Home))]
        [Route("api/homes/most")]
        public async Task<IHttpActionResult> GetMostConsumedHome()
        {
            Home home = await db.Homes.OrderByDescending(h => h.Counters.Max(c => c.Readings)).FirstOrDefaultAsync();
            if (home == null)
            {
                return NotFound();
            }

            return Ok(home);
        }

        // GET: api/Homes/less
        [ResponseType(typeof(Home))]
        [Route("api/homes/less")]
        public async Task<IHttpActionResult> GetLessConsumedHome()
        {
            Home home = await db.Homes.OrderBy(h => h.Counters.Max(c => c.Readings)).FirstOrDefaultAsync();
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

            return CreatedAtRoute("HomeApi", new { id = home.HomeId }, home);
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
            try
            {
                db.Homes.Remove(home);
                await db.SaveChangesAsync();
            }
            catch (Exception exp)
            {
                throw;
            }
            return Ok(home);
        }


        [ResponseType(typeof(WaterCounter))]
        [Route("api/homes/{homeid}/counters")]
        public async Task<IHttpActionResult> PostCounter(int homeId, WaterCounter counter)
        {
            Home home = await db.Homes.FindAsync(homeId);
            if (home == null)
            {
                return NotFound();
            }
           
            db.Entry(counter).State = EntityState.Added;

            home.Counters.Add(counter);
            await db.SaveChangesAsync();

            return Ok(counter);
        }

        [ResponseType(typeof(WaterCounter))]
        [Route("api/homes/{homeid}/counters/{counterId}")]
        public async Task<IHttpActionResult> PutCounter(int homeId, int counterId, WaterCounter counter)
        {
            Home home = await db.Homes.FindAsync(homeId);
            if (home == null)
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (home.Counters.All(c=>c.WaterCounterId != counterId))
            {
                return BadRequest();
            }
            
            try
            {
                var attached = home.Counters.Find(wc => wc.WaterCounterId == counterId);
                db.Entry(attached).CurrentValues.SetValues(counter);
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!WaterCounterExists(counterId))
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

        [ResponseType(typeof(WaterCounter))]
        [Route("api/homes/{homeid}/counters/{counterId}")]
        public async Task<IHttpActionResult> DeleteWaterCounter(int homeid, int counterid)
        {
            WaterCounter counter = await db.WaterCounters.FindAsync(counterid);
            if (counter == null)
            {
                return NotFound();
            }

            db.WaterCounters.Remove(counter);
            await db.SaveChangesAsync();

            return Ok(counter);
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

        private bool WaterCounterExists(int id)
        {
            return db.WaterCounters.Count(e => e.WaterCounterId == id) > 0;
        }
    }
}