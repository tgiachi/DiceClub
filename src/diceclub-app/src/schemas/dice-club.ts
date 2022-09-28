/* eslint-disable */
/* tslint:disable */
/*
 * ---------------------------------------------------------------
 * ## THIS FILE WAS GENERATED VIA SWAGGER-TYPESCRIPT-API        ##
 * ##                                                           ##
 * ## AUTHOR: acacode                                           ##
 * ## SOURCE: https://github.com/acacode/swagger-typescript-api ##
 * ---------------------------------------------------------------
 */

export interface BooleanRestResultObject {
  result?: boolean;
  error?: string | null;
  haveError?: boolean;
}

export interface CardStageAddRequest {
  cardName?: string | null;
  setCode?: string | null;

  /** @format uuid */
  languageId?: string;

  /** @format int32 */
  quantity?: number;
  isFoil?: boolean;
}

export interface DeckCreateRequest {
  deckName?: string | null;
  colors?: string[] | null;

  /** @format int32 */
  totalCards?: number;

  /** @format int32 */
  totalSideBoard?: number;
  manaCurves?: DeckManaCurve[] | null;
}

export enum DeckDetailCardType {
  Main = "Main",
  Land = "Land",
  SideBoard = "SideBoard",
}

export interface DeckDetailDto {
  /** @format uuid */
  id?: string;

  /** @format date-time */
  created?: string;

  /** @format date-time */
  updated?: string;

  /** @format uuid */
  deckMasterId?: string;

  /** @format uuid */
  cardId?: string;
  card?: MtgCardDto;

  /** @format int64 */
  quantity?: number;
  cardType?: DeckDetailCardType;
}

export interface DeckDetailDtoListRestResultObject {
  result?: DeckDetailDto[] | null;
  error?: string | null;
  haveError?: boolean;
}

export enum DeckFormat {
  Commander = "Commander",
  FreeForm = "FreeForm",
}

export interface DeckManaCurve {
  /** @format int32 */
  cmcCost?: number;

  /** @format int32 */
  count?: number;
}

export interface DeckManaCurvePreset {
  name?: string | null;
  manaCurve?: DeckManaCurve[] | null;
}

export interface DeckManaCurvePresetListRestResultObject {
  result?: DeckManaCurvePreset[] | null;
  error?: string | null;
  haveError?: boolean;
}

export interface DeckMasterDto {
  /** @format uuid */
  id?: string;

  /** @format date-time */
  created?: string;

  /** @format date-time */
  updated?: string;
  name?: string | null;

  /** @format uuid */
  ownerId?: string;
  colorIdentity?: string | null;
  format?: DeckFormat;

  /** @format int32 */
  cardCount?: number;
}

export interface DeckMasterDtoPaginatedRestResultObject {
  result?: DeckMasterDto[] | null;
  error?: string | null;
  haveError?: boolean;

  /** @format int32 */
  pageSize?: number;

  /** @format int32 */
  page?: number;

  /** @format int32 */
  pageCount?: number;

  /** @format int64 */
  count?: number;
}

export interface DeckMultipleDeckRequest {
  /** @format int32 */
  count?: number;

  /** @format int32 */
  totalCards?: number;

  /** @format int32 */
  sideBoardTotalCards?: number;
  colors?: string[] | null;
}

export interface DiceClubUserDto {
  /** @format uuid */
  id?: string;

  /** @format date-time */
  created?: string;

  /** @format date-time */
  updated?: string;
  email?: string | null;
  name?: string | null;
  last?: string | null;
  nickName?: string | null;
  isActive?: boolean;
  serialId?: string | null;
}

export interface DiceClubUserDtoPaginatedRestResultObject {
  result?: DiceClubUserDto[] | null;
  error?: string | null;
  haveError?: boolean;

  /** @format int32 */
  pageSize?: number;

  /** @format int32 */
  page?: number;

  /** @format int32 */
  pageCount?: number;

  /** @format int64 */
  count?: number;
}

export interface DiceClubUserDtoRestResultObject {
  result?: DiceClubUserDto;
  error?: string | null;
  haveError?: boolean;
}

export interface LoginRequestData {
  email?: string | null;
  password?: string | null;
}

export interface LoginRequestDataRestResultObject {
  result?: LoginRequestData;
  error?: string | null;
  haveError?: boolean;
}

export interface LoginResponseData {
  accessToken?: string | null;
  refreshToken?: string | null;

  /** @format date-time */
  accessTokenExpire?: string;
}

export interface LoginResponseDataRestResultObject {
  result?: LoginResponseData;
  error?: string | null;
  haveError?: boolean;
}

export interface MtgCardColorDto {
  /** @format uuid */
  id?: string;

  /** @format date-time */
  created?: string;

  /** @format date-time */
  updated?: string;
  name?: string | null;
  description?: string | null;
  imageUrl?: string | null;
}

export interface MtgCardColorDtoPaginatedRestResultObject {
  result?: MtgCardColorDto[] | null;
  error?: string | null;
  haveError?: boolean;

  /** @format int32 */
  pageSize?: number;

  /** @format int32 */
  page?: number;

  /** @format int32 */
  pageCount?: number;

  /** @format int64 */
  count?: number;
}

export interface MtgCardColorDtoRestResultObject {
  result?: MtgCardColorDto;
  error?: string | null;
  haveError?: boolean;
}

export interface MtgCardColorRelDto {
  /** @format uuid */
  id?: string;

  /** @format date-time */
  created?: string;

  /** @format date-time */
  updated?: string;
  color?: MtgCardColorDto;
}

export interface MtgCardDto {
  /** @format uuid */
  id?: string;

  /** @format date-time */
  created?: string;

  /** @format date-time */
  updated?: string;
  scryfallId?: string | null;

  /** @format int32 */
  mtgId?: number | null;
  name?: string | null;
  foreignNames?: string | null;
  printedName?: string | null;
  typeLine?: string | null;
  description?: string | null;
  language?: MtgCardLanguageDto;
  manaCost?: string | null;

  /** @format double */
  cmc?: number | null;

  /** @format int32 */
  power?: number | null;

  /** @format int32 */
  toughness?: number | null;

  /** @format int32 */
  collectorNumber?: number | null;
  set?: MtgCardSetDto;
  rarity?: MtgCardRarityDto;
  lowResImageUrl?: string | null;
  highResImageUrl?: string | null;
  type?: MtgCardTypeDto;

  /** @format int32 */
  cardMarketId?: number | null;

  /** @format int32 */
  quantity?: number;
  colors?: MtgCardColorRelDto[] | null;
  legalities?: MtgCardLegalityRelDto[] | null;

  /** @format uuid */
  ownerId?: string;
  isColorLess?: boolean;
  isMultiColor?: boolean;

  /** @format double */
  price?: number | null;
}

export interface MtgCardDtoPaginatedRestResultObject {
  result?: MtgCardDto[] | null;
  error?: string | null;
  haveError?: boolean;

  /** @format int32 */
  pageSize?: number;

  /** @format int32 */
  page?: number;

  /** @format int32 */
  pageCount?: number;

  /** @format int64 */
  count?: number;
}

export interface MtgCardLanguageDto {
  /** @format uuid */
  id?: string;

  /** @format date-time */
  created?: string;

  /** @format date-time */
  updated?: string;
  name?: string | null;
  code?: string | null;
}

export interface MtgCardLanguageDtoPaginatedRestResultObject {
  result?: MtgCardLanguageDto[] | null;
  error?: string | null;
  haveError?: boolean;

  /** @format int32 */
  pageSize?: number;

  /** @format int32 */
  page?: number;

  /** @format int32 */
  pageCount?: number;

  /** @format int64 */
  count?: number;
}

export interface MtgCardLanguageDtoRestResultObject {
  result?: MtgCardLanguageDto;
  error?: string | null;
  haveError?: boolean;
}

export interface MtgCardLegalityDto {
  /** @format uuid */
  id?: string;

  /** @format date-time */
  created?: string;

  /** @format date-time */
  updated?: string;
  name?: string | null;
}

export interface MtgCardLegalityDtoPaginatedRestResultObject {
  result?: MtgCardLegalityDto[] | null;
  error?: string | null;
  haveError?: boolean;

  /** @format int32 */
  pageSize?: number;

  /** @format int32 */
  page?: number;

  /** @format int32 */
  pageCount?: number;

  /** @format int64 */
  count?: number;
}

export interface MtgCardLegalityDtoRestResultObject {
  result?: MtgCardLegalityDto;
  error?: string | null;
  haveError?: boolean;
}

export interface MtgCardLegalityRelDto {
  /** @format uuid */
  id?: string;

  /** @format date-time */
  created?: string;

  /** @format date-time */
  updated?: string;
  cardLegality?: MtgCardLegalityDto;
  cardLegalityType?: MtgCardLegalityTypeDto;
}

export interface MtgCardLegalityTypeDto {
  /** @format uuid */
  id?: string;

  /** @format date-time */
  created?: string;

  /** @format date-time */
  updated?: string;
  name?: string | null;
}

export interface MtgCardLegalityTypeDtoPaginatedRestResultObject {
  result?: MtgCardLegalityTypeDto[] | null;
  error?: string | null;
  haveError?: boolean;

  /** @format int32 */
  pageSize?: number;

  /** @format int32 */
  page?: number;

  /** @format int32 */
  pageCount?: number;

  /** @format int64 */
  count?: number;
}

export interface MtgCardLegalityTypeDtoRestResultObject {
  result?: MtgCardLegalityTypeDto;
  error?: string | null;
  haveError?: boolean;
}

export interface MtgCardRarityDto {
  /** @format uuid */
  id?: string;

  /** @format date-time */
  created?: string;

  /** @format date-time */
  updated?: string;
  name?: string | null;
  image?: string | null;
}

export interface MtgCardRarityDtoPaginatedRestResultObject {
  result?: MtgCardRarityDto[] | null;
  error?: string | null;
  haveError?: boolean;

  /** @format int32 */
  pageSize?: number;

  /** @format int32 */
  page?: number;

  /** @format int32 */
  pageCount?: number;

  /** @format int64 */
  count?: number;
}

export interface MtgCardRarityDtoRestResultObject {
  result?: MtgCardRarityDto;
  error?: string | null;
  haveError?: boolean;
}

export interface MtgCardSetDto {
  /** @format uuid */
  id?: string;

  /** @format date-time */
  created?: string;

  /** @format date-time */
  updated?: string;
  code?: string | null;
  description?: string | null;
  image?: string | null;

  /** @format int32 */
  cardCount?: number;
}

export interface MtgCardSetDtoPaginatedRestResultObject {
  result?: MtgCardSetDto[] | null;
  error?: string | null;
  haveError?: boolean;

  /** @format int32 */
  pageSize?: number;

  /** @format int32 */
  page?: number;

  /** @format int32 */
  pageCount?: number;

  /** @format int64 */
  count?: number;
}

export interface MtgCardSetDtoRestResultObject {
  result?: MtgCardSetDto;
  error?: string | null;
  haveError?: boolean;
}

export interface MtgCardStageDto {
  /** @format uuid */
  id?: string;

  /** @format date-time */
  created?: string;

  /** @format date-time */
  updated?: string;
  scryfallId?: string | null;
  cardName?: string | null;
  imageUrl?: string | null;

  /** @format uuid */
  userId?: string;
  isFoil?: boolean;

  /** @format uuid */
  languageId?: string;
  isAdded?: boolean;

  /** @format int32 */
  quantity?: number;
}

export interface MtgCardStageDtoPaginatedRestResultObject {
  result?: MtgCardStageDto[] | null;
  error?: string | null;
  haveError?: boolean;

  /** @format int32 */
  pageSize?: number;

  /** @format int32 */
  page?: number;

  /** @format int32 */
  pageCount?: number;

  /** @format int64 */
  count?: number;
}

export interface MtgCardTypeDto {
  /** @format uuid */
  id?: string;

  /** @format date-time */
  created?: string;

  /** @format date-time */
  updated?: string;
  name?: string | null;
}

export interface MtgCardTypeDtoPaginatedRestResultObject {
  result?: MtgCardTypeDto[] | null;
  error?: string | null;
  haveError?: boolean;

  /** @format int32 */
  pageSize?: number;

  /** @format int32 */
  page?: number;

  /** @format int32 */
  pageCount?: number;

  /** @format int64 */
  count?: number;
}

export interface MtgCardTypeDtoRestResultObject {
  result?: MtgCardTypeDto;
  error?: string | null;
  haveError?: boolean;
}

export interface SearchCardRequest {
  description?: string | null;
  colors?: string[] | null;
  sets?: string[] | null;
  rarities?: string[] | null;
  languages?: string[] | null;
  types?: string[] | null;
  orderBy?: SearchCardRequestOrderBy;
}

export enum SearchCardRequestOrderBy {
  Set = "Set",
  Price = "Price",
  Name = "Name",
  Rarity = "Rarity",
  CardType = "CardType",
  CreatedDate = "CreatedDate",
  Quantity = "Quantity",
}

export interface StringRestResultObject {
  result?: string | null;
  error?: string | null;
  haveError?: boolean;
}

export namespace Api {
  /**
   * No description
   * @tags CardDeck
   * @name V1CardsDeckPresetManaCurvesList
   * @request GET:/api/v1/cards/deck/preset/mana_curves
   * @secure
   */
  export namespace V1CardsDeckPresetManaCurvesList {
    export type RequestParams = {};
    export type RequestQuery = {};
    export type RequestBody = never;
    export type RequestHeaders = {};
    export type ResponseBody = DeckManaCurvePresetListRestResultObject;
  }
  /**
   * No description
   * @tags CardDeck
   * @name V1CardsDeckRandomSingleDeckCreate
   * @request POST:/api/v1/cards/deck/random/single/deck
   * @secure
   */
  export namespace V1CardsDeckRandomSingleDeckCreate {
    export type RequestParams = {};
    export type RequestQuery = {};
    export type RequestBody = DeckCreateRequest;
    export type RequestHeaders = {};
    export type ResponseBody = BooleanRestResultObject;
  }
  /**
   * No description
   * @tags CardDeck
   * @name V1CardsDeckRandomMultipleDeckCreate
   * @request POST:/api/v1/cards/deck/random/multiple/deck
   * @secure
   */
  export namespace V1CardsDeckRandomMultipleDeckCreate {
    export type RequestParams = {};
    export type RequestQuery = {};
    export type RequestBody = DeckMultipleDeckRequest;
    export type RequestHeaders = {};
    export type ResponseBody = BooleanRestResultObject;
  }
  /**
   * No description
   * @tags CardDeck
   * @name V1CardsDeckList
   * @request GET:/api/v1/cards/deck
   * @secure
   */
  export namespace V1CardsDeckList {
    export type RequestParams = {};
    export type RequestQuery = { page?: number; pageSize?: number };
    export type RequestBody = never;
    export type RequestHeaders = {};
    export type ResponseBody = DeckMasterDtoPaginatedRestResultObject;
  }
  /**
   * No description
   * @tags CardDeck
   * @name V1CardsDeckMasterDetail
   * @request GET:/api/v1/cards/deck/master/{id}
   * @secure
   */
  export namespace V1CardsDeckMasterDetail {
    export type RequestParams = { id: string };
    export type RequestQuery = {};
    export type RequestBody = never;
    export type RequestHeaders = {};
    export type ResponseBody = DeckDetailDtoListRestResultObject;
  }
  /**
   * No description
   * @tags Cards
   * @name V1CardsSearchCreate
   * @request POST:/api/v1/cards/search
   * @secure
   */
  export namespace V1CardsSearchCreate {
    export type RequestParams = {};
    export type RequestQuery = { page?: number; pageSize?: number };
    export type RequestBody = SearchCardRequest;
    export type RequestHeaders = {};
    export type ResponseBody = MtgCardDtoPaginatedRestResultObject;
  }
  /**
   * No description
   * @tags CardStaging
   * @name V1CardsStagingAddCreate
   * @request POST:/api/v1/cards/staging/add
   * @secure
   */
  export namespace V1CardsStagingAddCreate {
    export type RequestParams = {};
    export type RequestQuery = {};
    export type RequestBody = CardStageAddRequest;
    export type RequestHeaders = {};
    export type ResponseBody = BooleanRestResultObject;
  }
  /**
   * No description
   * @tags CardStaging
   * @name V1CardsStagingList
   * @request GET:/api/v1/cards/staging
   * @secure
   */
  export namespace V1CardsStagingList {
    export type RequestParams = {};
    export type RequestQuery = { page?: number; pageSize?: number };
    export type RequestBody = never;
    export type RequestHeaders = {};
    export type ResponseBody = MtgCardStageDtoPaginatedRestResultObject;
  }
  /**
   * No description
   * @tags Colors
   * @name V1CardsColorsList
   * @request GET:/api/v1/cards/colors
   * @secure
   */
  export namespace V1CardsColorsList {
    export type RequestParams = {};
    export type RequestQuery = { page?: number; pageSize?: number };
    export type RequestBody = never;
    export type RequestHeaders = {};
    export type ResponseBody = MtgCardColorDtoPaginatedRestResultObject;
  }
  /**
   * No description
   * @tags Colors
   * @name V1CardsColorsCreate
   * @request POST:/api/v1/cards/colors
   * @secure
   */
  export namespace V1CardsColorsCreate {
    export type RequestParams = {};
    export type RequestQuery = {};
    export type RequestBody = MtgCardColorDto;
    export type RequestHeaders = {};
    export type ResponseBody = MtgCardColorDtoRestResultObject;
  }
  /**
   * No description
   * @tags Colors
   * @name V1CardsColorsPartialUpdate
   * @request PATCH:/api/v1/cards/colors
   * @secure
   */
  export namespace V1CardsColorsPartialUpdate {
    export type RequestParams = {};
    export type RequestQuery = {};
    export type RequestBody = MtgCardColorDto;
    export type RequestHeaders = {};
    export type ResponseBody = MtgCardColorDtoRestResultObject;
  }
  /**
   * No description
   * @tags Colors
   * @name V1CardsColorsDetail
   * @request GET:/api/v1/cards/colors/{id}
   * @secure
   */
  export namespace V1CardsColorsDetail {
    export type RequestParams = { id: string };
    export type RequestQuery = {};
    export type RequestBody = never;
    export type RequestHeaders = {};
    export type ResponseBody = MtgCardColorDtoRestResultObject;
  }
  /**
   * No description
   * @tags Colors
   * @name V1CardsColorsDelete
   * @request DELETE:/api/v1/cards/colors/{id}
   * @secure
   */
  export namespace V1CardsColorsDelete {
    export type RequestParams = { id: string };
    export type RequestQuery = {};
    export type RequestBody = never;
    export type RequestHeaders = {};
    export type ResponseBody = BooleanRestResultObject;
  }
  /**
   * No description
   * @tags Import
   * @name V1ImportMtgList
   * @request GET:/api/v1/import/mtg
   * @secure
   */
  export namespace V1ImportMtgList {
    export type RequestParams = {};
    export type RequestQuery = {};
    export type RequestBody = never;
    export type RequestHeaders = {};
    export type ResponseBody = BooleanRestResultObject;
  }
  /**
   * No description
   * @tags Import
   * @name V1ImportFormatCardcastleCreate
   * @request POST:/api/v1/import/format/cardcastle
   * @secure
   */
  export namespace V1ImportFormatCardcastleCreate {
    export type RequestParams = {};
    export type RequestQuery = {};
    export type RequestBody = { file?: File };
    export type RequestHeaders = {};
    export type ResponseBody = StringRestResultObject;
  }
  /**
   * No description
   * @tags Languages
   * @name V1CardsLanguagesList
   * @request GET:/api/v1/cards/languages
   * @secure
   */
  export namespace V1CardsLanguagesList {
    export type RequestParams = {};
    export type RequestQuery = { page?: number; pageSize?: number };
    export type RequestBody = never;
    export type RequestHeaders = {};
    export type ResponseBody = MtgCardLanguageDtoPaginatedRestResultObject;
  }
  /**
   * No description
   * @tags Languages
   * @name V1CardsLanguagesCreate
   * @request POST:/api/v1/cards/languages
   * @secure
   */
  export namespace V1CardsLanguagesCreate {
    export type RequestParams = {};
    export type RequestQuery = {};
    export type RequestBody = MtgCardLanguageDto;
    export type RequestHeaders = {};
    export type ResponseBody = MtgCardLanguageDtoRestResultObject;
  }
  /**
   * No description
   * @tags Languages
   * @name V1CardsLanguagesPartialUpdate
   * @request PATCH:/api/v1/cards/languages
   * @secure
   */
  export namespace V1CardsLanguagesPartialUpdate {
    export type RequestParams = {};
    export type RequestQuery = {};
    export type RequestBody = MtgCardLanguageDto;
    export type RequestHeaders = {};
    export type ResponseBody = MtgCardLanguageDtoRestResultObject;
  }
  /**
   * No description
   * @tags Languages
   * @name V1CardsLanguagesDetail
   * @request GET:/api/v1/cards/languages/{id}
   * @secure
   */
  export namespace V1CardsLanguagesDetail {
    export type RequestParams = { id: string };
    export type RequestQuery = {};
    export type RequestBody = never;
    export type RequestHeaders = {};
    export type ResponseBody = MtgCardLanguageDtoRestResultObject;
  }
  /**
   * No description
   * @tags Languages
   * @name V1CardsLanguagesDelete
   * @request DELETE:/api/v1/cards/languages/{id}
   * @secure
   */
  export namespace V1CardsLanguagesDelete {
    export type RequestParams = { id: string };
    export type RequestQuery = {};
    export type RequestBody = never;
    export type RequestHeaders = {};
    export type ResponseBody = BooleanRestResultObject;
  }
  /**
   * No description
   * @tags Legalities
   * @name V1CardsLegalitiesList
   * @request GET:/api/v1/cards/legalities
   * @secure
   */
  export namespace V1CardsLegalitiesList {
    export type RequestParams = {};
    export type RequestQuery = { page?: number; pageSize?: number };
    export type RequestBody = never;
    export type RequestHeaders = {};
    export type ResponseBody = MtgCardLegalityDtoPaginatedRestResultObject;
  }
  /**
   * No description
   * @tags Legalities
   * @name V1CardsLegalitiesCreate
   * @request POST:/api/v1/cards/legalities
   * @secure
   */
  export namespace V1CardsLegalitiesCreate {
    export type RequestParams = {};
    export type RequestQuery = {};
    export type RequestBody = MtgCardLegalityDto;
    export type RequestHeaders = {};
    export type ResponseBody = MtgCardLegalityDtoRestResultObject;
  }
  /**
   * No description
   * @tags Legalities
   * @name V1CardsLegalitiesPartialUpdate
   * @request PATCH:/api/v1/cards/legalities
   * @secure
   */
  export namespace V1CardsLegalitiesPartialUpdate {
    export type RequestParams = {};
    export type RequestQuery = {};
    export type RequestBody = MtgCardLegalityDto;
    export type RequestHeaders = {};
    export type ResponseBody = MtgCardLegalityDtoRestResultObject;
  }
  /**
   * No description
   * @tags Legalities
   * @name V1CardsLegalitiesDetail
   * @request GET:/api/v1/cards/legalities/{id}
   * @secure
   */
  export namespace V1CardsLegalitiesDetail {
    export type RequestParams = { id: string };
    export type RequestQuery = {};
    export type RequestBody = never;
    export type RequestHeaders = {};
    export type ResponseBody = MtgCardLegalityDtoRestResultObject;
  }
  /**
   * No description
   * @tags Legalities
   * @name V1CardsLegalitiesDelete
   * @request DELETE:/api/v1/cards/legalities/{id}
   * @secure
   */
  export namespace V1CardsLegalitiesDelete {
    export type RequestParams = { id: string };
    export type RequestQuery = {};
    export type RequestBody = never;
    export type RequestHeaders = {};
    export type ResponseBody = BooleanRestResultObject;
  }
  /**
   * No description
   * @tags LegalityTypes
   * @name V1CardsLegalityTypeList
   * @request GET:/api/v1/cards/legality_type
   * @secure
   */
  export namespace V1CardsLegalityTypeList {
    export type RequestParams = {};
    export type RequestQuery = { page?: number; pageSize?: number };
    export type RequestBody = never;
    export type RequestHeaders = {};
    export type ResponseBody = MtgCardLegalityTypeDtoPaginatedRestResultObject;
  }
  /**
   * No description
   * @tags LegalityTypes
   * @name V1CardsLegalityTypeCreate
   * @request POST:/api/v1/cards/legality_type
   * @secure
   */
  export namespace V1CardsLegalityTypeCreate {
    export type RequestParams = {};
    export type RequestQuery = {};
    export type RequestBody = MtgCardLegalityTypeDto;
    export type RequestHeaders = {};
    export type ResponseBody = MtgCardLegalityTypeDtoRestResultObject;
  }
  /**
   * No description
   * @tags LegalityTypes
   * @name V1CardsLegalityTypePartialUpdate
   * @request PATCH:/api/v1/cards/legality_type
   * @secure
   */
  export namespace V1CardsLegalityTypePartialUpdate {
    export type RequestParams = {};
    export type RequestQuery = {};
    export type RequestBody = MtgCardLegalityTypeDto;
    export type RequestHeaders = {};
    export type ResponseBody = MtgCardLegalityTypeDtoRestResultObject;
  }
  /**
   * No description
   * @tags LegalityTypes
   * @name V1CardsLegalityTypeDetail
   * @request GET:/api/v1/cards/legality_type/{id}
   * @secure
   */
  export namespace V1CardsLegalityTypeDetail {
    export type RequestParams = { id: string };
    export type RequestQuery = {};
    export type RequestBody = never;
    export type RequestHeaders = {};
    export type ResponseBody = MtgCardLegalityTypeDtoRestResultObject;
  }
  /**
   * No description
   * @tags LegalityTypes
   * @name V1CardsLegalityTypeDelete
   * @request DELETE:/api/v1/cards/legality_type/{id}
   * @secure
   */
  export namespace V1CardsLegalityTypeDelete {
    export type RequestParams = { id: string };
    export type RequestQuery = {};
    export type RequestBody = never;
    export type RequestHeaders = {};
    export type ResponseBody = BooleanRestResultObject;
  }
  /**
   * No description
   * @tags Login
   * @name V1LoginAuthCreate
   * @request POST:/api/v1/login/auth
   * @secure
   */
  export namespace V1LoginAuthCreate {
    export type RequestParams = {};
    export type RequestQuery = {};
    export type RequestBody = LoginRequestData;
    export type RequestHeaders = {};
    export type ResponseBody = LoginResponseDataRestResultObject;
  }
  /**
   * No description
   * @tags Login
   * @name V1LoginRefreshTokenCreate
   * @request POST:/api/v1/login/refresh_token
   * @secure
   */
  export namespace V1LoginRefreshTokenCreate {
    export type RequestParams = {};
    export type RequestQuery = {};
    export type RequestBody = LoginResponseData;
    export type RequestHeaders = {};
    export type ResponseBody = LoginRequestDataRestResultObject;
  }
  /**
   * No description
   * @tags Rarities
   * @name V1CardsRaritiesList
   * @request GET:/api/v1/cards/rarities
   * @secure
   */
  export namespace V1CardsRaritiesList {
    export type RequestParams = {};
    export type RequestQuery = { page?: number; pageSize?: number };
    export type RequestBody = never;
    export type RequestHeaders = {};
    export type ResponseBody = MtgCardRarityDtoPaginatedRestResultObject;
  }
  /**
   * No description
   * @tags Rarities
   * @name V1CardsRaritiesCreate
   * @request POST:/api/v1/cards/rarities
   * @secure
   */
  export namespace V1CardsRaritiesCreate {
    export type RequestParams = {};
    export type RequestQuery = {};
    export type RequestBody = MtgCardRarityDto;
    export type RequestHeaders = {};
    export type ResponseBody = MtgCardRarityDtoRestResultObject;
  }
  /**
   * No description
   * @tags Rarities
   * @name V1CardsRaritiesPartialUpdate
   * @request PATCH:/api/v1/cards/rarities
   * @secure
   */
  export namespace V1CardsRaritiesPartialUpdate {
    export type RequestParams = {};
    export type RequestQuery = {};
    export type RequestBody = MtgCardRarityDto;
    export type RequestHeaders = {};
    export type ResponseBody = MtgCardRarityDtoRestResultObject;
  }
  /**
   * No description
   * @tags Rarities
   * @name V1CardsRaritiesDetail
   * @request GET:/api/v1/cards/rarities/{id}
   * @secure
   */
  export namespace V1CardsRaritiesDetail {
    export type RequestParams = { id: string };
    export type RequestQuery = {};
    export type RequestBody = never;
    export type RequestHeaders = {};
    export type ResponseBody = MtgCardRarityDtoRestResultObject;
  }
  /**
   * No description
   * @tags Rarities
   * @name V1CardsRaritiesDelete
   * @request DELETE:/api/v1/cards/rarities/{id}
   * @secure
   */
  export namespace V1CardsRaritiesDelete {
    export type RequestParams = { id: string };
    export type RequestQuery = {};
    export type RequestBody = never;
    export type RequestHeaders = {};
    export type ResponseBody = BooleanRestResultObject;
  }
  /**
   * No description
   * @tags Sets
   * @name V1CardsSetsList
   * @request GET:/api/v1/cards/sets
   * @secure
   */
  export namespace V1CardsSetsList {
    export type RequestParams = {};
    export type RequestQuery = { page?: number; pageSize?: number };
    export type RequestBody = never;
    export type RequestHeaders = {};
    export type ResponseBody = MtgCardSetDtoPaginatedRestResultObject;
  }
  /**
   * No description
   * @tags Sets
   * @name V1CardsSetsCreate
   * @request POST:/api/v1/cards/sets
   * @secure
   */
  export namespace V1CardsSetsCreate {
    export type RequestParams = {};
    export type RequestQuery = {};
    export type RequestBody = MtgCardSetDto;
    export type RequestHeaders = {};
    export type ResponseBody = MtgCardSetDtoRestResultObject;
  }
  /**
   * No description
   * @tags Sets
   * @name V1CardsSetsPartialUpdate
   * @request PATCH:/api/v1/cards/sets
   * @secure
   */
  export namespace V1CardsSetsPartialUpdate {
    export type RequestParams = {};
    export type RequestQuery = {};
    export type RequestBody = MtgCardSetDto;
    export type RequestHeaders = {};
    export type ResponseBody = MtgCardSetDtoRestResultObject;
  }
  /**
   * No description
   * @tags Sets
   * @name V1CardsSetsDetail
   * @request GET:/api/v1/cards/sets/{id}
   * @secure
   */
  export namespace V1CardsSetsDetail {
    export type RequestParams = { id: string };
    export type RequestQuery = {};
    export type RequestBody = never;
    export type RequestHeaders = {};
    export type ResponseBody = MtgCardSetDtoRestResultObject;
  }
  /**
   * No description
   * @tags Sets
   * @name V1CardsSetsDelete
   * @request DELETE:/api/v1/cards/sets/{id}
   * @secure
   */
  export namespace V1CardsSetsDelete {
    export type RequestParams = { id: string };
    export type RequestQuery = {};
    export type RequestBody = never;
    export type RequestHeaders = {};
    export type ResponseBody = BooleanRestResultObject;
  }
  /**
   * No description
   * @tags Types
   * @name V1CardsTypesList
   * @request GET:/api/v1/cards/types
   * @secure
   */
  export namespace V1CardsTypesList {
    export type RequestParams = {};
    export type RequestQuery = { page?: number; pageSize?: number };
    export type RequestBody = never;
    export type RequestHeaders = {};
    export type ResponseBody = MtgCardTypeDtoPaginatedRestResultObject;
  }
  /**
   * No description
   * @tags Types
   * @name V1CardsTypesCreate
   * @request POST:/api/v1/cards/types
   * @secure
   */
  export namespace V1CardsTypesCreate {
    export type RequestParams = {};
    export type RequestQuery = {};
    export type RequestBody = MtgCardTypeDto;
    export type RequestHeaders = {};
    export type ResponseBody = MtgCardTypeDtoRestResultObject;
  }
  /**
   * No description
   * @tags Types
   * @name V1CardsTypesPartialUpdate
   * @request PATCH:/api/v1/cards/types
   * @secure
   */
  export namespace V1CardsTypesPartialUpdate {
    export type RequestParams = {};
    export type RequestQuery = {};
    export type RequestBody = MtgCardTypeDto;
    export type RequestHeaders = {};
    export type ResponseBody = MtgCardTypeDtoRestResultObject;
  }
  /**
   * No description
   * @tags Types
   * @name V1CardsTypesDetail
   * @request GET:/api/v1/cards/types/{id}
   * @secure
   */
  export namespace V1CardsTypesDetail {
    export type RequestParams = { id: string };
    export type RequestQuery = {};
    export type RequestBody = never;
    export type RequestHeaders = {};
    export type ResponseBody = MtgCardTypeDtoRestResultObject;
  }
  /**
   * No description
   * @tags Types
   * @name V1CardsTypesDelete
   * @request DELETE:/api/v1/cards/types/{id}
   * @secure
   */
  export namespace V1CardsTypesDelete {
    export type RequestParams = { id: string };
    export type RequestQuery = {};
    export type RequestBody = never;
    export type RequestHeaders = {};
    export type ResponseBody = BooleanRestResultObject;
  }
  /**
   * No description
   * @tags Users
   * @name V1UsersList
   * @request GET:/api/v1/users
   * @secure
   */
  export namespace V1UsersList {
    export type RequestParams = {};
    export type RequestQuery = { page?: number; pageSize?: number };
    export type RequestBody = never;
    export type RequestHeaders = {};
    export type ResponseBody = DiceClubUserDtoPaginatedRestResultObject;
  }
  /**
   * No description
   * @tags Users
   * @name V1UsersCreate
   * @request POST:/api/v1/users
   * @secure
   */
  export namespace V1UsersCreate {
    export type RequestParams = {};
    export type RequestQuery = {};
    export type RequestBody = DiceClubUserDto;
    export type RequestHeaders = {};
    export type ResponseBody = DiceClubUserDtoRestResultObject;
  }
  /**
   * No description
   * @tags Users
   * @name V1UsersPartialUpdate
   * @request PATCH:/api/v1/users
   * @secure
   */
  export namespace V1UsersPartialUpdate {
    export type RequestParams = {};
    export type RequestQuery = {};
    export type RequestBody = DiceClubUserDto;
    export type RequestHeaders = {};
    export type ResponseBody = DiceClubUserDtoRestResultObject;
  }
  /**
   * No description
   * @tags Users
   * @name V1UsersDetail
   * @request GET:/api/v1/users/{id}
   * @secure
   */
  export namespace V1UsersDetail {
    export type RequestParams = { id: string };
    export type RequestQuery = {};
    export type RequestBody = never;
    export type RequestHeaders = {};
    export type ResponseBody = DiceClubUserDtoRestResultObject;
  }
  /**
   * No description
   * @tags Users
   * @name V1UsersDelete
   * @request DELETE:/api/v1/users/{id}
   * @secure
   */
  export namespace V1UsersDelete {
    export type RequestParams = { id: string };
    export type RequestQuery = {};
    export type RequestBody = never;
    export type RequestHeaders = {};
    export type ResponseBody = BooleanRestResultObject;
  }
  /**
   * No description
   * @tags Utils
   * @name V1UtilsGeneratePasswordList
   * @request GET:/api/v1/utils/generate_password
   * @secure
   */
  export namespace V1UtilsGeneratePasswordList {
    export type RequestParams = {};
    export type RequestQuery = { password?: string };
    export type RequestBody = never;
    export type RequestHeaders = {};
    export type ResponseBody = StringRestResultObject;
  }
}
