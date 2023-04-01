# Cadmus VeLA Core

Core models for Cadmus VeLA.

- 🔗 [Cadmus VeLA API](https://github.com/vedph/cadmus-vela-api)
- 🔗 [Cadmus VeLA app](https://github.com/vedph/cadmus-vela-app)

- [Cadmus VeLA Core](#cadmus-vela-core)
  - [Data Model](#data-model)
    - [VeLA Parts](#vela-parts)
      - [GrfLocalizationPart](#grflocalizationpart)
      - [GrfSupportPart](#grfsupportpart)
      - [GrfFramePart](#grfframepart)
      - [GrfStatesPart](#grfstatespart)
      - [GrfWritingPart](#grfwritingpart)
      - [GrfFigurativePart](#grffigurativepart)
      - [GrfTechniquePart](#grftechniquepart)
    - [Items](#items)
  - [Original Spreadsheet](#original-spreadsheet)

## Data Model

Currently the core data model for VeLA includes 7 specialized parts and 13 general parts. In what follows, each part's model is represented with a list of properties. For each property its data type (string, boolean, etc.) and eventual thesaurus are specified in brackets; arrays are represented with suffix `[]` after the data type, so e.g. `string[]` means a list of strings. Some of the properties in turn are objects with other properties. The required properties are marked with an asterisk.

### VeLA Parts

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

### GrfFigurativePart

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

### Items

Currently the only item is the graffiti item, with parts conventionally grouped in these labelled sections:

- _summary_:
  - [GrfLocalizationPart](#grflocalizationpart)
  - [GrfSupportPart](#grfsupportpart)
  - [GrfFramePart](#grfframepart)
  - [GrfStatesPart](#grfstatespart)
  - NotePart with role `text`: this is the first draft of the text as copied on the spot.
  - NotePart with role `date`: note about date. The tag of the note can be used to classify the free text note as discussing a terminus ante, a terminus post, or their combination (=interval). When (and if) some reasonable datation can be inferred, it will then be specified using `HistoricalDatePart`.

- _details_:
  - [GrfWritingPart](#grfwritingpart)
  - [GrfTechniquePart](#grftechniquepart)
  - [GrfFigurativePart](#grffigurativepart)
  - HistoricalDatePart: this provides a structured datation model which is machine-actionable.
  - CategoriesPart with role `functions` (funerary, votive, etc.: 📚 thesaurus: `categories_functions`)
  - CategoriesPart with role `themes` (e.g. sport, politics, etc.: 📚 thesaurus: `categories_themes`)

- _text_:
  - TextPart: the edited text, susceptible of annotations via layers.
  - CommentsLayerPart
  - ChronologyLayerPart
  - LigaturesLayerPart

- _comment_:
  - CommentPart
  - NotePart

- _classification_:
  - MetadataPart
  - IndexKeywordsPart

- _references_:
  - BibliographyPart
  - DocReferencesPart
  - ExternalIdsPart

## Original Spreadsheet

The original schema was just a flat spreadsheet table, where some columns are grouped under so-called header columns, filled with color and without data, whose purpose is making all the following columns belonging to the same group. Often this is used to represent boolean features in a mutually exclusive relationship. Of course, this is just a hack due to the flat nature of the spreadsheet model.

- A = ID (e.g. `CASTELLO_01-0001`)
- B = image, I found it always empty. At any rate, once we have an ID, the image resources can be accessed via some transformation of it.
- C-E = area, sestriere, denominazione.
- F-K = funzione originaria, funzione attuale, tipologia struttura, interno/esterno, supporto, materiale.
- L = "datati" (boolean)
- M-O = terminus post, terminus ante, cronologia
  - P figurativi
  - Q testo
  - R numero
  - S cornice
- T tipo figurativo
- U tipo cornice
- V misure
- W numero righe
- X alfabeto
- Y lingua
- Z lingua ISO 639/3
- AA codice glottologico (?)
- AB tipologia grafica (?)
- AC tecnica esecuzione (header column)
  - AD presenza di disegno
  - AE presenza di preparazione del supporto
  - AF graffio
  - AG incisione
  - AH intaglio
  - AI disegno
  - AJ punzonatura
  - AK a rilievo
- AL strumento di esecuzione (header column)
  - AM chiodo
  - AN gradina
  - AO scalpello
  - AP sgorbia
  - AQ sega
  - AR bocciarda
  - AS grafite
  - AT matita di piombo
  - AU fumo di candela
  - AV inchiostro
  - AW vernice
  - AX lama (affilatura)
  - AY tipo di lama
- AZ caratteristiche grafiche (header column)
  - BA maiuscolo/minuscolo
  - BB sistema interpuntivo
  - BC nessi e legamenti
  - BD abbreviazioni
- BE monogrammi, lettere singole, etcc. (header column)
  - BF monogrammi
  - BG lettera singola
  - BH lettere non interpretabili
  - BI disegno non interpretabile
- BJ tipologia di argomento (header column): columns BK-CG
- CH categorie figurative (header column)
- CZ (header column): edizione e commento:
  - DA edizione
  - DB codice iconclass
  - DC commento
  - DD osservazioni sullo stato di conservazione
  - DE bibliografia
  - DF data primo rilievo
  - DG data ultima ricognizione
