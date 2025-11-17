using System;
using Newtonsoft.Json;

namespace RepasoApp01.Models;

public class ProductModel
{
    [JsonProperty("id", DefaultValueHandling = DefaultValueHandling.Ignore)]
    public string Id { get; set; }
    
    [JsonProperty("codigo")]
    public string Code { get; set; }
    
    [JsonProperty("descripcion")]
    public string Description { get; set; }
    
    [JsonProperty("marca")]
    public string Brand { get; set; }
    
    [JsonProperty("cantidad")]
    public decimal Volume { get; set; }
    
    [JsonProperty("limitado")]
    public bool IsLimited { get; set; }
    
    [JsonProperty("fecha_creacion")]
    public DateTime CreationDate { get; set; }
}