package org.kevin;


/**
 * This class provides MasxPruduct of a array
 * 
 * @author Kevin Echols
 *
 */
@SuppressWarnings({"unchecked", "unused"})
public final class  MaxProduct
{
    public static MaxProductResult getMaxProduct( int [] values ){
    	
    	if( values == null || values.length < 2){
    		throw new IllegalArgumentException("The array of values cannot be null and must have a legth greater than 1"); 
    	}
    	
    	MaxProductResult result = new MaxProductResult(values[0], values[1]);
    	
    	if(values.length == 2){
    		return result;
    	}
    	
    	for (int index = 2; (index < values.length); index++)
        {
            result.updateMaxValue(values[index]);
        }
    	
    	return result;
    }
    
    
    public static class MaxProductResult {
    	private int largerOrEqualOperand;
    	
    	public int getLargerOrEqualOperand() {
			return largerOrEqualOperand;
		}


		public int getSmallerOrEqualOperand() {
			return smallerOrEqualOperand;
		}


		private int smallerOrEqualOperand;
    	
    	
    	public MaxProductResult(int value1, int value2){
    		setValues(value1, value2);
    	}
    	
		
		public void setValues(int value1, int value2){
			if(value1 == value2){
				largerOrEqualOperand = value1;
				smallerOrEqualOperand = value1;
    		}
    		else {
    			largerOrEqualOperand = Math.max(value1, value2);
    			smallerOrEqualOperand = Math.min(value1, value2);
    		}
		}
		
		
		public void updateMaxValue(int value){
			if(value >= largerOrEqualOperand){
				smallerOrEqualOperand = largerOrEqualOperand;
				largerOrEqualOperand = value;
			}
		}
		
    }
}
