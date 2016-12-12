package org.kevin.digitalTurbine.poem;

import java.io.BufferedReader;
import java.io.IOException;
import java.io.InputStream;
import java.io.InputStreamReader;
import java.util.List;
import java.util.Random;

public class RandomPoemFactory {
    public interface RandomPoemGenerator {
        public Rule parseRule(String string);

        public RandomPoemGenerator withFile(String string);

        public String generateRandomPoem();
    }

    public interface GeneratorRules {
        public PoemRule getPoemRule();

        public LineRule getLineRule();

        public SentenceRule getSentenceRule(String ruleType);;

        public void setPoemRule(PoemRule rule);

        public void setLineRule(LineRule rule);

        public void setAdjectiveRule(AdjectiveRule rule);

        public void setNounRule(NounRule rule);

        public void setPronounRule(PronounRule rule);

        public void setVerbRule(VerbRule rule);

        public void setPrepositionRule(PrepositionRule rule);
    }

    public static GeneratorRules getGeneratorRules() {
        return new DefaultGeneratorRules();
    }

    private static class DefaultGeneratorRules implements GeneratorRules {
        private PoemRule poemRule;
        private LineRule lineRule;
        private NounRule nounRule;
        private PrepositionRule prepositionRule;
        private PronounRule pronounRule;
        private VerbRule verbRule;
        private AdjectiveRule adjectiveRule;

        public LineRule getLineRule() {
            return lineRule;
        }

        public PoemRule getPoemRule() {
            return poemRule;
        }

        public void setLineRule(LineRule rule) {
            this.lineRule = rule;
        }

        public void setAdjectiveRule(AdjectiveRule rule) {
            this.adjectiveRule = rule;
        }

        public void setNounRule(NounRule rule) {
            this.nounRule = rule;
        }

        public void setPoemRule(PoemRule rule) {
            this.poemRule = rule;
        }

        public void setPrepositionRule(PrepositionRule rule) {
            this.prepositionRule = rule;
        }

        public void setPronounRule(PronounRule rule) {
            this.pronounRule = rule;
        }

        public void setVerbRule(VerbRule rule) {
            this.verbRule = rule;
        }

        public SentenceRule getSentenceRule(String ruleType) {
            switch (RuleType.getType(ruleType)) {
            case POEM:
                return (SentenceRule) this.poemRule;
            case LINE:
                return (SentenceRule) this.lineRule;
            case ADJECTIVE:
                return this.adjectiveRule;
            case NOUN:
                return this.nounRule;
            case PRONOUN:
                return this.pronounRule;
            case VERB:
                return this.verbRule;
            case PREPOSITION:
                return this.prepositionRule;
            default:
                throw new IllegalArgumentException("Unable to determine Sentence Rule Type for Rule String " + ruleType);
            }
        }
    }

    
    public static RandomPoemGenerator getRandomPoemGenerator() {
        return new DefaultRandomPoemGenerator();
    }

    
    private static class DefaultRandomPoemGenerator implements RandomPoemGenerator {
        private GeneratorRules generatorRules = null;

        public Rule parseRule(String ruleString) {
            return RuleType.getRule(ruleString);
        }

        public RandomPoemGenerator withFile(String filename) {
            this.generatorRules = getGeneratorRules(filename);
            return this;
        }

        private GeneratorRules getGeneratorRules(String filename) {
            InputStream stream = this.getClass().getResourceAsStream(filename);
            BufferedReader bufferedReader = new BufferedReader(new InputStreamReader(stream));
            GeneratorRules generatorRules = RandomPoemFactory.getGeneratorRules();
            String ruleString;
            try {
                while ((ruleString = bufferedReader.readLine()) != null) {
                    switch (RuleType.getType(ruleString)) {
                    case POEM:
                        generatorRules.setPoemRule((PoemRule) RuleType.getRule(ruleString));
                        continue;
                    case LINE:
                        generatorRules.setLineRule((LineRule) RuleType.getRule(ruleString));
                        continue;
                    case ADJECTIVE:
                        generatorRules.setAdjectiveRule((AdjectiveRule) RuleType.getRule(ruleString));
                        continue;
                    case NOUN:
                        generatorRules.setNounRule((NounRule) RuleType.getRule(ruleString));
                        continue;
                    case PRONOUN:
                        generatorRules.setPronounRule((PronounRule) RuleType.getRule(ruleString));
                        continue;
                    case VERB:
                        generatorRules.setVerbRule((VerbRule) RuleType.getRule(ruleString));
                        continue;
                    case PREPOSITION:
                        generatorRules.setPrepositionRule((PrepositionRule) RuleType.getRule(ruleString));
                        continue;
                    default:
                        throw new IllegalArgumentException("Unable to determine Rule Type for Rule String " + ruleString);
                    }
                }
            }
            catch (IOException e) {
                throw new RuntimeException("Sorry! Unable to read the file \"" + filename + "\"", e);
            }
            return generatorRules;
        }

        
        public String generateRandomPoem() {
            if (generatorRules == null) {
                throw new IllegalStateException(
                        "Sorry! Unable to generate Poem because because no rules have been set via RandomPoemGenerator.withFile(\"fileName\").");
            }
            StringBuilder poem = new StringBuilder();
            for (Rule lineRule : generatorRules.getPoemRule().getReferenceRules()) {
                poem.append(generateWordsUsingRules(generatorRules.getLineRule()));
            }
            return poem.toString();
        }

        
        private String generateWordsUsingRules(Rule lineRule) {
            StringBuilder line = new StringBuilder();
            int maximumRecursedThroughReferences = getRandomValue(2, 7);
            line.append(generateWordsUsingSentenceRules(lineRule.getReferenceRules(), "", maximumRecursedThroughReferences));
            if (lineRule.getKeyWords().contains(KeyWordType.LINEBREAK))
                line.append("\r\n");
            else
                line.append(" ");
            return line.toString();
        }

        
        private String generateWordsUsingSentenceRules(List<Rule> referenceRules, String words, int recurseThroughReferences) {
            StringBuilder sentenceFragment = new StringBuilder(words + " ");
            SentenceRule randomReferenceRule = getSentenceRule((SentenceRule) referenceRules.get(getRandomValue(0, referenceRules.size() - 1)));
            List<String> possibleWords = randomReferenceRule.getPossibleWords();
            sentenceFragment.append(possibleWords.get(getRandomValue(0, possibleWords.size() - 1)));
            if (recurseThroughReferences <= 0 && getSentenceRule(randomReferenceRule).getKeyWords().contains(KeyWordType.END)) {
                return sentenceFragment.toString();
            }
            recurseThroughReferences = recurseThroughReferences - 1;
            return generateWordsUsingSentenceRules(randomReferenceRule.getReferenceRules(), sentenceFragment.toString(), recurseThroughReferences);
        }

        
        private SentenceRule getSentenceRule(SentenceRule randomReferenceRule) {
            return generatorRules.getSentenceRule(randomReferenceRule.getType().getDisplayName());
        }

        
        private int getRandomValue(int min, int max) {
            Random rand = new Random();
            return rand.nextInt(max - min + 1) + min;
        }
    }
}
