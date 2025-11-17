using System.Threading.Tasks;
using Avalonia.Collections;
using Microsoft.EntityFrameworkCore;
using RepasoApp01.Data;

namespace RepasoApp01.Services;

public class DBService
{
    //METODO ASÍNCRONO QUE RETORNA UNA LISTA DE USUARIOS
    //RETORNA UN TASK POR QUE LAS OPERACIONES DENTRO DEL MÉTODO
    //SE EJECUTAN DE FORMA ASÍNCRONA
    public async Task<AvaloniaList<Usuario>> ObtenerTodosLosUsuarios()
    {
        //CREA LA INSTANCIA DEL CONTEXTO DE LA BASE DE DATOS Y 
        //ASEGURA LA CORRECTA LIBERACIÓN DE FORMA ASÍNCRONA
        await using var db = new AppDbContext();

        //COMPRUEBA SI LA BASE DE DATOS, SI NO EXISTE LA CREA
        //ESTA LLAMADA NO BLIQUEA EL HILO PRINCIPAL
        // AWAIT INDICA QUE ADEMAS SE DEBE ESPERAR A REALIZAR LA LLAMADA
        // PARA CONTINUAR CON EL RESTO DEL MÉTODO
        await db.Database.EnsureCreatedAsync();
        
        
        //EJECUTA LA CONSULTA A LA BASE DE DATOS DE FORMA ASÍNCRONA
        //RETORNA EN FORMA DE List<Usuario>
        var lista = await db.Usuarios.ToListAsync();

        return new AvaloniaList<Usuario>(lista);

    }
}