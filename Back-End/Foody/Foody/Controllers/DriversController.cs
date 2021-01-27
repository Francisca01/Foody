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
    public class DriversController : ControllerBase
    {
        // GET: api/<DriversController>
        [HttpGet]
        public List<object> Get()//so pode ser acedido pelo admin
        {
            //token do user logado
            string token = Request.Headers["token"];
            if (token != null)
            {
                //vai buscar os utilizadores
                return UserService.GetUser(token, 1);
            }
            else
            {
                List<object> msg = new List<object>() { MessageService.AccessDenied() };
                return msg;
            }
        }

        // GET api/<DriversController>/5
        [HttpGet("{idUser}")]
        public object Get(int idUser)
        {
            //token do user logado
            string token = Request.Headers["token"];
            if (token != null)
            {
                //vai buscar o utilizadore
                return UserService.GetUserId(token, idUser);
            }
            else
            {
                return MessageService.AccessDenied();
            }


        }

        // PUT api/<DriversController>/5
        [HttpPut("{idUser}")]
        public Message Put(int idUser, [FromBody] User condutorUpdate)
        {
            //token do user logado
            string token = Request.Headers["token"];
            if (token != null)
            {
                return UserService.PutUser(token, condutorUpdate, idUser);
            }
            else
            {
                return MessageService.AccessDenied();
            }
           
        }

        // DELETE api/<DriversController>/5
        [HttpDelete("{idUser}")]
        public Message Delete(int idUser)
        {
            //token do user logado
            string token = Request.Headers["token"];
            if (token != null)
            {
                return UserService.DeleteUser(token, idUser);
            }
            else
            {
                return MessageService.AccessDenied();
            }
        }
    }
}
