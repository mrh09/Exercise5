using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using API.Context;
using API.Models;

namespace API.Controllers
{
    public class SuppliersController : ApiController
    {
        MyContext myContext = new MyContext();

        [HttpGet]
        public List<Supplier> Get()
        {
            return myContext.Suppliers.ToList();
        }

        [HttpGet]
        public Supplier Get(int id)
        {
            return myContext.Suppliers.Find(id);
        }

        [HttpPost]
        public IHttpActionResult Post(Supplier supplier)
        {
            myContext.Suppliers.Add(supplier);
            var result = myContext.SaveChanges();
            if (result > 0)
                return Ok();
            return BadRequest();
        }

        [HttpPut]
        public IHttpActionResult Put(Supplier supplier)
        {
            var get = Get(supplier.Id);
            if (get != null)
            {
                get.Name = supplier.Name;
                myContext.Entry(get).State = EntityState.Modified;
                var result = myContext.SaveChanges();
                if (result > 0)
                    return Ok();
                return BadRequest();
            }
            return NotFound();
        }

        [HttpDelete]
        public IHttpActionResult Delete(Supplier supplier)
        {
            var get = Get(supplier.Id);
            if (get != null)
            {
                myContext.Suppliers.Remove(get);
                var result = myContext.SaveChanges();
                if (result > 0)
                    return Ok();
                return BadRequest();
            }
            return NotFound();
        }
    }
}