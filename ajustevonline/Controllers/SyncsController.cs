using ajustevonline.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;

namespace ajustevonline.Controllers
{
    [RoutePrefix("api/syncs")]
    public class SyncsController : ApiController
    {
        private OnlineEntities dbOnline = new OnlineEntities();
        private MockUltEntities dbMock = new MockUltEntities();

        [HttpPost]
        //[ResponseType(typeof(string))]
        public async Task<IHttpActionResult> PostSyncs(SyncParams syncParams)
        {
            //var fotos = dbOnline.fotos
            //     .Where(foto => foto.idInspeccion == syncParams.IdOnline)
            //     .Where(foto => foto.uid != "sync");

            try
            {
                var changes = false;
                var fotos = dbOnline.fotos
                     .Where(foto => foto.idInspeccion == syncParams.IdOnline)
                     .Where(foto => foto.uid != "sync");


                //var fotos = from v in dbOnline.fotos
                //            where v.idInspeccion == syncParams.IdOnline && v.uid != "sync"
                //            orderby v.idInspeccion descending
                //            select v;

                //if (fotos == null)
                //{
                //    return Ok();
                //}

                foreach (var foto in fotos)
                {
                    if (changes == false)
                    {
                        changes = true;
                    }
                    var paths = new Paths
                    {
                        Path = foto.path
                    };
                    var img = new Images
                    {
                        IdInspeccion = 2,
                        Paths = paths
                    };

                    //dbMock.Paths.Add(path);
                    dbMock.Images.Add(img);

                    foto.uid = "sync";
                    //dbOnline.Entry(fotos).State = EntityState.Modified;

                }

                if (changes == true)
                {
                    await dbMock.SaveChangesAsync();
                    await dbOnline.SaveChangesAsync();
                }
                

                return Ok();
            }
            catch (Exception ex)
            {
                
                return InternalServerError(ex);
            }
        }
    }
}
