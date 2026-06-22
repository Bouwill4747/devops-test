$response1 = Invoke-WebRequest -URI http://jsonplaceholder.typicode.com/albums
$response2 = Invoke-WebRequest -URI http://jsonplaceholder.typicode.com/photos

$albums = $response1.Content | ConvertFrom-Json
$photos = $response2.Content | ConvertFrom-Json

# on regroupe les 5000 photos par albumId en une seule passe.
$report = $photos | Group-Object albumId | ForEach-Object { 

    # .Name est la cle du groupe, donc l'albumId (Group-Object le retourne toujours en string)
    $albumId = $_.Name 

    [PSCustomObject]@{
        # on le cast en [int] pour garder une colonne numerique
        AlbumId    = [int]$albumId

        # Le groupe ne connait pas le titre de l'album. On va le chercher dans $albums en faisant correspondre l'id de l'album a notre albumId.
        AlbumTitle = ($albums | Where-Object { $_.id -eq $albumId }).title

        # .Count donne directement le nombre de photos du groupe, pas besoin de compteur manuel
        PhotoCount = $_.Count
    }
}

# On trie le rapport par PhotoCount en ordre decroissant, on garde les 5 premiers
$report | Sort-Object PhotoCount -Descending | Select-Object -First 5