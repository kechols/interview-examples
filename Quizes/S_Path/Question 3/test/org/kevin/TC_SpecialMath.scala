package org.kevin

import org.scalatest._

class TC_SpecialMath {
    val specialMath = new SpecialMath
    
    assert(79 ==  specialMath.doMath(7))
    
    assert(10926 ==  specialMath.doMath(17))
    
    assert(2912089 ==  specialMath.doMath(90))
}
  