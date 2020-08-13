using Lazulisoft.WebApi2WithStartupClass.Api.Models;
using Lazulisoft.WebApi2WithStartupClass.Api.Repositories;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net;
using System.Web.Http;

namespace Lazulisoft.WebApi2WithStartupClass.Api.Controllers
{
    [RoutePrefix("api/v1/products")]
    public class ProductsController : ApiController
    {
        private readonly IProductRepository _repository;

        public ProductsController(IProductRepository repository)
        {
            _repository = repository;
        }

        [Route("", Name = nameof(GetAll))]
        public IEnumerable<Product> GetAll()
        {
            return _repository.GetAll();
        }

        [Route("{id:int}", Name = nameof(Get))]
        public IHttpActionResult Get(int id)
        {
            var product = _repository.GetById(id);
            if (product == null)
            {
                return NotFound();
            }
            return Ok(product);
        }

        [Route("", Name = nameof(Post))]
        public IHttpActionResult Post(Product product)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                _repository.Add(product);
                _repository.Commit();

                return CreatedAtRoute(nameof(Get), new { id = product.Id }, product);
            }
            catch (Exception ex)
            {
                Trace.TraceError("Error: " + ex.Message, product);
                return Content(HttpStatusCode.InternalServerError, "Error: " + ex.Message);
            }
        }

        [Route("{id:int}", Name = nameof(Put))]
        public IHttpActionResult Put([FromUri] int id, [FromBody] Product product)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var productToUpdate = _repository.GetById(id);

                if (productToUpdate == null)
                    return NotFound();

                productToUpdate.Name = product.Name;
                productToUpdate.Price = product.Price;
                productToUpdate.Category = product.Category;

                _repository.Update(productToUpdate);
                _repository.Commit();

                return Ok();
            }
            catch (Exception ex)
            {
                Trace.TraceError("Error: " + ex.Message, product);
                return Content(HttpStatusCode.InternalServerError, "Error: " + ex.Message);
            }
        }

        [Route("{id:int}", Name = nameof(Delete))]
        public IHttpActionResult Delete(int id)
        {
            try
            {
                //var productToUpdate = _repository.GetById(id);

                //if (productToUpdate == null)
                //    return NotFound();

                var productToUpdate = new Product { Id = id };

                _repository.Delete(productToUpdate);
                _repository.Commit();

                return Ok();
            }
            catch (Exception ex)
            {
                Trace.TraceError("Error: " + ex.Message, id);
                return Content(HttpStatusCode.InternalServerError, "Error: " + ex.Message);
            }
        }
    }
}