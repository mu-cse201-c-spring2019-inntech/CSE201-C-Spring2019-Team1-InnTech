import java.util.Scanner;

public class test {
	
	public static boolean checkUsername(String username) {
		if(username == null)
			return false;
		return true;
		
		}
		
	public static void main(String[] args) {
		Scanner input = new Scanner(System.in);
	
	    String username;
	    String password;
	
	    System.out.println("Log in");
	    System.out.print("username: ");
	    username = input.next();
	
	    System.out.println("password: ");
	    password = input.next();
	
	    //users check = new users(username, password);    
	    checkUsername(username);
	
	    if(username.equals(username) && password.equals(password)) 
	        System.out.println("You are logged in");
	}
}