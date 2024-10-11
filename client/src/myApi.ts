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

export interface AddCustomPropertyDto {
  /** @format int32 */
  productId?: number;
  propertyName?: string | null;
}

export interface CreateCustomerDto {
  name?: string | null;
  address?: string | null;
  phone?: string | null;
  email?: string | null;
}

export interface CreateOrderDto {
  /** @format date-time */
  orderDate?: string;
  /** @format date-time */
  deliveryDate?: string | null;
  status?: string | null;
  /** @format double */
  totalAmount?: number;
  /** @format int32 */
  customerId?: number;
  orderEntries?: CreateOrderEntryDto[] | null;
}

export interface CreateOrderEntryDto {
  /** @format int32 */
  quantity?: number;
  /** @format int32 */
  productId?: number;
}

export interface CreatePaperDto {
  name?: string | null;
  discontinued?: boolean;
  /** @format int32 */
  stock?: number;
  /** @format double */
  price?: number;
}

export interface Customer {
  /** @format int32 */
  id?: number;
  name?: string | null;
  address?: string | null;
  phone?: string | null;
  email?: string | null;
  orders?: Order[] | null;
}

export interface Order {
  /** @format int32 */
  id?: number;
  /** @format date-time */
  orderDate?: string;
  /** @format date-time */
  deliveryDate?: string | null;
  status?: string | null;
  /** @format double */
  totalAmount?: number;
  /** @format int32 */
  customerId?: number;
  customer?: Customer;
  orderEntries?: OrderEntry[] | null;
}

export interface OrderEntry {
  /** @format int32 */
  id?: number;
  /** @format int32 */
  quantity?: number;
  /** @format int32 */
  productId?: number;
  product?: Paper;
  /** @format int32 */
  orderId?: number;
  order?: Order;
}

export interface Paper {
  /** @format int32 */
  id?: number;
  name?: string | null;
  discontinued?: boolean;
  /** @format int32 */
  stock?: number;
  /** @format double */
  price?: number;
  paperProperties?: PaperProperty[] | null;
  orderEntries?: OrderEntry[] | null;
  quantity?: number;
}

export interface PaperProperty {
  /** @format int32 */
  paperId?: number;
  paper?: Paper;
  /** @format int32 */
  propertyId?: number;
  property?: Property;
}

export interface Property {
  /** @format int32 */
  id?: number;
  propertyName?: string | null;
  paperProperties?: PaperProperty[] | null;
}

export type QueryParamsType = Record<string | number, any>;
export type ResponseFormat = keyof Omit<Body, 'body' | 'bodyUsed'>;

export interface FullRequestParams extends Omit<RequestInit, 'body'> {
  /** set parameter to `true` for call `securityWorker` for this request */
  secure?: boolean;
  /** request path */
  path: string;
  /** content type of request body */
  type?: ContentType;
  /** query params */
  query?: QueryParamsType;
  /** format of response (i.e. response.json() -> format: "json") */
  format?: ResponseFormat;
  /** request body */
  body?: unknown;
  /** base url */
  baseUrl?: string;
  /** request cancellation token */
  cancelToken?: CancelToken;
}

export type RequestParams = Omit<
  FullRequestParams,
  'body' | 'method' | 'query' | 'path'
>;

export interface ApiConfig<SecurityDataType = unknown> {
  baseUrl?: string;
  baseApiParams?: Omit<RequestParams, 'baseUrl' | 'cancelToken' | 'signal'>;
  securityWorker?: (
    securityData: SecurityDataType | null,
  ) => Promise<RequestParams | void> | RequestParams | void;
  customFetch?: typeof fetch;
}

export interface HttpResponse<D extends unknown, E extends unknown = unknown>
  extends Response {
  data: D;
  error: E;
}

type CancelToken = Symbol | string | number;

export enum ContentType {
  Json = 'application/json',
  FormData = 'multipart/form-data',
  UrlEncoded = 'application/x-www-form-urlencoded',
  Text = 'text/plain',
}

export class HttpClient<SecurityDataType = unknown> {
  public baseUrl: string = '';
  private securityData: SecurityDataType | null = null;
  private securityWorker?: ApiConfig<SecurityDataType>['securityWorker'];
  private abortControllers = new Map<CancelToken, AbortController>();
  private customFetch = (...fetchParams: Parameters<typeof fetch>) =>
    fetch(...fetchParams);

  private baseApiParams: RequestParams = {
    credentials: 'same-origin',
    headers: {},
    redirect: 'follow',
    referrerPolicy: 'no-referrer',
  };

  constructor(apiConfig: ApiConfig<SecurityDataType> = {}) {
    Object.assign(this, apiConfig);
  }

  public setSecurityData = (data: SecurityDataType | null) => {
    this.securityData = data;
  };

  protected encodeQueryParam(key: string, value: any) {
    const encodedKey = encodeURIComponent(key);
    return `${encodedKey}=${encodeURIComponent(typeof value === 'number' ? value : `${value}`)}`;
  }

  protected addQueryParam(query: QueryParamsType, key: string) {
    return this.encodeQueryParam(key, query[key]);
  }

  protected addArrayQueryParam(query: QueryParamsType, key: string) {
    const value = query[key];
    return value.map((v: any) => this.encodeQueryParam(key, v)).join('&');
  }

  protected toQueryString(rawQuery?: QueryParamsType): string {
    const query = rawQuery || {};
    const keys = Object.keys(query).filter(
      (key) => 'undefined' !== typeof query[key],
    );
    return keys
      .map((key) =>
        Array.isArray(query[key])
          ? this.addArrayQueryParam(query, key)
          : this.addQueryParam(query, key),
      )
      .join('&');
  }

  protected addQueryParams(rawQuery?: QueryParamsType): string {
    const queryString = this.toQueryString(rawQuery);
    return queryString ? `?${queryString}` : '';
  }

  private contentFormatters: Record<ContentType, (input: any) => any> = {
    [ContentType.Json]: (input: any) =>
      input !== null && (typeof input === 'object' || typeof input === 'string')
        ? JSON.stringify(input)
        : input,
    [ContentType.Text]: (input: any) =>
      input !== null && typeof input !== 'string'
        ? JSON.stringify(input)
        : input,
    [ContentType.FormData]: (input: any) =>
      Object.keys(input || {}).reduce((formData, key) => {
        const property = input[key];
        formData.append(
          key,
          property instanceof Blob
            ? property
            : typeof property === 'object' && property !== null
              ? JSON.stringify(property)
              : `${property}`,
        );
        return formData;
      }, new FormData()),
    [ContentType.UrlEncoded]: (input: any) => this.toQueryString(input),
  };

  protected mergeRequestParams(
    params1: RequestParams,
    params2?: RequestParams,
  ): RequestParams {
    return {
      ...this.baseApiParams,
      ...params1,
      ...(params2 || {}),
      headers: {
        ...(this.baseApiParams.headers || {}),
        ...(params1.headers || {}),
        ...((params2 && params2.headers) || {}),
      },
    };
  }

  protected createAbortSignal = (
    cancelToken: CancelToken,
  ): AbortSignal | undefined => {
    if (this.abortControllers.has(cancelToken)) {
      const abortController = this.abortControllers.get(cancelToken);
      if (abortController) {
        return abortController.signal;
      }
      return void 0;
    }

    const abortController = new AbortController();
    this.abortControllers.set(cancelToken, abortController);
    return abortController.signal;
  };

  public abortRequest = (cancelToken: CancelToken) => {
    const abortController = this.abortControllers.get(cancelToken);

    if (abortController) {
      abortController.abort();
      this.abortControllers.delete(cancelToken);
    }
  };

  public request = async <T = any, E = any>({
    body,
    secure,
    path,
    type,
    query,
    format,
    baseUrl,
    cancelToken,
    ...params
  }: FullRequestParams): Promise<HttpResponse<T, E>> => {
    const secureParams =
      ((typeof secure === 'boolean' ? secure : this.baseApiParams.secure) &&
        this.securityWorker &&
        (await this.securityWorker(this.securityData))) ||
      {};
    const requestParams = this.mergeRequestParams(params, secureParams);
    const queryString = query && this.toQueryString(query);
    const payloadFormatter = this.contentFormatters[type || ContentType.Json];
    const responseFormat = format || requestParams.format;

    return this.customFetch(
      `${baseUrl || this.baseUrl || ''}${path}${queryString ? `?${queryString}` : ''}`,
      {
        ...requestParams,
        headers: {
          ...(requestParams.headers || {}),
          ...(type && type !== ContentType.FormData
            ? { 'Content-Type': type }
            : {}),
        },
        signal:
          (cancelToken
            ? this.createAbortSignal(cancelToken)
            : requestParams.signal) || null,
        body:
          typeof body === 'undefined' || body === null
            ? null
            : payloadFormatter(body),
      },
    ).then(async (response) => {
      const r = response.clone() as HttpResponse<T, E>;
      r.data = null as unknown as T;
      r.error = null as unknown as E;

      const data = !responseFormat
        ? r
        : await response[responseFormat]()
            .then((data) => {
              if (r.ok) {
                r.data = data;
              } else {
                r.error = data;
              }
              return r;
            })
            .catch((e) => {
              r.error = e;
              return r;
            });

      if (cancelToken) {
        this.abortControllers.delete(cancelToken);
      }

      if (!response.ok) throw data;
      return data;
    });
  };
}

/**
 * @title My API
 * @version v1
 */
export class Api<
  SecurityDataType extends unknown,
> extends HttpClient<SecurityDataType> {
  api = {
    /**
     * No description
     *
     * @tags Admin
     * @name AdminOrdersList
     * @request GET:/api/Admin/orders
     */
    adminOrdersList: (params: RequestParams = {}) =>
      this.request<Order[], any>({
        path: `/api/Admin/orders`,
        method: 'GET',
        format: 'json',
        ...params,
      }),

    /**
     * No description
     *
     * @tags Admin
     * @name AdminPropertiesCreate
     * @request POST:/api/Admin/properties
     */
    adminPropertiesCreate: (
      data: AddCustomPropertyDto,
      params: RequestParams = {},
    ) =>
      this.request<void, any>({
        path: `/api/Admin/properties`,
        method: 'POST',
        body: data,
        type: ContentType.Json,
        ...params,
      }),

    /**
     * No description
     *
     * @tags Admin
     * @name AdminOrderStatusPartialUpdate
     * @request PATCH:/api/Admin/order/status/{id}
     */
    adminOrderStatusPartialUpdate: (
      id: number,
      data: string,
      params: RequestParams = {},
    ) =>
      this.request<void, any>({
        path: `/api/Admin/order/status/${id}`,
        method: 'PATCH',
        body: data,
        type: ContentType.Json,
        ...params,
      }),

    /**
     * No description
     *
     * @tags Customer
     * @name CustomerCreate
     * @request POST:/api/Customer
     */
    customerCreate: (data: CreateCustomerDto, params: RequestParams = {}) =>
      this.request<void, any>({
        path: `/api/Customer`,
        method: 'POST',
        body: data,
        type: ContentType.Json,
        ...params,
      }),

    /**
     * No description
     *
     * @tags Customer
     * @name CustomerDetail
     * @request GET:/api/Customer/{id}
     */
    customerDetail: (id: number, params: RequestParams = {}) =>
      this.request<void, any>({
        path: `/api/Customer/${id}`,
        method: 'GET',
        ...params,
      }),

    /**
     * No description
     *
     * @tags Order
     * @name OrderCreate
     * @request POST:/api/Order
     */
    orderCreate: (data: CreateOrderDto, params: RequestParams = {}) =>
      this.request<void, any>({
        path: `/api/Order`,
        method: 'POST',
        body: data,
        type: ContentType.Json,
        ...params,
      }),

    /**
     * No description
     *
     * @tags Order
     * @name OrderDetail
     * @request GET:/api/Order/{id}
     */
    orderDetail: (id: number, params: RequestParams = {}) =>
      this.request<void, any>({
        path: `/api/Order/${id}`,
        method: 'GET',
        ...params,
      }),

    /**
     * No description
     *
     * @tags Order
     * @name OrderCustomerDetail
     * @request GET:/api/Order/customer/{customerId}
     */
    orderCustomerDetail: (customerId: number, params: RequestParams = {}) =>
      this.request<void, any>({
        path: `/api/Order/customer/${customerId}`,
        method: 'GET',
        ...params,
      }),

    /**
     * No description
     *
     * @tags Product
     * @name ProductList
     * @request GET:/api/Product
     */
    productList: (
      query?: {
        search?: string;
        filter?: string;
        sort?: string;
      },
      params: RequestParams = {},
    ) =>
      this.request<Paper[], any>({
        path: `/api/Product`,
        method: 'GET',
        query: query,
        format: 'json',
        ...params,
      }),

    /**
     * No description
     *
     * @tags Product
     * @name ProductCreate
     * @request POST:/api/Product
     */
    productCreate: (data: CreatePaperDto, params: RequestParams = {}) =>
      this.request<Paper, any>({
        path: `/api/Product`,
        method: 'POST',
        body: data,
        type: ContentType.Json,
        format: 'json',
        ...params,
      }),

    /**
     * No description
     *
     * @tags Product
     * @name ProductDiscontinueCreate
     * @request POST:/api/Product/discontinue/{id}
     */
    productDiscontinueCreate: (id: number, params: RequestParams = {}) =>
      this.request<void, any>({
        path: `/api/Product/discontinue/${id}`,
        method: 'POST',
        ...params,
      }),

    /**
     * No description
     *
     * @tags Product
     * @name ProductRestockCreate
     * @request POST:/api/Product/restock/{id}
     */
    productRestockCreate: (
      id: number,
      data: number,
      params: RequestParams = {},
    ) =>
      this.request<void, any>({
        path: `/api/Product/restock/${id}`,
        method: 'POST',
        body: data,
        type: ContentType.Json,
        ...params,
      }),
  };
}
