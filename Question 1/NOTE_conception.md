# Note - Choix de conception (Q1)

`Item` est une classe abstraite: un item générique ne devrait pas être instancié, seuls les types concrets (Table, Paddle, Balls) existent. Les attributs communs et le constructeur vivent dans `Item`, et chaque sous-classe passe ses valeurs avec `: base(...)` pour éviter de répéter le code.

`getFullPrice()` est abstraite et redéfinie par chaque sous-classe. Les trois versions sont présentement identiques (prix x 1.20), donc une méthode concrète dans `Item` enlèverait la répétition; j'ai gardé l'abstraction pour permettre un calcul de prix différent par type plus tard.
