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
  - [CategoriesPart](https://github.com/vedph/cadmus-general/blob/master/docs/categories.md):`fn` for function of inscription/graffiti (S)
  - 🌟❗ `EpiSupportPart` (🔑 `it.vedph.epigraphy.support`), refactored:
    - `material`\* (`string` 📚 `epi-support-materials`, BW)
    - `originalFn` (`string` 📚 `epi-support-functions`, M)
    - `currentFn` (`string` 📚 `epi-support-functions`, O)
    - `originalType` (`string` 📚 `epi-support-types`, N)
    - `currentType` (`string` 📚 `epi-support-types`, P)
    - `objectType` (`string` 📚 `epi-support-object-types`, R)
    - `indoor` (`boolean`, Q)
    - `supportSize` (`PhysicalSize`, BY)
    - `hasField` (`boolean`, CF)
    - `fieldSize` (`PhysicalSize`, CG)
    - `hasMirror` (`boolean`, CC)
    - `mirrorSize` (`PhysicalSize`, BZ)
    - `hasFrame` (`boolean`, CD)
    - `frame` (`string`, CE)
    - `counts` (`DecoratedCount[]`, 📚 `epi-support-count-types` DF): e.g. rows, columns, etc.
    - `features` (📚 `epi-support-features`, S): e.g. ruling, etc.
    - `hasDamnatio` (`boolean`, CB)
    - `note` (`string`, 5000)
  - [PhysicalStatesPart](https://github.com/vedph/cadmus-general/blob/master/docs/physical-states.md)
  - [NotePart](https://github.com/vedph/cadmus-general/blob/master/docs/note.md):`txt`: this is the first draft of the text as copied on the spot.
  - [NotePart](https://github.com/vedph/cadmus-general/blob/master/docs/note.md):`dat`: note about date. The tag of the note can be used to classify the free text note as discussing a terminus ante, a terminus post, or their combination (=interval). When (and if) some reasonable datation can be inferred, it will then be specified using `HistoricalDatePart`.

- **details** (dettagli):
  - 🌟❗ `EpiWritingPart` (🔑 `it.vedph.epigraphy.writing`), refactored:
    - `system`\* (📚 `epi-writing-systems`, usually [ISO 15924](https://unicode.org/iso15924/iso15924-codes.html))
    - `script`\* (📚 `epi-writing-scripts`, DJ)
    - `casing` (`string`, 📚 `epi-writing-casings`): prevalent casing if applicable.
    - `features` (DI+DK, 📚 `epi-writing-features`)
    - `note` (`string`, 5000)
  - 🌟 `EpiTechniquePart` (🔑 `it.vedph.epigraphy.technique`):
    - `techniques`\* (`string[]`, 📚 `epi-technique-types`)
    - `tools`\* (`string[]`, 📚 `epi-technique-tools`)
    - `note` (`string`, 5000)
  - [HistoricalDatePart](https://github.com/vedph/cadmus-general/blob/master/docs/historical-date.md)
  - [CategoriesPart](https://github.com/vedph/cadmus-general/blob/master/docs/categories.md):`lng` (📚 `categories_lng`) for a hierarchical taxonomy with this structure (which allows extending the list and picking more than a single language for multilingual texts):
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

---

## Original Spreadsheet

The original schema is just a flat spreadsheet table, where some columns are grouped under so-called header columns, filled with color and without data, whose purpose is making all the following columns belonging to the same group. Often this is used to represent boolean features in a mutually exclusive relationship. Of course, this is just a hack due to the flat nature of the spreadsheet model.

For all the value types:

- _whitespaces_ are inconsistent: normalize and trim.
- _casing_ is inconsistent: lowercase everything.
- _null_ values are strings which represent the absence of a value. As such, they must be treated as if the cell did not contain any value at all: `n\d`.

Columns have been [refactored](https://docs.google.com/spreadsheets/d/1Palm6ltRzc7zCXIsZcpRx4gf6QX7yjjXrzQvZMTpjmE/edit?gid=832312466#gid=832312466).

The ID after 🎯 represents the target for the column, and the one after ⚙️ the parser used by the [CLI import tool](https://github.com/vedph/cadmus-vela-tool). The symbol 📚 means a closed set of values, usually corresponding to a thesaurus; the symbol ☯️ means a string value representing a 3-state value: `si`, `no`, `n/d` or `n\d`.

### Metadata

- A (no label) (string): ID (e.g. `CASTELLO_01-0001`) 🎯 `item.title`, `MetadataPart` id ⚙️ `Row`.
- B `immagine`: ignored. The link between image and item is via ID (column A).
- C `stato` (📚 string) 🎯 `item.flags`: the editing state of the item (in lavorazione, importata, lavorata, rilevata).
- D `convalida` (boolean) 🎯 `item.flags`: "convalidata" editing state.
- E `autore` (string) 🎯 `MetadataPart`.`author`.
- F `segmento progetto` (📚 string) 🎯 `item.flags` (vela urbana, vela monastica, vela palazzo ducale, imai).

### Location

- (G) `contesto attuale di conservazione` 🎯 `DistrictLocationPart`:
  - H `provincia` (📚 string: see e.g. <https://github.com/p1mps/regioni-province-comuni-italia/blob/master/regioni_province.csv>)
  - I `città` (string)
  - J `centri/localita'` (string, e.g. Cannareggio)
  - K `localizzazione` (string, e.g. Fondamenta Daniele Canal)
  - L `denominazione struttura` (string, e.g. Chiesa Santa Maria dei Servi)

### Context Support

- M `funzione origin.` (string 📚 `epi-support-functions`: privata, pubblica, religiosa, n/d) 🎯 `EpiSupportPart`.`originalFn`.
- N `tipologia originaria della struttura` (string 📚 `epi-support-types`: abitazione, biblioteca, caserma, castello, chiostro, colonnato, convento, edificio di culto, magazzino, monastero, museo, ospizio, palazzo, ponte, pozzo, prigione, scuderia, scuola, seminario, stalla, strada, torre, ufficio pubblico, n/d) 🎯 `EpiSupportPart`.`originalType`.
- O `funzione attuale` (same as M) 🎯 `EpiSupportPart`.`currentFn`.
- P `tipologia attuale` (same as N) 🎯 `EpiSupportPart`.`currentType`.
- Q `interno/esterno` (string: interno, esterno) 🎯 `EpiSupportPart`.`indoor`.
- R `supporto` (string 📚 `epi-support-object-types`: arredo ecclesiastico, balaustra, colonna, cornice, davanzale, finestra, gradino, lapide (graffito su), muro, panchina, pavimentazione stradale, pavimento, pilastro, porta, pozzo, stipite, suppellettile, volta) 🎯 `EpiSupportPart`.`objectType`.
- (S) `funzione dell'epigrafe/graffito`: 🎯 `CategoriesPart`:`fn` (📚 `categories_fn`). All the cells have ☯️ type:
  - T `testo`
  - U `monogramma`
  - V `lettera singola`
  - W `lettere non interpretabili`

### Language

- (X) `alfabeto`: 🎯 `CategoriesPart`.`lng` (📚 `categories_lng`)
  - Y `lingua` (📚 string: ARM, CHI, ENG, DUT, FRE, GER, GRC, GRE, ITA, JPN, LAT, N\D)
  - Z `lingua (ISO-639-3)` (📚 string: ARA, DEU, ELL, ENG, FRA, GRC, ITA, JPN, LAT, VEC, N\D)
  - AA `codice glottologico` (📚 string: ANCI1242, ARME1259, ITAL1282, LATI1261, LITE1248, MEDI1251, MODE1248, NUCL1643, STAN1290, STAN1293, STAN1295, VENE1258, N\D: see [Glottolog](https://glottolog.org/) codes)

### Content

- (AB) `contenuto` 🎯 `CategoriesPart`.`cnt` (📚 `categories_cnt`). All the cells have ☯️ type:
  - AC `amore`
  - AD `augurale`
  - AE `autentica di reliquie`
  - AF `bollo laterizio`
  - AG `calendario`
  - AH `celebrativa`
  - AI `citazione`
  - AJ `commemorativa`
  - AK `consacrazione`
  - AL `dedicatoria`
  - AM `devozionale`
  - AN `didascalica`
  - AO `documentaria`
  - AP `esegetica`
  - AQ `esortativa`
  - AR `ex voto`
  - AS `firma`
  - AT `funeraria`
  - AU `imprecazione`
  - AV `infamante`
  - AW `iniziale\i nome persona`
  - AX `insulto`
  - AY `invocativa`
  - AZ `marchio edile`
  - BA `nome`
  - BB `nome di luogo`
  - BC `parlante`
  - BD `politica`
  - BE `poesia`
  - BF `prosa`
  - BG `prostituzione`
  - BH `preghiera`
  - BI `religiosa`
  - BJ `saluto`
  - BK `segnaletica`
  - BL `sigla`
  - BM `sport`
  - BN `funzione non definibile` (this can be combined with others in the case it applies to just a letter or other isolated sign in the context of a text)
- BO `numeri` (☯️ string) 🎯 `CategoriesPart`.`cnt` (📚 `categories_cnt`):
  - BP `cifra` (📚 string: araba, armena, cirillica, glagolitica, romana, n\d)

### Date

- BQ `cronologia` (☯️ string) 🎯 `HistoricalDatePart`:
  - BR `data` (string: possible formats are `R SECOLO` where `R` is an uppercase Roman number, or `YYYY` for a year)
  - BS `datazione` (☯️ string)
  - BT `secolo` (same format as BR)
  - BU `termine post quem` (same format as BR)
  - BV `termine ante quem` (same format as BR): note that we can have both terminus ante and terminus post for an interval. In this case BS is the same as BU+BV.

### Material Support

- BW `materia` (string 📚 `epi-support-materials`: cemento, ceramica, laterizio, legno, materiale litico, metallo, vetro) 🎯 `EpiSupportPart`.`material`.
- (BX) `misure`:
  - BY `misure supporto` (string: width and height in cm in the form `NXN`; decimals use dot) 🎯 `EpiSupportPart`.`supportSize`.
  - BZ `misure specchio` (same format as BY) 🎯 `EpiSupportPart`.`mirrorSize`.
- CA `stato di conservazione` (📚 string: disperso, frammento, frammento contiguo, frammento isolato, integro, mutilo, reimpiego)
  - CB `damnatio` (☯️ string) 🎯 `EpiSupportPart`.`hasDamnatio`.
- CC `specchio` (☯️ string) 🎯 `EpiSupportPart`.`hasMirror`.
  - CD `cornice` (☯️ string) 🎯 `EpiSupportPart`.`hasFrame`.
  - CE `tipo di cornice` (string) `EpiSupportPart`.`frame`.
- (CF) `campo` 🎯 `EpiSupportPart`.`hasField`:
  - CG `misure` 🎯 `EpiSupportPart`.`fieldSize`.

### Techniques and Tools

All columns here map to 🎯 `EpiTechniquePart` except when specified otherwise.

- (CH) `tecnica di esecuzione`: except where specified, cells have ☯️ type:
  - CI `solco` (string)
  - CJ `a rilievo`
  - CK `disegno`
  - CL `graffio`
  - CM `incisione`
  - CN `intaglio`
  - CO `punzonatura`
- (CP) `strumento di esecuzione`: cells have ☯️ type:
  - CQ `bocciarda`
  - CR `carbocino` (sic)
  - CS `fumo di candela`
  - CT `gradina`
  - CU `grafite`
  - CV `inchiostro`
  - CW `matita di piombo`
  - CX `scalpello`
  - CY `sega`
  - CZ `sgorbia`
  - DA `strumento accuminato`
  - DB `vernice`
  - DC `lama (affilatura)`
  - (DD) `impaginazione del testo`:
    - DE `rigatura` (☯️ string) 🎯 `EpiSupportPart`.`features`.
    - DF `numero righe` (integer) 🎯 `EpiSupportPart`.`counts`.`rows`.
    - DG `note` (string) 🎯 `EpiSupportPart`.`note`.
    - DH `presenza di preparazione del supporto` (☯️ string) 🎯 `EpiSupportPart`.`features`.

### Writing

- DI `scrittura` (📚 string: maiuscola, maiuscola e minuscola, minuscola, n\d) 🎯 `EpiWritingPart`.`casing`
  - DJ `tipologia grafica caratteri latini` (📚 string: cancelleresca, capitale epigrafica, capitale libraria, capitale romanica, carolina, corsiva nuova, curiale, gotica, insulare, italica, mercantesca, merovingica, minuscola diplomatica, onciale, semionciale, umanistica, visigotica, altro, n\d) 🎯 `EpiWritingPart`.`script`
  - DK `segni grafici particolari` (☯️ string: si, no, n\d) 🎯 `EpiWritingPart`.`features` (📚 `epi-writing-features`). All cells have ☯️ type:
    - DL `abbreviazioni`
    - DM `nessi e legamenti`
    - DN `lettere incluse`
    - DO `lettere sovrapposte`
    - DP `punteggiatura`
    - DQ `segni di interpunzione`

### Figurative

- DR `figurativo` (☯️ string) 🎯 `CategoriesPart`:`fig` (📚 `categories_fig`). All cells have ☯️ type:
  - DS `disegno non interpretabile`
  - DT `abbigliamento`
  - DU `animale`
  - DV `architettura`
  - DW `arma`
  - DX `armatura`
  - DY `bandiera`
  - DZ `busto`
  - EA `croce`
  - EB `cuore`
  - EC `erotico`
  - ED `figura umana`
  - EE `geometrico`
  - EF `gioco`
  - EG `imbarcazione`
  - EH `lingua`
  - EI `paesaggio`
  - EJ `pianta`
  - EK `simbolo zodiacale`
  - EL `stemma`
  - EM `volto`

### Other Metadata

- (EN) `ricognizione`: 🎯 `PhysicalStatesPart`:
  - EO `data prima ricognizione` (string with format `DD/MM/YYYY`)
  - EP `data ultima ricognizione` (string with format `DD/MM/YYYY`)

### Unstructured Data

These data are temporarily stored in metadata, all prefixed by `_`. These should then be used as a source to fill the appropriate part.

- (EQ) `edizione e commento`
  - ER `edizione` 🎯 `MetadataPart`.`_edition`
  - ES `commento` 🎯 `MetadataPart`.`_comment`
  - ET `bibliografia` 🎯 `MetadataPart`.`_biblio`

## History

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
