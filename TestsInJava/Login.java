/*
    @Description: System test & Black box test for login page
    @Author: Li Liu
    @Date: 04/05/2019
*/
package LL;
import java.util.HashMap;
import java.util.Scanner;

public class Login {
	public static void main(String[] args) {
		System.out.println("---------------test for login---------------");
		System.out.println("---------------Set up database---------------");
		Scanner myObj = new Scanner(System.in);
		
	    System.out.println("Enter username");
	    String usernameData = myObj.nextLine();
	    
	    System.out.println("Enter password");
	    String passwordData = myObj.nextLine();
	    
	    HashMap<String, String> database = new HashMap<String, String>();
	    database.put(usernameData, passwordData);
	    
	    System.out.println("Database");
	    System.out.println("Your username: " + usernameData);
	    System.out.println("Your password: " + database.get(usernameData));
	    
	    System.out.println("---------------test for login---------------");
	    System.out.println("Login");
	    System.out.println("username");
	    String username= myObj.nextLine();
	    
	    System.out.println("password");
	    String password = myObj.nextLine();
	    if(database.get(username) == null)
	    	System.out.println("There is no username in database as " + username);
	    else if(database.get(username).equals(password)) {
	    	System.out.println("Login successful");
	    	System.out.println("Welcome! " + username);
	    }
	    else
	    	System.out.println("Wrong password");
	}

}
