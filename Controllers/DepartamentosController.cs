using _3_Examen;
using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using _3_Examen.DTOs;
using _3_Examen.Entidades;
namespace _3_Examen.Controllers;
 
    [ApiController]
    [Route("departamentos")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "EsAdmin")]

    public class JuegosController : ControllerBase
    {
        private readonly ApplicationDbContext dbContext;
        private readonly IMapper mapper;
        private readonly IConfiguration configuration;

        public JuegosController(ApplicationDbContext context, IMapper mapper, IConfiguration configuration)
        {
            this.dbContext = context;
            this.mapper = mapper;
            this.configuration = configuration;
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult<List<GetDepartamentoDTO>>> Get()
        {
            var departamento = await dbContext.Departamento.ToListAsync();
            return mapper.Map<List<GetDepartamentoDTO>>(departamento);
        }


        [HttpGet("{id:int}", Name = "obtenerdepartamento")]
        public async Task<ActionResult<DepartamentoDTOConInquilinos>> Get(int id)
        {
            var juego = await dbContext.Departamento
                .Include(juegoDB => juegoDB.DepartamentoInquilino)
                .ThenInclude(juegoDatoDB => juegoDatoDB.Inquilino)
                .FirstOrDefaultAsync(juegoDB => juegoDB.Id == id);

            if (juego == null)
            {
                return NotFound();
            }

            return mapper.Map<DepartamentoDTOConInquilinos>(juego);

        }

        [HttpGet("{nombre}")]
        public async Task<ActionResult<List<GetDepartamentoDTO>>> Get([FromRoute] string nombre)
        {
            var juegos = await dbContext.Departamento.Where(juegoBD => juegoBD.Numero.Contains(nombre)).ToListAsync();

            return mapper.Map<List<GetDepartamentoDTO>>(juegos);

        }

        [HttpPost]
        public async Task<ActionResult> Post([FromBody] DepartamentoDTO juegoDto)
        {
            //Ejemplo para validar desde el controlador con la BD con ayuda del dbContext

            var existeJuegoMismoNombre = await dbContext.Departamento.AnyAsync(x => x.Numero == juegoDto.Numero);

            var juego = mapper.Map<Departamento>(juegoDto);

            dbContext.Add(juego);
            await dbContext.SaveChangesAsync();

            var juegoDTO = mapper.Map<GetDepartamentoDTO>(juego);

            return CreatedAtRoute("obtenerdepartamento", new { id = juego.Id }, juegoDTO);
        }

        [HttpPut("{id:int}")] // api/series/1
        public async Task<ActionResult> Put(DepartamentoDTO juegoCreacionDTO, int id)
        {
            var exist = await dbContext.Departamento.AnyAsync(x => x.Id == id);
            if (!exist)
            {
                return NotFound();
            }

            var juego = mapper.Map<Departamento>(juegoCreacionDTO);
            juego.Id = id;

            dbContext.Update(juego);
            await dbContext.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult> Delete(int id)
        {
            var exist = await dbContext.Departamento.AnyAsync(x => x.Id == id);
            if (!exist)
            {
                return NotFound("El Recurso no fue encontrado.");
            }

            dbContext.Remove(new Departamento()
            {
                Id = id
            });
            await dbContext.SaveChangesAsync();
            return Ok();
        }
    }
