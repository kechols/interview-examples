package org.kevin;

import junit.framework.TestCase;
import org.kevin.MaxProduct.MaxProductResult;

/**
 * This class provides a unit test for Base64
 * 
 * @author Kevin Echols
 *
 */
@SuppressWarnings({"unchecked", "unused"})
public class TC_MaxProduct extends TestCase
{
	public void testThrowIllegalArgumentException() throws Exception {
		try{
			MaxProductResult actualResult = MaxProduct.getMaxProduct(new int[]{2});
			fail("Expected an illegal argument exception");
		}
		catch(IllegalArgumentException ex){
			// If we get here we passed the expectation
		}
    }
	
	
	public void testShouldHaveProductOf2() throws Exception {
		MaxProductResult expectedResult = new MaxProductResult(1, 2);
		
		MaxProductResult actualResult = MaxProduct.getMaxProduct(new int[]{1,2});
		int actualProduct = actualResult.getLargerOrEqualOperand() * actualResult.getSmallerOrEqualOperand();
		
        assertEquals(expectedResult.getLargerOrEqualOperand(), actualResult.getLargerOrEqualOperand());
        assertEquals(expectedResult.getSmallerOrEqualOperand(), actualResult.getSmallerOrEqualOperand());
        assertEquals(2,actualProduct);
    }
	
	
	
	public void testShouldHaveProductOf6() throws Exception {
		MaxProductResult expectedResult = new MaxProductResult(2, 3);
		
		MaxProductResult actualResult = MaxProduct.getMaxProduct(new int[]{1,2,3});
		int actualProduct = actualResult.getLargerOrEqualOperand() * actualResult.getSmallerOrEqualOperand();
		
        assertEquals(expectedResult.getLargerOrEqualOperand(), actualResult.getLargerOrEqualOperand());
        assertEquals(expectedResult.getSmallerOrEqualOperand(), actualResult.getSmallerOrEqualOperand());
        assertEquals(6, actualProduct);
        
        
        // Test for 6 by changing the order
        actualResult = MaxProduct.getMaxProduct(new int[]{3,2,1});
        
        assertEquals(expectedResult.getLargerOrEqualOperand(), actualResult.getLargerOrEqualOperand());
        assertEquals(expectedResult.getSmallerOrEqualOperand(), actualResult.getSmallerOrEqualOperand());
        assertEquals(6, actualProduct);
    }
	
	
	public void testShouldHaveProductOf20() throws Exception {
		MaxProductResult expectedResult = new MaxProductResult(4, 5);
		
		MaxProductResult actualResult = MaxProduct.getMaxProduct(new int[]{0,4,1, 5, 3});
		int actualProduct = actualResult.getLargerOrEqualOperand() * actualResult.getSmallerOrEqualOperand();
		
        assertEquals(expectedResult.getLargerOrEqualOperand(), actualResult.getLargerOrEqualOperand());
        assertEquals(expectedResult.getSmallerOrEqualOperand(), actualResult.getSmallerOrEqualOperand());
        assertEquals(20, actualProduct);
    }
}
