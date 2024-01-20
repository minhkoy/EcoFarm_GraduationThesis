export const ACCOUNT_TYPE = {
  SA: 'SuperAdmin',
  ADMIN: 'Admin',
  SELLER: 'Seller',
  CUSTOMER: 'Customer',
} as const

export const ACCESS_TOKEN = 'accessToken'

export const ERROR_CODES = {
  BAD_REQUEST: 400,
  UNAUTHORIZED: 401,
  FORBIDDEN: 403,
  NOT_FOUND: 404,
  INTERNAL_SERVER_ERROR: 500,
}

export const GENDER = {
  MALE: 'MALE',
  FEMALE: 'FEMALE',
  OTHERS: 'OTHERS',
} as const

export const CURRENCY_TYPE = {
  VND: 'VND',
  USD: 'USD',
} as const

export const SERVICE_PACKAGE_APPROVAL_STATUS = {
  Approved: 'Approved',
  Rejected: 'Rejected',
  Pending: 'Pending',
} as const

export enum PACKAGE_STATUS {
  NotStarted = 0,
  Started,
  Ended,
}

export enum PACKAGE_REGISTER_STATUS {
  OpenForRegister = 0,
  ClosedForRegister = 1,
}
export const PACKAGE_TYPE = {
  Tourism: 'Tourism',
  Farming: 'Farming',
  PetCare: 'PetCare',
  Multiple: 'Multiple',
  Others: 'Others',
}

export const PAYMENT_METHOD = [
  [1, 'Tiền mặt'],
  [2, 'Chuyển khoản'],
  [3, 'Ví điện tử'],
] as const

//export const PAYMENT_METHOD_2

export enum ORDER_STATUS {
  WaitingSellerConfirm = 1,
  SellerConfirmed,
  Preparing,
  Received,
  Shipping,
  Shipped,
  RejectedBySeller,
  CancelledByCustomer,
}

export const ORDER_STATUS_COLORS_2 = [
  [ORDER_STATUS.WaitingSellerConfirm, 'orange'],
  [ORDER_STATUS.SellerConfirmed, 'yellow'],
  [ORDER_STATUS.Preparing, 'blue'],
  [ORDER_STATUS.Received, 'lime'],
  [ORDER_STATUS.Shipping, 'cyan'],
  [ORDER_STATUS.Shipped, 'green'],
  [ORDER_STATUS.RejectedBySeller, 'red'],
  [ORDER_STATUS.CancelledByCustomer, 'red'],

]

export const ORDER_STATUS_COLORS = new Map<number, string>([
  [ORDER_STATUS.WaitingSellerConfirm, 'orange'],
  [ORDER_STATUS.SellerConfirmed, 'yellow'],
  [ORDER_STATUS.Preparing, 'blue'],
  [ORDER_STATUS.Received, 'lime'],
  [ORDER_STATUS.Shipping, 'cyan'],
  [ORDER_STATUS.Shipped, 'green'],
  [ORDER_STATUS.RejectedBySeller, 'red'],
  [ORDER_STATUS.CancelledByCustomer, 'red'],
])

export const ORDER_STATUS_NAME = [
  { status: ORDER_STATUS.WaitingSellerConfirm, statusName: 'Chờ nhà cung cấp/ chủ trang trại xác nhận' },
  { status: ORDER_STATUS.SellerConfirmed, statusName: 'Nhà cung cấp/ chủ trang trại đã xác nhận' },
  { status: ORDER_STATUS.Preparing, statusName: 'Nhà cung cấp/ chủ trang trại đang chuẩn bị hàng' },
  { status: ORDER_STATUS.Shipping, statusName: 'Đang giao hàng' },
  { status: ORDER_STATUS.Shipped, statusName: 'Đã giao hàng' },
  { status: ORDER_STATUS.Received, statusName: 'Đã nhận được hàng' },
  { status: ORDER_STATUS.RejectedBySeller, statusName: 'Nhà cung cấp/ chủ trang trại đã từ chối' },
  { status: ORDER_STATUS.CancelledByCustomer, statusName: 'Đã hủy bởi khách hàng' },
] //as const