using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Net;
using System.IO;
using System;
using System.Data;
using System.Data.Common;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;


public class dataBaseManager : MonoBehaviour
{
    private static IDbConnection dbConnection;
    private static string connectionString="";
    public static dataBaseManager instance = null;
    public dataBaseManager()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
       
       
    }
   
    public static async Task<DataTable> GetDataTableAsync(string sql)
    {
         DbConnection dbConnection = new MySqlConnection(connectionString);
        dbConnection.Open(); ;
        DbCommand command = dbConnection.CreateCommand();
        command.CommandText = sql;

        string tableName = "User";
        TaskCompletionSource<DataTable> source = new TaskCompletionSource<DataTable>();
        var resultTable = new DataTable(tableName ?? command.CommandText);
        DbDataReader dataReader = null;

      

        try
        {
            await command.Connection.OpenAsync();
            dataReader = await command.ExecuteReaderAsync(CommandBehavior.Default);
            resultTable.Load(dataReader);
            source.SetResult(resultTable);
        }
        catch (Exception ex)
        {
            source.SetException(ex);
        }
        finally
        {
            if (dataReader != null)
                dataReader.Close();

            command.Connection.Close();
        }

        return resultTable;
    }
}
