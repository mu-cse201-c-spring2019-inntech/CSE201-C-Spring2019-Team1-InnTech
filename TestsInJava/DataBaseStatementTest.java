/*
    @Description: Unit test & White box test for SQL statement(sort)
    @Author: Li Liu
    @Date: 05/05/2019
*/
package LL;

import java.sql.Connection;
import java.sql.DriverManager;
import java.sql.ResultSet;
import java.sql.Statement;

public class DataBaseStatementTest {

	public static void main(String[] args) {
		String[] databaseStatement = {"genre","name","platform","rating","developer"};
		for(int i = 0; i < databaseStatement.length; i++) {
		try {
			Connection myConn = DriverManager.getConnection("jdbc:mysql://localhost:3306/test","root","root");		
			Statement myStmt = myConn.createStatement();
			ResultSet myRs = myStmt.executeQuery("select * from GameApps ORDER BY " + databaseStatement[i]);
			
			System.out.println("Test Case " + i + " Order by " + databaseStatement[i]);
			System.out.println("------------------------------------------------------------------------------------------- \n");
			while(myRs.next()) {
				System.out.println(
						"gameId: " + myRs.getString("gameId") +
						" name: " + myRs.getString("name") 
						+ " genre: " + myRs.getString("genre") + " developer: "
						+ myRs.getString("developer") + " platform: " +
						myRs.getString("platform") + " rating: " + 
						myRs.getString("rating"));
			}
			System.out.println("-------------------------------------------------------------------------------------------");
		}catch(Exception e) {
			e.printStackTrace();
		}
		}
	}
}
