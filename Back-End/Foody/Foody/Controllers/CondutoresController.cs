﻿using System.Collections.Generic;
using System.Linq;
using Foody.Models;
using Foody.Utils;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Foody.Controllers
{
    [Route("api/[controller]")]
    //[ApiController]
    public class CondutoresController : ControllerBase
    {
        // GET: api/<CondutoresController>
        [HttpGet]
        public List<object> Get()//so pode ser acedido pelo admin
        {
            //token do user logado
            string token = Request.Headers["token"][0];

            //vai buscar os utilizadores
            return UserService.GetUser(token, 1);
        }

        // GET api/<CondutoresController>/5
        [HttpGet("{idUtilizador}")]
        public object Get(int idUtilizador)
        {
            //token do user logado
            string token = Request.Headers["token"][0];

            //vai buscar o utilizadore
            return UserService.GetUserId(token, idUtilizador);
        }

        // PUT api/<CondutoresController>/5
        [HttpPut("{idUtilizador}")]
        public object Put(int idUtilizador, [FromBody] Utilizador condutorUpdate)
        {
            //token do user logado
            string token = Request.Headers["token"][0];

            return UserService.PutUser(token, condutorUpdate, idUtilizador);
        }

        // DELETE api/<CondutoresController>/5
        [HttpDelete("{idUtilizador}")]
        public object Delete(int idUtilizador)
        {
            //token do user logado
            string token = Request.Headers["token"][0];

            return UserService.DeleteUser(token, idUtilizador);
        }
    }
}
