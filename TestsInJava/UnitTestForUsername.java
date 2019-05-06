/*
    @Description: Unit test & White box test for username
    @Author: Li Liu
    @Date: 05/05/2019
*/
package LL;

import java.util.ArrayList;

public class UnitTestForUsername {

	public static void main(String[] args) {
		
		String [] usernameTestCases = {"liul16@miamioh.edu","test@gmail.com","hello","nonEmail","123456789",
				"liu123456","liu!123LLLL","!test@gmail.com","tryme@123.321","hey@doit","jack.com","Tom@asd*@cds",
				"more#@123asd.dsa","testcase@.","123@.testcase","test.test@test","123@321.456","***@2469.sss",
				"******","tryme@asd.www","bill@123.321","Do*@123&.com!","!!!@###.***","last@test.case","bye@bye"};
		
		for(int i = 0; i < usernameTestCases.length; i++) {
			if(usernameTestCases[i].matches("^[_A-Za-z0-9-\\+]+(\\.[_A-Za-z0-9-]+)*@"
					+ "[A-Za-z0-9-]+(\\.[A-Za-z0-9]+)*(\\.[A-Za-z]{1,})$")) {
				System.out.println("<" + usernameTestCases[i] + ">" + " is valid username \n");
			}
			else {
				System.out.println("<" + usernameTestCases[i]  + ">" + " is invalid username \n");
			}
		}
			
	}

}
