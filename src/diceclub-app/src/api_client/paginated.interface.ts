export interface PaginatedRestResultObject<TResult> {
  result?: TResult[] | null;
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