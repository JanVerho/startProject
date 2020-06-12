# startProject
EindProgrammeren5

Vertrek van de eindopdracht van programmeren 4.

 
Git (10 punten)

Maak een repository aan in Bitbucket (of Github)
Voeg mij toe als collaborator: verapeetersthomasmore (Bitbucket)  of verapeeters-thomasmore (Github)
Voeg de code van je project toe aan deze repository.
Noot: De .sln (solution) file staat in de root-directory van het project. Dit moet ook de root-directory van de git repository zijn.
Werk nu voor elke story/taak van deze opdracht op een nieuwe branch.
Commit regelmatig.
Als de story/taak klaar is merge dan de branch naar master. Je mag wel tussendoor ook mergen naar master maar op master moet altijd een werkende versie staan. 
Gebruik altijd duidelijke commit messages zodat ik kan zien welke story/taak je uitgevoerd hebt.
Merge regelmatig
 

Eerste stap: kijk je project na voor het volgende:  (10 punten)

Juist gebruik van async
Juist gebruik van LINQ.
Geen overbodige toArray/toList etc.
Laat zoveel mogelijk filtering en sortering door de database doen. 
Denk eraan dat de data uit de database gehaald wordt op het moment dat je toArray of toList doet.
Naamgeving: Namen van variabelen, functies, classes moeten duidelijk zijn.
Coding standards:  Namen van variabelen, functies, classes moeten voldoen aan de standaarden (volg hierin de aanbevelingen van Visual Studio).
Geen overbodige of ongebruikte code, variabelen of functies. Je mag wel code/variabelen/functies toevoegen om de leesbaarheid te verbeteren.
Geen uitgecommentarieerde code.
Geen duplicatie.
Scope van een variabele zo klein mogelijk houden. Als een variabele slechts binnen een block gebruikt wordt, dan definieer en initialiseer je die in dat block.
Nederlands moet je NIET opkuisen
 

Maak unit tests voor de filter.  (10 punten)

Voeg een unit test project toe
De code waarin je de filtering doet extract je in een aparte class (in een folder Logic) zodat je unit tests kan maken voor deze class
Het is niet de bedoeling dat de unit test werkt met data van de database! Je wil immers niet je unit test aanpassen als je gebruiker de data in de database ge-editeerd heeft via de CRUD-pagina's
Hoe beginnen we hier dan aan?
De functie filter in de nieuwe class Logic/Filter.cs krijgt als
Input:  
IEnumerable<Product>
in index.cshtml.cs geven we hier db.Products aan mee. Dit is immers een DbSet en dus een IEnumerable.
In de test kunnen we dan een simpele array van Products meegeven
De inhoud van je filterVelden
Dit kunnen Strings zijn of ints - de bedoeling is dat zo veel mogelijk logic ivm de filters naar deze functie verhuist. Dus als er in je code ergens expliciet geconverteerd wordt van strings naar ints dan maak je hier best strings van.  Of als je weet dat er geen filter is als dit een lege string is dan is het ook beter om van deze parameters strings te maken.
Output
IEnumerable<Product>
De bedoeling van de filter is:
geef alle planten die bloeien tussen weekNrFlowerStart en weekNrFlowerEnd
Als enkel 1 van de 2 filterwaarden gegeven is dan filter je enkel op start of enkel end
Als enkel 1 van de 2 filterwaarden gegeven is dan filter je enkel op de opgegeven waarde
Met flowerStart en flowerEnd:
geef alle planten die bloeien tussen weekNrFlowerStart en weekNrFlowerEnd
Als enkel 1 van de 2 filterwaarden gegeven is dan filter je enkel op start of enkel end
Met color en hoogte:
Als enkel 1 van de 2 filterwaarden gegeven is dan filter je enkel op de opgegeven waarde
Bewijs met je tests dat je code rekening houdt met speciale gevallen - maar alleen indien nuttig! Dus als er bepaalde waarden onmogelijk in de functie kunnen binnenkomen bij gebruik van de webapplicatie dan hoef je daar geen test gevallen voor te voorzien.
 

Voeg een nieuwe form toe in dezelfde pagina waar je de filter toont.  (10 punten)

De form krijgt 2 input velden: Number (aantal) en Name (ProductName)
Name is een <select> input field
De form heeft een submit knop die deze 2 velden met method=POST opstuurt naar de server
In de Server houden we op een of andere manier bij welke bestellingen gedaan zijn.  Deze moeten niet in de database toegevoegd worden.  (maar dat mag als je dat zelf handiger vindt) 
Als je de submit knop duwt voeg dan een OrderLine toe aan de Array/Lijst/Tabel van OrderLines 
Toon een samenvatting van de bestelde producten op deze pagina: toon per besteld product het bestelde aantal
Als de applicatie herstart wordt is het niet nodig dat deze lijst behouden blijft 
 

Validatie voor alle input velden  (10 punten)

Zorg ervoor dat alle input velden gevalideerd worden met een goede error message
De validatie moet zowel client side als server side gebeuren
 

Pimp de bestel form met Javascript  (10 punten)

Zorg ervoor dat de gebruiker een aantal letters kan intypen waardoor de getoonde <options> in de <select> beperkt worden tot de producten die deze letters bevatten (zoals we bij Kassa gedaan hebben)
 

Pimp extra met Javascript  (10 punten)

Voeg nog een interessante verbetering toe met javascript naar eigen keuze. Er is maar 1 ding belangrijk: hetgeen je doet met javascript moet iets zijn dat je niet server-side kan doen. 
