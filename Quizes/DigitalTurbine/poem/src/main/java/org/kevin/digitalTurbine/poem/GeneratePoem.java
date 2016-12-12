package org.kevin.digitalTurbine.poem;

import  org.kevin.digitalTurbine.poem.RandomPoemFactory.RandomPoemGenerator;

public class GeneratePoem {
    public static void main(String[] args) {
        String fileName = "/resources/appiaTestRules.txt";
        if (args.length != 0)
            fileName = args[0];
        RandomPoemGenerator randomPoemGenerator = RandomPoemFactory.getRandomPoemGenerator();
        System.err.println(randomPoemGenerator.withFile(fileName).generateRandomPoem());
    }
}
