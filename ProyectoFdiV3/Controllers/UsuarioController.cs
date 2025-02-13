using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using ProyectoFDI.API.V3.Models;
using ProyectoFdiV3.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

[Route("api/[controller]")]
[ApiController]
public class UsuarioController : ControllerBase
{
    private readonly ProyectoFdiV3DbContext _context;
    private readonly IConfiguration _config;


    public UsuarioController(ProyectoFdiV3DbContext context, IConfiguration config)
    {
        _context = context;
        _config = config;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Usuario>>> GetUsuarios()
    {
        return await _context.Usuarios.ToListAsync();
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Usuario>> GetUsuario(int id)
    {
        var usuario = await _context.Usuarios.FindAsync(id);
        if (usuario == null)
        {
            return NotFound();
        }
        return usuario;
    }

    [HttpPost]
    public async Task<ActionResult<Usuario>> PostUsuario(Usuario usuario)
    {
        _context.Usuarios.Add(usuario);
        await _context.SaveChangesAsync();
        return CreatedAtAction("GetUsuario", new { id = usuario.IdUsu }, usuario);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> PutUsuario(int id, Usuario usuario)
    {
        if (id != usuario.IdUsu)
        {
            return BadRequest();
        }

        _context.Entry(usuario).State = EntityState.Modified;
        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!UsuarioExists(id))
            {
                return NotFound();
            }
            else
            {
                throw;
            }
        }

        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult<Usuario>> DeleteUsuario(int id)
    {
        var usuario = await _context.Usuarios.FindAsync(id);
        if (usuario == null)
        {
            return NotFound();
        }

        _context.Usuarios.Remove(usuario);
        await _context.SaveChangesAsync();

        return usuario;
    }

    [HttpPost("/api/Usuario/login")]
    public async Task<ActionResult<UsuarioLoginResponse>> LoginUsuario(UsuarioLoginRequest usuarioRequest)
    {

        var usuario = await _context.Usuarios.FirstOrDefaultAsync(u => u.NombreUsu == usuarioRequest.NombreUsu);

        if (usuario == null)
        {
            return Unauthorized("Credenciales no validas");
        }
        if (usuario.ClaveUsu != usuarioRequest.ClaveUsu)
        {
            return Unauthorized("Credenciales no validas");
        }

        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

        var claims = new[]
        {
                new Claim(JwtRegisteredClaimNames.Sub, usuario.NombreUsu),
                new Claim("Roles", usuario.RolesUsu),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

        var Sectoken = new JwtSecurityToken(
            issuer: _config["Jwt:Issuer"],
            audience: _config["Jwt:Issuer"],
            claims: claims,
            expires: DateTime.Now.AddMinutes(5),
            signingCredentials: credentials
        );

        string? token = new JwtSecurityTokenHandler().WriteToken(Sectoken);

        UsuarioLoginResponse usuarioResponse = new UsuarioLoginResponse()
        {
            IdUsu = usuario.IdUsu,
            NombreUsu = usuario.NombreUsu,
            TokenUsu = token,
            RolesUsu = usuario.RolesUsu,
            ActivoUsu = usuario.ActivoUsu
        };

        return Ok(usuarioResponse);
    }

    [HttpGet("/api/Usuario/validarlogin")]
    [Authorize]
    public ActionResult ValidarLogin()
    {

        var userClaims = User.Claims.Select(c => new { c.Type, c.Value }).ToList();
        return Ok(new { Message = "Token válido", Claims = userClaims });
    }

    private bool UsuarioExists(int id)
    {
        return _context.Usuarios.Any(e => e.IdUsu == id);
    }
}
