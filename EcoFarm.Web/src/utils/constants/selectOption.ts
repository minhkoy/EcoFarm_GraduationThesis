import { ACCOUNT_TYPE } from './enums'

export const SELECT_ACCOUNT_TYPE = [
  { value: ACCOUNT_TYPE.SELLER, label: 'seller' },
  { value: ACCOUNT_TYPE.CUSTOMER, label: 'customer' },
]

// export const SELECT_PAYMENT_METHOD = [
//   { value: PAYMENT_METHOD.ServicePackage, label: 'service-package' },
//   { value: PAYMENT_METHOD.Service, label: 'service' },
//   { value: PAYMENT_METHOD.SellingProduct, label: 'selling-product' },
// ]

// export const SELECT_ORDER_STATUS = [
//   { value: ORDER_STATUS_NAME.CancelledByCustomer, label: 'cancelled-by-customer' },
//   { value: ORDER_STATUS_NAME.Preparing, label: 'preparing' },
//   { value: ORDER_STATUS_NAME.Received, label: 'received' },
//   { value: ORDER_STATUS_NAME.RejectedBySeller, label: 'rejected-by-seller' },
//   { value: ORDER_STATUS_NAME.SellerConfirmed, label: 'seller-confirmed' },
//   { value: ORDER_STATUS_NAME.Shipped, label: 'shipped' },
//   { value: ORDER_STATUS_NAME.Shipping, label: 'shipping' },
//   { value: ORDER_STATUS_NAME.WaitingSellerConfirm, label: 'waiting-seller-confirm' },
// ]

export const SELECT_LIMIT = [
  { value: 10, label: '10' },
  { value: 20, label: '20' },
  { value: 30, label: '30' },
  { value: 50, label: '50' },
]

export const SELECT_RATING = [
  { value: 0, label: 'Không đánh giá' }, //XXX
  { value: 1, label: '1' },
  { value: 2, label: '2' },
  { value: 3, label: '3' },
  { value: 4, label: '4' },
  { value: 5, label: '5' },
]