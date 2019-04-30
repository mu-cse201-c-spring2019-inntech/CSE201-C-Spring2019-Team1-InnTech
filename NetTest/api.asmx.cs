using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.Services;
using System.Xml;
using System.Xml.Serialization;

namespace _385WebExample {
	/// <summary>
	/// 
	///		Author:			Mike Stahr
	///		Created:		9-20-2017
	///		Last Updated:	3-27-2019
	///
	///		Last Update:	Bug fix, added more flexibility in streaming data using send() method with style
	/// </summary>

	[WebService(Namespace = "http://tempuri.org/")]
	[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
	[System.ComponentModel.ToolboxItem(false)]
	[System.Web.Script.Services.ScriptService]
	public class api : System.Web.Services.WebService {
        // ========================================================================================
        //					START - DO NOT CHANGE
        // ========================================================================================
        private const string dbConfig = "DefaultConnection";
		#region ######################################################################################################################################################## Database Stuff

		private string conn = System.Configuration.ConfigurationManager.ConnectionStrings[dbConfig].ConnectionString;
		private List<SqlParameter> parameters = new List<SqlParameter>();

		// This method is used in conjuction with a "user defined table" in the database
		public DataTable sqlExec(string sql, DataTable dt, string udtblParam) {
			DataTable ret = new DataTable();

			try {
				using (SqlConnection objConn = new SqlConnection(conn)) {
					SqlCommand cmd = new SqlCommand(sql, objConn);
					cmd.CommandType = CommandType.StoredProcedure;
					SqlParameter tvparam = cmd.Parameters.AddWithValue(udtblParam, dt);
					tvparam.SqlDbType = SqlDbType.Structured;
					objConn.Open();
					ret.Load(cmd.ExecuteReader(CommandBehavior.CloseConnection));
				}
			} catch (Exception e) {
				setDataTableToError(ret, e);
			}
			parameters.Clear();
			return ret;
		}

		public DataTable sqlExec(string sql) {
			return sqlExecDataTable(sql);
		}

		public Object sqlExecFunction(string fn) {
			DataSet userDataset = new DataSet();
			Object ret = null;
			try {
				using (SqlConnection objConn = new SqlConnection(conn)) {
					objConn.Open();
					SqlCommand command = new SqlCommand(fn, objConn);
					command.CommandType = CommandType.Text;
					command.Parameters.AddRange(parameters.ToArray());
					ret = command.ExecuteScalar();
					objConn.Close();
				}
			} catch (Exception e) {
				throw e;
			}

			parameters.Clear();
			return ret;
		}

		public DataTable sqlExecDataTable(string sql) {
			DataSet userDataset = new DataSet();
			try {
				using (SqlConnection objConn = new SqlConnection(conn)) {
					SqlDataAdapter myCommand = new SqlDataAdapter(sql, objConn);
					myCommand.SelectCommand.CommandType = CommandType.StoredProcedure;
					myCommand.SelectCommand.Parameters.AddRange(parameters.ToArray());
					myCommand.Fill(userDataset);
				}
			} catch (Exception e) {
				//userDataset.Tables.Add();
				//setDataTableToError(userDataset.Tables[0], e);
				throw e;
			}

			parameters.Clear();
			if (userDataset.Tables.Count == 0) userDataset.Tables.Add();
			return userDataset.Tables[0];
		}

		public DataSet sqlExecDataSet(string sql) {

			DataSet userDataset = new DataSet();
			try {
				using (SqlConnection objConn = new SqlConnection(conn)) {
					SqlDataAdapter myCommand = new SqlDataAdapter(sql, objConn);
					myCommand.SelectCommand.CommandType = CommandType.StoredProcedure;
					myCommand.SelectCommand.Parameters.AddRange(parameters.ToArray());
					myCommand.Fill(userDataset);
				}
			} catch (Exception e) {
				userDataset.Tables.Add();
				setDataTableToError(userDataset.Tables[0], e);
			}

			parameters.Clear();
			return userDataset;
		}

		private void setDataTableToError(DataTable tbl, Exception e) {

			tbl.Columns.Add(new DataColumn("Error", typeof(Exception)));

			DataRow row = tbl.NewRow();
			row["Error"] = e;
			try {
				tbl.Rows.Add(row);
			} catch (Exception) { }
		}

		public void addParam(string name, object value) {
			parameters.Add(new SqlParameter(name, value));
		}

		#endregion

		#region ######################################################################################################################################################## Serializer

		private void send(object obj, serializeStyle style) {
			try {
				switch (style) {
					case serializeStyle.DATA_SET: serializeDataSet(sqlExecDataSet((string)obj)); break;
					case serializeStyle.DATA_TABLE: serializeDataTable(sqlExecDataTable((string)obj)); break;
					case serializeStyle.OBJECT: serializeObject(obj); break;
					case serializeStyle.SINGLE_TABLE_ROW: serializeSingleDataTableRow(sqlExecDataTable((string)obj)); break;
					case serializeStyle.DICTIONARY: serializeDictionary((Dictionary<object, object>)obj); break;
					case serializeStyle.GENERAL: serialize(obj); break;
					default: serialize("Invalid serialization"); break;
				}
			} catch (Exception e) {
				serialize("Error during send(): " + e.Message);
			}
		}


		private List<Dictionary<string, object>> getTableRows(DataTable dt) {
			List<Dictionary<string, object>> rows = new List<Dictionary<string, object>>();
			Dictionary<string, object> row;
			row = new Dictionary<string, object>();
			foreach (DataRow dr in dt.Rows) {
				row = new Dictionary<string, object>();
				foreach (DataColumn col in dt.Columns)
					row.Add(col.ColumnName, dr[col]);
				rows.Add(row);
			}
			return rows;
		}

		// Streams out a JSON string
		public void streamJson(string jsonString) {
			try {

				jsonString = jsonString.Trim();
				HttpContext.Current.Response.Clear();
				HttpContext.Current.Response.ContentType = "application/json";
				HttpContext.Current.Response.StatusCode = 200;
				HttpContext.Current.Response.StatusDescription = "";
				HttpContext.Current.Response.AddHeader("content-length", jsonString.Length.ToString());     // Need this line to remove the d:"null" in the stream out
				HttpContext.Current.Response.Write(jsonString);                                             // BUT... with it there are times when the json string doesn't complete... UGH!!!!!!
				HttpContext.Current.Response.Flush();
				HttpContext.Current.ApplicationInstance.CompleteRequest();
			} catch { }
		}

		// NEW - Converts a returned JSON dataset to a json object
		public void streamJson(DataSet ds) {
			string ret = "";
			try {
				foreach (DataTable dt in ds.Tables)
					foreach (DataRow dr in dt.Rows)
						ret += dr.ItemArray[0];
			} catch (Exception e) {
				ret = "";
			}

			streamJson(ret);
		}

		// Simple method to serialize an object into a JSON string and write it to the Response Stream
		public void serialize(Object obj) {
			try {
				streamJson(new JavaScriptSerializer().Serialize(obj));
			} catch (Exception e) {
				streamJson(new JavaScriptSerializer().Serialize("Invalid serializable object. r2w Error 2212: " + e.Source));
			}
		}

		// Generate and serialize a single row from a returned data table. Method will only return the first row - even if there are more.
		public void serializeSingleDataTableRow(DataTable dt) {
			serializeSingleDataTableRow(dt, "");
		}

		public void serializeSingleDataTableRow(DataTable dt, params string[] excludeColumns) {
			Dictionary<string, object> row = new Dictionary<string, object>();

			if (dt.Rows.Count > 0)
				foreach (DataColumn col in dt.Columns)
					if (!excludeColumns.Contains(col.ColumnName))
						row.Add(col.ColumnName, dt.Rows[0][col]);
			serialize(row);
		}

		// Serialize an entire table retreived from a data call
		public void serializeDataTable(DataTable dt) {
			serialize(getTableRows(dt));
		}

		// Serialize an multiple tables retreived from a data call
		public void serializeDataSet(DataSet ds) {
			List<object> ret = new List<object>();

			foreach (DataTable dt in ds.Tables)
				ret.Add(getTableRows(dt));
			serialize(ret);
		}

		// Converting an object to XML status
		public void serializeXML<T>(T value) {
			string ret = "";

			if (value != null) {
				try {
					HttpContext.Current.Response.Clear();
					HttpContext.Current.Response.ContentType = "text/xml";

					var xmlserializer = new XmlSerializer(typeof(T));
					var stringWriter = new StringWriter();

					using (var writer = XmlWriter.Create(stringWriter)) {
						xmlserializer.Serialize(writer, value);
						ret = stringWriter.ToString();
					}
				} catch (Exception) { }
				HttpContext.Current.Response.Write(ret);
				HttpContext.Current.Response.Flush();
				HttpContext.Current.ApplicationInstance.CompleteRequest();
			}
		}

		// Serialize a dictionary object to avoid having to create more classes
		public void serializeDictionary(Dictionary<object, object> dic) {
			serialize(dic.ToDictionary(item => item.Key.ToString(), item => item.Value.ToString()));
		}

		// Using generics this method will serialize a JSON package into a class structure or return a new instance of the class on error
		//public T _download_serialized_json_data<T>(string url) where T : new() {
		//    using (var w = new WebClient()) {
		//        try { return JsonConvert.DeserializeObject<T>(w.DownloadString(url)); } catch (Exception) { return new T(); }
		//    }
		//}

		// Probably don't need this as one can just type "serialize(object to serialize);" but if every we do we have it.   
		// Not sure it will work for objects that have arrays of other objects though...
		public void serializeObject(Object obj) {
			Dictionary<string, object> row = new Dictionary<string, object>();
			row = new Dictionary<string, object>();
			var prop = obj.GetType().GetProperties();

			foreach (var props in prop)
				row.Add(props.Name, props.GetGetMethod().Invoke(obj, null));
			serialize(row);
		}


		#endregion

		#region ######################################################################################################################################################## Internal Methods

		private void sendEmail(string from, string to, string cc, string bcc, string subject, string message) {
			SmtpClient mailClient = null;
			try {
				string pw = "your password goes here";
				mailClient = new SmtpClient("smtp.gmail.com", 587);  //'465
				NetworkCredential cred = new NetworkCredential("youremail@gmail.com", pw);  // You can also use your @miamioh.edu account
				MailMessage msg = new MailMessage();
				msg.IsBodyHtml = true;
				msg.From = new MailAddress(from);
				msg.To.Add(to);
				msg.Subject = subject;
				msg.Body = "<html><head><title></title></head><body>" + HttpUtility.HtmlDecode(message) + "</body></html>";
				msg.ReplyToList.Add(from);
				if (cc.Trim().Length > 0) msg.CC.Add(cc);
				if (bcc.Trim().Length > 0) msg.Bcc.Add(bcc);
				mailClient.EnableSsl = true;
				mailClient.Credentials = cred;
				mailClient.Send(msg);
			} catch (Exception e) { streamJson(e.Message); } finally {
				try { mailClient.Dispose(); mailClient = null; } catch { }
			}
		}

		#endregion

		#region ######################################################################################################################################################## Internal Classes

		private enum serializeStyle {
			GENERAL,
			DATA_SET,
			DATA_TABLE,
			DICTIONARY,
			OBJECT,
			SINGLE_TABLE_ROW
		}

		public class PermissionError {

			public int errorCode;
			public string message;

			public PermissionError() : this("You do not have permission to use this service", 0) { }

			public PermissionError(string message, int errorCode) {
				this.message = message;
				this.errorCode = errorCode;
			}

			public PermissionError(string message) : this(message, 0) { }

			public PermissionError(int errorCode) : this("You do not have permission to use this service", errorCode) { }

		}

        #endregion
        // ========================================================================================
        //					END - DO NOT CHANGE
        // ========================================================================================


        /* Example of a connection string that points to the AP database on the localdb SQL Server
		  <connectionStrings>
			<add name="DefaultConnection" connectionString="Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=AP;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=True" providerName="System.Data.SqlClient" />
		  </connectionStrings>

		*/

        // Methods
        #region ######################################################################################################################################################## Methods
        [WebMethod]
        public void getAllGames()
        {
            send("spShowAllGames", serializeStyle.DATA_TABLE);
        }

        [WebMethod]
        public void getGameByName(string @searchName)
        {
            addParam("@name", @searchName);
            send("spSearchByName", serializeStyle.DATA_TABLE);
        }

        [WebMethod]
        public void getGameByGenre(string @searchGenre)
        {
            addParam("@genre", @searchGenre);
            send("spSearchByGenre", serializeStyle.DATA_TABLE);
        }
        #endregion

    }
}
