import request from '@/utils/request'

export function page (index, limit) {
  return request({
    method: 'get',
    url: '/api/account/acountlist/' + index + '/' + limit
  })
}
// 获取账本详细信息
export function getAccountById (id) {
  return request({
    method: 'get',
    url: '/api/account/account/' + id
  })
}
// 根据账本ID获取账本消费记录
export function getDealList (accountid, index, limit) {
  return request({
    method: 'get',
    url: '/api/deal/list/' + accountid + '/' + index + '/' + limit
  })
}
