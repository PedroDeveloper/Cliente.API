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

namespace Endereco.API.Controllers
{
	[EnableCors("Policy1")]
	[ApiController]
	[Route("api/[controller]")]

	public class EnderecoController : ControllerBase
	{
		private readonly ILoggerFactory _loggerFactory;
		private readonly ILogger<EnderecoController> _log;
		private readonly IConfiguration _config;

		public EnderecoController(ILoggerFactory loggerFactory, IConfiguration config)
		{
			_loggerFactory = loggerFactory;
			_log = loggerFactory.CreateLogger<EnderecoController>();
			_config = config;
		}


		[HttpGet]
		
		public IActionResult Get(string name = null)
		{
			EnderecoBO enderecoBO;
			List<EnderecoModel> enderecos;
			ObjectResult response;

			try
			{
				_log.LogInformation("Starting Get()");

				enderecoBO = new EnderecoBO(_loggerFactory, _config);
				enderecos = enderecoBO.Get(name);

				response = Ok(enderecos);

				_log.LogInformation($"Finishing Get() with '{enderecos.Count}' results");
			}
			catch (Exception ex)
			{
				_log.LogError(ex.Message);
				response = StatusCode(500, ex.Message);
			}

			return response;
		}

		// GET: api/Cliente/5
		[HttpGet("{id}", Name = "GetEndereco")]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		[ProducesResponseType(StatusCodes.Status500InternalServerError)]
		public IActionResult Get(int id)
		{
			EnderecoBO enderecoBO;
			EnderecoModel endereco;
			ObjectResult response;

			try
			{
				_log.LogInformation($"Starting Get( {id} )");

				enderecoBO = new EnderecoBO(_loggerFactory, _config);

				endereco = enderecoBO.Get(id);

				if (endereco != null)
				{
					response = Ok(endereco);
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
		public IActionResult Post([FromBody] EnderecoModel endereco)
		{
			EnderecoBO enderecoBO;
			ObjectResult response;

			try
			{
				_log.LogInformation($"Starting Post('{JsonConvert.SerializeObject(endereco, Formatting.None)}')");

				enderecoBO = new EnderecoBO(_loggerFactory, _config);

				endereco = enderecoBO.Insert(endereco);

				response = Ok(endereco);

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
		public IActionResult Put(int id, EnderecoModel endereco)
		{
			EnderecoBO enderecoBO;
			ObjectResult response;

			try
			{
				_log.LogInformation($"Starting Put( {id}, '{JsonConvert.SerializeObject(endereco, Formatting.None)}')");

				enderecoBO = new EnderecoBO(_loggerFactory, _config);

				endereco.ID_end = id;
				endereco = enderecoBO.Update(endereco);

				response = Ok(endereco);

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
			EnderecoBO enderecoBO;
			ObjectResult response;

			try
			{
				_log.LogInformation($"Starting Delete( {id} )");

				enderecoBO = new EnderecoBO(_loggerFactory, _config);
				enderecoBO.Delete(id);

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
