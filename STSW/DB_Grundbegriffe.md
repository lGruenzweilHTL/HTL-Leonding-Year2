# Grundbegriffe und Normalformen einer Datenbank

## Grundbegriffe

### 1. Attribut
Ein **Attribut** beschreibt eine Eigenschaft oder ein Merkmal von Entitäten in einer Datenbank. In einer relationalen Datenbank entspricht ein Attribut einer Spalte in einer Tabelle.

**Beispiel:** In einer Tabelle "Kunden" könnten Attribute wie "Name", "Adresse", "Telefonnummer" und "E-Mail" vorhanden sein.

### 2. Tabelle
Eine **Tabelle** ist eine Sammlung von Daten, die in Zeilen und Spalten organisiert sind. Jede Tabelle stellt eine Entität oder eine Beziehung zwischen Entitäten dar.

**Beispiel:** Eine Tabelle "Kunden" könnte alle relevanten Informationen zu den Kunden enthalten.

### 3. Primärschlüssel
Ein **Primärschlüssel** ist ein Attribut (oder eine Kombination von Attributen), das jede Zeile in einer Tabelle eindeutig identifiziert.

**Beispiel:** In der Tabelle "Kunden" könnte die "Kundennummer" der Primärschlüssel sein.

### 4. Fremdschlüssel
Ein **Fremdschlüssel** ist ein Attribut (oder eine Kombination von Attributen), das auf den Primärschlüssel einer anderen Tabelle verweist, um eine Beziehung zwischen den beiden Tabellen herzustellen.

**Beispiel:** In der Tabelle "Bestellungen" könnte die "Kundennummer" als Fremdschlüssel auf die "Kundennummer" in der Tabelle "Kunden" verweisen.

### 5. Entität
Eine **Entität** ist ein Objekt oder eine Instanz, über die Daten gespeichert werden. In einer relationalen Datenbank entspricht eine Entität in der Regel einer Tabelle.

**Beispiel:** Ein Kunde ist eine Entität, die durch eine Zeile in der Tabelle "Kunden" beschrieben wird.

## Normalformen

### 1. Normalform (1NF)
**Ziel:** Alle Attribute müssen atomar (unteilbar) sein.

**Regeln:**
- Jede Zeile muss eine eindeutige Identifikation (Primärschlüssel) haben.
- Alle Werte in einer Spalte müssen denselben Datentyp haben.
- Keine Wiederholungsgruppen oder mehrwertigen Attribute dürfen existieren.

**Beispiel:**
Nicht in 1NF:
| Kundennr | Name      | Telefonnummern          |
|----------|-----------|-------------------------|
| 1        | Max Müller| 12345, 67890            |

In 1NF:
| Kundennr | Name      | Telefonnummer |
|----------|-----------|---------------|
| 1        | Max Müller| 12345         |
| 1        | Max Müller| 67890         |

### 2. Normalform (2NF)
**Ziel:** Alle nicht-schlüssel Attribute müssen voll funktional vom gesamten Primärschlüssel abhängen.

**Regeln:**
- Keine partielle Abhängigkeit. Das bedeutet, dass in einer Tabelle mit einem zusammengesetzten Primärschlüssel, kein nicht-schlüssel Attribut nur von einem Teil des Primärschlüssels abhängt.

**Beispiel:** 
Nicht in 2NF:
| Bestell-ID | Produkt-ID | Produktname | Preis |
|------------|------------|-------------|-------|
| 1          | 101        | T-Shirt     | 20    |
| 1          | 102        | Jeans       | 40    |

In 2NF:
**Bestellungen**
| Bestell-ID | Produkt-ID | Preis |
|------------|------------|-------|
| 1          | 101        | 20    |
| 1          | 102        | 40    |

**Produkte**
| Produkt-ID | Produktname |
|------------|-------------|
| 101        | T-Shirt     |
| 102        | Jeans       |

### 3. Normalform (3NF)
**Ziel:** Alle nicht-schlüssel Attribute müssen direkt vom Primärschlüssel abhängen und nicht von anderen nicht-schlüssel Attributen.

**Regeln:**
- Keine transitive Abhängigkeit. Das bedeutet, dass kein nicht-schlüssel Attribut von einem anderen nicht-schlüssel Attribut abhängt.

**Beispiel:**
Nicht in 3NF:
| Kundennr | Name      | Adresse       | Kundenzone |
|----------|-----------|---------------|------------|
| 1        | Max Müller| Hauptstr. 1   | Zone A     |
| 2        | Julia Schmitt| Lindenstr. 2 | Zone B     |

In 3NF:
**Kunden**
| Kundennr | Name      | Adresse       |
|----------|-----------|---------------|
| 1        | Max Müller| Hauptstr. 1   |
| 2        | Julia Schmitt| Lindenstr. 2 |

**Zonen**
| Kundennr | Kundenzone |
|----------|------------|
| 1        | Zone A     |
| 2        | Zone B     |

### 4. Boyce-Codd-Normalform (BCNF)
**Ziel:** Alle funktionalen Abhängigkeiten müssen von einem Super-Schlüssel abhängen.

**Regeln:**
- Eine Tabelle ist in BCNF, wenn sie in 3NF ist und zusätzlich alle funktionalen Abhängigkeiten von einem Super-Schlüssel abhängen.

### 5. 4. Normalform (4NF)
**Ziel:** Verhindern von mehrwertigen Abhängigkeiten, bei denen mehrere unabhängige Attribute gleichzeitig von einem Primärschlüssel abhängen.

**Regeln:**
- Eine Tabelle ist in 4NF, wenn sie in BCNF ist und keine mehrwertigen Abhängigkeiten existieren.

### 6. 5. Normalform (5NF)
**Ziel:** Verhindern von Join-Abhängigkeiten, die dazu führen, dass eine Tabelle in mehrere Teile aufgeteilt werden muss, um die ursprüngliche Information korrekt wiederherzustellen.

**Regeln:**
- Eine Tabelle ist in 5NF, wenn sie in 4NF ist und keine Join-Abhängigkeiten existieren, die zu Redundanzen führen.
