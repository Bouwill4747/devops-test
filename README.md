# Test technique DevOps - William Bourbonnière

## Contexte

Ce test a été une belle occasion de plonger dans des problèmes DevOps concrets et de montrer comment je travaille et comment j'apprends. Comme je postule pour un stage, je l'aborde en toute transparence. Pour chaque question, j'indique ce que je maîtrisais déjà, ce qui était nouveau pour moi, et comment j'ai comblé l'écart.

Mon objectif n'est pas de prétendre tout connaître. C'est de démontrer que même quand une syntaxe ou un concept précis m'est inconnu, je suis capable de chercher, de comprendre le *pourquoi*, de vérifier mon résultat, et de livrer du code qui fonctionne. Autrement dit: quelqu'un d'autonome qui apprend vite et qui devient rapidement un atout pour l'équipe.

---

## Question 1 - .NET / POO

- **Ce que je connaissais:** les concepts de POO (héritage, abstraction, polymorphisme, encapsulation), vus en cours de programmation orientée objet et appliqués dans des projets en Python (classes de base abstraites).
- **Ce qui était nouveau:** la syntaxe C# elle-même.
- **Comment je l'ai abordé:** j'ai traduit ma logique POO du Python vers le C# (deux points pour l'héritage, mot-clé `override` explicite, constructeurs, propriétés). J'ai ensuite refactoré pour mettre les attributs communs dans un constructeur de base, afin d'éviter la répétition.
- **Statut:** compile sans warning, affiche les 6 items avec la taxe de 20%. Une courte note de conception est incluse.

## Question 2 - PowerShell / JSON

- **Ce que je connaissais:** bien. PowerShell en autoformation (pipeline d'objets, `Group-Object`, `Sort-Object`, objets personnalisés).
- **Ce qui était nouveau:** rien de majeur, c'est la question la plus dans mes cordes.
- **Comment je l'ai abordé:** récupération et parsing du JSON, regroupement des photos par album, construction d'objets personnalisés, tri décroissant et top 5.
- **Statut:** fonctionne, la sortie est incluse.

## Question 3 - Docker / MSSQL

- **Ce que je connaissais:** Docker via mon homelab (builds multi-stage, healthcheck, compose, entrypoint).
- **Ce qui était nouveau:** la restauration d'une base MSSQL **au moment du build** (démarrer le serveur en arrière-plan et restaurer dans le même `RUN`), et les spécificités de SQL Server sur Linux.
- **Difficulté et recherche:** l'image récente a déplacé `sqlcmd` vers `tools18` et exige maintenant le flag `-C` (certificat). J'ai débogué méthodiquement (logs, `find`, test manuel de la commande) et créé un petit shim pour respecter le script fourni sans le modifier. J'ai aussi traité un avertissement de sécurité sur le secret (secret BuildKit au build, mot de passe injecté au `run`).
- **Statut:** image multi-stage propre, base `INTERVIEW_TEST` cuite dedans, `restore_db.sh` absent de l'image finale. Un guide détaillé et une note de restauration sont inclus.

## Question 4 - CI/CD

- **Ce que je connaissais:** les concepts CI/CD et la structure d'un pipeline YAML (autoformation Azure DevOps, j'ai écrit un pipeline Azure complet de bout en bout).
- **Ce qui était nouveau:** la syntaxe propre à GitHub Actions (les actions du marketplace).
- **Comment je l'ai abordé:** complété le `cd.yml` (checkout, login GHCR, build et push), avec les permissions du jeton et le passage du secret de build vers le Dockerfile de la Q3.
- **Statut:** `cd.yml` complet (build + push vers GHCR). Non déployé en réel (non requis par l'exercice). Une note sur la sécurisation des secrets est incluse, appuyée sur mon expérience au CTF NorthSec (extraction de jetons committés dans du CI).

---

## Où je me situe

Mes forces sont du côté infrastructure et sécurité, avec du scripting solide. Sur les morceaux qui m'étaient nouveaux (la syntaxe C#, les spécificités MSSQL, GitHub Actions), je les ai appris en cours de route, en cherchant la documentation, en comprenant le raisonnement derrière, et en vérifiant chaque étape plutôt qu'en copiant sans comprendre.

C'est le type de coéquipier que je veux être: autonome, rigoureux, et capable de monter en compétence rapidement sur ce que je ne connais pas encore.
