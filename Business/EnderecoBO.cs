using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using Cliente.API.Data.Repository;
using Cliente.API.Model;

namespace Cliente.API.Business
{
	public class EnderecoBO
	{
		private readonly ILoggerFactory _loggerFactory;
		private readonly ILogger<EnderecoBO> _log;
		private readonly IConfiguration _config;

		public EnderecoBO(ILoggerFactory loggerFactory, IConfiguration config)
		{
			_loggerFactory = loggerFactory;
			_log = loggerFactory.CreateLogger<EnderecoBO>();
			_config = config;
		}

		#region Change Data

		public EnderecoModel Insert(EnderecoModel endereco)
		{
			EnderecoRepository EnderecoRepository;

			try
			{
				EnderecoRepository = new EnderecoRepository(_loggerFactory, _config);

				if (endereco.ID_end == 0)
				{
					endereco = EnderecoRepository.Insert(endereco);
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
			EnderecoRepository enderecoRepository;

			try
			{
				enderecoRepository = new EnderecoRepository(_loggerFactory, _config);

				if (endereco.ID_end == 0)
				{
					throw new Exception("ID diferente de 0, avalie a utilização do POST");
				}
				else
				{
					enderecoRepository.Update(endereco);
				}
			}
			catch (Exception ex)
			{
				throw ex;
			}

			return endereco;
		}

		public void Delete(int id)
		{
			EnderecoRepository enderecoRepository;
			
			EnderecoModel endereco;

			try
			{
				if (id == 0)
				{
					throw new Exception("ID inválido");
				}
				else
				{
					enderecoRepository = new EnderecoRepository(_loggerFactory, _config);
					endereco = Get(id);
					if (enderecoRepository != null)
					{

						enderecoRepository.Delete(id);
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

		public EnderecoModel Get(int id)
		{
			EnderecoRepository enderecoRepository;
			EnderecoModel endereco;

			try
			{
				enderecoRepository = new EnderecoRepository(_loggerFactory, _config);

				endereco = enderecoRepository.Get(id);
			}
			catch (Exception ex)
			{
				throw ex;
			}

			return endereco;
		}

		public List<EnderecoModel> Get( string name = null)
		{
			EnderecoRepository enderecoRepository;
			List<EnderecoModel> enderecos;

			try
			{
				enderecoRepository = new EnderecoRepository(_loggerFactory, _config);

				enderecos = enderecoRepository.Get(name);
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
