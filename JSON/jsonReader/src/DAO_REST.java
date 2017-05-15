
import java.sql.Connection;
import java.sql.PreparedStatement;
import java.sql.SQLException;


public class DAO_REST {
    public static void save(Restaurant r) throws SQLException{
        Connection c = DAO_CONNECT.connect();
        PreparedStatement ps = null;
        try{
            ps = 
                    c.prepareStatement("insert into Restaurante (nome, morada, rating, preco_medio, contacto, imagem) values (?, ?, ?, ?, ?, ?)");

                ps.setString(1, r.name);
                ps.setString(2, r.addr);
                ps.setFloat(3, r.rating);
                ps.setFloat(4, -1);
                ps.setString(5, r.contact);
                ps.setString(6, r.foto);            
                ps.executeUpdate();
        }
        finally{c.close();ps.close();}
    }
}
