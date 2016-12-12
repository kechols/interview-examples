package org.kevin.digitalTurbine.poem;

import java.util.List;

public interface SentenceRule extends Rule {
    List<String> getPossibleWords();
}
