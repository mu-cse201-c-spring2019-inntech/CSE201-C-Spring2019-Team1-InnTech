/*
    @Description: System test & Black box test for register page
    @Author: Li Liu
    @Date: 04/05/2019
*/
package LL;

import java.util.HashMap;
import java.util.Scanner;

public class Register {
	public static void main(String[] args) {
	Scanner myObj = new Scanner(System.in);
	
    System.out.println("Enter username");
    String username = myObj.nextLine();
    
    System.out.println("Enter password");
    String password = myObj.nextLine();
    
    System.out.println("Enter confirm password");
    String password2 = myObj.nextLine();
    
    if(username.equals("")&&password.equals("")) {
    	System.out.println("Username is needed");
    	System.out.println("Password is needed");
    }
    else if(username.equals(""))
    	System.out.println("Username is needed");
    else if(password.equals(""))
    	System.out.println("Password is needed");
    else if(!username.matches("^[_A-Za-z0-9-\\+]+(\\.[_A-Za-z0-9-]+)*@"
			+ "[A-Za-z0-9-]+(\\.[A-Za-z0-9]+)*(\\.[A-Za-z]{1,})$")) {
		System.out.println("Invalid username");
	}
    else if(password.length() < 6)
    	System.out.println("The Password must be at least 6 characters long.");
    else if(!password.matches(".*[a-zA-Z]+.*"))
    	System.out.println("Passwords must have at least one non letter or digit character. Passwords must have at least one lowercase ('a'-'z'). Passwords must have at least one uppercase ('A'-'Z').");
    else if(!password.matches(".*[a-z]+.*"))
    	System.out.println("Passwords must have at least one non letter or digit character. Passwords must have at least one lowercase ('a'-'z').");
    else if(!password.matches(".*[A-Z]+.*"))
    	System.out.println("Passwords must have at least one non letter or digit character. Passwords must have at least one lowercase ('A'-'Z').");
    else if(password.replaceAll("[A-Za-z0-9]", "").length() == 0)
    	System.out.println("Passwords must have at least one non letter or digit character.");
    else if(!password.equals(password2))
    	System.out.println("The password and confirmation password do not match.");
    else {
    HashMap<String, String> database = new HashMap<String, String>();
    database.put(username, password);
    
    System.out.println("Register successful");
    System.out.println("Database");
    System.out.println("Your username: " + username);
    System.out.println("Your password: " + database.get(username));
    }
}
}
