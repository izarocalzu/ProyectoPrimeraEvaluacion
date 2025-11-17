using System;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Duende.IdentityModel.Jwk;
using Duende.IdentityModel.OidcClient;
using RepasoApp01.Data;

namespace RepasoApp01.Services;

public class GoogleAuthService
{
    public async Task<Usuario> LoginAsync(Usuario user)
    {
        using ( var database = new AppDbContext())
            database.Database.EnsureCreated();
        
        var client = await CreateClient();

        //ENTRA SI EL USUARIO TIENE UN TOKEN DE REFRESCO, ES DECIR,
        //SE HA LOGUEADO EN EL SISTEMA ALGUNA VEZ
        if (!string.IsNullOrEmpty(user.RefreshToken))
        {
            var refreshResult = await client.RefreshTokenAsync(user.RefreshToken);
            if (!refreshResult.IsError)
            {
                Console.WriteLine( "SESION RESTAURADA" );
                return user;
            }
            Console.WriteLine("NO SE PUEDO COMPROBAR EL TOKEN, SE PEDIRA LOGIN NUEVAMENTE");
        }
        
        var result = await client.LoginAsync();
        if (result.IsError)
        {
            Console.WriteLine("ERROR AL REGISTRAR USUARIO");
        }

        var googlesub = result.User.FindFirst("sub")?.Value;
        var email = result.User.FindFirst("email")?.Value;
        var imagen = result.User.FindFirst("picture")?.Value;
        var nombre = result.User.FindFirst("name")?.Value;
        Console.WriteLine(googlesub+" "+email+" "+imagen+" "+nombre);
        var db = new AppDbContext();
        var usuario = db.Usuarios.FirstOrDefault(u => u.GoogleSub == googlesub);
        if (usuario == null)
        {
            usuario = new Usuario()
            {
                GoogleSub = googlesub,
                Email = email,
                Nombre = nombre,
                ImageUrl = imagen,
                RefreshToken = result.RefreshToken
            };
            db.Usuarios.Add(usuario);
            await db.SaveChangesAsync();
            Console.WriteLine("Usuario REGISTRADO CON EXITO");
        }
        else
        {
            Console.WriteLine("EL USUARIO YA EXISTE");
        }


    db.Dispose();
    return usuario;
    }
    
    public async Task<OidcClient> CreateClient()
    {
        using var http = new HttpClient();
        var keySet = await http.GetStringAsync("https://www.googleapis.com/oauth2/v3/certs");
        var jwks = new JsonWebKeySet(keySet);

        var options = new OidcClientOptions
        {
            Authority = "https://accounts.google.com",
            ClientId = "68647029085-8mi0ogh6lmrjps2hot385ieuqq55t4uf.apps.googleusercontent.com",
            ClientSecret = "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",
            Scope = "openid profile email",
            RedirectUri = "http://127.0.0.1:7890/",
            Browser = new SystemBrowser(7890),
            ProviderInformation = new ProviderInformation
            {
                AuthorizeEndpoint = "https://accounts.google.com/o/oauth2/v2/auth",
                TokenEndpoint = "https://oauth2.googleapis.com/token",
                UserInfoEndpoint = "https://openidconnect.googleapis.com/v1/userinfo",
                IssuerName = "https://accounts.google.com",
                KeySet = jwks
            }
        };

        return new OidcClient(options);
    }
}