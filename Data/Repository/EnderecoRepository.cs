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
	public class EnderecoRepository
	{

		private readonly ILoggerFactory _loggerFactory;
		private readonly ILogger<EnderecoRepository> _log;
		private readonly IConfiguration _config;

		public EnderecoRepository(ILoggerFactory loggerFactory, IConfiguration config)
		{
			_loggerFactory = loggerFactory;
			_log = loggerFactory.CreateLogger<EnderecoRepository>();
			_config = config;
		}

		#region LoadModel

		private List<EnderecoModel> Load(DataSet data)
		{
			List<EnderecoModel> enderecos;
			EnderecoModel endereco;

			try
			{
				enderecos = new List<EnderecoModel>();

				foreach (DataRow row in data.Tables[0].Rows)
				{
					endereco = new EnderecoModel();

					endereco.ID_end = row.Field<int>("ID_end");
					endereco.ID_cliente = row.Field<int>("ID_cliente");
					endereco.Nome_end = row.Field<string>("Nome_end");
					endereco.Tel_end = row.Field<string>("Tel_end");
					endereco.Cep = row.Field<string>("Cep");
					endereco.Endereco = row.Field<string>("Endereco");
					endereco.Numero = row.Field<string>("Numero");
					endereco.Complemento = row.Field<string>("Complemento");
					endereco.Bairro = row.Field<string>("Bairro");
					endereco.Cidade = row.Field<string>("Cidade");
					endereco.Estado = row.Field<string>("Estado");



					enderecos.Add(endereco);
				}
			}
			catch (Exception ex)
			{
				throw ex;
			}

			return enderecos;
		}

		#endregion

		#region Change Data

		public EnderecoModel Insert(EnderecoModel Endereco)
		{
			SqlHelper dataConnection;
			SqlCommand command;

			try
			{
				dataConnection = new SqlHelper(_loggerFactory, _config);

				command = new SqlCommand($@"INSERT INTO Endereco
											(
												 ID_end
												,ID_cliente
												,Nome_end
												,Tel_end
												,Cep
												,Endereco	
												,Numero
												,Complemento
												,Bairro
												,Cidade
												,Estado
											)
										 OUTPUT inserted.ID 
										 VALUES
											(
												 @ID_end
												,@ID_cliente
												,@Nome_end
												,@Tel_end
												,@Cep
												,@Endereco	
												,@Numero
												,@Complemento
												,@Bairro
												,@Cidade
												,@Estado
											)");

				command.Parameters.AddWithValue("ID_end", Endereco.ID_end.AsDbValue());
				command.Parameters.AddWithValue("ID_cliente", Endereco.ID_cliente.AsDbValue());
				command.Parameters.AddWithValue("Nome_end", Endereco.Nome_end.AsDbValue());
				command.Parameters.AddWithValue("Tel_end", Endereco.Tel_end.AsDbValue());
				command.Parameters.AddWithValue("Cep", Endereco.Cep.AsDbValue());
				command.Parameters.AddWithValue("Endereco", Endereco.Endereco.AsDbValue());
				command.Parameters.AddWithValue("Numero", Endereco.Numero.AsDbValue());
				command.Parameters.AddWithValue("Complemento", Endereco.Complemento.AsDbValue());
				command.Parameters.AddWithValue("Bairro", Endereco.Bairro.AsDbValue());
				command.Parameters.AddWithValue("Cidade", Endereco.Cidade.AsDbValue());
				command.Parameters.AddWithValue("Estado", Endereco.Cidade.AsDbValue());

				Endereco.ID_end = (int)dataConnection.ExecuteScalar(command);
			}
			catch (Exception ex)
			{
				throw ex;
			}

			return Endereco;
		}

		public EnderecoModel Update(EnderecoModel Endereco)
		{
			SqlHelper dataConnection;
			SqlCommand command;

			try
			{
				dataConnection = new SqlHelper(_loggerFactory, _config);

				command = new SqlCommand($@"UPDATE Endereco SET

												 ID_end =@ID_end
												,ID_cliente=@ID_cliente
												,Nome_end= @Nome_end
												,Tel_end= @Tel_end
												,Cep= @Cep
												,Endereco=	@Endereco
												,Numero= @Numero
												,Complemento= @Complemento
												,Bairro= @Bairro
												,Cidade= @Cidade
												,Estado= @Estado

											WHERE ID_end = @ID_end");

				command.Parameters.AddWithValue("ID_end", Endereco.ID_end.AsDbValue());
				command.Parameters.AddWithValue("ID_cliente", Endereco.ID_cliente.AsDbValue());
				command.Parameters.AddWithValue("Nome_end", Endereco.Nome_end.AsDbValue());
				command.Parameters.AddWithValue("Tel_end", Endereco.Tel_end.AsDbValue());
				command.Parameters.AddWithValue("Cep", Endereco.Cep.AsDbValue());
				command.Parameters.AddWithValue("Endereco", Endereco.Endereco.AsDbValue());
				command.Parameters.AddWithValue("Numero", Endereco.Numero.AsDbValue());
				command.Parameters.AddWithValue("Complemento", Endereco.Complemento.AsDbValue());
				command.Parameters.AddWithValue("Bairro", Endereco.Bairro.AsDbValue());
				command.Parameters.AddWithValue("Cidade", Endereco.Cidade.AsDbValue());
				command.Parameters.AddWithValue("Estado", Endereco.Cidade.AsDbValue());

				dataConnection.ExecuteNonQuery(command);
			}
			catch (Exception ex)
			{
				throw ex;
			}

			return Endereco;
		}

		public bool Delete(int id)
		{
			SqlHelper dataConnection;
			SqlCommand command;
			int result;

			try
			{
				dataConnection = new SqlHelper(_loggerFactory, _config);

				command = new SqlCommand($@"DELETE from Endereco WHERE ID_end = @ID_end");

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

		public EnderecoModel Get(int id)
		{
			SqlHelper dataConnection;
			SqlCommand command;
			DataSet dataSet;

			EnderecoModel Cliente;

			try
			{
				dataConnection = new SqlHelper(_loggerFactory, _config);

				command = new SqlCommand($"SELECT * FROM Endereco WHERE ID_end = @ID_end");
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

		public List<EnderecoModel> Get(string Nome = null)
		{
			SqlHelper dataConnection;
			SqlCommand command;
			DataSet dataSet;

			List<EnderecoModel> Endereco;
			List<string> clauses;

			try
			{
				dataConnection = new SqlHelper(_loggerFactory, _config);

				command = new SqlCommand($"SELECT * FROM Endereco");

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

				Endereco = Load(dataSet);
			}
			catch (Exception ex)
			{
				throw ex;
			}

			return Endereco;
		}

		#endregion
	}
}



