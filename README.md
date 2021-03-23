# FitnessDiaryApp

Kopi av prosjektbeskrivelse:

FitnessDiaryApp

By Kim-Andre Engebretsen

Navn: Kim-Andre Engebretsen

Tittel: FitnessDiaryApp

For at skal kjøre:
Appen MÅ kjøres på skolens nettverk, altså at man befinner seg på høgskolens område og er tilkoblet deres nett. Appen kan IKKE kjøres med en VPN tilkobling til skolen fordi da klarer den ikke å gjøre GET requests til et eksternt API som blir benyttet i appen. 

Hvis det ønskes å kjøre Web APIet før man kjører appen så sørg for å fjerne denne connection stringen fra web.config i APIet og app.config i DataAccess:

<connectionStrings>
    <add name="FitnessDiary.DataAccess.FitnessDiaryContext" connectionString="Data Source=donau.hiof.no;Initial Catalog=kimandre;Persist Security Info=True;User ID=kimandre;Password=7dS4d9" providerName="System.Data.SqlClient" />
  </connectionStrings>

Dette for å slippe at donau databasen blir slettet ved et uhell.

Hva er ikke fullstendig ferdig:
Webviewet i add exercise skulle hatt fram og tilbake knapper, dette rakk jeg ikke å fullføre.
Brukeren skulle egentlig få lov til å velge bilder når de lager en exercise, dette rakk jeg ikke å implementere.

Et par bugs:
Listviewen i WorkoutScheduler siden som lister opp alle workoutEvents og Sessions kan hende at den bugger seg noen ganger og ender opp med å vise duplikater, prøv å slette duplikaten, legg til en ny session eller start appen på nytt for at listviewen skal vise riktig.

Datepickeren i WorkoutScheduler når man skal legge til en session er null som standard. Derfor hvis man ønsker å legge til en session på nåværende dato så må man velge en annen dato først og så velge tilbake til datoen i dag. Ellers så er daten null.

Hvis Webviewet i Add Exercise ikke viser noe, prøv å tast noe i URL-teksboksen og trykk på refresh knappen ved siden av. 


Fikk ikke til å sortere Exercisene som ligger i donau databasen på muskeltyper. Så alle exercises som ligger på donau databasen blir listet opp uansett hvilke muskeltype man velger.

Hva har blitt laget?
Her har det blitt laget en treningsdagbok for folk som ønsker å lage en oversikt over hvilke øvelser de ønsker å gjøre på hvilke dager.
Når brukeren åpner appen så blir alle muskeltyper (bodytypes) listet opp
Brukeren kan trykke en muskeltype:
Alle exercises som tilhører muskeltypen som ble trykket på blir listet opp. Bare exercises fra det eksterne APIet.
Alle exercises som ligger på Donau databasen blir listet opp uannsett.
Brukeren kan trykke på en exercise hvor det kommer opp tittel, muskeltype og directions på hvordan man skal gjøre exercisen.
Hvis brukeren trykker på en exercise som er listet opp fra det eksterne APIet (under Online) så kan de velge å legge den til i donau databasen.
Hvis brukeren trykker på en exercise som ligger i donau databasen (under added) så har man også tilgang til å edite og delete exercisen.
I Hamburger menyen til venstre så kan brukeren trykke på Add Exercise, Workout Scheduler, Home (Da kommer man tilbake til start) eller Achievements (dette rakk jeg ikke å gjøre ferdig).
Når brukeren trykker på Add Exercise, så kommer brukeren til en ny side med en liste av tekstbokser som brukeren bruker til å taste inn nødvendig informasjon til Exercisen som de ønsker å legge til. Her er det også et webview på siden, det er da tenkt at brukeren kan google ting hvis de lurer på noe. Brukeren kan taste et eller annet i URL-tekstboksen og trykke på Refreshknappen på siden. Da blir det som de har skrevet i URL-tekstboksen googlet i Webviewet. Save Exercise knappen som befinner seg øverst i høyre hjørnet blir aktiv når brukeren har valgt nødvendig informasjon for å legge til exercisen. Exercisen blir lagret til donau databasen og man kan finne den ved å trykke på Home og trykke på en muskeltype i starten.
Når brukeren trykker på Workout Scheduler, så kommer man til en ny side hvor det er en listview som lister opp alle datoer og sessions som tilhører de datoene. Under hver session så er det edit og delete knapper hvor brukeren kan endre på en session eller slette den. Øverst til høyre så kan brukeren trykke på Add session knappen hvor brukeren velger en dato og taster inn nødvendig informasjon om den session som brukeren skal legge til. Brukeren trykker på Add og listviewen på siden oppdateres.

Hva ble ikke ferdig:
Som beskrevet i Øving 5 så var det ønskelig med et achievement system som ga brukeren poeng hvis de la til nok exercises og sessions. Her måtte jeg da i tillegg ha lagt til en login funksjon og lagd en egen tabell for brukere på donau. Dette fikke jeg da ikke tid til.
Det var også ønskelig å la brukeren legge til bilder når de skulle legge til exercises, dette rakk jeg heller ikke.
Til slutt så var det gunstig å la brukeren kunne exportere alle datoer med sessions til en google kalendar. Dette ble droppet.

Kommentar:
Siden prosjektet har tatt utgangspunkt i fra Template10s hamburger app så finnes det kode som ikke jeg har laget men som jeg har latt være å fjerne. Som for eksempel:
DetailPage
SettingsPage
Busy
Splash
Shell
Og deres tilhørende ViewModels.
Det er også blitt benyttet litt kode som jeg har funnet på nettet, disse er markert med en link over eller under koden.
Alle Sessions som listes i WorkoutSchedulerPagen har en ExerciseID som blir vist i listviewet, i ettertanke så ser jeg at det heller bare burde vært Tittelen på Exercisen siden IDen ikke sier noe for brukeren.

