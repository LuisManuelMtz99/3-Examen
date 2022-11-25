using AutoMapper;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using _3_Examen.DTOs;
using _3_Examen.Entidades;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using _3_Examen;

namespace _3_Examen.Controllers
{
    [ApiController]
    [Route("inquilinos")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class DatosController : ControllerBase
    {
        private readonly ApplicationDbContext dbContext;
        private readonly IMapper mapper;
        private readonly UserManager<IdentityUser> userManager;


        public DatosController(ApplicationDbContext context, IMapper mapper)
        {
            this.dbContext = context;
            this.mapper = mapper;
        }

        [HttpGet]
        [HttpGet("/listadoInquilino")]
        public async Task<ActionResult<List<Inquilino>>> GetAll()
        {
            return await dbContext.Inquilino.ToListAsync();
        }

        [HttpGet("{id:int}", Name = "obtenerInquilino")]
        public async Task<ActionResult<InquilinoDTOConDepartamento>> GetById(int id)
        {
            var dato = await dbContext.Inquilino

                .Include(datoDB => datoDB.DepartamentoInquilino)
                .ThenInclude(juegoDatoDB => juegoDatoDB.Departamento)
                //.Include(tipoDB => tipoDB.Tipos)
                .FirstOrDefaultAsync(x => x.Id == id);

            if (dato == null)
            {
                return NotFound();
            }


            return mapper.Map<InquilinoDTOConDepartamento>(dato);
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<ActionResult> Post(int datoId, InquilinoCreacionDTO datoCreacionDTO)
        {
            var emailClaim = HttpContext.User.Claims.Where(claim => claim.Type == "email").FirstOrDefault();
            var email = emailClaim.Value;

            var usuario = await userManager.FindByEmailAsync(email);
            var usuarioId = usuario.Id;

            var existeClase = await dbContext.Inquilino.AnyAsync(claseDB => claseDB.Id == datoId);
            if (!existeClase)
            {
                return NotFound();
            }

            var curso = mapper.Map<Inquilino>(datoCreacionDTO);
            curso.Id = datoId;
            curso.UsuarioId = usuarioId;
            dbContext.Add(curso);
            await dbContext.SaveChangesAsync();

            var datoDTO = mapper.Map<InquilinoDTO>(curso);

            return CreatedAtRoute("obtenerInquilino", new { id = curso.Id, claseId = datoId }, datoDTO);
        }


        [HttpPut("{id:int}")]
        public async Task<ActionResult> Put(int id, InquilinoCreacionDTO datoCreacionDTO)
        {
            var datoDB = await dbContext.Inquilino
                .Include(x => x.DepartamentoInquilino)
                .FirstOrDefaultAsync(x => x.Id == id);

            if (datoDB == null)
            {
                return NotFound();
            }

            datoDB = mapper.Map(datoCreacionDTO, datoDB);

 

            await dbContext.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult> Delete(int id)
        {
            var exist = await dbContext.Inquilino.AnyAsync(x => x.Id == id);
            if (!exist)
            {
                return NotFound("El Recurso no fue encontrado.");
            }

            dbContext.Remove(new Inquilino { Id = id });
            await dbContext.SaveChangesAsync();
            return Ok();
        }

     

        [HttpPatch("{id:int}")]
        public async Task<ActionResult> Patch(int id, JsonPatchDocument<InquilinoPatchDTO> patchDocument)
        {
            if (patchDocument == null) { return BadRequest(); }

            var categoriaDB = await dbContext.Inquilino.FirstOrDefaultAsync(x => x.Id == id);

            if (categoriaDB == null) { return NotFound(); }

            var categoriaDTO = mapper.Map<InquilinoPatchDTO>(categoriaDB);

            patchDocument.ApplyTo(categoriaDTO);

            var isValid = TryValidateModel(categoriaDTO);

            if (!isValid)
            {
                return BadRequest(ModelState);
            }

            mapper.Map(categoriaDTO, categoriaDB);

            await dbContext.SaveChangesAsync();
            return NoContent();
        }
    }
}