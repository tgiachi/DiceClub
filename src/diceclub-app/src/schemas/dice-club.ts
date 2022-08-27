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

export interface DiceClubGroupDto {
	/** @format uuid */
	id?: string;

	/** @format date-time */
	created?: string;

	/** @format date-time */
	updated?: string;
	groupName?: string | null;
	isAdmin?: boolean;
}

export interface DiceClubGroupDtoPaginationObject {
	result?: DiceClubGroupDto[] | null;

	/** @format int32 */
	page?: number;

	/** @format int32 */
	size?: number;

	/** @format int32 */
	totalPages?: number;

	/** @format int64 */
	count?: number;
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
	password?: string | null;
	isActive?: boolean;
	serialId?: string | null;
	refreshToken?: string | null;
}

export interface DiceClubUserDtoPaginationObject {
	result?: DiceClubUserDto[] | null;

	/** @format int32 */
	page?: number;

	/** @format int32 */
	size?: number;

	/** @format int32 */
	totalPages?: number;

	/** @format int64 */
	count?: number;
}

export interface InventoryCategoryDto {
	/** @format uuid */
	id?: string;

	/** @format date-time */
	created?: string;

	/** @format date-time */
	updated?: string;
	name?: string | null;
	parserType?: string | null;
}

export interface InventoryDto {
	/** @format uuid */
	id?: string;

	/** @format date-time */
	created?: string;

	/** @format date-time */
	updated?: string;
	name?: string | null;
	author?: string | null;
	description?: string | null;
	serialNumber?: string | null;

	/** @format uuid */
	ownerId?: string | null;
	category?: InventoryCategoryDto;
	image?: string | null;
}

export interface InventoryDtoPaginationObject {
	result?: InventoryDto[] | null;

	/** @format int32 */
	page?: number;

	/** @format int32 */
	size?: number;

	/** @format int32 */
	totalPages?: number;

	/** @format int64 */
	count?: number;
}

export interface LoginRequestData {
	email?: string | null;
	password?: string | null;
}

export interface LoginResponseData {
	accessToken?: string | null;
	refreshToken?: string | null;

	/** @format date-time */
	accessTokenExpire?: string;
}

export namespace Api {
	/**
	 * No description
	 * @tags Groups
	 * @name V1GroupsListList
	 * @request GET:/api/v1/groups/list
	 */
	export namespace V1GroupsListList {
		export type RequestParams = {};
		export type RequestQuery = {};
		export type RequestBody = never;
		export type RequestHeaders = {};
		export type ResponseBody = DiceClubGroupDto[];
	}
	/**
	 * No description
	 * @tags Groups
	 * @name V1GroupsInsertCreate
	 * @request POST:/api/v1/groups/insert
	 */
	export namespace V1GroupsInsertCreate {
		export type RequestParams = {};
		export type RequestQuery = {};
		export type RequestBody = DiceClubGroupDto;
		export type RequestHeaders = {};
		export type ResponseBody = DiceClubGroupDto;
	}
	/**
	 * No description
	 * @tags Groups
	 * @name V1GroupsListPaginateList
	 * @request GET:/api/v1/groups/list/paginate
	 */
	export namespace V1GroupsListPaginateList {
		export type RequestParams = {};
		export type RequestQuery = { page?: number; pageSize?: number };
		export type RequestBody = never;
		export type RequestHeaders = {};
		export type ResponseBody = DiceClubGroupDtoPaginationObject;
	}
	/**
	 * No description
	 * @tags Groups
	 * @name V1GroupsUpdatePartialUpdate
	 * @request PATCH:/api/v1/groups/update
	 */
	export namespace V1GroupsUpdatePartialUpdate {
		export type RequestParams = {};
		export type RequestQuery = {};
		export type RequestBody = DiceClubGroupDto;
		export type RequestHeaders = {};
		export type ResponseBody = DiceClubGroupDto;
	}
	/**
	 * No description
	 * @tags Groups
	 * @name V1GroupsDeleteDelete
	 * @request DELETE:/api/v1/groups/delete/{id}
	 */
	export namespace V1GroupsDeleteDelete {
		export type RequestParams = { id: string };
		export type RequestQuery = {};
		export type RequestBody = never;
		export type RequestHeaders = {};
		export type ResponseBody = boolean;
	}
	/**
	 * No description
	 * @tags Inventory
	 * @name V1InventoryAddBookCreate
	 * @request POST:/api/v1/inventory/add/book/{isbn}
	 */
	export namespace V1InventoryAddBookCreate {
		export type RequestParams = { isbn: string };
		export type RequestQuery = {};
		export type RequestBody = never;
		export type RequestHeaders = {};
		export type ResponseBody = void;
	}
	/**
	 * No description
	 * @tags Inventory
	 * @name V1InventoryGetAllList
	 * @request GET:/api/v1/inventory/get/all
	 */
	export namespace V1InventoryGetAllList {
		export type RequestParams = {};
		export type RequestQuery = { pageNumber?: number; pageSize?: number };
		export type RequestBody = never;
		export type RequestHeaders = {};
		export type ResponseBody = InventoryDtoPaginationObject;
	}
	/**
	 * No description
	 * @tags Inventory
	 * @name V1InventoryGetDetail
	 * @request GET:/api/v1/inventory/get/{id}
	 */
	export namespace V1InventoryGetDetail {
		export type RequestParams = { id: string };
		export type RequestQuery = {};
		export type RequestBody = never;
		export type RequestHeaders = {};
		export type ResponseBody = InventoryDto;
	}
	/**
	 * No description
	 * @tags Login
	 * @name V1LoginAuthCreate
	 * @request POST:/api/v1/login/auth
	 */
	export namespace V1LoginAuthCreate {
		export type RequestParams = {};
		export type RequestQuery = {};
		export type RequestBody = LoginRequestData;
		export type RequestHeaders = {};
		export type ResponseBody = void;
	}
	/**
	 * No description
	 * @tags Login
	 * @name V1LoginRefreshTokenCreate
	 * @request POST:/api/v1/login/refresh_token
	 */
	export namespace V1LoginRefreshTokenCreate {
		export type RequestParams = {};
		export type RequestQuery = {};
		export type RequestBody = LoginResponseData;
		export type RequestHeaders = {};
		export type ResponseBody = void;
	}
	/**
	 * No description
	 * @tags TestAuth
	 * @name V1TestauthTestAuthList
	 * @request GET:/api/v1/testauth/test_auth
	 */
	export namespace V1TestauthTestAuthList {
		export type RequestParams = {};
		export type RequestQuery = {};
		export type RequestBody = never;
		export type RequestHeaders = {};
		export type ResponseBody = void;
	}
	/**
	 * No description
	 * @tags Users
	 * @name V1UsersListList
	 * @request GET:/api/v1/users/list
	 */
	export namespace V1UsersListList {
		export type RequestParams = {};
		export type RequestQuery = {};
		export type RequestBody = never;
		export type RequestHeaders = {};
		export type ResponseBody = DiceClubUserDto[];
	}
	/**
	 * No description
	 * @tags Users
	 * @name V1UsersInsertCreate
	 * @request POST:/api/v1/users/insert
	 */
	export namespace V1UsersInsertCreate {
		export type RequestParams = {};
		export type RequestQuery = {};
		export type RequestBody = DiceClubUserDto;
		export type RequestHeaders = {};
		export type ResponseBody = DiceClubUserDto;
	}
	/**
	 * No description
	 * @tags Users
	 * @name V1UsersListPaginateList
	 * @request GET:/api/v1/users/list/paginate
	 */
	export namespace V1UsersListPaginateList {
		export type RequestParams = {};
		export type RequestQuery = { page?: number; pageSize?: number };
		export type RequestBody = never;
		export type RequestHeaders = {};
		export type ResponseBody = DiceClubUserDtoPaginationObject;
	}
	/**
	 * No description
	 * @tags Users
	 * @name V1UsersUpdatePartialUpdate
	 * @request PATCH:/api/v1/users/update
	 */
	export namespace V1UsersUpdatePartialUpdate {
		export type RequestParams = {};
		export type RequestQuery = {};
		export type RequestBody = DiceClubUserDto;
		export type RequestHeaders = {};
		export type ResponseBody = DiceClubUserDto;
	}
	/**
	 * No description
	 * @tags Users
	 * @name V1UsersDeleteDelete
	 * @request DELETE:/api/v1/users/delete/{id}
	 */
	export namespace V1UsersDeleteDelete {
		export type RequestParams = { id: string };
		export type RequestQuery = {};
		export type RequestBody = never;
		export type RequestHeaders = {};
		export type ResponseBody = boolean;
	}
	/**
	 * No description
	 * @tags Utils
	 * @name V1UtilsGeneratePasswordList
	 * @request GET:/api/v1/utils/generate_password
	 */
	export namespace V1UtilsGeneratePasswordList {
		export type RequestParams = {};
		export type RequestQuery = { password?: string };
		export type RequestBody = never;
		export type RequestHeaders = {};
		export type ResponseBody = void;
	}
}
