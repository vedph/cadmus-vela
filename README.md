# Cadmus VeLA Core

- 🔗 [Cadmus VeLA API](https://github.com/vedph/cadmus-vela-api)
- 🔗 [Cadmus VeLA app](https://github.com/vedph/cadmus-vela-app)

Core models for Cadmus VeLA 3. Old models for VeLA 2 are [here](old-models.md).

## Models

A single item represents a graffiti/inscription. Each item has these metadata (letters in brackets refer to the original [spreadsheet](#original-spreadsheet) column):

- ID (A)
- flags: include editorial state (C) and project segment (F). Flags are:
  - in lavorazione
  - importata
  - lavorata
  - rilevata
  - convalidata
  - nascosta
  - urbana
  - monastica
  - ducale
  - IMAI
- title (A)
- description

The item parts are:

- **summary** (sintesi):
  - [MetadataPart](https://github.com/vedph/cadmus-general/blob/master/docs/metadata.md): used metadata are:
    - `id` (A)
    - `author` (E)
    - `_edition`: temporary store for edition (ER)
    - `_comment`: temporary store for comment (ES)
    - `_biblio`: temporary store for bibliography (ET)
  - 🌟 [DistrictLocationPart](#districtlocationpart)
  - [GeoAssertedLocationsPart](https://github.com/vedph/cadmus-geo/blob/master/docs/asserted-locations.md)
  - [EpiSupportPart](https://github.com/vedph/cadmus-epigraphy/blob/master/docs/epi-support.md)
  - [CategoriesPart](https://github.com/vedph/cadmus-general/blob/master/docs/categories.md):`fn` for function of inscription/graffiti (S)
  - [PhysicalStatesPart](https://github.com/vedph/cadmus-general/blob/master/docs/physical-states.md)
  - [NotePart](https://github.com/vedph/cadmus-general/blob/master/docs/note.md):`txt`: this is the first draft of the text as copied on the spot.
  - [HistoricalDatePart](https://github.com/vedph/cadmus-general/blob/master/docs/historical-date.md)

- **details** (dettagli):
  - [EpiWritingPart](https://github.com/vedph/cadmus-epigraphy/blob/master/docs/epi-writing.md)
  - [EpiTechniquePart](https://github.com/vedph/cadmus-epigraphy/blob/master/docs/epi-technique.md)
  - [CategoriesPart](https://github.com/vedph/cadmus-general/blob/master/docs/categories.md):`lng` (📚 `categories_lng`) for a hierarchical taxonomy with this structure (which would also allow extending the list and picking more than a single language for multilingual texts):
    - languages: Y entries
    - ISO639: Z entries
    - glottolog: AA entries
  - [CategoriesPart](https://github.com/vedph/cadmus-general/blob/master/docs/categories.md):`cnt` (📚 `categories_cnt`) for content, covering:
    - content: AB entries
    - numbers: BO entries (in this case BO is redundant)
  - [CategoriesPart](https://github.com/vedph/cadmus-general/blob/master/docs/categories.md):`fig` (📚 `categories_fig`) for figurative features (DR)

- **text** (testo):
  - [TokenTextPart](https://github.com/vedph/cadmus-general/blob/master/docs/token-text.md): the edited text, susceptible of annotations via layers.
  - [CommentsLayerFragment](https://github.com/vedph/cadmus-general/blob/master/docs/fr.comment.md) (📚 `comment-categories`)
  - [ChronologyLayerFragment](https://github.com/vedph/cadmus-general/blob/master/docs/fr.chronology.md)
  - [EpiLigaturesLayerFragment](fr.it.vedph.epigraphy.ligatures) (📚 `epi-ligatures-types`)

- **comment** (commento):
  - [CommentPart](https://github.com/vedph/cadmus-general/blob/master/docs/comment.md) (📚 `comment-categories`)
  - [NotePart](https://github.com/vedph/cadmus-general/blob/master/docs/note.md)

- **references** (riferimenti):
  - [BibliographyPart](https://github.com/vedph/cadmus-general/blob/master/docs/bibliography.md) (📚 `bibliography-author-roles`, `bibliography-languages`, `bibliography-types`)
  - [DocReferencesPart](https://github.com/vedph/cadmus-general/blob/master/docs/doc-references.md)
  - [ExternalIdsPart](https://github.com/vedph/cadmus-general/blob/master/docs/external-ids.md)

### DistrictLocationPart

A new generic part for a single, district-based location with configurable hierarchy and presets. Derived from obsoleted `GrfLocalizationPart`.

- `DistrictLocationPart` (🔑 `it.vedph.district-location`):
  - `place`\* (🧱 `ProperName`):
    - `language` (`string`, 📚 `district-name-languages`)
    - `tag` (`string`)
    - `pieces` (`ProperNamePiece[]`, 📚 `district-name-piece-types`, providing 3 levels: area, sestriere, location):
      - `type`\* (`string`)
      - `value`\* (`string`)
  - `note` (`string` 5000)

>Levels are provided via a hierarchical-like thesaurus, where each component is defined by a simple ID with any number of children entries, each with a composite ID, e.g.:

```json
{
  "id": "district-name-piece-types@en",
  "entries": [
    {
      "id": "p*",
      "value": "provincia"
    },
    {
      "id": "c*",
      "value": "città"
    },
    {
      "id": "a*",
      "value": "area"
    },
    {
      "id": "a.cr",
      "value": "Cannareggio"
    },
    {
      "id": "a.cs",
      "value": "Castello"
    },
    {
      "id": "a.dd",
      "value": "Dorsoduro"
    },
    {
      "id": "a.sm",
      "value": "San Marco"
    },
    {
      "id": "a.sp",
      "value": "San Polo"
    },
    {
      "id": "a.sc",
      "value": "Santa Croce"
    },
    {
      "id": "l*",
      "value": "località"
    },
    {
      "id": "s*",
      "value": "struttura"
    },
    {
      "id": "_order",
      "value": "p c a l s"
    }
  ]
},
```

>Here, the asterisk suffix means a parent entry, while entries with children have their values composed by the parent entry ID without the asterisk, plus dot and another ID, like `a.cr`. Finally, the `_order` entry is used to define the hierarchical order of components.

- other generic epigraphical parts could be available, but are not included here:
  - [EpiFormulaPatterns](https://github.com/vedph/cadmus-epigraphy/blob/master/docs/epi-formula-patterns.md)
  - [EpiSigns](https://github.com/vedph/cadmus-epigraphy/blob/master/docs/epi-signs.md)
  - [EpiSupportFragments](https://github.com/vedph/cadmus-epigraphy/blob/master/docs/epi-support-frr.md)
  - [EpiLigaturesLayerFragment](https://github.com/vedph/cadmus-epigraphy/blob/master/docs/fr.epi-ligatures.md)

## Item Facet

The single item facet is structured as follows, reflecting the general ordering used in the original spreadsheet. There are 11 generic parts, 3 epigraphic parts, 2 generic text layer parts, and 1 epigraphic layer part.

- sintesi
  - metadati: `MetadataPart`
  - localizzazione: `DistrictLocationPart`
  - supporto: `EpiSupportPart`
  - funzione: `CategoriesPart:fn`
  - lingua: `CategoriesPart:lng`
  - contenuto: `CategoriesPart:cnt`
  - cronologia: `HistoricalDatePart`
  - stati: `PhysicalStatesPart`
- dettagli
  - tecnica: `EpiTechniquePart`
  - scrittura: `EpiWritingPart`
  - figurativo: `CategoriesPart:fig`
- testo:
  - bozza: `NotePart:txt`
  - testo edito: `TokenTextPart`
  - commenti: `CommentsLayerFragment`
  - cronologia: `ChronologyLayerFragment`
  - legature: `EpiLigaturesLayerFragment`
- commento:
  - commento: `CommentPart`
  - note: `NotePart`
- rierimenti:
  - bibliografia `BibliographyPart`
  - riferimenti `DocReferencesPart`
  - ID esterni `ExternalIdsPart`

---

## Original Spreadsheet

The original schema is just a flat spreadsheet table, where some columns are grouped under so-called header columns, filled with color and without data, whose purpose is making all the following columns belonging to the same group. Often this is used to represent boolean features in a mutually exclusive relationship. Of course, this is just a hack due to the flat nature of the spreadsheet model.

For all the value types:

- _whitespaces_ are inconsistent: normalize and trim.
- _casing_ is inconsistent: lowercase everything.
- _null_ values are strings which represent the absence of a value. As such, they must be treated as if the cell did not contain any value at all: `n\d`.

Columns have been [refactored](https://docs.google.com/spreadsheets/d/1Palm6ltRzc7zCXIsZcpRx4gf6QX7yjjXrzQvZMTpjmE/edit?gid=832312466#gid=832312466).

The ID after 🎯 represents the target for the column, and the one after ⚙️ the parser used by the [CLI import tool](https://github.com/vedph/cadmus-vela-tool). The symbol 📚 means a closed set of values, usually corresponding to a thesaurus; the symbol ☯️ means a string value representing a 3-state value: `si`, `no`, `n/d` or `n\d`. The symbol ⚙️ refers to the software module responsible of the import process for each datum in the [CLI tool](https://github.com/vedph/cadmus-vela-tool).

### Metadata

- A (no label) (string): ID (e.g. `CASTELLO_01-0001`) 🎯 `item.title`, `MetadataPart`.`id` ⚙️ [Row](https://github.com/vedph/cadmus-vela-tool/blob/master/Cadmus.Vela.Import/RowEntryRegionParser.cs).
- B `immagine`: ignored. The link between image and item is via ID (column A).
- C `stato` (📚 string) 🎯 `item.flags`: the editing state of the item (in lavorazione, importata, lavorata, rilevata). ⚙️ [ColEdState](https://github.com/vedph/cadmus-vela-tool/blob/master/Cadmus.Vela.Import/ColEdStateEntryRegionParser.cs).
- D `convalida` (☯️ boolean) 🎯 `item.flags`: "convalidata" editing state. ⚙️ [ColValidated](https://github.com/vedph/cadmus-vela-tool/blob/master/Cadmus.Vela.Import/ColValidatedEntryRegionParser.cs).
- E `autore` (CSV string) 🎯 `MetadataPart`.`author`: there can be 1 or more authors, separated by comma. Each author will become an `author` metadata entry. ⚙️ [ColAuthor](https://github.com/vedph/cadmus-vela-tool/blob/master/Cadmus.Vela.Import/ColAuthorEntryRegionParser.cs).
- F `segmento progetto` (📚 string) 🎯 `item.flags` (vela urbana, vela monastica, vela palazzo ducale, imai). ⚙️ [ColProject](https://github.com/vedph/cadmus-vela-tool/blob/master/Cadmus.Vela.Import/ColProjectEntryRegionParser.cs).

### Location

- (G) `contesto attuale di conservazione` 🎯 `DistrictLocationPart` ⚙️ [ColArea](https://github.com/vedph/cadmus-vela-tool/blob/master/Cadmus.Vela.Import/ColAreaEntryRegionParser.cs):
  - H `provincia` (📚 string: see e.g. <https://github.com/p1mps/regioni-province-comuni-italia/blob/master/regioni_province.csv>). When specified, this will be validated against the canonical list of provinces.
  - I `citta'` (string)
  - J `centri/localita'` (📚 string: Cannareggio, Castello, Dorsoduro, San Marco, San Polo, Santa Croce)
  - K `localizzazione` (string, e.g. Fondamenta Daniele Canal)
  - L `denominazione struttura` (string, e.g. Chiesa Santa Maria dei Servi)

### Context Support

- M `funzione originaria della struttura` (string 📚 `epi-support-functions`: privata, pubblica, religiosa, n/d) 🎯 `EpiSupportPart`.`originalFn` ⚙️ [ColOriginalFn](https://github.com/vedph/cadmus-vela-tool/blob/master/Cadmus.Vela.Import/ColOriginalFnEntryRegionParser.cs)
- N `tipologia originaria della struttura` (string 📚 `epi-support-types`: abitazione, biblioteca, caserma, castello, chiostro, colonnato, convento, edificio di culto, magazzino, monastero, museo, ospizio, palazzo, ponte, pozzo, prigione, scuderia, scuola, seminario, stalla, strada, torre, ufficio pubblico, n/d) 🎯 `EpiSupportPart`.`originalType` ⚙️ [ColOriginalType](https://github.com/vedph/cadmus-vela-tool/blob/master/Cadmus.Vela.Import/ColOriginalTypeEntryRegionParser.cs)
- O `funzione attuale` (same as M) 🎯 `EpiSupportPart`.`currentFn` ⚙️ [ColCurrentFn](https://github.com/vedph/cadmus-vela-tool/blob/master/Cadmus.Vela.Import/ColCurrentFnEntryRegionParser.cs)
- P `tipologia attuale struttura` (same as N) 🎯 `EpiSupportPart`.`currentType` ⚙️ [ColCurrentType](https://github.com/vedph/cadmus-vela-tool/blob/master/Cadmus.Vela.Import/ColCurrentTypeEntryRegionParser.cs)
- Q `interno/esterno` (string: interno, esterno) 🎯 `EpiSupportPart`.`indoor` ⚙️ [ColIndoor](https://github.com/vedph/cadmus-vela-tool/blob/master/Cadmus.Vela.Import/ColIndoorEntryRegionParser.cs)
- R `supporto` (string 📚 `epi-support-object-types`: arredo ecclesiastico, balaustra, colonna, cornice, davanzale, finestra, gradino, lapide (graffito su), muro, panchina, pavimentazione stradale, pavimento, pilastro, porta, pozzo, stipite, suppellettile, volta) 🎯 `EpiSupportPart`.`objectType` ⚙️ [ColSupport](https://github.com/vedph/cadmus-vela-tool/blob/master/Cadmus.Vela.Import/ColSupportEntryRegionParser.cs)
- S 🌟 `in situ\extra situm` (string: in situ, extra situm, n/d).
- (T) `origine`: 🎯 `CategoriesPart`:`fn` (📚 `categories_fn`) ⚙️ [ColFn](https://github.com/vedph/cadmus-vela-tool/blob/master/Cadmus.Vela.Import/ColFnEntryRegionParser.cs).
  - U 🌟 `provenianza` (sic) ??
- (V) `funzione dell'epigrafe/graffito`:
  - W `graffito\epigrafe`: not used, all items are graffiti.
  - X `testo` ☯️
  - Y `monogramma` ☯️
  - Z `lettera singola` ☯️
  - AA `lettere non interpretabili` ☯️

### Language

- (AB) `alfabeto`: 🎯 `CategoriesPart`.`lng` (📚 `categories_lng`) ⚙️ [ColLanguage](https://github.com/vedph/cadmus-vela-tool/blob/master/Cadmus.Vela.Import/ColLanguageEntryRegionParser.cs): drop AD AE:
  - AC `lingua` (📚 string: ARM, CHI, ENG, DUT, FRE, GER, GRC, GRE, ITA, JPN, LAT, N\D)

>If required, the ISO639-3 code used here can be converted into a single [BCP47](https://observablehq.com/@galopin/bcp-47-language-subtag-registry) code, which is a widely used standard and also allows for customized tags.

### Content

- (AD) `contenuto` 🎯 `CategoriesPart`.`cnt` (📚 `categories_cnt`) ⚙️ [ColContent](https://github.com/vedph/cadmus-vela-tool/blob/master/Cadmus.Vela.Import/ColContentEntryRegionParser.cs). All the cells have ☯️ type:
  - AE `amore`
  - AF `augurale`
  - AG `autentica di reliquie`
  - AH `bollo laterizio`
  - AI `calendario`
  - AJ `celebrativa`
  - AK `citazione`
  - AL `commemorativa`
  - AM `consacrazione`
  - AN `dedicatoria`
  - AO `devozionale`
  - AP `didascalica`
  - AQ `documentaria`
  - AR `esegetica`
  - AS `esortativa`
  - AT `ex voto`
  - AU `firma`
  - AV `funeraria`
  - AW `imprecazione`
  - AX `infamante`
  - AY `iniziale\i nome persona`
  - AZ `insulto`
  - BA `invocativa`
  - BB `marchio edile`
  - BC `nome`
  - BD `nome di luogo`
  - BE `parlante`
  - BF `politica`
  - BG `poesia`
  - BH `prosa`
  - BI `prostituzione`
  - BJ `preghiera`
  - BK `religiosa`
  - BL `saluto`
  - BM `segnaletica`
  - BN `sigla`
  - BO `sport`
  - BP `funzione non definibile` (this can be combined with others in the case it applies to just a letter or other isolated sign in the context of a text)
- BQ `numeri` (☯️ string) 🎯 `CategoriesPart`.`cnt` (📚 `categories_cnt`) ⚙️ [ColContent](https://github.com/vedph/cadmus-vela-tool/blob/master/Cadmus.Vela.Import/ColContentEntryRegionParser.cs):
  - BR `cifra` (📚 string: araba, armena, cirillica, glagolitica, romana, n\d)

### Date

- BS `cronologia` (string): età classica, età medioevale, età moderna, età contemporanea, n/d. 🎯 `MetadataPart` `era` ⚙️ [ColEra](https://github.com/vedph/cadmus-vela-tool/blob/master/Cadmus.Vela.Import/ColEraEntryRegionParser.cs).

The following columns target 🎯 `HistoricalDatePart` ⚙️ [ColDatation](https://github.com/vedph/cadmus-vela-tool/blob/master/Cadmus.Vela.Import/ColDatationEntryRegionParser.cs):

- BT `data` (string: possible formats are `R SECOLO` where `R` is an uppercase Roman number, or `YYYY` for a year)
- BU `datazione` (☯️ string)
- BV `secolo` (same format as BT)
- BW `termine post quem` (same format as BT)
- BX `termine ante quem` (same format as BT): note that we can have both terminus ante and terminus post for an interval. In this case BS is the same as BU+BV.

### Material Support

- BY `materia` (string 📚 `epi-support-materials`: cemento, ceramica, laterizio, legno, materiale litico, metallo, vetro) 🎯 `EpiSupportPart`.`material` ⚙️ [ColMaterial](https://github.com/vedph/cadmus-vela-tool/blob/master/Cadmus.Vela.Import/ColMaterialEntryRegionParser.cs).
- (BZ) `misure` ⚙️ [ColSize](https://github.com/vedph/cadmus-vela-tool/blob/master/Cadmus.Vela.Import/ColSizeEntryRegionParser.cs):
  - CA `misure supporto` (string: width and height in cm in the form `NXN`; decimals use dot) 🎯 `EpiSupportPart`.`supportSize` ⚙️ [ColSize](https://github.com/vedph/cadmus-vela-tool/blob/master/Cadmus.Vela.Import/ColSizeEntryRegionParser.cs).
  - CB `misure specchio` (same format as BY) 🎯 `EpiSupportPart`.`mirrorSize` ⚙️ [ColSupportFields](https://github.com/vedph/cadmus-vela-tool/blob/master/Cadmus.Vela.Import/ColSupportFieldsEntryRegionParser.cs).
- CC `stato di conservazione` (📚 `physical-states`: string: disperso, frammento, frammento contiguo, frammento isolato, integro, mutilo, reimpiego) 🎯 `PhysicalStatesPart` ⚙️ [ColStates](https://github.com/vedph/cadmus-vela-tool/blob/master/Cadmus.Vela.Import/ColStatesEntryRegionParser.cs).
  - CD `damnatio` (☯️ string) 🎯 `EpiSupportPart`.`hasDamnatio` ⚙️ [ColSupportFields](https://github.com/vedph/cadmus-vela-tool/blob/master/Cadmus.Vela.Import/ColSupportFieldsEntryRegionParser.cs).
- CE `specchio` (☯️ string) 🎯 `EpiSupportPart`.`hasMirror` ⚙️ [ColSupportFields](https://github.com/vedph/cadmus-vela-tool/blob/master/Cadmus.Vela.Import/ColSupportFieldsEntryRegionParser.cs).
  - CF `cornice` (☯️ string) 🎯 `EpiSupportPart`.`hasFrame` ⚙️ [ColSupportFields](https://github.com/vedph/cadmus-vela-tool/blob/master/Cadmus.Vela.Import/ColSupportFieldsEntryRegionParser.cs).
  - CG `tipo di cornice` (string) `EpiSupportPart`.`frame` ⚙️ [ColSupportFields](https://github.com/vedph/cadmus-vela-tool/blob/master/Cadmus.Vela.Import/ColSupportFieldsEntryRegionParser.cs).
- (CH) `campo` 🎯 `EpiSupportPart`.`hasField` ⚙️ [ColSupportFields](https://github.com/vedph/cadmus-vela-tool/blob/master/Cadmus.Vela.Import/ColSupportFieldsEntryRegionParser.cs):
  - CI `misure` (same format as CA) 🎯 `EpiSupportPart``fieldSize` ⚙️ [ColSize](https://github.com/vedph/cadmus-vela-tool/blob/master/Cadmus.Vela.Import/ColSizeEntryRegionParser.cs).

### Techniques and Tools

All columns here map to 🎯 `EpiTechniquePart` except when specified otherwise.

- (CJ) `tecnica di esecuzione` (📚 `epi-technique-types`): except where specified, cells have ☯️ type ⚙️ [ColTech](https://github.com/vedph/cadmus-vela-tool/blob/master/Cadmus.Vela.Import/ColTechEntryRegionParser.cs):
  - CK `solco` (string)
  - CL `a rilievo`
  - CM `disegno`
  - CN `graffio`
  - CO `incisione`
  - CP `intaglio`
  - CQ `punzonatura`
- (CR) `strumento di esecuzione` (📚 `epi-technique-tools`): cells have ☯️ type ⚙️ [ColTech](https://github.com/vedph/cadmus-vela-tool/blob/master/Cadmus.Vela.Import/ColTechEntryRegionParser.cs):
  - CS `bocciarda`
  - CT `carbocino` (sic): this is a typo. To be sure, we will allow both "carbocino" and "carboncino".
  - CU `fumo di candela`
  - CV `gradina`
  - CW `grafite`
  - CX `inchiostro`
  - CY `matita di piombo`
  - CZ `scalpello`
  - DA `sega`
  - DB `sgorbia`
  - DC `strumento accuminato`
  - DD `vernice`
  - DE `lama (affilatura)`
  - (DF) `impaginazione del testo` ⚙️ [ColLayout](https://github.com/vedph/cadmus-vela-tool/blob/master/Cadmus.Vela.Import/ColLayoutEntryRegionParser.cs):
    - DG `rigatura` (☯️ string) 🎯 `EpiSupportPart`.`features` (📚 `epi-support-features`).
    - DH `numero righe` (integer) 🎯 `EpiSupportPart`.`counts`.`rows` (📚 `epi-support-count-types`).
    - DI `note` (string) 🎯 `EpiSupportPart`.`note`.
    - DJ `presenza di preparazione del supporto` (☯️ string) 🎯 `EpiSupportPart`.`features` (📚 `epi-support-features`).

### Writing

- DK `scrittura` (📚 `epi-writing-casings`: maiuscola, maiuscola e minuscola, minuscola, n\d) 🎯 `EpiWritingPart`.`casing` ⚙️ [ColWriting](https://github.com/vedph/cadmus-vela-tool/blob/master/Cadmus.Vela.Import/ColWritingEntryRegionParser.cs):
  - DL `tipologia grafica caratteri latini` (📚 `epi-writing-scripts`: cancelleresca, capitale epigrafica, capitale libraria, capitale romanica, carolina, corsiva nuova, curiale, gotica, insulare, italica, mercantesca, merovingica, minuscola diplomatica, onciale, semionciale, umanistica, visigotica, altro, n\d) 🎯 `EpiWritingPart`.`script`
  - DM `segni grafici particolari` 🎯 `EpiWritingPart`.`features` (📚 `epi-writing-features`). All cells have ☯️ type:
    - DN `abbreviazioni`
    - DO `nessi e legamenti`
    - DP `lettere incluse`
    - DQ `lettere sovrapposte`
    - DR `punteggiatura`
    - DS `segni di interpunzione`

### Figurative

- DT `figurativo` (📚 `categories_fig`) 🎯 `CategoriesPart`:`fig` ⚙️ [ColFigurative](https://github.com/vedph/cadmus-vela-tool/blob/master/Cadmus.Vela.Import/ColFigurativeEntryRegionParser.cs). All cells have ☯️ type:
  - DU `disegno non interpretabile`
  - DV `abbigliamento`
  - DW `animale`
  - DX `architettura`
  - DY `arma`
  - DZ `armatura`
  - EA `bandiera`
  - EB `busto`
  - EC `croce`
  - ED `cuore`
  - EE `erotico`
  - EF `figura umana`
  - EG `geometrico`
  - EH `gioco`
  - EI `imbarcazione`
  - EJ `lingua`
  - EK `paesaggio`
  - EL `pianta`
  - EM `simbolo zodiaco`
  - EN `stemma`
  - EO `volto`

### Other Metadata

- (EP) `ricognizione`: 🎯 `PhysicalStatesPart` ⚙️ [ColStates](https://github.com/vedph/cadmus-vela-tool/blob/master/Cadmus.Vela.Import/ColStatesEntryRegionParser.cs):
  - EQ `data prima ricognizione` (string with format `DD/MM/YYYY`)
  - ER `data ultima ricognizione` (string with format `DD/MM/YYYY`)

### Unstructured Data

These data except for `edizione` are temporarily stored in metadata, all prefixed by `_`. These should then be used as a source to fill the appropriate part.

- (ES) `edizione e commento` ⚙️ [ColEdition](https://github.com/vedph/cadmus-vela-tool/blob/master/Cadmus.Vela.Import/ColEditionEntryRegionParser.cs)
  - ET `edizione` 🎯 `NotePart:txt`
  - EU `commento` 🎯 `MetadataPart`.`_comment`
  - EV `bibliografia` 🎯 `MetadataPart`.`_biblio`

## History

### 3.0.1

- 2024-11-20: updated packages (epigraphy).

### 3.0.0

- 2024-11-19: ⚠️ upgraded to .NET 9.

### 2.1.12

- 2024-10-30: updated packages.

### 2.1.11

- 2024-10-10: updated packages.

### 2.1.10

- 2024-09-17: updated packages.

### 2.1.9

- 2024-07-19: updated packages.

### 2.1.8

- 2024-05-15: updated packages.

### 2.1.7

- 2024-02-11:
  - updated test packages.
  - fixed missing tag in `VelaRepositoryProvider`.

### 2.1.4

- 2024-01-30: added `glottologCodes` and `prevalentCasing` to `GrfWritingPart`.

### 2.1.3

- 2024-01-30: added `damnatio` type to `GrfLocalizationPart`.

### 2.1.2

- 2024-01-29: fixes to `ToString` in parts.

### 2.1.1

- 2024-01-28: updated packages.

### 2.1.0

- 2024-01-26:
  - `GrfWritingPart.Scripts` multiple instead of `Script`.
  - `GrfLocalizationPart.Period` added.

### 2.0.1

- 2024-01-04: updated packages.

### 2.0.0

- 2023-11-27: ⚠️ Upgraded to .NET 8.

### 1.0.1

- 2023-06-28: updated packages.
