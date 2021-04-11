using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using Cliente.API.Data.Repository;
using Cliente.API.Model;

namespace Cliente.API.Business
{
	public class ClienteEnderecoBO
	{
		private readonly ILoggerFactory _loggerFactory;
		private readonly ILogger<ClienteEnderecoBO> _log;
		private readonly IConfiguration _config;

		public ClienteEnderecoBO(ILoggerFactory loggerFactory, IConfiguration config)
		{
			_loggerFactory = loggerFactory;
			_log = loggerFactory.CreateLogger<ClienteEnderecoBO>();
			_config = config;
		}

		#region Change Data

		public EnderecoModel Insert(EnderecoModel endereco)
		{
			ClienteEnderecoRepository ClienteEnderecoRepository;

			try
			{
				ClienteEnderecoRepository = new ClienteEnderecoRepository(_loggerFactory, _config);

				if (endereco.ID_end == 0)
				{
					endereco = ClienteEnderecoRepository.Insert(endereco);
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

			return endereco;
		}

		public EnderecoModel Update(EnderecoModel endereco)
		{
			ClienteEnderecoRepository clienteEnderecoRepository;

			try
			{
				clienteEnderecoRepository = new ClienteEnderecoRepository(_loggerFactory, _config);

				if (endereco.ID_end == 0)
				{
					throw new Exception("ID diferente de 0, avalie a utilização do POST");
				}
				else
				{
					clienteEnderecoRepository.Update(endereco);
				}
			}
			catch (Exception ex)
			{
				throw ex;
			}

			return endereco;
		}

		

		#endregion

		#region Retrieve Repository

		public List<EnderecoModel> Get(int id)
		{
			ClienteEnderecoRepository clienteEnderecoRepository;
			List<EnderecoModel> endereco;

			try
			{
				clienteEnderecoRepository = new ClienteEnderecoRepository(_loggerFactory, _config);

				endereco = clienteEnderecoRepository.Get(id);
			}
			catch (Exception ex)
			{
				throw ex;
			}

			return endereco;
		}

		public List<EnderecoModel> Get( string name = null)
		{
			ClienteEnderecoRepository clienteEnderecoRepository;
			List<EnderecoModel> enderecos;

			try
			{
				clienteEnderecoRepository = new ClienteEnderecoRepository(_loggerFactory, _config);

				enderecos = clienteEnderecoRepository.Get(name);
			}
			catch (Exception ex)
			{
				throw ex;
			}

			return enderecos;
		}

		#endregion
	}

	
}
