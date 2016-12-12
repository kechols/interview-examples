package org.kevin.digitalTurbine.poem;

import static org.junit.Assert.*;
import org.junit.Test;

public class SentenceRuleTest {

    private final static String NOUN_REFERENCE_RULE = "NOUN:\r\n" + "heart|sun|moon|thunder|fire|time|wind|sea|river|flavor"
            + "|wave|willow|rain|tree|flower|field|meadow|pasture|harvest|water|father|mother|brother|sister" + "<VERB>|<PREPOSITION>|$END";

    private final static String ADJECTIVE_REFERENCE_RULE = "ADJECTIVE: black|white|dark|light|bright|murky|muddy|clear <NOUN>|<ADJECTIVE>|$END";

    private final static String PRONOUN_REFERENCE_RULE = "PRONOUN: my|your|his|her <NOUN>|<ADJECTIVE>";

    private final static String VERB_REFERENCE_RULE = "VERB:\r\n"
            + " runs|walks|stands|climbs|crawls|flows|flies|transcends|ascends|descends|sinks <PREPOSITION>|<PRONOUN>|$END";

    private final static String PREPOSITION_REFERENCE_RULE = "PREPOSITION:\r\n"
            + " above|across|against|along|among|around|before|behind|beneath|beside|between|beyond|during|inside|onto|outside|under|underneath|upon|with|without|through "
            + " <NOUN>|<PRONOUN>|<ADJECTIVE>";

    @Test
    public void shouldParseNounRule() {
        NounRule nounRule = new NounRule(NOUN_REFERENCE_RULE);
        assertEquals(2, nounRule.getReferenceRules().size());
        assertTrue(nounRule.getReferenceRules().get(0) instanceof VerbRule);
        assertTrue(nounRule.getReferenceRules().get(1) instanceof PrepositionRule);
        assertEquals(1, nounRule.getKeyWords().size());
        assertTrue(nounRule.getKeyWords().get(0).equals(KeyWordType.END));
        assertEquals(24, nounRule.getPossibleWords().size());
        assertEquals("heart", nounRule.getPossibleWords().get(0));
        assertEquals("sister", nounRule.getPossibleWords().get(23));
    }

    
    @Test
    public void shouldParseAdjectiveRule() {
        AdjectiveRule adjectiveRule = new AdjectiveRule(ADJECTIVE_REFERENCE_RULE);
        assertEquals(2, adjectiveRule.getReferenceRules().size());
        assertTrue(adjectiveRule.getReferenceRules().get(0) instanceof NounRule);
        assertTrue(adjectiveRule.getReferenceRules().get(1) instanceof AdjectiveRule);
        assertEquals(1, adjectiveRule.getKeyWords().size());
        assertTrue(adjectiveRule.getKeyWords().get(0).equals(KeyWordType.END));
        assertEquals(8, adjectiveRule.getPossibleWords().size());
        assertEquals("black", adjectiveRule.getPossibleWords().get(0));
        assertEquals("clear", adjectiveRule.getPossibleWords().get(7));
    }
    

    @Test
    public void shouldParsePronounRule() {
        PronounRule pronounRule = new PronounRule(PRONOUN_REFERENCE_RULE);
        assertEquals(2, pronounRule.getReferenceRules().size());
        assertTrue(pronounRule.getReferenceRules().get(0) instanceof NounRule);
        assertTrue(pronounRule.getReferenceRules().get(1) instanceof AdjectiveRule);
        assertEquals(0, pronounRule.getKeyWords().size());
        assertEquals(4, pronounRule.getPossibleWords().size());
        assertEquals("my", pronounRule.getPossibleWords().get(0));
        assertEquals("her", pronounRule.getPossibleWords().get(3));
    }
    

    @Test
    public void shouldParseVerbRule() {
        VerbRule verbRule = new VerbRule(VERB_REFERENCE_RULE);
        assertEquals(2, verbRule.getReferenceRules().size());
        assertTrue(verbRule.getReferenceRules().get(0) instanceof PrepositionRule);
        assertTrue(verbRule.getReferenceRules().get(1) instanceof PronounRule);
        assertEquals(1, verbRule.getKeyWords().size());
        assertTrue(verbRule.getKeyWords().get(0).equals(KeyWordType.END));
        assertEquals(11, verbRule.getPossibleWords().size());
        assertEquals("runs", verbRule.getPossibleWords().get(0));
        assertEquals("sinks", verbRule.getPossibleWords().get(10));
    }
    

    @Test
    public void shouldParsePrepositionRule() {
        PrepositionRule prepositionRule = new PrepositionRule(PREPOSITION_REFERENCE_RULE);
        assertEquals(3, prepositionRule.getReferenceRules().size());
        assertTrue(prepositionRule.getReferenceRules().get(0) instanceof NounRule);
        assertTrue(prepositionRule.getReferenceRules().get(1) instanceof PronounRule);
        assertTrue(prepositionRule.getReferenceRules().get(2) instanceof AdjectiveRule);
        assertEquals(0, prepositionRule.getKeyWords().size());
        assertEquals(22, prepositionRule.getPossibleWords().size());
        assertEquals("above", prepositionRule.getPossibleWords().get(0));
        assertEquals("through", prepositionRule.getPossibleWords().get(21));
    }
}
