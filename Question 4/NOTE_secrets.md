# Note - Sécuriser les secrets dans un pipeline réel

Principe de base: un secret ne doit jamais apparaître en clair dans le code, dans le YAML du pipeline, ni dans une couche d'image. Tout ce qui est commité ou cuit dans une image est récupérable (à NorthSec, j'ai vu à quel point des jetons committés dans du CI/CD sont faciles à extraire).

Concrètement:

- **Stockage chiffré.** Les secrets vivent dans le coffre de la plateforme (GitHub Actions: repo ou org secrets), jamais dans le fichier. On les référence avec `${{ secrets.NOM }}`, et GitHub les masque automatiquement dans les logs.
- **Jeton de courte durée plutôt qu'un PAT.** Utiliser le `GITHUB_TOKEN` généré automatiquement à chaque run, limité au repo et expirant à la fin, au lieu d'un Personal Access Token longue durée qui traîne.
- **Moindre privilège.** Ne donner au jeton que les permissions nécessaires (ici `packages: write`, `contents: read`), rien de plus.
- **Secrets de build jamais dans une couche.** Passer les secrets de build via BuildKit (`--mount=type=secret`, ou l'input `secrets:` de `docker/build-push-action`), pour qu'ils soient montés seulement pendant l'étape et jamais écrits dans l'image livrée. C'est l'approche que j'utiliserais pour le `SA_PASSWORD` en production; dans l'exercice de la Q3, il est défini via `ENV` pour respecter la commande de build attendue du README.
- **OIDC pour le cloud.** Pour s'authentifier à un fournisseur infonuagique (Azure, AWS), préférer la fédération OIDC à des identifiants stockés: le pipeline obtient un jeton temporaire à la demande, donc aucun secret longue durée à gérer ni à faire tourner.
- **Hygiène.** Fichiers de secrets exclus du repo (`.gitignore`) et du contexte de build (`.dockerignore`), rotation régulière des secrets, et ne jamais faire `echo` d'un secret dans les logs.
