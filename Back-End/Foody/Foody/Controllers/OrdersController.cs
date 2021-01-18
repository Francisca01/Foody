﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Foody.Models;
using Foody.Utils;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Foody.Controllers
{
    [Route("api/[controller]")]
    //[ApiController]
    public class OrdersController : ControllerBase
    {
        // GET api/<OrdersController>/5
        [HttpGet("myOrders")]
        public List<object> GetUserOrders()
        {
            //token do user logado
            string token = Request.Headers["token"][0];

            int[] userLoggedIn = UserService.UserLoggedIn(token);

            //verifica se o utilizador tem alguma Encomenda
            // o valor -1 deixa o utilizador passar para ver as suas orders
            if (OrderService.VerifyOrderAccess(userLoggedIn[0], -1))
            {
                return OrderService.GetOrdersUserId(userLoggedIn[0]);
            }
            else
            {
                List<object> msg = new List<object>() { MessageService.WithoutResults() };
                return msg;
            }
        }

        // GET api/<OrdersController>/5
        [HttpGet("{idOrder}")]
        public object Get(int idOrder)
        {
            //token do user logado
            string token = Request.Headers["token"][0];

            int[] userLoggedIn = UserService.UserLoggedIn(token);

            //verifica se o utilizador tem alguma Encomenda
            if (OrderService.VerifyOrderAccess(userLoggedIn[0], idOrder))
            {
                return OrderService.GetOrderId(idOrder);
            }
            else
            {
                return MessageService.AccessDenied();
            }
        }

        // POST api/<OrdersController>
        [HttpPost]
        public Message Post([FromBody] OrderProduct newOrderProduct)
        {
            //token do user com a sessão iniciada
            string token = Request.Headers["token"][0];

            int[] userLoggedIn = UserService.UserLoggedIn(token);

            return OrderService.VerifyOrder(userLoggedIn, newOrderProduct, false, -1, -1);
        }

        // POST api/<OrdersController>
        [HttpPost("{idOrder}")]
        public Message PostAddOrder(int idOrder, [FromBody] OrderProduct newOrderProduct)
        {
            //adiciona o product a order atual
            newOrderProduct.idOrder = idOrder;

            //token do user com a sessão iniciada
            string token = Request.Headers["token"][0];

            int[] userLoggedIn = UserService.UserLoggedIn(token);

            return OrderService.VerifyOrder(userLoggedIn, newOrderProduct, false, -1, -1);
        }

        // PUT api/<OrdersController>/5
        [HttpPut("{idOrder}/{idProduct}")]
        public Message Put(int idOrder, int idProduct, [FromBody] OrderProduct orderProductUpdate)
        {
            //token do user com a sessão iniciada
            string token = Request.Headers["token"][0];

            int[] userLoggedIn = UserService.UserLoggedIn(token);

            return OrderService.VerifyOrder(userLoggedIn, orderProductUpdate, true, idOrder, idProduct);
        }

        // DELETE api/<OrdersController>/5
        [HttpDelete("{idOrder}")]
        public Message Delete(int idOrder)
        {
            //token do user com a sessão iniciada
            string token = Request.Headers["token"][0];

            int[] userLoggedIn = UserService.UserLoggedIn(token);

            return OrderService.DeleteOrder(userLoggedIn[0], idOrder, -1);
        }

        [HttpDelete("{idOrder}/{idProduct}")]
        public Message Delete(int idOrder, int idProduct)
        {
            //token do user com a sessão iniciada
            string token = Request.Headers["token"][0];

            int[] userLoggedIn = UserService.UserLoggedIn(token);

            return OrderService.DeleteOrder(userLoggedIn[0], idOrder, idProduct);
        }
    }
}
