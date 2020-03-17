using System;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Xml;
using System.Xml.Linq;
using System.Xml.XPath;

namespace adonet_core_sqlserver_sp
{
  class Program
  {
    static void Main(string[] args)
    {
      //создаем строку подключения к БД
      SqlConnectionStringBuilder sqlConnectionStringBuilder =
          new SqlConnectionStringBuilder();
      // "Data Source=SQL5052.site4now.net;Initial Catalog=DB_A56693_demo;User Id=DB_A56693_demo_admin;Password=Passw0rd;"
      sqlConnectionStringBuilder.UserID = @"DB_A56693_demo_admin";
      sqlConnectionStringBuilder.Password = @"Passw0rd";
      sqlConnectionStringBuilder.InitialCatalog = @"DB_A56693_demo";
      sqlConnectionStringBuilder.DataSource = @"SQL5052.site4now.net";
      string connectionString = sqlConnectionStringBuilder.ToString();

      //создаем объект соединения
      SqlConnection conn = new SqlConnection(connectionString);

      try
      {
        //открываем соединение
        conn.Open();

        //создаем команду обращения к процедуре в БД
        SqlCommand getXMLCommand = conn.CreateCommand();
        getXMLCommand.CommandType = CommandType.StoredProcedure;
        getXMLCommand.CommandText = "dbo.sp_getJSON";
        // getXMLCommand.CommandText = "dbo.sp_getXML";

        //запускаем команду и выводим ответ процедуры на экран
        String xmlString = (String)getXMLCommand.ExecuteScalar();
        Console.WriteLine(xmlString);

        /*//еще раз выполняем команду и записываем ответ в объект XPathDocument
        XPathDocument resultXPathDocument =
            new XPathDocument(getXMLCommand.ExecuteXmlReader());

        //записываем результат из объекта XPathDocument в файл
        using (XmlTextWriter xmlTextWriter = new XmlTextWriter(@"./result.xml", Encoding.UTF8))
        {
          xmlTextWriter.Formatting = Formatting.Indented;
          resultXPathDocument.CreateNavigator().WriteSubtree(xmlTextWriter);
        }
        Console.WriteLine("An XML file was received n saved!");

        //читаем содержимое файла в объект XDocument и добавляем заголовок
        XDocument resultXDocument = new XDocument();
        resultXDocument = XDocument.Load("./result.xml");
        resultXDocument.Declaration = new XDeclaration("1.0", "utf-8", "yes");

        //сохраняем результат с добавленным заголовком из объекта XDocument в файл
        resultXDocument.Save("./result.xml");
        Console.WriteLine("The header was added!");

        Console.ReadKey();*/
      }
      catch (Exception ex)
      {
        System.Console.WriteLine(ex.Message);
      }
      finally
      {
        conn.Close();
      }
    }
  }
}
