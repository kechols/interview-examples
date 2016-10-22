package org.kevin

class SpecialMath {
  def doMath( value : Int ) : Int = {
    	if(value==0){
    		return 0;
    	}
    	else if(value == 1){
    		return 1;
    	}
    	return value + doMath(value - 1) + doMath(value - 2);
    }
}