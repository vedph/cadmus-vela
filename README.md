# Cadmus VeLA Core

Core models for Cadmus VeLA.

- [Cadmus VeLA API](https://github.com/vedph/cadmus-vela-api)
- [Cadmus VeLA app](https://github.com/vedph/cadmus-vela-app)

Currently the only item is the graffiti item, with the parts listed below.

## Data Model

### GrfSummaryPart

- 🔑 ID: `it.vedph.graffiti.summary`

Essential information about a graffiti. This corresponds to the data core which might also be derived from external sources.

(1) location

- `place`\* (🧱 `ProperName`):
  - `language` (`string`, thesaurus: `grf-place-languages`)
  - `tag` (`string`)
  - `pieces` (`ProperNamePiece[]`, thesaurus: `grf-place-piece-types`):
    - `type`\* (`string`)
    - `value`\* (`string`)
- `supportType`\* (`string`, thesaurus: `grf-support-types`)
- `objectType` (`string`, thesaurus: `grf-support-object-types`)
- `originalFn` (`string`, thesaurus: `grf-support-functions`)
- `currentFn`\* (`string`, thesaurus: `grf-support-functions`)
- `indoor`\* (`boolean`)

(2) material

- `material`\* (`string`, thesaurus: `grf-support-materials`)
- `description`\* (`string`, 5000)

(3) identification

- `size`\* (🧱 `PhysicalSize`):
  - `tag` (`string`, thesaurus: `physical-size-tags`)
  - `w` (`PhysicalDimension`):
    - `value`\* (`number`)
    - `unit`\* (`string`, thesaurus: `physical-size-units`)
    - `tag` (`string`, thesaurus: `physical-size-dim-tags`)
  - `h` (`PhysicalDimension`)
  - `d` (`PhysicalDimension`)
  - `note` (`string`)
- `date`\* (🧱 `HistoricalDate`)
  - `a` (`Datation`):
    - `value` (`int`)
    - `isCentury` (`bool`)
    - `isSpan` (`bool`)
    - `isApproximate` (`bool`)
    - `isDubious` (`bool`)
    - `day` (`short`)
    - `month` (`short`)
    - `hint` (`string`)
  - `b` (`Datation`)
- `features` (`string[]`; thesaurus: `grf-features`)
- `figDescription` (`string`, 5000)
- `frameDescription` (`string`, 5000)
- `lastSeen`\* (`date`, usually UTC)

(4) additional

This section contains all the additional properties in comparison with the summary core.

- `states` (`GrfSupportState[]`):
  - `type`\* (`string`, thesaurus: `grf-support-states`)
  - `date`\* (`date`, usually UTC)
  - `reporter`\* (`string`, 100)
  - `note` (`string`, 5000)

### GrfTechniquePart

- 🔑 ID: `it.vedph.graffiti.technique`

Techniques and tools.

- `techniques`\* (`string[]`, thesaurus: `grf-techniques`)
- `tools`\* (`string[]`, thesaurus: `grf-tools`)
- `note` (`string`, 5000)

### GrfWritingPart

- 🔑 ID: `it.vedph.graffiti.writing`

Writing description.

- `system`\* (`string`, thesaurus: `grf-writing-systems`, usually ISO 15924 lowercase)
- `languages`\* (`string[]`, thesaurus: `grf-writing-languages`, usually ISO 639-3)
- `type`\* (`string`, thesaurus: `grf-writing-types`)
- `counts` (`DecoratedCount`[]):
  - `id`\* (`string`, thesaurus: `grf-writing-count-ids`)
  - `tag` (`string`, thesaurus: `grf-writing-count-tags`)
  - `value`\* (`number`)
  - `note` (`string`)
- `features` (`string[]`, thesaurus, `grf-writing-features`)
- `hasPoetry` (`boolean`)
- `metres` (`string[]`, thesaurus: `grf-writing-metres`)

### GrfFigurativePart

- 🔑 ID: `it.vedph.graffiti.figurative`

Figurative description.

- `frameType` (`string`, thesaurus: `grf-fig-frame-types`)
- `type` (`string`, thesaurus: `grf-fig-types`, hierarchical)
- `features` (`string[]`, thesaurus: `grf-fig-features`)
- `note` (`string`, 5000)

### Other Parts

- metadata
- categories
- keywords
- comment
- note
- bibliography
- doc references
- external IDs
- text
- comment layer
- chronology layer

### Graffiti Item Layout

- graffiti group:
  - summary
  - technique
  - writing
  - figurative

- general group:
  - metadata
  - categories
  - keywords

- comment group:
  - comment
  - note

- references group:
  - bibliography
  - doc references
  - external IDs

- text group:
  - text
  - comment layer
  - chronology layer

## Original Spreadsheet

TODO update

The original schema was just a flat spreadsheet table, where some columns are grouped under so-called header columns, filled with color and without data, whose purpose is making all the following columns belonging to the same group. Often this is used to represent boolean features in a mutually exclusive relationship. Of course, this is just a hack due to the flat nature of the spreadsheet model.

- A = ID (e.g. `CASTELLO_01-0001`): this can just be the item's **title** and eventually an EID in **metadata part**.
- B = image, I found it always empty. At any rate, once we have an ID, the image resources can be accessed via some transformation of it.
- C-E = area, sestriere, denominazione: **toponyms part** hierarchy. 📚
- F-K = funzione originaria, funzione attuale, tipologia struttura, interno/esterno, supporto, materiale: **epigraphic support part** (📚 `epi-support-materials`, `epi-support-functions`, `epi-support-object-types`, `epi-support-types`).
- L = "datati" (boolean): eventually in **metadata part**.
- M-O = terminus post, terminus ante, cronologia: **datation part**.
- **writing part**: script features (📚 `epi-writing-script-features`):
  - P figurativi
  - Q testo
  - R numero
  - S cornice
- T tipo figurativo: **support part**: figurative type (📚 `epi-writing-fig-types`).
- U tipo cornice: **writing part**: figurative features (📚 `epi-writing-fig-features`).
- V misure: **support part**: size.
- W numero righe: **writing part**: counts (📚 `decorated-count-ids`, `decorated-count-tags`).
- X alfabeto: **writing part**: system.
- Y lingua: **writing part**: languages.
- Z lingua ISO 639/3: as above.
- AA codice glottologico (?)
- AB tipologia grafica (?)
- AC tecnica esecuzione (header column): **writing part**: technique (📚 `epi-writing-techniques`):
  - AD presenza di disegno
  - AE presenza di preparazione del supporto
  - AF graffio
  - AG incisione
  - AH intaglio
  - AI disegno
  - AJ punzonatura
  - AK a rilievo
- AL strumento di esecuzione (header column): **writing part**: tool (📚 `epi-writing-tools`):
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
- AZ caratteristiche grafiche (header column): **writing part**: script features (📚 `epi-writing-script-features`):
  - BA maiuscolo/minuscolo
  - BB sistema interpuntivo
  - BC nessi e legamenti (note: you might also provide more details with the **ligatures layer** with 📚 `epi-ligature-types`).
  - BD abbreviazioni
- BE monogrammi, lettere singole, etcc. (header column): **writing part**: script features (📚 `epi-writing-script-features`):
  - BF monogrammi
  - BG lettera singola
  - BH lettere non interpretabili
  - BI disegno non interpretabile
- BJ tipologia di argomento (header column): columns BK-CG: **categories part** (📚 `categories`).
- CH categorie figurative (header column): **writing part**: columns CH-CY: figurative type and features (📚 `epi-writing-fig-types`, `epi-writing-fig-features`).
- CZ (header column): edizione e commento:
  - DA edizione: **bibliography part** (📚 `bibliography-author-roles`, `bibliography-languages`, `bibliography-types`).
  - DB codice iconclass: in **writing part**: figurative type/features (in IDs).
  - DC commento: **comment part**.
  - DD osservazioni sullo stato di conservazione: **support part**.
  - DE bibliografia: **bibliography part**.
  - DF data primo rilievo: **metadata part**, or add to support part, depending on how typical we can estimate this property.
  - DG data ultima ricognizione: **support part**.
