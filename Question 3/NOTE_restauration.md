# Note - Comment la base est restaurée

La restauration se fait **au moment du build**, pas au démarrage du conteneur, pour que l'image soit livrée avec les données déjà dedans.

1. Dans l'étape `builder`, on démarre `sqlservr` en arrière-plan, puis on lance le `restore_db.sh` fourni dans le même `RUN`. C'est nécessaire parce que SQL Server doit être en train de rouler pour que `sqlcmd` puisse exécuter `data.sql`, et qu'un `RUN` isolé ne garde pas un processus vivant entre deux instructions.
2. `restore_db.sh` attend le boot du serveur (50 s), puis exécute `data.sql` via `sqlcmd`, ce qui crée la base `INTERVIEW_TEST`. Les fichiers de données écrits dans `/var/opt/mssql` sont capturés dans la couche d'image.
3. L'image récente a déplacé `sqlcmd` vers `/opt/mssql-tools18/bin` et exige maintenant le flag `-C`. Comme le script fourni ne peut pas être modifié, un petit shim au chemin `/opt/mssql-tools/bin/sqlcmd` redirige vers la v18 en ajoutant `-C`.
4. Une deuxième étape (image finale) copie uniquement les données cuites depuis le `builder` avec `COPY --from=builder`. Le `restore_db.sh` n'est jamais copié, donc l'image livrée reste propre.

Pour cet exercice, `SA_PASSWORD` est défini via `ENV`, afin que l'image se construise avec la commande de build attendue du README (`docker build -t ... .`). Dans un vrai pipeline, je l'injecterais plutôt comme secret de build pour ne jamais le cuire dans l'image; voir la note de la Q4 sur la sécurisation des secrets.
