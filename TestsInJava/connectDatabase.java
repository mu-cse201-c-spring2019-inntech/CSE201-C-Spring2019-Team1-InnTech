/*
    @Description: System test & Black box test for connecting database
    @Author: Li Liu
    @Date: 04/05/2019
*/
package LL;
import java.sql.*;

public class connectDatabase {
	public static void main(String[] args) {
		
		try {
			Connection myConn = DriverManager.getConnection("jdbc:mysql://localhost:3306/test","root","root");		
			Statement myStmt = myConn.createStatement();
			ResultSet myRs = myStmt.executeQuery("select * from GameApps");
			
			System.out.println("Database connect successfully \n");
			while(myRs.next()) {
				System.out.println(
						"gameId: " + myRs.getString("gameId") +
						" name: " + myRs.getString("name") 
						+ " genre: " + myRs.getString("genre") + " developer: "
						+ myRs.getString("developer") + " platform: " +
						myRs.getString("platform") + " rating: " + 
						myRs.getString("rating"));
			}
		}
		catch(Exception e) {
			e.printStackTrace();
		}
	}
}
