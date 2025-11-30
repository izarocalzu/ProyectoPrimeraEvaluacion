using System;
using CommunityToolkit.Mvvm.ComponentModel;
using Newtonsoft.Json;

namespace ProyectoPrimeraEvaluacion_Izaro.Models;

public partial class ProductModel : ObservableObject
{

    public ProductModel()
    {
        
    }
    
    public ProductModel(ProductModel original)
    {
        this.Id = original.Id;
        this.Code = original.Code;
        this.Description = original.Description;
        this.Brand = original.Brand;
        this.Volume = original.Volume;
        this.IsLimited = original.IsLimited;
        this.CreationDate = original.CreationDate;
    }
    
    /*public ProductModel(int id, string code, string desc, string brand, decimal ml, bool limited, DateTime date)
    {
        
    }*/
    
    [JsonProperty("id", DefaultValueHandling = DefaultValueHandling.Ignore)]
    public string Id { get; set; }
    
    [JsonProperty("code")]
    public string Code { get; set; }
    
    [JsonProperty("description")]
    public string Description { get; set; }
    
    [JsonProperty("brand")]
    public string Brand { get; set; }
    
    [JsonProperty("volume")]
    public double Volume { get; set; }
    
    [JsonProperty("limitado")]
    public bool IsLimited { get; set; }
    
    [JsonProperty("creation_date")]
    public DateTime CreationDate { get; set; }
}