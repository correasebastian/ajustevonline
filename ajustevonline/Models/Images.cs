//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ajustevonline.Models
{
    using Newtonsoft.Json;
    using System;
    using System.Collections.Generic;

    public partial class Images
    {
        public int Id { get; set; }
        public int IdInspeccion { get; set; }
        public int IdPath { get; set; }

        [JsonIgnore]
        public virtual InspeccionesUlt InspeccionesUlt { get; set; }
        //[JsonIgnore]
        public virtual Paths Paths { get; set; }
    }
}
