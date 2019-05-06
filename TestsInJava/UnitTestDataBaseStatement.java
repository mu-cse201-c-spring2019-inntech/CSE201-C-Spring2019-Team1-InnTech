/*
    @Description: Unit test & White box test for SQL statement(where)
    @Author: Li Liu
    @Date: 05/05/2019
*/
package LL;
import java.sql.Connection;
import java.sql.DriverManager;
import java.sql.ResultSet;
import java.sql.Statement;

public class UnitTestDataBaseStatement {

	public static void main(String[] args) {
		String[] databaseStatement = {"FlappyBird","Tiny Wings","Doodle Jump","2048","Angry Birds","Plants vs. Zombies"};
		for(int i = 0; i < databaseStatement.length; i++) {
		try {
			Connection myConn = DriverManager.getConnection("jdbc:mysql://localhost:3306/test","root","root");		
			Statement myStmt = myConn.createStatement();
			ResultSet myRs = myStmt.executeQuery("select * from GameApps where name = '" + databaseStatement[i] + "'");
			
			System.out.println("Test Case " + i + " Search by " + databaseStatement[i]);
			System.out.println("-------------------------------------------------------------------------------------------");
			while(myRs.next()) {
				System.out.println(
						"gameId: " + myRs.getString("gameId") +
						" name: " + myRs.getString("name") 
						+ " genre: " + myRs.getString("genre") + " developer: "
						+ myRs.getString("developer") + " platform: " +
						myRs.getString("platform") + " rating: " + 
						myRs.getString("rating"));
			}
			System.out.println("------------------------------------------------------------------------------------------- \n\n\n");
		}catch(Exception e) {
			e.printStackTrace();
		}
		}
	}

}
