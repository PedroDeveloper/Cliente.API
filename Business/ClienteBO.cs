using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using Cliente.API.Data.Repository;
using Cliente.API.Model;

namespace Cliente.API.Business
{
	public class ClienteBO
	{
		private readonly ILoggerFactory _loggerFactory;
		private readonly ILogger<ClienteBO> _log;
		private readonly IConfiguration _config;

		public ClienteBO(ILoggerFactory loggerFactory, IConfiguration config)
		{
			_loggerFactory = loggerFactory;
			_log = loggerFactory.CreateLogger<ClienteBO>();
			_config = config;
		}

		#region Change Data

		public ClienteModel Insert(ClienteModel cliente)
		{
			ClienteRepository ClienteRepository;

			try
			{
				ClienteRepository = new ClienteRepository(_loggerFactory, _config);

				if (cliente.ID == 0)
				{
					cliente = ClienteRepository.Insert(cliente);
				}
				else
				{
					throw new Exception("ID diferente de 0, avalie a utilização do PUT");
				}
			}
			catch (Exception ex)
			{
				throw ex;
			}

			return cliente;
		}

		public ClienteModel Update(ClienteModel cliente)
		{
			ClienteRepository clienteRepository;

			try
			{
				clienteRepository = new ClienteRepository(_loggerFactory, _config);

				if (cliente.ID == 0)
				{
					throw new Exception("ID diferente de 0, avalie a utilização do POST");
				}
				else
				{
					clienteRepository.Update(cliente);
				}
			}
			catch (Exception ex)
			{
				throw ex;
			}

			return cliente;
		}

		public void Delete(int id)
		{
			ClienteRepository clienteRepository;
			
			ClienteModel cliente;

			try
			{
				if (id == 0)
				{
					throw new Exception("ID inválido");
				}
				else
				{
					clienteRepository = new ClienteRepository(_loggerFactory, _config);
					cliente = Get(id);
					if (clienteRepository != null)
					{

						clienteRepository.Delete(id);
					}
				}
			}
			catch (Exception ex)
			{
				throw ex;
			}

		}

		#endregion

		#region Retrieve Repository

		public ClienteModel Get(int id)
		{
			ClienteRepository clienteRepository;
			ClienteModel cliente;

			try
			{
				clienteRepository = new ClienteRepository(_loggerFactory, _config);

				cliente = clienteRepository.Get(id);
			}
			catch (Exception ex)
			{
				throw ex;
			}

			return cliente;
		}

		public List<ClienteModel> Get(string name = null)
		{
			ClienteRepository clienteRepository;
			List<ClienteModel> clientes;

			try
			{
				clienteRepository = new ClienteRepository(_loggerFactory, _config);

				clientes = clienteRepository.Get(name);
			}
			catch (Exception ex)
			{
				throw ex;
			}

			return clientes;
		}

		#endregion
	}

	
}
