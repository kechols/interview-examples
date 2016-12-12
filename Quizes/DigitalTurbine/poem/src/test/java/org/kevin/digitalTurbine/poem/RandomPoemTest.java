package org.kevin.digitalTurbine.poem;

import static org.junit.Assert.assertEquals;
import org.junit.Test;
import org.kevin.digitalTurbine.poem.RandomPoemFactory.RandomPoemGenerator;

public class RandomPoemTest {

	@Test
    public void shouldParsePoemRule() throws Exception {
        RandomPoemGenerator randomPoemGenerator = RandomPoemFactory.getRandomPoemGenerator();
        Rule poemRule = randomPoemGenerator.parseRule("POEM: <LINE> <LINE> <LINE> <LINE> <LINE>");
        assertEquals(5, poemRule.getReferenceRules().size());

        poemRule = randomPoemGenerator.parseRule("POEM: <LINE> <LINE>");
        assertEquals(2, poemRule.getReferenceRules().size());

        poemRule = randomPoemGenerator.parseRule("POEM:");
        assertEquals(0, poemRule.getReferenceRules().size());
    }

}
