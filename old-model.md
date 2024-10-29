# Old Model

This is the old model for Cadmus VeLA versions 1-2. Version 3 has been completely refactored to include other materials.

## Data Model

Currently the core data model for VeLA includes 7 specialized parts and 13 general parts. In what follows, each part's model is represented with a list of properties. For each property its data type (string, boolean, etc.) and eventual thesaurus are specified in brackets; arrays are represented with suffix `[]` after the data type, so e.g. `string[]` means a list of strings. Some of the properties in turn are objects with other properties. The required properties are marked with an asterisk.

### Items

Currently the only item is the _graffiti_ item, with parts conventionally grouped in these labelled sections:

- _summary_ ("sintesi"):
  - [GrfLocalizationPart](#grflocalizationpart)
  - [CategoriesPart](#categoriespart) with role `feature` (generic graffiti features; 📚 `categories_feature`)
  - [GrfSupportPart](#grfsupportpart)
  - [GrfFramePart](#grfframepart)
  - [GrfStatesPart](#grfstatespart)
  - [NotePart](#notepart) with role `text`: this is the first draft of the text as copied on the spot.
  - [NotePart](#notepart) with role `date`: note about date. The tag of the note can be used to classify the free text note as discussing a terminus ante, a terminus post, or their combination (=interval). When (and if) some reasonable datation can be inferred, it will then be specified using `HistoricalDatePart`.

- _details_ ("dettagli"):
  - [GrfWritingPart](#grfwritingpart)
  - [GrfTechniquePart](#grftechniquepart)
  - [GrfFigurativePart](#grffigurativepart)
  - [HistoricalDatePart](#historicaldatepart): this provides a structured datation model which is machine-actionable.
  - [CategoriesPart](#categoriespart) with role `topic` (e.g. sport, politics, etc.: 📚 `categories_topic`)

- _text_ ("testo"):
  - [TokenTextPart](#tokentextpart): the edited text, susceptible of annotations via layers.
  - [CommentsLayerFragment](#commentslayerfragment) (📚 `comment-categories`)
  - [ChronologyLayerFragment](#chronologylayerfragment)
  - [LigaturesLayerFragment](#ligaturelayerfragment) (📚 `epi-ligature-types`)

- _comment_ ("commento"):
  - [CommentPart](#commentpart) (📚 `comment-categories`)
  - [NotePart](#notepart)

- _classification_ ("classificazione"):
  - [MetadataPart](#metadatapart)
  - [IndexKeywordsPart](#indexkeywordspart) (📚 `languages`)

- _references_ ("riferimenti"):
  - [BibliographyPart](#bibliographypart) (📚 `bibliography-author-roles`, `bibliography-languages`, `bibliography-types`)
  - [DocReferencesPart](#docreferencespart)
  - [ExternalIdsPart](#externalidspart)

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
- `period`\* (`string`, 📚 thesaurus: `grf-periods`)
- `objectType`\* (`string`, 📚 thesaurus: `grf-support-object-types`)
- `damnatio` (`string`, 📚 thesaurus: `grf-damnatio-types`)
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
- `glottologCodes` (`string[]`, 📚 thesaurus: `grf-writing-glottologs`)
- `scripts`\* (`string[]`, 📚 thesaurus: `grf-writing-scripts`): paleographic script(s) (e.g. gothic, merchant, etc.).
- `casing`\* (`string`, 📚 thesaurus: `grf-writing-casing`)
- `prevalentCasing`\* (`string`, 📚 thesaurus: `grf-writing-prevalent-casing`)
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

### Generic Parts

These parts belong to the generic set of Cadmus parts.

#### BibliographyPart

Generic bottom-up bibliography.

- `entries` (`BibEntry[]`):
  - `key` (`string`)
  - `typeId`\* (`string`): the type identifier for the entry and its container: e.g. book, journal article, book article, proceedings article, journal review, ebook, site, magazine, newspaper, tweet, TV series, etc.
  - `authors` (`BibAuthor[]`): 1 or more authors, each having a first name, a last name, and an optional role ID. The role IDs are usually drawn from a thesaurus, and mostly used for contributors:
    - `firstName` (string): first name.
    - `lastName`\* (string): last name.
    - `roleId` (`string`): role ID.
  - `title`\* (`string`)
  - `language`\* (`string`): the ISO 639-3 letters (primary) language code of the bibliographic entry.
  - `container` (`string`): the optional container: a journal, a book, a collection of proceedings, etc.
  - `contributors` (`BibAuthor[]`): 0 or more contributors, with the same properties of the authors. Usually they also have some role specified, e.g. "editor" for the editor of a book collecting a number of articles from different authors, "translator", "organization", etc.
  - `edition` (short): the optional edition number. Default is 0.
  - `number` (string): the optional alphanumeric number (e.g. for a journal).
  - `publisher` (string): the optional publisher name.
  - `yearPub` (short): the optional year of publication. Default is 0.
  - `placePub` (string): the optional place of publication.
  - `location` (string): the location identifier for the bibliographic item, e.g. an URL or a DOI.
  - `accessDate` (Date): the optional last access date, typically used for web resources.
  - `firstPage` (short)
  - `lastPage` (short)
  - `keywords` (`Keyword[]`):
    - `language` (string)
    - `value` (string)
  - `note` (`string`)

#### CategoriesPart

A set of categories which can be assigned to the item. In this project we use a couple of such parts referring to two distinct taxonomies, for support functions and content themes. While the generic thesaurus for this part is named `categories`, role-based category parts add to this name a suffix built with `_` and the role ID (thus, `categories_function` and `categories_topic`).

- `categories` (`string[]`)

#### CommentPart

A generic free-text comment with formatting and some metadata.

- `comment` (`Comment`):
  - `tag` (`string`)
  - `text` (`string`, Markdown)
  - `references` (`DocReference[]`):
    - `type` (`string`)
    - `tag` (`string`)
    - `citation` (`string`)
    - `note` (`string`)
  - `links` (`AssertedCompositeId[]`):
    - `target` (`PinTarget`):
      - `gid` (`string`)
      - `label` (`string`)
      - `itemId` (`string`)
      - `partId` (`string`)
      - `partTypeId` (`string`)
      - `roleId` (`string`)
      - `name` (`string`)
      - `value` (`string`)
    - `scope` (`string`)
    - `tag` (`string`)
    - `assertion` (`Assertion`):
      - `tag` (`string`)
      - `rank` (`short`)
      - `references` (`DocReference[]`)
      - `note` (`string`)
  - `categories` (`string[]`)
  - `keywords` (`IndexKeyword[]`):
    - `language` (`string`)
    - `value` (`string`)
    - `indexId` (`string`)
    - `note` (`string`)
    - `tag` (`string`)

#### DocReferencesPart

Generic short documental references.

- `references` (`DocReference[]`):
  - `type` (`string`)
  - `tag` (`string`)
  - `citation` (`string`)
  - `note` (`string`)

#### ExternalIdsPart

External identifiers associated to the item.

- `ids` (`AssertedId[]`):
  - `tag` (`string`)
  - `value`\* (`string`)
  - `scope` (`string`)
  - `assertion` (`Assertion`):
    - `tag` (`string`)
    - `rank` (`short`)
    - `references` (`DocReference[]`)
    - `note` (`string`)

#### HistoricalDatePart

A single historical datation.

- `date` (`HistoricalDate`):
  - `a`\* (`Datation`):
    - `value`\* (`int`): the numeric value of the point. Its interpretation depends on other points properties: it may represent a year or a century, or a span between two consecutive Gregorian years.
    - `isCentury` (boolean): true if value is a century number; false if it's a Gregorian year.
    - `isSpan` (boolean): true if the value is the first year of a pair of two consecutive years. This is used for calendars which span across two Gregorian years, e.g. 776/5 BC.
    - `month` (short): the month number (1-12) or 0.
    - `day` (short): the day number (1-31) or 0.
    - `isApproximate` (boolean): true if the point is approximate ("about").
    - `isDubious` (boolean): true if the point is dubious ("perhaphs").
    - `hint` (string): a short textual hint used to better explain or motivate the datation point.
  - `b` (`Datation`)
- `references` (`DocReference[]`):
  - `type` (`string`)
  - `tag` (`string`)
  - `citation` (`string`)
  - `note` (`string`)

#### IndexKeywordsPart

- `keywords` (`IndexKeyword[]`):
  - `language` (`string`)
  - `value` (`string`)
  - `indexId` (`string`)
  - `note` (`string`)
  - `tag` (`string`)

#### MetadataPart

A generic container for name=value metadata.

- `metadata` (`Metadatum[]`):
  - `type` (`string`)
  - `name` (`string`)
  - `value` (`string`)

#### NotePart

A generic free text note with Markdown markup.

- `tag` (string)
- `note`\* (string, Markdown)

#### TokenTextPart

A token-based text which can get annotations from layer parts.

- `citation` (`string`)
- `lines`\* (`TextLine[]`):
  - `y`\* (`int`)
  - `text`\* (`string`)

#### ChronologyLayerFragment

A layer fragment to annotate a portion of text with a chronological indication.

- `location` (`string`)
- `tag` (`string`)
- `label` (`string`)
- `eventId` (`string`)
- `date` (`HistoricalDate`): see [above](#historicaldatepart)

#### CommentsLayerFragment

A layer fragment to annotate a portion of text with a comment. The comment has the usual `location` (common to all fragments) and the same model as [CommentPart.comment](#commentpart).

#### LigatureLayerFragment

A layer fragment to annotate a portion of text with a ligature.

- `location` (`string`)
- `type` (`int`): a numeric value: 0=none, 1=ligature, 2=inversion, 3=overlap, 4=replacement, 5=graft, 6=inclusion, 7=connection, 8=complex (Manzella 1987 149-151).

## Spreadsheet Columns

Columns marked as "header columns" are always empty and serve to group the next columns together, until the next header column. When not specified, the type is `string`.

The ID after 🎯 represents the target for the column, and the one after ⚙️ the parser used by the [CLI import tool](https://github.com/vedph/cadmus-vela-tool).

- A ID (no label: e.g. `CASTELLO_01-0001`) 🎯 `item.title`, `MetadataPart` id ⚙️ `Row`.
- B `immagine`: ignored.
- C-E (3-5) = `area`, `sestriere`, `denominazione` 🎯 `GrfLocalizationPart` ⚙️ `ColArea`
- F = `funzione originaria` 🎯 `GrfLocalizationPart.note` ⚙️ `ColOriginalFn`
- G `funzione attuale` 🎯 `GrfLocalizationPart.function` (📚 `categories_functions`) ⚙️ `ColCurrentFn`
- H `tipologia struttura` 🎯 `GrfLocalizationPart.objectType` (📚 `grf-support-object-types`) ⚙️ `ColStructType`
- I `interno/esterno` 🎯 `GrfLocalizationPart.indoor` ⚙️ `ColIndoor`
- J `supporto` 🎯 `GrfSupportPart.type` (📚 `grf-support-types`) ⚙️ `ColSupport`
- K `materiale` 🎯 `GrfSupportPart.material` (📚 `grf-support-materials`) ⚙️ `ColMatType`
- L `età` (string) one of `età romana`, `età medioevale`, `età moderna`, `età contemporanea` 🎯 `GrfLocalizationPart.period` (📚 `grf-periods`) ⚙️ `ColPeriod`
- M `datati` (boolean): apparently this just tells whether a date is specified in the next columns.
- N-P (14-16) = `terminus post`, `terminus ante`, `cronologia`. 🎯 `HistoricalDatePart` ⚙️ `ColDatation`. A single cell contains any of these formats:
  - `R SECOLO` where `R` is an uppercase Roman number.
  - `YYYY` year.

>Possible combinations: N, O, P, N+O, N+P=N, O+P=N. This is because N/O are termini and can occur together for an interval, but for some reason in this case P copies the value from N/O and must be ignored.

- Q `figurativi` (boolean) 🎯 `CategoriesPart:feature` (📚 `categories_feature`) ⚙️ `ColFeatures`
- R `testo` (boolean) 🎯 `CategoriesPart:feature` (📚 `categories_feature`) ⚙️ `ColFeatures`
- S `numeri` (boolean) 🎯 `CategoriesPart:feature` (📚 `categories_feature`) ⚙️ `ColFeatures`
- T `cornice` (boolean) 🎯 `CategoriesPart:feature` (📚 `categories_feature`) ⚙️ `ColFeatures`

- U `tipo figurativo` 🎯 `GrfFramePart.figure` ⚙️ `ColFig`
- V `tipo cornice` 🎯 `GrfFramePart.frame` ⚙️ `ColFig`
- W `misure` width and height in cm in the form `NXN`; decimals use dot 🎯 `GrfFramePart.size` ⚙️ `ColSize`

- X `numero righe` (int) 🎯 `GrfWritingPart.counts` ⚙️ `ColWriting`
- Y `alfabeto` 🎯 `GrfWritingPart.system` (📚 `grf-writing-systems`) ⚙️ `ColWriting`
- Z `lingua`: ignored, this is just the full form (e.g. "Italiano") corresponding to the AA code.
- AA `lingua (iso-639-3)` (ISO639-3) 🎯 `GrfWritingPart.languages` (📚 `grf-writing-languages`) ⚙️ `ColWriting`
- AB `codice glottologico` [Glottolog](https://glottolog.org/) codes (📚 `grf-writing-glottologs`) ⚙️ `ColWriting`
- AC `tipologia scrittura`: separated by comma 🎯 `GrfWritingPart.script` (📚 `grf-writing-scripts`) ⚙️ `ColWriting`
- AD `tipologia grafica` (`corsivo`, `maiuscolo`, `maiuscolo e minuscolo`, `minuscolo`, `n\d`) 🎯 `GrfWritingPart.casing` (📚 `grf-writing-casing`) ⚙️ `ColWriting`

- AE `tecnica di esecuzione`: header column 🎯 `GrfTechniquePart.techniques` (📚 `grf-techniques`)
  - AF `presenza di disegno preparatorio` (boolean) ⚙️ `ColTech`
  - AG `presenza di preparazione del supporto` (boolean) ⚙️ `ColTech`
  - AH `graffio` (boolean) ⚙️ `ColTech`
  - AI `incisione` (boolean) ⚙️ `ColTech`
  - AJ `intaglio` (boolean) ⚙️ `ColTech`
  - AK `disegno` (boolean) ⚙️ `ColTech`
  - AL `punzonatura` (boolean) ⚙️ `ColTech`
  - AM `rubricatura` (boolean) 🎯 `GrfWritingPart.hasRubrics` ⚙️ `ColWriting`
  - AN `a rilievo` (boolean) ⚙️ `ColTech`

- AO `strumento di esecuzione`: header column 🎯 `GrfTechniquePart.tools` (📚 `grf-tools`) ⚙️ `ColTech`:
  - AP `chiodo` (boolean)
  - AQ `gradina` (boolean)
  - AR `scalpello` (boolean)
  - AS `sgorbia` (boolean)
  - AT `sega` (boolean)
  - AU `bocciarda` (boolean)
  - AV `grafite` (boolean)
  - AW `matita di piombo` (boolean)
  - AX `fumo di candela` (boolean)
  - AY `inchiostro` (boolean)
  - AZ `vernice` (boolean)
  - BA `lama (affilatura)` (boolean)
  - BB `tipo di lama` (string): values are only `lama curva`, `lama dritta` or empty. We thus provide two entries in the thesaurus for these values.

- BC `damnatio`: header column.
  - BD `presenza di damnatio` (`parziale`, `totale`, `non presente` or empty) 🎯 `GrfLocalizationPart.damnatio` (📚 `grf-damnatio-types`) ⚙️ `ColDamnatio`

- BE `caratteristiche grafiche`: header column, all targeting 🎯 `GrfWritingPart.scriptFeatures` (📚 `grf-writing-script-features`) using ⚙️ `ColWriting` except when stated otherwise:
  - BF `maiuscolo\minuscolo prevalente`: values are `maiuscolo prevalente`, `minuscolo prevalente`, `N\D`, empty (📚 `grf-writing-prevalent-casing`) ⚙️ `ColWriting`
  - BG `sistema interpuntivo` (boolean)
  - BH `nessi e legamenti` (boolean)
  - BI `rigatura` (boolean) 🎯 `GrfWritingPart.hasRuling`
  - BJ `abbreviazioni` (boolean)

- BK `monogrammi, lettere singole, ecc`: header column, all targeting 🎯 `GrfWritingPart.letterFeatures` (📚 `grf-writing-letter-features`) using ⚙️ `ColWriting`:
  - BL `monogrammi` (boolean)
  - BM `lettera singola` (boolean)
  - BN `lettere non interpretabili` (boolean): this also sets an item flag.
  - BO `disegno non interpretabile` (boolean): this also sets an item flag.

- BP `tipologia di argomento`: header column, all targeting 🎯 `CategoriesPart:topic` (📚 `categories_topic`) unless specified otherwise:
  - BQ `funeraria` (boolean)
  - BR `commemorativa` (boolean)
  - BS `firma` (boolean)
  - BT `celebrativa` (boolean)
  - BU `esortativa` (boolean)
  - BV `didascalica` (boolean)
  - BW `iniziale\i nome persona` (boolean)
  - BX `sigla` (boolean)
  - BY `segnaletica` (boolean)
  - BZ `citazione` (boolean)
  - CA `infamante` (boolean)
  - CB `sport` (boolean) 🎯
  - CC `prostituzione` (boolean)
  - CD `politica` (boolean)
  - CE `religiosa` (boolean)
  - CF `preghiera` (boolean)
  - CG `ex voto` (boolean)
  - CH `amore` (boolean)
  - CI `prosa` (boolean) 🎯 `GrfWritingPart.hasProse`
  - CJ `poesia` (boolean) 🎯 `GrfWritingPart.hasPoetry`
  - CK `parlanti` (boolean)
  - CL `insulto` (boolean)
  - CM `imprecazioni` (boolean)
  - CN `nome di luogo` (boolean)
  - CO `saluti` (boolean)

- CP `categorie figurative`: header column 🎯 `GrfFigurativePart.types` (📚 `grf-figurative-types`) ⚙️ `ColFigTypes`:
  - CQ `parti anatomiche` (boolean)
  - CR `volti` (boolean)
  - CS `busto` (boolean)
  - CT `figura umana` (boolean)
  - CU `erotici` (boolean)
  - CV `croce` (boolean)
  - CW `cuore` (boolean)
  - CX `architetture` (boolean)
  - CY `paesaggi` (boolean)
  - CZ `geometrico` (boolean)
  - DA `imbarcazioni` (boolean)
  - DB `piante` (boolean)
  - DC `gioco` (boolean)
  - DD `arma` (boolean)
  - DE `armatura` (boolean)
  - DF `stemma` (boolean)
  - DG `bandiera` (boolean)
  - DH `animale` (boolean)
  - DI `simbolo zodiaco` (boolean)
  - DJ `grafitto da affilitura` (boolean)

- DK `edizione e commento`: header column:
  - DL `edizione` 🎯 `BibliographyPart`: manually filled.
  - DM `codice iconclass`: obsolete, ignore.
  - DN `commento` 🎯 `NotePart` ⚙️ `ColComment`
  - DO `osservazioni sullo stato di conservazione`, 🎯 `GrfStatesPart.note` ⚙️ `ColStates`
  - DP `bibliografia` 🎯 `BibliographyPart`: manually filled.
  - DQ `data primo rilievo` (GG/MM/AAAA) 🎯 `GrfStatesPart.states` ⚙️ `ColStates`
  - DR `data ultima ricognizione` (GG/MM/AAAA) 🎯 `GrfStatesPart.states` ⚙️ `ColStates`
