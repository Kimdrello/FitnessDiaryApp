using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using FitnessDiary.DataAccess;
using FitnessDiary.Model;

namespace FitnessDiary.Data.Api.Controllers
{
    public class ExceptionLogsController : ApiController
    {
        private FitnessDiaryContext db = new FitnessDiaryContext();

        // GET: api/ExceptionLogs
        public IQueryable<ExceptionLog> GetExceptionLogs()
        {
            return db.ExceptionLogs;
        }

        // GET: api/ExceptionLogs/5
        [ResponseType(typeof(ExceptionLog))]
        public async Task<IHttpActionResult> GetExceptionLog(Guid id)
        {
            ExceptionLog exceptionLog = await db.ExceptionLogs.FindAsync(id);
            if (exceptionLog == null)
            {
                return NotFound();
            }

            return Ok(exceptionLog);
        }

        // PUT: api/ExceptionLogs/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutExceptionLog(Guid id, ExceptionLog exceptionLog)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != exceptionLog.ExceptionID)
            {
                return BadRequest();
            }

            db.Entry(exceptionLog).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ExceptionLogExists(id))
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

        // POST: api/ExceptionLogs
        [ResponseType(typeof(ExceptionLog))]
        public async Task<IHttpActionResult> PostExceptionLog(ExceptionLog exceptionLog)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.ExceptionLogs.Add(exceptionLog);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (ExceptionLogExists(exceptionLog.ExceptionID))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = exceptionLog.ExceptionID }, exceptionLog);
        }

        // DELETE: api/ExceptionLogs/5
        [ResponseType(typeof(ExceptionLog))]
        public async Task<IHttpActionResult> DeleteExceptionLog(Guid id)
        {
            ExceptionLog exceptionLog = await db.ExceptionLogs.FindAsync(id);
            if (exceptionLog == null)
            {
                return NotFound();
            }

            db.ExceptionLogs.Remove(exceptionLog);
            await db.SaveChangesAsync();

            return Ok(exceptionLog);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ExceptionLogExists(Guid id)
        {
            return db.ExceptionLogs.Count(e => e.ExceptionID == id) > 0;
        }
    }
}
