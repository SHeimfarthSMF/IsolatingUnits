Ziel: Es soll ein Unit-Test für den Accounting Report geschrieben werden. Dabei sollen alle Abhängigkeiten isoliert werden.

Die Aufgabe soll in folgenden Teilaufgaben gelöst werden
1. Schreiben einer Test-Methode, die nach dem 3A-Muster aufgebaut ist und das zu testende System baut, den AccountingReport berechnet und anschließend überprüft, ob das Ergebnis mindestens eine Zeile enthält. Vorerst sollen für die Abhängigkeiten die jeweiligen Implementierungen genommen werden. Für den Logger soll ein Dummy programmiert werden (ohne Library).
2. Der Dummy soll nun mit NSubsitute implementiert werden.
3. Statt der richtigen Implementierungen sollen nun Stubs (mit NSubstitute verwendet werden). Der Unit-Test soll dann so erweitert werden, dass auch die Berechnung des Accounting Reports überprüft wird.
4. Es soll ein weiterer (redundanter) Test geschrieben werden. Diesmal sollen Mocks genutzt werden, die die Interaktionen mit den restlichen Systemen überprüfen sollen.
5. Diskussion: Welche Test-Methodik ist sinnvoll? Stubs, Mocks? Was würdet ihr in der Realität machen?
