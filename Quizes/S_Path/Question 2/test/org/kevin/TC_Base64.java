package org.kevin;

import junit.framework.TestCase;

/**
 * This class provides a unit test for Base64
 * 
 * @author Kevin Echols
 *
 */
@SuppressWarnings({"unchecked", "unused"})
public class TC_Base64 extends TestCase
{
	public void testConvert() throws Exception {
		String expectedResult = "RXZpZGludA==";
		
		String actualResult = Base64.encodeHexStringToBase64("45766964696e74");
		
        assertEquals(expectedResult, actualResult);
    }
}
