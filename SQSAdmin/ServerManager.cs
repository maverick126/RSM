using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Data.Odbc;
using System.Data.SqlClient;
using System.Windows.Forms;
using System.Xml;



/// <summary>
/// Summary description for Server
/// </summary>
public sealed class ServerManager
{

	private string sSQLConnectionString;

	//private OdbcConnection myConnection;
	//private OdbcDataAdapter myAdapter;
	private DataSet myDataSet;

	private SqlConnection mySQLConnection;
	private SqlDataAdapter mySQLAdapter;

	public ServerManager()
	{
		//
		// TODO: Add constructor logic here
		//
		if (LoadVariablesFromFile())
		{

		}	

	}
	public bool LoadVariablesFromFile()
	{
		
	

		//loading from web.config file
		MetriconCommon.LogToFile("", "", "ServerManager.LoadVariablesFromFile", "");
		SetConnectionString();

		
		return true;
	}


	public void SetConnectionString()
	{
        this.sSQLConnectionString = MetriconCommon.getConnectionString();
	}


	public DataSet ExecuteSQLQuery(string sqlQuery)
	{

		try
		{
			mySQLConnection = new SqlConnection(this.sSQLConnectionString);
			mySQLAdapter = new SqlDataAdapter(sqlQuery, mySQLConnection);
			myDataSet = new DataSet();
			mySQLConnection.Open();
			mySQLAdapter.FillSchema(myDataSet, SchemaType.Source);
			mySQLAdapter.Fill(myDataSet);

		}
		catch (Exception e)
		{
			MetriconCommon.LogToFile("", "", "ServerManager.ExecuteSQLQuery", e.Message.ToString());
		}
		finally
		{
			//if there are no tables add default table
			if (myDataSet.Tables.Count == 0)
			{
				myDataSet.Tables.Add(new DataTable("NoRecords"));
			}
			mySQLConnection.Close();
			mySQLConnection.Dispose();
		}
		return myDataSet;

	}
	public DataSet ExecuteSQLQuery(string sqlStoredProcedure, SqlParameter[] myParameters)
	{

		try
		{
			mySQLConnection = new SqlConnection(this.sSQLConnectionString);
			mySQLAdapter = new SqlDataAdapter(sqlStoredProcedure, mySQLConnection);
			myDataSet = new DataSet();
			
			//mySQLAdapter.SelectCommand = new SqlCommand(sqlStoredProcedure);
            mySQLAdapter.SelectCommand.CommandTimeout=7200;
			mySQLAdapter.SelectCommand.Connection = mySQLConnection;
			mySQLAdapter.SelectCommand.CommandType = CommandType.StoredProcedure;
			
			mySQLConnection.Open();
			foreach (SqlParameter tempParam in myParameters)
			{
			
				mySQLAdapter.SelectCommand.Parameters.Add(tempParam);
			}

			mySQLAdapter.Fill(myDataSet);
			

		}
		catch (Exception e)
		{
			MetriconCommon.LogToFile("", "", "ServerManager.ExecuteSQLQuery", e.Message.ToString());
			throw e;
		}
		finally
		{
			//if there are no tables add default table
			if (myDataSet.Tables.Count == 0)
			{
				myDataSet.Tables.Add(new DataTable("NoRecords"));
			}
			mySQLConnection.Close();
			mySQLConnection.Dispose();
		}
		return myDataSet;
	}
	public DataSet GetPage(DataSet dsTemp, int pageSize, int   pageNumber)
	{
		DataSet dsMain = new DataSet();
		dsMain.Tables.Add(new DataTable());
		

		if (pageNumber < 0)
			pageNumber = 1;

		if (dsTemp == null)
		{
			return dsMain;
		}

		if (dsTemp.Tables.Count == 0)
		{
			return dsMain;
		}

		if (dsTemp.Tables[0].Rows.Count <= pageSize)
		{
			return dsMain;
		}

		if (dsTemp.Tables[0].Rows.Count == 0)
		{
			return dsMain;
		}

		
		int runningPageNumber = 0; //first page
		int rowCount = 0;
		int totalRowCount = dsTemp.Tables[0].Rows.Count;

		if (totalRowCount < pageSize)
			rowCount = pageSize - 1;



		foreach (DataColumn col in dsTemp.Tables[0].Columns)
		{
			dsMain.Tables[0].Columns.Add(new DataColumn(col.ColumnName));
		}

		foreach (DataRow row in dsTemp.Tables[0].Rows)
		{
			rowCount += 1;
			if ((rowCount - (runningPageNumber * pageSize)) > pageSize)
			{
				
				runningPageNumber += 1;

			}

			if (runningPageNumber > pageNumber)
			{
				break;
			}

			//if the running page number equals the page number
			if (runningPageNumber == pageNumber)
			{
				//add records to main ds

				try
				{
				
					dsMain.Tables[0].ImportRow(row);


				}
				catch(Exception e)
				{
                    MessageBox.Show(e.ToString());
				}

			}
		}
		return dsMain;

	}
	public DataSet GetPage(DataRow[] dsRow,DataColumnCollection dsCol, int pageSize, int pageNumber)
	{
		DataSet dsMain = new DataSet();
		dsMain.Tables.Add(new DataTable());

		if (pageNumber < 0)
			pageNumber = 1;

		if (dsRow == null)
		{
			return dsMain;
		}

		if (dsRow.Length == 0)
		{
			return dsMain;
		}

		if (dsRow.Length <= pageSize)
		{
			
			foreach (DataColumn col in dsCol)
			{
				dsMain.Tables[0].Columns.Add(new DataColumn(col.ColumnName));
			}

			foreach (DataRow row in dsRow)
			{
				try
				{

					dsMain.Tables[0].ImportRow(row);


				}
				catch (Exception e)
				{
                    MessageBox.Show(e.ToString());
				}
			}
			return dsMain;
		}

		if (dsRow.Length == 0)
		{
			return dsMain;
		}

	
		int runningPageNumber = 0; //first page
		int rowCount = 0;
		int totalRowCount = dsRow.Length;

		if (totalRowCount < pageSize)
			rowCount = pageSize - 1;



		foreach (DataColumn col in dsCol)
		{
			dsMain.Tables[0].Columns.Add(new DataColumn(col.ColumnName));
		}

		foreach (DataRow row in dsRow)
		{
			rowCount += 1;
			if ((rowCount - (runningPageNumber * pageSize)) > pageSize)
			{

				runningPageNumber += 1;

			}

			if (runningPageNumber > pageNumber)
			{
				break;
			}

			//if the running page number equals the page number
			if (runningPageNumber == pageNumber)
			{
				//add records to main ds

				try
				{

					dsMain.Tables[0].ImportRow(row);


				}
				catch (Exception e)
				{
                    MessageBox.Show(e.ToString());
				}

			}
		}
		return dsMain;

	}

	


}
