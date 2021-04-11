using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Cliente.API.Business;
using Cliente.API.Model;

namespace Cliente.API.Controllers
{
	[EnableCors("Policy1")]
	[ApiController]
	[Route("api/[controller]")]

	public class ClienteController : ControllerBase
	{
		private readonly ILoggerFactory _loggerFactory;
		private readonly ILogger<ClienteController> _log;
		private readonly IConfiguration _config;

		public ClienteController(ILoggerFactory loggerFactory, IConfiguration config)
		{
			_loggerFactory = loggerFactory;
			_log = loggerFactory.CreateLogger<ClienteController>();
			_config = config;
		}


		[HttpGet]
		
		public IActionResult Get(string name = null)
		{
			ClienteBO clienteBO;
			List<ClienteModel> clientes;
			ObjectResult response;

			try
			{
				_log.LogInformation("Starting Get()");

				clienteBO = new ClienteBO(_loggerFactory, _config);
				clientes = clienteBO.Get(name);

				response = Ok(clientes);

				_log.LogInformation($"Finishing Get() with '{clientes.Count}' results");
			}
			catch (Exception ex)
			{
				_log.LogError(ex.Message);
				response = StatusCode(500, ex.Message);
			}

			return response;
		}

		// GET: api/Cliente/5
		[HttpGet("{id}", Name = "GetCliente")]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		[ProducesResponseType(StatusCodes.Status500InternalServerError)]
		public IActionResult Get(int id)
		{
			ClienteBO clienteBO;
			ClienteModel cliente;
			ObjectResult response;

			try
			{
				_log.LogInformation($"Starting Get( {id} )");

				clienteBO = new ClienteBO(_loggerFactory, _config);

				cliente = clienteBO.Get(id);

				if (cliente != null)
				{
					response = Ok(cliente);
				}
				else
				{
					response = NotFound(string.Empty);
				}

				_log.LogInformation($"Finishing Get( {id} )");
			}
			catch (Exception ex)
			{
				_log.LogError(ex.Message);
				response = StatusCode(500, ex.Message);
			}

			return response;
		}

		[HttpPost]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status500InternalServerError)]
		public IActionResult Post([FromBody] ClienteModel cliente)
		{
			ClienteBO clienteBO;
			ObjectResult response;

			try
			{
				_log.LogInformation($"Starting Post('{JsonConvert.SerializeObject(cliente, Formatting.None)}')");

				clienteBO = new ClienteBO(_loggerFactory, _config);

				cliente = clienteBO.Insert(cliente);

				response = Ok(cliente);

				_log.LogInformation($"Finishing Post");
			}
			catch (Exception ex)
			{
				_log.LogError(ex.Message);
				response = StatusCode(500, ex.Message);
			}

			return response;
		}

		// PUT: api/Cliente/5
		[HttpPut("{id}")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status500InternalServerError)]
		public IActionResult Put(int id, ClienteModel cliente)
		{
			ClienteBO clienteBO;
			ObjectResult response;

			try
			{
				_log.LogInformation($"Starting Put( {id}, '{JsonConvert.SerializeObject(cliente, Formatting.None)}')");

				clienteBO = new ClienteBO(_loggerFactory, _config);

				cliente.ID = id;
				cliente = clienteBO.Update(cliente);

				response = Ok(cliente);

				_log.LogInformation($"Finishing Put( {id} )");
			}	
			catch (Exception ex)
			{
				_log.LogError(ex.Message);
				response = StatusCode(500, ex.Message);
			}

			return response;
		}

		// DELETE: api/ApiWithActions/5
		[HttpDelete("{id}")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		[ProducesResponseType(StatusCodes.Status500InternalServerError)]
		public IActionResult Delete(int id)
		{
			ClienteBO clienteBO;
			ObjectResult response;

			try
			{
				_log.LogInformation($"Starting Delete( {id} )");

				clienteBO = new ClienteBO(_loggerFactory, _config);
				clienteBO.Delete(id);

				response = Ok(string.Empty);

				_log.LogInformation($"Finishing Delete( {id} )");
			}
			catch (Exception ex)
			{
				_log.LogError(ex.Message);
				response = StatusCode(500, ex.Message);
			}

			return response;
		}
	}
}
