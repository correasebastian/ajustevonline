using ajustevonline.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace ajustevonline.Controllers
{
     [RoutePrefix("api/uploads")]
    public class UploadsController : ApiController
    {
        
             private OnlineEntities db = new OnlineEntities();



             [HttpPost]

             public async Task<IHttpActionResult> PostUpload(FileToUpload FileToUpload)
             {

                 if (FileToUpload == null)
                 {
                     return BadRequest();
                 }

                 if (!ModelState.IsValid)
                 {
                     return BadRequest(ModelState);
                 }
                 try
                 {

                     string[] arrayData = FileToUpload.base64Data.Split(',');
                     var mappedPath = System.Web.Hosting.HostingEnvironment.MapPath("~/img");
                     string imgData = string.Empty;
                     if (arrayData.Length == 2)
                     {
                         imgData = arrayData[1];
                     }
                     if (arrayData.Length == 1)
                     {
                         imgData = arrayData[0];
                     }

                     DateTime d = DateTime.Now;




                     string rootLocal = @"D:\AppStorage"; // @"C:\Users\administrator\Documents\Visual Studio 2013\Projects\2015\noviembre\api2PostFiles\api2PostFiles\imgs";
                     string relative = Path.Combine(d.Year.ToString(), d.ToString("MMMM"), d.Day.ToString(), FileToUpload.placa);
                     string folder = Path.Combine(rootLocal, relative);


                     //bool exists = System.IO.Directory.Exists(folder);

                     //if (!exists) // no es necesaria esta verificacion 
                     System.IO.Directory.CreateDirectory(folder);

                     string relativeUri = Path.Combine(relative, FileToUpload.name);
                     //string baseUri = "http://190.145.39.139/s3/";
                     var baseUri = "http://ajustevsiva.com/Img/fotos/";
                     var uri = new Uri(new Uri(baseUri), relativeUri);
                     string file = Path.Combine(folder, FileToUpload.name);
                     if (await WriteStreamToFileAsync(imgData, file))
                     {
                         var f = new fotos
                         {
                             fecha = DateTime.Now,
                             idFoto = FileToUpload.idFoto,
                             idInspeccion = FileToUpload.idInspeccion,
                             path = uri.ToString()

                         };
                         db.fotos.Add(f);
                         await db.SaveChangesAsync();
                         //return Ok( new { path = f.path });
                         //new { path:uri.ToString() }

                         return Ok(new UploadResult { Path = uri.ToString() });
                     }
                     else
                     {
                         return InternalServerError();
                     }


                 }
                 catch (Exception ex)
                 {
                     return InternalServerError(ex);
                 }


             }

             private async Task<Boolean> WriteStreamToFileAsync(string imgData, string filePath)
             {

                 try
                 {
                     byte[] bytes = Convert.FromBase64String(imgData);
                     // FileSystem.OpenFile is a wrapper for System.IO.OpenFile...
                     using (FileStream stream = new FileStream(filePath, FileMode.Create))
                     {
                         await stream.WriteAsync(bytes, 0, bytes.Length);
                         await stream.FlushAsync();
                     }
                     return true;
                 }
                 catch (Exception ex)
                 {
                     throw;
                 }
             }
         }
    }

