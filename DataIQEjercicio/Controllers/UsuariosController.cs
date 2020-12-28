using DataIQEjercicio.Data;
using DataIQEjercicio.Modelo;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

// 6 - crear el controlador tipo api e ir a las propiedades del proy / depurar y poner este api/controlador como inicio

// 7 - realizar todas las acciones desde postman/compass/chrome, setear en postman ssl off

// 8 - instalar nuget Swagger: Swashbuckle.AspNetCore 5.6.3

namespace DataIQEjercicio.Controllers
{
    [Route("api/usuarios")]
   // [Route("api/usuarios/{id:guid}")]
    [ApiController]
    public class UsuariosController : ControllerBase
    {
        private readonly UsuarioDB _usuarioDB;

        public UsuariosController(UsuarioDB usuarioDB) 
        {
            _usuarioDB = usuarioDB;
        }

        /// <summary>
        /// Lista completa de usuarios
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        //public IActionResult Get()
        //{
        //    return Ok(_usuarioDB.Get());
        //}

        public async Task<IActionResult> Get()
        {
            return Ok(await _usuarioDB.GetAsync());
        }

        /// <summary>
        /// Usuario por su Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id:length(24)}", Name = "GetUsuario")]
        public async Task<IActionResult> GetByIdA(string id)
        {
            var  usuario = await _usuarioDB.GetByIdAsync(id);
            if (usuario == null)
            {
                return NotFound();
            }
            return Ok(usuario);
        }

        /// <summary>
        /// Insetar un usuario nuevo
        /// </summary>
        /// <param name="usuario"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult Create(Usuario usuario)
        {
            _usuarioDB.Create(usuario);
            return CreatedAtRoute("GetUsuario", new
            {
                id = usuario.Id.ToString()
            }, usuario);
        }

        /// <summary>
        /// Modificar usuario por Id
        /// </summary>
        /// <param name="id"></param>
        /// <param name="usu"></param>
        /// <returns></returns>
        [HttpPut("{id:length(24)}")]
        public IActionResult Update(string id, Usuario usu)
        {
            var cliente = _usuarioDB.GetByIdAsync(id);
            if (cliente == null)
            {
                return NotFound();
            }
            _usuarioDB.Update(id, usu);

            return NoContent();
        }

        /// <summary>
        /// Eliminar un usuario por Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id:length(24)}")]
        public async Task<IActionResult> DeleteById(string id)
        {
            var usuario = await _usuarioDB.GetByIdAsync(id);

            if (usuario == null)
            {
                return NotFound();
            }

            _usuarioDB.DeleteById(usuario.Id);

            return NoContent();
        }

    }
}
