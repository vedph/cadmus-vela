# Cadmus VeLA Core

- 🔗 [Cadmus VeLA API](https://github.com/vedph/cadmus-vela-api)
- 🔗 [Cadmus VeLA app](https://github.com/vedph/cadmus-vela-app)

Core models for Cadmus VeLA.

- [Cadmus VeLA Core](#cadmus-vela-core)
  - [Data Model](#data-model)
    - [Items](#items)
    - [VeLA Parts](#vela-parts)
      - [GrfLocalizationPart](#grflocalizationpart)
      - [GrfSupportPart](#grfsupportpart)
      - [GrfFigurativePart](#grffigurativepart)
      - [GrfFramePart](#grfframepart)
      - [GrfStatesPart](#grfstatespart)
      - [GrfWritingPart](#grfwritingpart)
      - [GrfTechniquePart](#grftechniquepart)
  - [Original Spreadsheet](#original-spreadsheet)
    - [Values](#values)
    - [Columns](#columns)
  - [History](#history)
    - [2.0.1](#201)
    - [2.0.0](#200)
    - [1.0.1](#101)

## Data Model

Currently the core data model for VeLA includes 7 specialized parts and 13 general parts. In what follows, each part's model is represented with a list of properties. For each property its data type (string, boolean, etc.) and eventual thesaurus are specified in brackets; arrays are represented with suffix `[]` after the data type, so e.g. `string[]` means a list of strings. Some of the properties in turn are objects with other properties. The required properties are marked with an asterisk.

### Items

Currently the only item is the _graffiti_ item, with parts conventionally grouped in these labelled sections:

- _summary_ ("sintesi"):
  - [GrfLocalizationPart](#grflocalizationpart)
  - [GrfSupportPart](#grfsupportpart)
  - [GrfFramePart](#grfframepart)
  - [GrfStatesPart](#grfstatespart)
  - `NotePart` with role `text`: this is the first draft of the text as copied on the spot.
  - `NotePart` with role `date`: note about date. The tag of the note can be used to classify the free text note as discussing a terminus ante, a terminus post, or their combination (=interval). When (and if) some reasonable datation can be inferred, it will then be specified using `HistoricalDatePart`.

- _details_ ("dettagli"):
  - [GrfWritingPart](#grfwritingpart)
  - [GrfTechniquePart](#grftechniquepart)
  - [GrfFigurativePart](#grffigurativepart)
  - `HistoricalDatePart`: this provides a structured datation model which is machine-actionable.
  - `CategoriesPart` with role `functions` (funerary, votive, etc.: 📚 thesaurus: `categories_functions`)
  - `CategoriesPart` with role `themes` (e.g. sport, politics, etc.: 📚 thesaurus: `categories_themes`)

- _text_ ("testo"):
  - `TextPart`: the edited text, susceptible of annotations via layers.
  - `CommentsLayerPart` (📚 `comment-categories`)
  - `ChronologyLayerPart`
  - `LigaturesLayerPart` (📚 `epi-ligature-types`)

- _comment_ ("commento"):
  - `CommentPart` (📚 `comment-categories`)
  - `NotePart`

- _classification_ ("classificazione"):
  - `MetadataPart`
  - `IndexKeywordsPart` (📚 `languages`)

- _references_ ("riferimenti"):
  - `BibliographyPart` (📚 `bibliography-author-roles`, `bibliography-languages`, `bibliography-types`)
  - `DocReferencesPart`
  - `ExternalIdsPart`

### VeLA Parts

These models are specific to the VeLA project, but designed to fit a more generic schema for graffiti.

#### GrfLocalizationPart

- 🔑 ID: `it.vedph.graffiti.localization`

Graffiti localization.

- `place`\* (🧱 `ProperName`):
  - `language` (`string`, 📚 thesaurus: `grf-place-languages`)
  - `tag` (`string`)
  - `pieces` (`ProperNamePiece[]`, 📚 thesaurus: `grf-place-piece-types`, providing 3 levels: area, sestriere, location):
    - `type`\* (`string`)
    - `value`\* (`string`)
- `objectType`\* (`string`, 📚 thesaurus: `grf-support-object-types`)
- `function`\* (`string`, 📚 thesaurus: `grf-support-functions`)
- `note` (`string` 5000): note about original function.
- `indoor`\* (`boolean`)

#### GrfSupportPart

- 🔑 ID: `it.vedph.graffiti.support`

Material support.

- `type`\* (`string`, 📚 thesaurus: `grf-support-types`)
- `material`\* (`string`, 📚 thesaurus: `grf-support-materials`)
- `note` (`string`, 5000)

#### GrfFigurativePart

- 🔑 ID: `it.vedph.graffiti.figurative`

Figurative part description.

- `types` (`string[]`, 📚 thesaurus: `grf-figurative-types`)
- `description` (`string`, 5000)

#### GrfFramePart

- 🔑 ID: `it.vedph.graffiti.frame`

Frame, size and figure.

- `size`\* (🧱 `PhysicalSize`):
  - `tag` (`string`, 📚 thesaurus: `physical-size-tags`)
  - `w` (`PhysicalDimension`):
    - `value`\* (`number`)
    - `unit`\* (`string`, 📚 thesaurus: `physical-size-units`)
    - `tag` (`string`, 📚 thesaurus: `physical-size-dim-tags`)
  - `h` (`PhysicalDimension`)
  - `d` (`PhysicalDimension`)
  - `note` (`string`)
- `figure` (`string`, 5000)
- `frame` (`string`, 5000)

#### GrfStatesPart

- 🔑 ID: `it.vedph.graffiti.states`

State(s) reported for the graffiti.

- `states`\* (`GrfState[]`):
  - `type`\* (`string`, 📚 thesaurus: `grf-states`)
  - `date`\* (`date`)
  - `reporter`\* (`string`)
  - `note` (`string`, 5000)

#### GrfWritingPart

- 🔑 ID: `it.vedph.graffiti.writing`

Writing description.

- `system`\* (`string`, 📚 thesaurus: `grf-writing-systems`, usually ISO 15924 lowercase): writing system. This is required because there are cases where writing system and languages are not the same, e.g. you write Greek text with Latin letters.
- `languages`\* (`string[]`, 📚 thesaurus: `grf-writing-languages`, usually ISO 639-3)
- `script`\* (`string`, 📚 thesaurus: `grf-writing-scripts`): paleographic script (e.g. gothic, merchant, etc.).
- `casing`\* (`string`, 📚 thesaurus: `grf-writing-casing`)
- `scriptFeatures` (`string[]`; 📚 thesaurus: `grf-writing-script-features`)
- `letterFeatures` (`string[]`; 📚 thesaurus: `grf-writing-letter-features`)
- `counts` (`DecoratedCount`[]):
  - `id`\* (`string`, 📚 thesaurus: `grf-writing-count-ids`)
  - `tag` (`string`, 📚 thesaurus: `grf-writing-count-tags`)
  - `value`\* (`number`)
  - `note` (`string`)
- `hasRuling` (`boolean`)
- `ruling` (`string`, 5000)
- `hasRubrics` (`boolean`)
- `rubrics` (`string`, 5000)
- `hasProse` (`boolean`)
- `hasPoetry` (`boolean`)
- `metres` (`string[]`, 📚 thesaurus: `grf-writing-metres`)

#### GrfTechniquePart

- 🔑 ID: `it.vedph.graffiti.technique`

Techniques and tools.

- `techniques`\* (`string[]`, 📚 thesaurus: `grf-techniques`)
- `tools`\* (`string[]`, 📚 thesaurus: `grf-tools`)
- `note` (`string`, 5000)

## Original Spreadsheet

The original schema was just a flat spreadsheet table, where some columns are grouped under so-called header columns, filled with color and without data, whose purpose is making all the following columns belonging to the same group. Often this is used to represent boolean features in a mutually exclusive relationship. Of course, this is just a hack due to the flat nature of the spreadsheet model.

### Values

For all the value types:

- _whitespaces_ are inconsistent: normalize and trim.
- _casing_ is inconsistent: lowercase everything.
- _null_ values are strings which represent the absence of a value. As such, they must be treated as if the cell did not contain any value at all: `n\d`.

The following types can be defined:

- `int`: a single positive integer number.
- `float`: a single positive floating point number.
- `boolean`: `si` = true, `no` or empty = false.
- `string`: a string, representing a value or a null as defined above.
- `ISO639-3`: a single ISO-639-3 language code or empty.

### Columns

Columns marked as "header columns" are always empty and serve to group the next columns together, until the next header column. When not specified, the type is `string`.

- A (1) ID (no label: e.g. `CASTELLO_01-0001`) 🎯 `item.title`, `MetadataPart` id.
- B (2) `immagine`: ?? I found it always empty. At any rate, once we have an ID, the image resources can be accessed via some transformation of it.
- C-E (3-5) = `area`, `sestriere`, `denominazione` 🎯 `GrfLocalizationPart`
- F (6) = `funzione originaria` 🎯 `GrfLocalizationPart.note`
- G (7) `funzione attuale` 🎯 `GrfLocalizationPart.function`
- H (8) `tipologia struttura` 🎯 `GrfWritingPart.script`
- I (9) `interno/esterno` 🎯 `GrfLocalizationPart.indoor`
- J (10) `supporto` 🎯 `GrfSupportPart.type`
- K (11) `materiale` 🎯 `GrfSupportPart.material`
- L (12) `eta` (boolean) ??
- M (13) `datati` (boolean): apparently this just tells whether a date is specified in the next columns.
- N-P (14-16) = `terminus post`, `terminus ante`, `cronologia`. ??type of content? Only numbers? Centuries? 🎯 `HistoricalDatePart`
- Q (17) `figurativi` (boolean) 🎯 `GrfFigurativePart.types`
- R (18) `testo` (boolean) 🎯 `GrfFigurativePart.types`
- S (19) `numeri` (boolean) 🎯 `GrfFigurativePart.types`
- T (20) `cornice` (boolean) 🎯 `GrfFigurativePart.types`
- U (21) `tipo figurativo` ??🎯 `GrfFramePart.figure`
- V (22) `tipo cornice` 🎯 ??`GrfFramePart.frame`
- W (23) `misure` ?? 🎯 ??`GrfFramePart.size`
- X (24) `numero righe` (int) 🎯 `GrfWritingPart.counts`
- Y (25) `alfabeto` 🎯 `GrfWritingPart.system`
- Z (26) `lingua`: ??relation with AA?
- AA (27) `lingua (iso-639-3)` (ISO639-3) 🎯 `GrfWritingPart.languages`
- AB (28) `codice glottologico` ??
- AC (29) `tipologia grafica` ??
- AD (30) `tecnica di esecuzione`: header column 🎯 `GrfTechniquePart.techniques`
  - AE (31) `presenza di disegno preparatorio` (boolean)
  - AF (32) `presenza di preparazione del supporto` (boolean)
  - AG (33) `graffio` (boolean)
  - AH (34) `incisione` (boolean)
  - AI (35) `intaglio` (boolean)
  - AJ (36) `disegno` (boolean)
  - AK (37) `punzonatura` (boolean)
  - AL (38) `a rilievo` (boolean)
- AM (39) `strumento di esecuzione`: header column 🎯 `GrfTechniquePart.tools`
  - AN (40) `chiodo` (boolean)
  - AO (41) `gradina` (boolean)
  - AP (42) `scalpello` (boolean)
  - AQ (43) `sgorbia` (boolean)
  - AR (44) `sega` (boolean)
  - AS (45) `bocciarda` (boolean)
  - AT (46) `grafite` (boolean)
  - AU (47) `matita di piombo` (boolean)
  - AV (48) `fumo di candela` (boolean)
  - AW (49) `inchiostro` (boolean)
  - AX (50) `vernice` (boolean)
  - AY (51) `lama (affilatura)` (boolean)
  - AZ (52) `tipo di lama`
- BA (53) `damnatio`: header column.
  - BB (54) `presenza di damnatio` (boolean) 🎯 ??`CategoriesPart:themes` adding a new entry to the thesaurus
- BC (55) `caratteristiche grafiche`: header column.
  - BD (56) `maiuscolo\minuscolo prevalente`: values are `maiuscolo prevalente`, `minuscono prevalente`, `N\D`, empty 🎯 `GrfWritingPart.casing`
  - BE (57) `sistema interpuntivo` (boolean) 🎯 `GrfWritingPart.scriptFeatures`
  - BF (58) `nessi e legamenti` (boolean) 🎯 `GrfWritingPart.scriptFeatures`
  - BG (59) `rigatura` (boolean) 🎯 `GrfWritingPart.hasRuling`
  - BH (60) `abbreviazioni` (boolean) 🎯 `GrfWritingPart.scriptFeatures`
- BI (61) `monogrammi, lettere singole, ecc`: header column.
  - BJ (62) `monogrammi` (boolean) 🎯 `GrfWritingPart.letterFeatures`
  - BK (63) `lettera singola` (boolean) 🎯 `GrfWritingPart.letterFeatures`
  - BL (64) `lettere non interpretabili` (boolean) 🎯 `GrfWritingPart.letterFeatures`
  - BM (65) `disegno non interpretabile` (boolean) ??
- BN (66) `tipologia di argomento`: header column 🎯 `CategoriesPart:functions.categories`
  - BO (67) `funeraria` (boolean)
  - BP (68) `commemorativa` (boolean)
  - BQ (69) `firma` (boolean)
  - BR (70) `celebretiva` (sic) (boolean)
  - BS (71) `esortativa` (boolean)
  - BT (72) `didascalica` (boolean)
  - BU (73) `iniziale\i nome persona` (boolean)
  - BV (74) `sigla` (boolean)
  - BW (75) `segnaletica` (boolean)
  - BX (76) `citazione` (boolean)
  - BY (77) `infamante` (boolean)
  - BZ (78) `sport` (boolean)
  - CA (79) `prostituzione` (boolean)
  - CB (80) `politica` (boolean)
  - CC (81) `religiosa` (boolean)
  - CD (82) `preghiera` (boolean)
  - CE (83) `ex voto` (boolean)
  - CF (84) `amore` (boolean)
  - CG (85) `prosa` (boolean)
  - CH (86) `poesia` (boolean)
  - CI (87) `parlanti` (boolean)
  - CJ (88) `insulto` (boolean)
  - CK (89) `imprecazioni` (boolean)
  - CL (90) `nome di luogo` (boolean)
  - CM (91) `saluti` (boolean)
- CN (92) `categorie figurative`: header column 🎯 `GrfFigurativePart.types`
  - CO (93) `parti anatomiche` (boolean)
  - CP (94) `volti` (boolean)
  - CQ (95) `busto` (boolean)
  - CR (96) `figura umana` (boolean)
  - CS (97) `erotici` (boolean)
  - CT (98) `croce` (boolean)
  - CU (99) `cuore` (boolean)
  - CV (100) `architetture` (boolean)
  - CW (101) `paesaggi` (boolean)
  - CX (102) `geometrico` (boolean)
  - CY (103) `imbarcazioni` (boolean)
  - CZ (104) `piante` (boolean)
  - DA (105) `gioco` (boolean)
  - DB (106) `arma` (boolean)
  - DC (107) `armatura` (boolean)
  - DD (108) `stemma` (boolean)
  - DE (109) `bandiera` (boolean)
  - DF (110) `animale` (boolean)
  - DG (111) `simbolo zodiaco` (boolean)
  - DH (112) `grafitto da affilitura` (boolean)
- DI (113) `edizione e commento`: header column.
  - DJ (114) `edizione` 🎯 `BibliographyPart`, manually filled
  - DK (115) `codice iconclass` ??why just 1?
  - DL (116) `commento` 🎯 `NotePart`
  - DM (117) `osservazioni sullo stato di conservazione` ??
  - DN (118) `bibliografia` 🎯 `BibliographyPart`, manually filled
  - DO (119) `data primo rilievo` 🎯 `GrfStatesPart.states`
  - DP (120) `data ultima ricognizione` 🎯 `GrfStatesPart.states`

## History

### 2.0.1

- 2024-01-04: updated packages.

### 2.0.0

- 2023-11-27: ⚠️ Upgraded to .NET 8.

### 1.0.1

- 2023-06-28: updated packages.
