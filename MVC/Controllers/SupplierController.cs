using MVC.GlobalVaribale;
using MVC.ViewModel;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace MVC.Controllers
{
    public class SupplierController : Controller
    {

        GlobalVariable globalvariable = new GlobalVariable();

        // GET: Supplier
        public async Task<ActionResult> Index()
        {
            IEnumerable<SupplierVM> suppliers = null;

            using (var client = new HttpClient())
            {
                // define URL API
                client.BaseAddress = new Uri(globalvariable.Url());

                // define Request
                HttpResponseMessage responseTask = await client.GetAsync("Suppliers");


                if (responseTask.IsSuccessStatusCode)
                {
                    var readTask = responseTask.Content.ReadAsStringAsync().Result;
                    suppliers = JsonConvert.DeserializeObject<IEnumerable<SupplierVM>>(readTask);
                }
                else
                {
                    suppliers = Enumerable.Empty<SupplierVM>();
                    ModelState.AddModelError(string.Empty, "Server error. Please contact administrator.");
                }
            }
            return View(suppliers);
        }

        //Create
        public JsonResult Create(SupplierVM supplierVM)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(globalvariable.Url());
                var myContent = JsonConvert.SerializeObject(supplierVM);
                var buffer = System.Text.Encoding.UTF8.GetBytes(myContent);
                var byteContent = new ByteArrayContent(buffer);
                byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                var responseTask = client.PostAsync("Suppliers", byteContent).Result;
                return Json(responseTask);
            }
        }

        // Edit
        //get
        public async Task<ActionResult> Edit(int id = 0)
        {
            IEnumerable<SupplierVM> suppliers = null;

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(globalvariable.Url());

                HttpResponseMessage responseTask = await client.GetAsync("supplier?id" + id.ToString());
                
                if (responseTask.IsSuccessStatusCode)
                {
                    var readTask = responseTask.Content.ReadAsStringAsync().Result;
                    suppliers = JsonConvert.DeserializeObject<IEnumerable<SupplierVM>>(readTask);       
                }
            }
            return View(suppliers);
        }


        public JsonResult Edit(SupplierVM supplierVM)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(globalvariable.Url());
                var myContent = JsonConvert.SerializeObject(supplierVM);
                var buffer = System.Text.Encoding.UTF8.GetBytes(myContent);
                var byteContent = new ByteArrayContent(buffer);
                byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                var responseTask = client.PutAsync("Suppliers", byteContent).Result;
                return Json(responseTask);
            }
        }

        //Delete
        public JsonResult Delete(int id)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(globalvariable.Url());
                /*var myContent = JsonConvert.SerializeObject(id);
                var buffer = System.Text.Encoding.UTF8.GetBytes(myContent);
                var byteContent = new ByteArrayContent(buffer);
                byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");*/
                HttpResponseMessage deleteTask = client.DeleteAsync("Suppliers/" + id.ToString()).Result;
                return Json(deleteTask);
            }
        }

        /* public JsonResult Edit(int id = 0)
         {
             //SupplierVM supplier = null;
             using (var client = new HttpClient())
             {
                 // define URL API
                 client.BaseAddress = new Uri(globalvariable.Url());

                 //var responseTask = client.GetAsync("Supplier/" + id.ToString());
                 //responseTask.Wait();

                 //var result = responseTask.Result;
                 if (id == 0)
                     //{
                     return View(SupplierVM());
                 /*var myContent = JsonConvert.SerializeObject(id);
                 var buffer = System.Text.Encoding.UTF8.GetBytes(myContent);
                 var byteContent = new ByteArrayContent(buffer);
                 byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                 //var readTask = result.Content.r
                 //supplier = readTask.Result;
                 //}
                 else
                 {
                     var myContent = JsonConvert.SerializeObject(id);
                     var buffer = System.Text.Encoding.UTF8.GetBytes(myContent);
                     var byteContent = new ByteArrayContent(buffer);
                     byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                     var responseTask = client.GetAsync("Supplier/" + id.ToString()).Result;
                     return View(responseTask.Content.ReadAsAsync<SupplierVM>().Result);
                 }
             }
             //return View(readTask);
         }

         [HttpPost]
         public JsonResult Edit(SupplierVM supplier)
         {
             using (var client = new HttpClient())
             {
                 client.BaseAddress = new Uri(globalvariable.Url());
                 var myContent = JsonConvert.SerializeObject(supplier);
                 var buffer = System.Text.Encoding.UTF8.GetBytes(myContent);
                 var byteContent = new ByteArrayContent(buffer);
                 byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                 var putTask = client.PutAsync<SupplierVM>("supplier", supplier);
                 putTask.Wait();

                 var result = putTask.Result;
                 if (result.IsSuccessStatusCode)
                 {

                     return Json(putTask);
                 }
             }
             return View(student);
         } */

    }
}