/*
    @Description: Unit test & White box test for password
    @Author: Li Liu
    @Date: 05/05/2019
*/
package LL;

public class UnitTestForPassword {

	public static void main(String[] args) {
		String [] passwordTestCases = {"hello","123","123456","LLiu123456!","testT","ttt1234","testCase123","!!123Li","Iaminvaild123",
				"Iamvalid123!","Li!1","testcasetestcase1212!!","passwordInvalid123","Val!dPassword1","RED123456","!@#$%$^*&%^^&jshdbjIINFI3214",
				"HelloFromOtherSide","123321asddsa","Casetest2468addg...","dameda@.@32113A","@_@33333","333333332222aaaaa()(09()()A","lASRa24356982734",
				"A1D!2A","123456asdA*","0 0 0 a A !","        ","A B 1 2 ! c d ","TimeT0SayGoodBye!","bYeBy3***","S111Yy0u","last@case.com"};
		
		for(int i = 0; i < passwordTestCases.length; i++) {
		if(passwordTestCases[i].length() < 6) {
			System.out.println("Sorry! " + "<" + passwordTestCases[i] + "> is invaild password");
	    	System.out.println("password must be at least 6 characters long. \n");
		}
	    else if(!passwordTestCases[i].matches(".*[a-zA-Z]+.*")) {
	    	System.out.println("Sorry! " + "<" + passwordTestCases[i] + "> is invaild password");
	    	System.out.println("password must have at least one non letter or digit character. password must have at least one lowercase ('a'-'z'). password must have at least one uppercase ('A'-'Z'). \n");
	    }
	    else if(!passwordTestCases[i].matches(".*[a-z]+.*")) {
	    	System.out.println("Sorry! " + "<" + passwordTestCases[i] + "> is invaild password");
	    	System.out.println("password must have at least one non letter or digit character. password must have at least one lowercase ('a'-'z'). \n");
	    	}
	    else if(!passwordTestCases[i].matches(".*[A-Z]+.*")) {
	    	System.out.println("Sorry! " + "<" + passwordTestCases[i] + "> is invaild password");
	    	System.out.println("password must have at least one non letter or digit character. password must have at least one lowercase ('A'-'Z'). \n");
	    	}
	    else if(passwordTestCases[i].replaceAll("[A-Za-z0-9]", "").length() == 0) {
	    	System.out.println("Sorry! " + "<" + passwordTestCases[i] + "> is invaild password");
	    	System.out.println("password must have at least one non letter or digit character. \n");
	    }
	    else {
	    	System.out.println("Congrats! " + "<" + passwordTestCases[i] + ">");
	    	System.out.println("is valid password! \n");
	    }
		}
		
	}
}
