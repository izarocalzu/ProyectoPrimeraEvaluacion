using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Avalonia.Collections;
using Newtonsoft.Json;
using ProyectoPrimeraEvaluacion_Izaro.Models;

namespace ProyectoPrimeraEvaluacion_Izaro.Services;

public class APIService
{
    private HttpClient client;
    
    private const string TABLE_ENDPOINT = "rest/v1/perfume"; 

    public APIService()
    {
        client = new HttpClient();
        client.BaseAddress = new Uri("http://192.160.50.21:7000/");
        client.DefaultRequestHeaders.Add("apikey", "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyAgCiAgICAicm9sZSI6ICJhbm9uIiwKICAgICJpc3MiOiAic3VwYWJhc2UtZGVtbyIsCiAgICAiaWF0IjogMTY0MTc2OTIwMCwKICAgICJleHAiOiAxNzk5NTM1NjAwCn0.dc_X5iR_VP_qT0zsiyj_I_OZ2T9FtRU2BBNWN8Bu4GE");
    }

    
    public async Task CrearProducto(ProductModel producto)
    {
        var jsonProduct = JsonConvert.SerializeObject(producto);
        var request = new HttpRequestMessage(HttpMethod.Post, TABLE_ENDPOINT) // Usamos el nuevo endpoint
        {
            Content = new StringContent(jsonProduct, Encoding.UTF8, "application/json")
        };
        
        request.Headers.Add("Prefer", "return=representation");
        var response = await client.SendAsync(request);

        if (!response.IsSuccessStatusCode)
        {
            var errorBody = await response.Content.ReadAsStringAsync();
            Console.WriteLine("Error al crear producto. Servidor dice: " + errorBody);
            throw new Exception("Error al crear producto. Status: " + response.StatusCode);
        }
    }
    
    public async Task<bool> ModificarProducto(ProductModel producto)
    {
        var jsonProduct = JsonConvert.SerializeObject(producto);
        // Usamos el nuevo endpoint
        var request = new HttpRequestMessage(HttpMethod.Patch, $"{TABLE_ENDPOINT}?id=eq." + producto.Id)
        {
            Content = new StringContent(jsonProduct, Encoding.UTF8, "application/json")
        };
        request.Headers.Add("Prefer", "return=reprentation");
        var response = await client.SendAsync(request);

        if (!response.IsSuccessStatusCode)
        {
            throw new Exception("Error al actualizar producto. Status: " + response.StatusCode);
        }
        
        var body = await response.Content.ReadAsStringAsync();

        if (string.IsNullOrEmpty(body))
        {
            throw new  Exception("Error al actualizar producto. Status: " + response.StatusCode);
        }
        
        return true;
    }

    public async Task<bool> EliminarProducto(ProductModel producto)
    {
        // Usamos el nuevo endpoint
        var request = new HttpRequestMessage(HttpMethod.Delete, $"{TABLE_ENDPOINT}?id=eq." + producto.Id);
        
        request.Headers.Add("Prefer", "return=reprentation");
        var response = await client.SendAsync(request);

        if (!response.IsSuccessStatusCode)
        {
            throw new Exception("Error al eliminar producto. Status: " + response.StatusCode);
        }
        
        var body = await response.Content.ReadAsStringAsync();

        if (string.IsNullOrEmpty(body))
        {
            throw new  Exception("Error al eliminar producto. Status: " + response.StatusCode);
        }

        return true;
    }

    public async Task<AvaloniaList<ProductModel>> ObtenerProductos()
    {
        var request = new HttpRequestMessage(HttpMethod.Get, TABLE_ENDPOINT);
        var response = await client.SendAsync(request);

        if (!response.IsSuccessStatusCode)
        {
            throw new Exception("Error al obtener productos. Status: " + response.StatusCode);
        }

        var listaString = await response.Content.ReadAsStringAsync();
        
        try
        {
            return JsonConvert.DeserializeObject<AvaloniaList<ProductModel>>(listaString);
        }
        catch (JsonException ex)
        {
            Console.WriteLine("Error de deserialización. JSON recibido: " + listaString);
            throw new Exception("Error al procesar la lista de productos: " + ex.Message);
        }
    }
}