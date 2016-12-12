package org.kevin.digitalTurbine.poem;

import static org.junit.Assert.*;
import org.junit.Test;

public class LineRuleTest {
	
	@Test
    public void shouldParseLineRuleReferenceRule() {
        LineRule lineRule = new LineRule("LINE: <NOUN>|<PREPOSITION>|<PRONOUN> $LINEBREAK");
        assertEquals(3, lineRule.getReferenceRules().size());
        assertTrue(lineRule.getReferenceRules().get(0) instanceof NounRule);
        assertTrue(lineRule.getReferenceRules().get(1) instanceof PrepositionRule);
        assertTrue(lineRule.getReferenceRules().get(2) instanceof PronounRule);
        assertEquals(1, lineRule.getKeyWords().size());
        assertTrue(lineRule.getKeyWords().get(0).equals(KeyWordType.LINEBREAK));

        lineRule.setReferenceRule("<NOUN>|<PRONOUN> $END");
        assertEquals(2, lineRule.getReferenceRules().size());
        assertTrue(lineRule.getReferenceRules().get(0) instanceof NounRule);
        assertTrue(lineRule.getReferenceRules().get(1) instanceof PronounRule);
        assertEquals(1, lineRule.getKeyWords().size());
        assertTrue(lineRule.getKeyWords().get(0).equals(KeyWordType.END));
    }
}
