using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using Cliente.API.Data.Base;
using Cliente.API.Model;


namespace Cliente.API.Data.Repository
{
    public class ClienteRepository
    {

		private readonly ILoggerFactory _loggerFactory;
		private readonly ILogger<ClienteRepository> _log;
		private readonly IConfiguration _config;

		public ClienteRepository(ILoggerFactory loggerFactory, IConfiguration config)
		{
			_loggerFactory = loggerFactory;
			_log = loggerFactory.CreateLogger<ClienteRepository>();
			_config = config;
		}

		#region LoadModel

		private List<ClienteModel> Load(DataSet data)
		{
			List<ClienteModel> clientes;
			ClienteModel cliente;

			try
			{
				clientes = new List<ClienteModel>();

				foreach (DataRow row in data.Tables[0].Rows)
				{
					cliente = new ClienteModel();

					cliente.ID = row.Field<int>("ID");
					cliente.Cnpj = row.Field<string>("Cnpj");
					cliente.Cpf = row.Field<string>("Cpf");
					cliente.Nome = row.Field<string>("Nome");
					cliente.DtNas = row.Field<string>("DtNas");
					cliente.RazaoSoci = row.Field<string>("RazaoSoci");
					cliente.Email = row.Field<string>("Email");
					cliente.Tel = row.Field<string>("tel");



					clientes.Add(cliente);
				}
			}
			catch (Exception ex)
			{
				throw ex;
			}

			return clientes;
		}

		#endregion

		#region Change Data

		public ClienteModel Insert(ClienteModel cliente)
		{
			SqlHelper dataConnection;
			SqlCommand command;

			try
			{
				dataConnection = new SqlHelper(_loggerFactory, _config);

				command = new SqlCommand($@"INSERT INTO cliente
											(
												 
												Cnpj
												,Cpf
												,Nome
												,DtNas
												,RazaoSoci
												,Email
												,Tel
											)
										 OUTPUT inserted.ID 
										 VALUES
											(
												 
												@Cnpj
												,@Cpf
												,@Nome
												,@DtNas
												,@RazaoSoci
												,@Email
												,@Tel
											)");

				
				command.Parameters.AddWithValue("Cnpj", cliente.Cnpj.AsDbValue());
				command.Parameters.AddWithValue("Cpf", cliente.Cpf.AsDbValue());
				command.Parameters.AddWithValue("Nome", cliente.Nome.AsDbValue());
				command.Parameters.AddWithValue("DtNas", cliente.DtNas.AsDbValue());
                command.Parameters.AddWithValue("RazaoSoci", cliente.RazaoSoci.AsDbValue());
				command.Parameters.AddWithValue("Email", cliente.Email.AsDbValue());
				command.Parameters.AddWithValue("Tel", cliente.Tel.AsDbValue());


				cliente.ID = (int)dataConnection.ExecuteScalar(command);
			}
			catch (Exception ex)
			{
				throw ex;
			}

			return cliente;
		}

		public ClienteModel Update(ClienteModel Cliente)
		{
			SqlHelper dataConnection;
			SqlCommand command;

			try
			{
				dataConnection = new SqlHelper(_loggerFactory, _config);

				command = new SqlCommand($@"UPDATE cliente SET
 
												Cnpj = @Cnpj
												,Cpf = @Cpf
												,Nome = @Nome
												,DtNas =@DtNasc
												,RazaoSoci =@RazaoSoci
												,Email =@Email
												,Tel =@Tel

											WHERE ID = @ID");

				command.Parameters.AddWithValue("ID", Cliente.ID.AsDbValue());
				command.Parameters.AddWithValue("Cnpj", Cliente.Cnpj.AsDbValue());
				command.Parameters.AddWithValue("Cpf", Cliente.Cpf.AsDbValue());
				command.Parameters.AddWithValue("Nome", Cliente.Nome.AsDbValue());
				command.Parameters.AddWithValue("DtNasc", Cliente.DtNas.AsDbValue());
				command.Parameters.AddWithValue("RazaoSoci", Cliente.RazaoSoci.AsDbValue());
				command.Parameters.AddWithValue("Email", Cliente.Email.AsDbValue());
				command.Parameters.AddWithValue("Tel", Cliente.Tel.AsDbValue());


				dataConnection.ExecuteNonQuery(command);
			}
			catch (Exception ex)
			{
				throw ex;
			}

			return Cliente;
		}

		public bool Delete(int id)
		{
			SqlHelper dataConnection;
			SqlCommand command;
			int result;

			try
			{
				dataConnection = new SqlHelper(_loggerFactory, _config);

				command = new SqlCommand($@"DELETE from cliente WHERE ID = @ID");

				command.Parameters.AddWithValue("ID", id.AsDbValue());

				result = dataConnection.ExecuteNonQuery(command);
			}
			catch (Exception ex)
			{
				throw ex;
			}

			return (result > 0);
		}

		#endregion

		#region Retrieve Data

		public ClienteModel Get(int id)
		{
			SqlHelper dataConnection;
			SqlCommand command;
			DataSet dataSet;

			ClienteModel Cliente;

			try
			{
				dataConnection = new SqlHelper(_loggerFactory, _config);

				command = new SqlCommand($"SELECT * FROM cliente WHERE ID = @ID");
				command.Parameters.AddWithValue("ID", id.AsDbValue());

				dataSet = dataConnection.ExecuteDataSet(command);

				Cliente = Load(dataSet).FirstOrDefault();
			}
			catch (Exception ex)
			{
				throw ex;
			}

			return Cliente;
		}

		public List<ClienteModel> Get(string Nome = null)
		{
			SqlHelper dataConnection;
			SqlCommand command;
			DataSet dataSet;

			List<ClienteModel> Clientes;
			List<string> clauses;

			try
			{
				dataConnection = new SqlHelper(_loggerFactory, _config);

				command = new SqlCommand($"SELECT * FROM cliente");

				clauses = new List<string>();
				if (!string.IsNullOrEmpty(Nome))
				{
					clauses.Add($"Nome like '%' + @Nome + '%'");
					command.Parameters.AddWithValue("Nome", Nome.AsDbValue());
				}

				if (clauses.Count > 0)
				{
					command.CommandText += $" WHERE {string.Join(" and ", clauses)}";
				}

				dataSet = dataConnection.ExecuteDataSet(command);

				Clientes = Load(dataSet);
			}
			catch (Exception ex)
			{
				throw ex;
			}

			return Clientes;
		}

		#endregion
	}

}

