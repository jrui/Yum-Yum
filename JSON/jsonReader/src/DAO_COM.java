
import java.sql.Connection;
import java.sql.PreparedStatement;
import java.sql.SQLException;
import java.util.ArrayList;

/*
 * To change this license header, choose License Headers in Project Properties.
 * To change this template file, choose Tools | Templates
 * and open the template in the editor.
 */

/**
 *
 * @author Carlos
 */
public class DAO_COM {
    public static void save(String coms) throws SQLException{
        Connection c = DAO_CONNECT.connect();
        PreparedStatement ps = null;
        try{
        ps = 
                c.prepareStatement("insert into Comentario (descricao) values (?)");
    
            ps.setString(1, coms);
            ps.executeUpdate();
        }
        finally{c.close();ps.close();}
    }
}
