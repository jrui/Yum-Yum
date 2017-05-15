import java.sql.*;  

public class DAO_CONNECT {   

      // Create a variable for the connection string.  
      private static String connectionUrl = "jdbc:sqlserver://localhost\\SQLEXPRESS:1433;databasename=Yum-Yum;;";
      // Declare the JDBC objects.  
      public static Connection con = null;
      
     // private static String user = "Carlos\\Carlos";
     // private static String pass = "";
      
      static{
            try {  
               // Establish the connection.  
               Class.forName("com.microsoft.sqlserver.jdbc.SQLServerDriver");  
               con = DriverManager.getConnection(connectionUrl);
            }  

            // Handle any errors that may have occurred.  
            catch (Exception e) {  
               e.printStackTrace();  
            }
      }
      
   public static Connection connect() throws SQLException {
        Connection con = DriverManager.getConnection(connectionUrl);
        return con;
    }
}  