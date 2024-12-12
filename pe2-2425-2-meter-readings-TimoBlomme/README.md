# Opgave: Meteradministratie Systeem

## Doel:
Ontwikkel een applicatie voor het registreren en beheren van meterstanden voor verschillende soorten meters, zoals luchtkwaliteit, zonnepanelen, elektriciteit en water. De applicatie moet de gebruiker in staat stellen om meterstanden toe te voegen, weer te geven en te verwijderen.

## Functionaliteit:

### 1. Weergave van Meter Types:
- De gebruiker moet de keuze hebben om verschillende soorten meters te selecteren:
  - Luchtkwaliteit
  - Zonnepanelen
  - Elektriciteit
  - Water
- De applicatie moet de juiste invoervelden tonen op basis van het geselecteerde meter type.
  - **Luchtkwaliteit**: CO2- en PM2.5-waarden.
  - **Zonnepanelen**: Energieproductie (kWh).
  - **Elektriciteit**: Energieverbruik (kWh) en type verbruik (pieken of dalen).
  - **Water**: Waterverbruik (liter).

### 2. Toevoegen van Meterstand:
- Wanneer de gebruiker de gegevens voor een bepaalde meterstand invoert en op 'Toevoegen' klikt:
  - De meterstand moet gevalideerd worden, inclusief:
    - Correcte meter ID.
    - Geldige datum.
    - Geldige numerieke waarden voor de metingen.
- Als de gegevens geldig zijn, moet de meterstand worden toegevoegd aan de administratie.

### 3. Weergave en Verwijdering van Meterstanden:
- De applicatie moet alle opgeslagen meterstanden tonen in een lijst.
- De gebruiker moet de details van een geselecteerde meterstand kunnen bekijken.
- De gebruiker moet de mogelijkheid hebben om een geselecteerde meterstand te verwijderen uit de administratie.

### 4. Foutafhandeling:
- Fouten zoals ongeldige invoer (bijvoorbeeld een onjuist meter ID of een ontbrekende datum) moeten op een duidelijke manier aan de gebruiker worden getoond.
- Alle fouten moeten correct worden afgehandeld met behulp van `try-catch` blokken.

### 5. Incrementeer en Decrementeer Aantal Meterstanden:
- Elke keer dat een meterstand wordt toegevoegd, moet het totale aantal geregistreerde meterstanden worden verhoogd.
- Bij het verwijderen van een meterstand moet het aantal meterstanden afnemen.

## Specificaties:

### UI:
- De applicatie moet een gebruikersinterface bevatten waar de gebruiker:
  - Een meter type kan selecteren.
  - De nodige gegevens voor de geselecteerde meterstand kan invoeren.
  --  Dit is een oefening op het aanmaken van **dynamische controls**, controls die aangemaakt worden in code.
  Bekijk onderstaande code om een **TextBlock** en een **TextBox** toe te voegen aan een stackpanel met als naam **stpControls**
  #### Codefragment 💻
```
   private void AddControlToUI(string labelText, string textBoxName)
 {
     TextBlock textBlock = new TextBlock { Text = labelText, Margin = new Thickness(0, 0, 10, 0) };
     TextBox textBox = new TextBox { Name = textBoxName };
     stpControls.Children.Add(textBlock);
     stpControls.Children.Add(textBox);
 }
```

  - Meterstanden kan toevoegen en verwijderen.

### Validatie:
- De meter ID moet een geldig geheel getal zijn.
- De datum moet geselecteerd worden.
- De invoerwaarden moeten numerieke waarden zijn, bijvoorbeeld voor CO2, PM2.5, verbruik, productie, etc.
- Er kunnen slechts **5** meterstanden worden ingegeven.

### Foutmeldingen:
Bij ongeldige invoer moeten duidelijke foutmeldingen worden getoond, zoals:
  - "Invalid meter ID."
  - "Select a valid date"
  - "Invalid input-value."

## Vereisten:

### Technologie:
De applicatie moet in **C#** zijn geschreven met gebruik van de **WPF-framework** voor de GUI.

### Kerncomponenten:
- `MeterAdmin`: Een klasse voor het beheren van meterstanden (toevoegen, verwijderen).
- `MeterReading`: Een abstracte klasse voor meterstanden met verschillende afgeleiden klassen voor de specifieke meter types (AirQualityMeterReading, SolarPanelMeterReading, etc.).
- `MeterType`: Een enumeratie die de verschillende soorten meters specificeert (AirQuality, SolarPanel, Electricity, Water).
- `ConsumptionType`: Een enumeratie die voor een elektriciteitsmeter specificeert of het een verbruik plaatsvond in een **piekuur (PEAKLOAD)** of **daluur(OFFPEAKLOAD)**.

### UI-elementen:
- **ComboBox** voor het selecteren van het meter type.
- **TextBoxen** voor het invoeren van numerieke waarden.
- **DatePicker** voor het selecteren van een datum.
- **Lijst (ListBox)** voor het weergeven van alle geregistreerde meterstanden.

## Voorbeeldstappen:

### 1. Meter Type Selecteren:
De gebruiker selecteert bijvoorbeeld 'Zonnepanelen' in de ComboBox.

### 2. Invoervelden Weergeven:
Er wordt een TextBox weergegeven waarin de gebruiker de energieproductie (kWh) kan invoeren.

### 3. Toevoegen van Meterstand:
Na het invullen van de gegevens, klikt de gebruiker op de knop 'Toevoegen'. Als de invoer geldig is, wordt de meterstand toegevoegd.

### 4. Lijst met Meterstanden:
De gebruiker ziet de toegevoegde meterstand in de lijst.

### 5. Verwijderen van Meterstand:
De gebruiker selecteert een meterstand en klikt op 'Verwijderen' om deze uit de administratie te verwijderen.

## Nog te bepalen extra Functionaliteit
- Zorg voor **SeedingData** waar je al 2 meterstanden registreert  (💪💪)
- Toon de readings per **MeterType** (💪💪)
- Zorg voor een functionaliteit om de **Datum** van de meteringave te kunnen aanpassen (💪💪)
- Zorg voor een functionaliteit om de **(productie,verbruik,CO2, pm25)** van de meteringave te kunnen aanpassen  op basis van het type meterstand  (💪💪💪💪)

## Animatie apllicatie
![Werking Applicatie (zonder fouten)](/Animatie.gif)