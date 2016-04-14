using ajustevonline.Models;

using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web.Http;
using System.IO.Compression;

using System;
using Ionic.Zip;
using System.Threading.Tasks;

namespace ajustevonline.Controllers
{
    [RoutePrefix("api/downloads")]
    public class DownloadsController : ApiController
    {
        private OnlineEntities db = new OnlineEntities();


        //[HttpGet]
        //public HttpResponseMessage getzip(string Id)
        //{


        //    var folderPath = @"D:\AppStorage\2016\abril\14\KIKE1";
        //    var baseOutputStream = new MemoryStream();
        //    ZipOutputStream zipOutput = new ZipOutputStream(baseOutputStream);
        //    zipOutput.IsStreamOwner = false;

        //    /*  
        //    * Higher compression level will cause higher usage of reources 
        //    * If not necessary do not use highest level 9 
        //    */

        //    zipOutput.SetLevel(3);
        //    byte[] buffer = new byte[4096];
        //    foreach (var file in Directory.GetFiles(folderPath))
        //    {
        //        ZipEntry entry = new ZipEntry(Path.GetFileName(file));
        //        entry.DateTime = DateTime.Now;
        //        zipOutput.PutNextEntry(entry);

        //        using (FileStream fs = System.IO.File.OpenRead(file))
        //        {
        //            int sourceBytes = 0;
        //            do
        //            {
        //                sourceBytes = fs.Read(buffer, 0, buffer.Length);
        //                zipOutput.Write(buffer, 0, sourceBytes);
        //            } while (sourceBytes > 0);
        //        }
        //    }

        //    zipOutput.Finish();
        //    zipOutput.Close();

        //    /* Set position to 0 so that cient start reading of the stream from the begining */
        //    baseOutputStream.Position = 0;

        //    /* Set custom headers to force browser to download the file instad of trying to open it */
        //    return new FileStreamResult(baseOutputStream, "application/x-zip-compressed")
        //    {
        //        FileDownloadName = "Archive.zip"
        //    };
        //}


        //[HttpGet]
        ////[Route("getzip/{Id}")]
        //public async Task<HttpResponseMessage> getzip(string Id)
        //{



        //    inspecciones inspecciones = await db.inspecciones.FindAsync(Id);
        //    if (inspecciones == null)
        //    {
        //        return new HttpResponseMessage(HttpStatusCode.NotFound);
        //    }


        //    var D = (from v in db.fotos
        //             where v.idInspeccion == Id
        //             orderby v.idInspeccion descending
        //             select v).FirstOrDefault();

        //    //var H = (from v in db.fotos
        //    //         where v.idInspeccion == Id
        //    //         orderby v.idInspeccion descending
        //    //         select v);

        //    var localPath = D.path.Replace("http://ajustevsiva.com/Img/fotos/", @"D:\AppStorage\").Replace("/", @"\");
        //    var dir = Path.GetDirectoryName(localPath);
        //    var filename = inspecciones.placa + ".zip";

        //    var path = Path.Combine(dir, filename);


        //    ////var filename = D.placa + ".zip";
        //    //var filename = inspecciones.placa + ".zip";

        //    //var path = Path.Combine(dir, filename);
        //    //using (ZipFile zip = new ZipFile())
        //    //{
        //    //    foreach (var foto in H)
        //    //    {
        //    //        var fi = foto.path.Replace("http://ajustevsiva.com/Img/fotos/", @"D:\AppStorage\").Replace("/", @"\");
        //    //        zip.AddFile(fi);

        //    //    }
        //    //    zip.Save(path);
        //    //}

        //    //var dir = @"C:\Temp\fotos";
        //    //var dirDefault = @"D:\NewIns\Files\InsImg\Fotos";


        //    using (ZipFile zip = new ZipFile())
        //    {

        //        zip.AddDirectory(dir);
        //        zip.Save(path);
        //    }


        //    var result = new HttpResponseMessage(HttpStatusCode.OK);
        //    var stream = new FileStream(path, FileMode.Open);
        //    result.Content = new StreamContent(stream);
        //    result.Content.Headers.ContentType =
        //        new MediaTypeHeaderValue("application/octet-stream");
        //    result.Content.Headers.ContentDisposition = new ContentDispositionHeaderValue("attachment")
        //    {
        //        FileName = filename
        //    };
        //    return result;
        //}



        [HttpGet]
        //[Route("getzip/{Id}")]
        public async Task<HttpResponseMessage> getzip(string Id)
        {



            try
            {

                inspecciones inspecciones = await db.inspecciones.FindAsync(Id);
                if (inspecciones == null)
                {
                    return new HttpResponseMessage(HttpStatusCode.NotFound);
                }
                var H = (from v in db.fotos
                         where v.idInspeccion == Id
                         orderby v.idInspeccion descending
                         select v);

                var localPath = @"D:\AppStorage\zip\";
                var dir = Path.GetDirectoryName(localPath);
                var filename = inspecciones.placa + ".zip";

                var path = Path.Combine(dir, filename);
                using (ZipFile zip = new ZipFile())
                {
                    foreach (var foto in H)
                    {
                        var fi = Path.GetFullPath(foto.path.Replace("http://ajustevsiva.com/Img/fotos/", @"D:\AppStorage\").Replace("/", @"\"));
                        zip.AddFile(fi, "");

                    }
                    zip.Save(path);
                }



                var result = new HttpResponseMessage(HttpStatusCode.OK);
                var stream = new FileStream(path, FileMode.Open);
                result.Content = new StreamContent(stream);
                result.Content.Headers.ContentType =
                    new MediaTypeHeaderValue("application/octet-stream");
                result.Content.Headers.ContentDisposition = new ContentDispositionHeaderValue("attachment")
                {
                    FileName = filename
                };
                return result;

            }
            catch (Exception)
            {

                return new HttpResponseMessage(HttpStatusCode.InternalServerError);
            };        

        }


    }
}
