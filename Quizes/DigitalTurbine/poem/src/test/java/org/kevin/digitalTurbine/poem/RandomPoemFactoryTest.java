package org.kevin.digitalTurbine.poem;

import org.junit.Test;

import org.junit.Assert;
import org.junit.Before;

import org.kevin.digitalTurbine.poem.RandomPoemFactory.RandomPoemGenerator;

public class RandomPoemFactoryTest {
    private RandomPoemGenerator randomPoemGenerator = null;

    @Before
    public void setUp() {
        randomPoemGenerator = RandomPoemFactory.getRandomPoemGenerator();
    }

    
    @Test
    public void shouldReturnRandomPoemGenerator() {
        Assert.assertNotNull(randomPoemGenerator);
        Assert.assertTrue(randomPoemGenerator instanceof RandomPoemGenerator);
    }

    
    @Test
    public void shouldParseFileFileAndGeneratePoem() {
        RandomPoemGenerator poemGenerator = randomPoemGenerator.withFile("/resources/appiaTestRules.txt");
        System.err.println(poemGenerator.generateRandomPoem());
    }
}
