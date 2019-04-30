import java.sql.Connection;
import java.sql.DriverManager;
import java.util.Scanner;


/**
 *
 * @author 1BestCsharp
 */
public class MyConnection {
    
    
    public static void main(String[] args) {
        Connection con = null;
        try {
            Class.forName("com.mysql.jdbc.Driver");
            con = DriverManager.getConnection("jdbc:mysql://localhost/java_login_register", "root", "");
        } catch (Exception ex) {
            System.out.println(ex.getMessage());
        }
	}
    
}    